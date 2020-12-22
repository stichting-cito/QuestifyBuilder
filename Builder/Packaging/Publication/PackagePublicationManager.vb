Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

Public Class PackagePublicationManager
    Implements IDisposable


    Private WithEvents _exportManager As ExportManager
    Private _additionalResources As List(Of String)
    Private _useContainerPackage As Boolean



    Private Sub _exportManager_PublicationProgress(sender As Object, e As ProgressEventArgs) Handles _exportManager.ExportProgress
        OnPublicationProgress(e)
    End Sub

    Private Sub _exportManager_StartPublication(sender As Object, e As StartEventArgs) Handles _exportManager.StartExport
        OnStartPublication(e)
    End Sub



    Public Event StartPublication As EventHandler(Of StartEventArgs)

    Protected Sub OnStartPublication(e As StartEventArgs)
        RaiseEvent StartPublication(Me, e)
    End Sub

    Public Event PublicationProgress As EventHandler(Of ProgressEventArgs)

    Protected Sub OnPublicationProgress(e As ProgressEventArgs)
        RaiseEvent PublicationProgress(Me, e)
    End Sub



    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")>
    Public ReadOnly Property PublishedResources() As List(Of String)
        Get
            Return _exportManager.PublishedResources
        End Get
    End Property

    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")>
    Public ReadOnly Property AdditionalResources() As List(Of String)
        Get
            If _additionalResources Is Nothing Then
                _additionalResources = New List(Of String)
            End If
            Return _additionalResources
        End Get
    End Property



    Public Function Publish(bgWorker As BackgroundWorker, test As AssessmentTestViewBase, customBankPropertyMetaData As MetaDataCollection) As Boolean
        Dim resourcesOnRootLevel As New ResourceEntryCollection()

        resourcesOnRootLevel.AddRange(GetAdditionalResourceEntries())

        Return _exportManager.ExportResources(bgWorker, customBankPropertyMetaData, resourcesOnRootLevel)
    End Function

    Public Function Publish(bgWorker As BackgroundWorker, testPackage As TestPackageViewBase, customBankPropertyMetaData As MetaDataCollection) As Boolean
        Dim resourcesOnRootLevel As New ResourceEntryCollection()

        resourcesOnRootLevel.AddRange(GetAdditionalResourceEntries())

        Return _exportManager.ExportResources(bgWorker, customBankPropertyMetaData, resourcesOnRootLevel)
    End Function

    Public Function GetAllResourcesForTest(test As AssessmentTestViewBase) As List(Of String)
        Dim allTestResources As ResourceEntryCollection
        allTestResources = test.GetAllResourcesForTest

        Return _exportManager.GetAllResourceNames(allTestResources)
    End Function



    Protected Function GetAdditionalResourceEntries() As ResourceEntryCollection
        Dim collection As New ResourceEntryCollection

        For Each resourceName As String In _additionalResources
            collection.Add(New ResourceEntry(resourceName))
        Next

        Return collection
    End Function




    Public Sub New(source As ResourceManagerBase, packageRoot As String)
        _exportManager = New ExportManager(source, packageRoot)
    End Sub

    Public Sub New(source As ResourceManagerBase, packageRoot As String, useContainerPackage As Boolean, containerResourceManager As ManifestResourceManager, publishedResources As List(Of String))
        _exportManager = New ExportManager(source, packageRoot, useContainerPackage, containerResourceManager, publishedResources)
        _useContainerPackage = useContainerPackage
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub



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


End Class


