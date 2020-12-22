Imports Cito.Tester.Common
Imports System.Linq

Public Class ImportManager
    Implements IDisposable


    Private _sourceResourceManager As ResourceManagerBase
    Private _destinationResourceManagerFactory As IResourceManagerFactory
    Private _publishedResources As List(Of String)



    Public Property DestinationResourceManager() As ResourceManagerBase

    Public ReadOnly Property PublishedResources() As List(Of String)
        Get
            If _publishedResources Is Nothing Then
                _publishedResources = New List(Of String)
            End If
            Return _publishedResources
        End Get
    End Property



    Public Event StartImport As EventHandler(Of StartEventArgs)

    Protected Sub OnStartImport(e As StartEventArgs)
        RaiseEvent StartImport(Me, e)
    End Sub

    Public Event ImportProgress As EventHandler(Of ProgressEventArgs)

    Protected Sub OnImportProgress(e As ProgressEventArgs)
        RaiseEvent ImportProgress(Me, e)
    End Sub

    Public Event HandleConflict As EventHandler(Of GenericConflictEventArgs)

    Protected Sub OnHandleConflict(e As GenericConflictEventArgs)
        RaiseEvent HandleConflict(Me, e)
    End Sub

    Public Event CustomBankPropertiesRemoved As EventHandler(Of ImportCustomBankPropertiesRemovedArgs)

    Protected Sub OnCustomBankPropertiesRemoved(e As ImportCustomBankPropertiesRemovedArgs)
        RaiseEvent CustomBankPropertiesRemoved(Me, e)
    End Sub



    Public Function ImportResources(resources As ResourceEntryCollection) As Boolean
        Dim returnValue As Boolean = True
        OnStartImport(New StartEventArgs(resources.Count))
        Dim processedMap As New HashSet(Of String)
        Dim resourcesToImport As New Queue(Of ResourceEntry)
        For Each res As ResourceEntry In resources
            BuildResourceQueueRecursive(resourcesToImport, processedMap, res)
        Next

        While (resourcesToImport.Any())
            Dim copyList = New List(Of ResourceEntry)
            Dim index = 0
            While (index < 50 AndAlso resourcesToImport.Count > 0)
                copyList.Add(resourcesToImport.Dequeue())
                index += 1
            End While
            Using destinationResourceManagerwrapper As IResourceManagerWrapper = _destinationResourceManagerFactory.Create()
                DestinationResourceManager = destinationResourceManagerwrapper.ResourceManager
                For Each res In copyList
                    OnImportProgress(New ProgressEventArgs(String.Format(My.Resources.ProcessingResource, res.Name)))
                    CopyResource(res, destinationResourceManagerwrapper.ResourceManager)
                Next
            End Using
        End While
        Return returnValue
    End Function



    Private Sub CopyResource(resource As ResourceEntry, destinationResourceManager As ResourceManagerBase)
        If Not PublishedResources.Contains(resource.Name) Then
            Try
                Using sResource As StreamResource = _sourceResourceManager.GetResource(resource.Name)
                    Try
                        CheckForMultipleTreeStructures(sResource)
                        destinationResourceManager.PutResource(sResource)
                    Catch ex As DuplicateResourceException
                        Dim e As New GenericConflictEventArgs(resource.Name, ex.BankId, ex.BankContextId)

                        OnHandleConflict(e)

                        If e.Cancel Then

                            Dim buffer(1023) As Byte
                            Dim count As Integer = buffer.Length
                            Do
                                count = sResource.ResourceObject.Read(buffer, 0, count)
                                If count = 0 Then Exit Do
                            Loop

                        Else
                            destinationResourceManager.UpdateResource(sResource)
                        End If
                    Catch ex As Exception
                        Throw ex
                    Finally
                        PublishedResources.Add(resource.Name)
                    End Try
                End Using
            Catch ex As Exception
                Throw New ResourceException(String.Format(My.Resources.Error_WhileImportingResource, resource.Name), ex)
            End Try
        End If
    End Sub

    Private Sub CheckForMultipleTreeStructures(ByRef sResource As StreamResource)
        If sResource.MetaData.OfType(Of MetaDataTreeStructure).Count > 1 Then
            Dim treeStructures = sResource.MetaData.OfType(Of MetaDataTreeStructure).Select(Function(ts) ts.Name).ToArray()
            sResource.MetaData.RemoveAll(Function(md) TypeOf md Is MetaDataTreeStructure)
            OnCustomBankPropertiesRemoved(New ImportCustomBankPropertiesRemovedArgs(sResource.Name, treeStructures))
        End If
    End Sub

    Private Sub BuildResourceQueueRecursive(ByRef queue As Queue(Of ResourceEntry), ByRef processedMap As HashSet(Of String), resource As ResourceEntry)

        If Not processedMap.Contains(resource.Name) Then
            processedMap.Add(resource.Name)
            For Each dependentResourceReference As DependentResource In _sourceResourceManager.GetDependentResourcesForResource(resource.Name)
                Dim dependentResourceEntry As ResourceEntry = _sourceResourceManager.GetResourceEntry(dependentResourceReference.Name)
                If dependentResourceEntry IsNot Nothing Then
                    BuildResourceQueueRecursive(queue, processedMap, dependentResourceEntry)
                Else
                    Throw New NoNullAllowedException(String.Format(My.Resources.Error_Manifest, resource.Name, dependentResourceReference.Name))
                End If
            Next
            queue.Enqueue(resource)

        End If
    End Sub

    Private Function ImportResources(bankRoot As Uri, password As String, bankId As Integer) As Boolean
        Throw New NotImplementedException
    End Function



    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                If _sourceResourceManager IsNot Nothing AndAlso TypeOf _sourceResourceManager Is IDisposable Then
                    DirectCast(_sourceResourceManager, IDisposable).Dispose()
                End If
            End If

        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub



    Public Sub New(source As ResourceManagerBase, destinationResourceManagerFactory As IResourceManagerFactory)
        _sourceResourceManager = source
        _destinationResourceManagerFactory = destinationResourceManagerFactory
    End Sub

End Class


Public Class StartImportEventArgs
    Inherits EventArgs

    Private _numberOfResources As Integer

    Public ReadOnly Property NumberOfResources() As Integer
        Get
            Return _numberOfResources
        End Get
    End Property

    Public Sub New(numberOfResources As Integer)
        _numberOfResources = numberOfResources
    End Sub

End Class



Public Class ImportProgressEventArgs
    Inherits EventArgs

    Public ReadOnly Property StatusMessage As String

    Public ReadOnly Property Value As Integer

    Public Sub New(sMessage As String, value As Integer)
        StatusMessage = sMessage
        Me.Value = value
    End Sub
End Class



Public Class ImportCustomBankPropertiesRemovedArgs
    Inherits EventArgs

    Public ReadOnly Property ResourceName As String

    Public ReadOnly Property CustomBankPropertyNames As List(Of String)

    Public Sub New(resourceName As String, ParamArray propertyNames() As String)
        Me.ResourceName = resourceName
        CustomBankPropertyNames = New List(Of String)
        CustomBankPropertyNames.AddRange(propertyNames)
    End Sub

End Class


