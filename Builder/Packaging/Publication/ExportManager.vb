Imports System.ComponentModel
Imports Cito.Tester.Common

Public Class ExportManager
    Implements IDisposable


    Private _packageRoot As String
    Private _publishedResources As List(Of String)
    Private _traversedResources As List(Of String)
    Private _sourceResourceManager As ResourceManagerBase
    Private _containerResourceManager As ManifestResourceManager
    Private _useContainerPackage As Boolean



    Public ReadOnly Property UseContainerPackage() As Boolean
        Get
            Return _useContainerPackage
        End Get
    End Property

    Public ReadOnly Property PackageRoot() As String
        Get
            Return _packageRoot
        End Get
    End Property

    Public ReadOnly Property SourceResourceManager() As ResourceManagerBase
        Get
            Return _sourceResourceManager
        End Get
    End Property

    Public ReadOnly Property PublishedResources() As List(Of String)
        Get
            If _publishedResources Is Nothing Then
                _publishedResources = New List(Of String)
            End If
            Return _publishedResources
        End Get
    End Property

    Public Property TraversedResources() As List(Of String)
        Get
            If _traversedResources Is Nothing Then
                _traversedResources = New List(Of String)
            End If
            Return _traversedResources
        End Get
        Set(value As List(Of String))
            _traversedResources = value
        End Set
    End Property



    Public Event StartExport As EventHandler(Of StartEventArgs)

    Protected Sub OnStartExport(e As StartEventArgs)
        RaiseEvent StartExport(Me, e)
    End Sub

    Public Event ExportProgress As EventHandler(Of ProgressEventArgs)

    Protected Sub OnExportProgress(e As ProgressEventArgs)
        RaiseEvent ExportProgress(Me, e)
    End Sub





    Public Overridable Function ExportResources(bgWorker As BackgroundWorker, customBankPropertyMetaData As MetaDataCollection, resources As ResourceEntryCollection) As Boolean
        Dim processCompleted As Boolean
        Dim _destinationManifest As ResourceManifest = New ResourceManifest

        Using _destinationResourceManager As ManifestResourceManager = New ManifestResourceManager(_destinationManifest, customBankPropertyMetaData, New Uri(PackageRoot), Guid.NewGuid.ToString)

            processCompleted = DoExportResources(bgWorker,
                                                resources,
                                                _destinationResourceManager)

            If processCompleted Then
                OnExportProgress(New ProgressEventArgs(My.Resources.ExportManager_UpdateManifest))
                _destinationResourceManager.UpdateManifest()
            End If

        End Using

        Return processCompleted
    End Function

    Public Function DoExportResources(bgWorker As BackgroundWorker,
                                     resources As ResourceEntryCollection,
                                     destination As ManifestResourceManager) As Boolean
        Dim index As Integer = 1
        Dim _destinationResourceManager = destination
        Dim exportAsWell As New Dictionary(Of String, String)
        Dim exported As New Dictionary(Of String, String)

        OnStartExport(New StartEventArgs(resources.Count))

        For Each resource As ResourceEntry In resources

            OnExportProgress(New ProgressEventArgs(String.Format(My.Resources.ProcessingResource0, resource.Name), index))

            If bgWorker IsNot Nothing AndAlso bgWorker.CancellationPending Then Return False

            If _useContainerPackage Then
                Using sResource As StreamResource = SourceResourceManager.GetResource(resource.Name)
                    If sResource IsNot Nothing Then _destinationResourceManager.PutResource(sResource)
                End Using
                CopyResourceRecursively(resource, _containerResourceManager, True, index)
            Else

                Dim rToExport As ResourceEntry = resource

                While rToExport IsNot Nothing
                    Dim tmp As List(Of String)
                    Try
                        tmp = CopyResource2(rToExport, _destinationResourceManager, index)
                    Catch ex As Exception
                        Throw New ResourceException(String.Format(My.Resources.Error_WhilePublishingResource, rToExport.Name), ex)
                    End Try

                    If (Not exported.ContainsKey(rToExport.Name)) Then exported.Add(rToExport.Name, rToExport.Name)
                    For Each e As String In tmp
                        If (Not exportAsWell.ContainsKey(e)) AndAlso (Not exported.ContainsKey(e)) Then exportAsWell.Add(e, e)
                    Next

                    rToExport = Nothing

                    If (exportAsWell.Count > 0) Then
                        Dim e = exportAsWell.Keys.GetEnumerator() : e.MoveNext()
                        Dim firstKey = e.Current
                        Try
                            rToExport = SourceResourceManager.GetResourceEntry(firstKey)
                        Catch ex As Exception
                            Throw New Exception("Could not find resource referenced by " & resource.ToString)
                        End Try
                        Debug.Assert(rToExport IsNot Nothing)
                        exportAsWell.Remove(firstKey)
                    End If

                End While

            End If
            index += 1
        Next

        Return True
    End Function

    Public Function GetAllResourceNames(resources As ResourceEntryCollection) As List(Of String)

        TraversedResources.Clear()

        Dim eStart As New StartEventArgs(resources.Count)
        OnStartExport(eStart)

        For Each resource As ResourceEntry In resources
            Dim eProgress As New ProgressEventArgs(String.Format(My.Resources.GettingResource0, resource.Name))
            OnExportProgress(eProgress)
            TraverseResourceRecursively(resource)
        Next

        Return TraversedResources

    End Function



    Private Sub TraverseResourceRecursively(resource As ResourceEntry)
        If Not TraversedResources.Contains(resource.Name) Then
            Try
                Using sResource As StreamResource = SourceResourceManager.GetResource(resource.Name)
                    If sResource IsNot Nothing Then
                        TraversedResources.Add(resource.Name)
                    End If

                    If sResource IsNot Nothing Then
                        For Each dependentResourceReference As DependentResource In sResource.DependentResources
                            Dim dependentResourceEntry As ResourceEntry = SourceResourceManager.GetResourceEntry(dependentResourceReference.Name)
                            If dependentResourceEntry IsNot Nothing Then
                                TraverseResourceRecursively(dependentResourceEntry)
                            Else
                                Throw New NoNullAllowedException(String.Format(My.Resources.Error_Manifest, resource.Name, dependentResourceReference.Name))
                            End If
                        Next
                    End If
                End Using
            Catch ex As Exception
                Throw New ResourceException($"Error while traversing resource with name '{resource.Name}'", ex)
            End Try
        End If
    End Sub

    Private Sub CopyResourceRecursively(resource As ResourceEntry, destinationResourceManager As ManifestResourceManager, excludeTest As Boolean, index As Integer)
        If Not PublishedResources.Contains(resource.Name) Then
            Try
                Using sResource As StreamResource = SourceResourceManager.GetResource(resource.Name)
                    If excludeTest = False Then
                        If sResource IsNot Nothing Then
                            OnExportProgress(New ProgressEventArgs(String.Format(My.Resources.ProcessingResource0, resource.Name), index))
                            destinationResourceManager.PutResource(sResource)
                            PublishedResources.Add(resource.Name)
                        End If
                    End If

                    If sResource IsNot Nothing Then
                        For Each dependentResourceReference As DependentResource In sResource.DependentResources
                            Dim dependentResourceEntry As ResourceEntry = SourceResourceManager.GetResourceEntry(dependentResourceReference.Name)
                            If dependentResourceEntry IsNot Nothing Then
                                CopyResourceRecursively(dependentResourceEntry, destinationResourceManager, False, index)
                            Else
                                Throw New NoNullAllowedException(String.Format(My.Resources.Error_Manifest, resource.Name, dependentResourceReference.Name))
                            End If
                        Next
                    End If
                End Using
            Catch ex As Exception
                Throw New ResourceException(String.Format(My.Resources.Error_WhilePublishingResource, resource.Name), ex)
            End Try
        End If
    End Sub

    Function CopyResource2(resource As ResourceEntry, destinationResourceManager As ManifestResourceManager, index As Integer) As List(Of String)
        Dim copyTheseAsWell As New List(Of String)

        If Not PublishedResources.Contains(resource.Name) Then
            Try
                Using sResource As StreamResource = SourceResourceManager.GetResource(resource.Name)
                    If sResource IsNot Nothing Then
                        OnExportProgress(New ProgressEventArgs(String.Format(My.Resources.ProcessingResource0, resource.Name), index))
                        For Each d As DependentResource In sResource.DependentResources
                            copyTheseAsWell.Add(d.Name)
                        Next

                        destinationResourceManager.PutResource(sResource)
                        PublishedResources.Add(resource.Name)
                    End If
                End Using
            Catch ex As Exception
                Throw New ResourceException(String.Format(My.Resources.Error_WhilePublishingResource, resource.Name), ex)
            End Try
        End If
        Return copyTheseAsWell
    End Function

    Private Function ImportResources(packageRoot As Uri, password As String, bankId As Integer) As Boolean
        Throw New NotImplementedException
    End Function



    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If

        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub



    Public Sub New(source As ResourceManagerBase, destinationPackageRoot As String)
        _sourceResourceManager = source
        _packageRoot = destinationPackageRoot
    End Sub

    Public Sub New(source As ResourceManagerBase, destinationPackageRoot As String,
                    useContainerPackage As Boolean, container As ManifestResourceManager, publishedResources As List(Of String))
        _sourceResourceManager = source
        _packageRoot = destinationPackageRoot
        _containerResourceManager = container
        If useContainerPackage Then
            _publishedResources = publishedResources
            _useContainerPackage = useContainerPackage
        End If
    End Sub


End Class


Public Class GenericConflictEventArgs
    Inherits EventArgs


    Private _resourceName As String
    Private _bankId As Integer = -1
    Private _bankContextId As Integer = -1
    Private _cancel As Boolean = True



    Public Property ResourceName() As String
        Get
            Return _resourceName
        End Get
        Private Set(value As String)
            _resourceName = value
        End Set
    End Property

    Public ReadOnly Property BankId() As Integer
        Get
            Return _bankId
        End Get
    End Property

    Public ReadOnly Property BankContextId() As Integer
        Get
            Return _bankContextId
        End Get
    End Property

    Public Property Cancel() As Boolean
        Get
            Return _cancel
        End Get
        Set(value As Boolean)
            _cancel = value
        End Set
    End Property


    Public Sub New(resourceName As String, bankId As Integer, bankContextId As Integer)
        Me.ResourceName = resourceName
        _bankId = bankId
        _bankContextId = bankContextId
    End Sub


End Class


