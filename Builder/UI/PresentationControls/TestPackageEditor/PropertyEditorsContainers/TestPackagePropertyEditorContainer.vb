
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic

Public Class TestPackagePropertyEditorContainer


    Public Sub InitializePropertyEditorsAndSetDataSource(ByVal testPackageModel As TestPackage, ByVal viewTypes As List(Of String))
        Me.SuspendLayout()

        For Each propertyEditorBaseRef As TestPackageEditorControlBase In Me.CurrentPropertyEditorControls
            Dim currentPropertyEditor As ITestPackageEditorPropertyEditor = DirectCast(propertyEditorBaseRef, ITestPackageEditorPropertyEditor)
            RemoveHandler propertyEditorBaseRef.DataChanged, AddressOf PropertyEditor_DataChanged
            RemoveHandler currentPropertyEditor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            RemoveHandler currentPropertyEditor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            RemoveHandler currentPropertyEditor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            RemoveHandler currentPropertyEditor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest
            currentPropertyEditor.TestPackageEntity = Nothing

            propertyEditorBaseRef.Dispose()
        Next
        Me.Controls.Clear()
        Me.CurrentPropertyEditorControls.Clear()

        Dim genericPropertyEditor As New General_TestPackagePropertiesEditor()
        InitializePropertyEditor(genericPropertyEditor, testPackageModel)
        genericPropertyEditor.ToggleCodeField(Me.TestPackageEntity.IsNew)

        For Each viewType In viewTypes
            Dim editor As ITestPackageEditorPropertyEditor = GetPropertyEditorBasedInViewType(viewType)
            InitializePropertyEditor(editor, testPackageModel)
        Next

        Me.ResumeLayout()
    End Sub

    Public Sub ToggleCodeField(ByVal enabled As Boolean)
        For Each editor As TestPackageEditorControlBase In Me.CurrentPropertyEditorControls
            If TypeOf editor Is General_TestPackagePropertiesEditor Then
                DirectCast(editor, General_TestPackagePropertiesEditor).ToggleCodeField(enabled)
                Exit For
            End If
        Next
    End Sub

    Private Function GetPropertyEditorBasedInViewType(ByVal viewType As String) As ITestPackageEditorPropertyEditor
        Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If plugin Is Nothing Then
            Return Nothing
        End If

        Return plugin.GetTestPackagePropertiesEditor()
    End Function

    Private Sub InitializePropertyEditor(ByVal editor As ITestPackageEditorPropertyEditor, ByVal dataSource As TestPackage)
        If editor IsNot Nothing Then
            AddHandler editor.DataChanged, AddressOf PropertyEditor_DataChanged
            AddHandler editor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            AddHandler editor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            AddHandler editor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            AddHandler editor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest

            editor.TestPackageEntity = Me.TestPackageEntity
            editor.CurrentDataSource = dataSource

            Me.CreateFrameForPropertyEditorAndAddToContainer(DirectCast(editor, TestPackageEditorControlBase), editor.FrameTitle)
            Me.CurrentPropertyEditorControls.Add(DirectCast(editor, TestPackageEditorControlBase))
        End If
    End Sub

    Private Sub PropertyEditor_CommandExecuteRequest(ByVal sender As Object, ByVal e As CommandExecuteRequestEventArgs)
        OnCommandExecuteRequest(e)
    End Sub

    Private Sub PropertyEditor_DataChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OnDataChanged(e)
    End Sub

    Private Sub PropertyEditor_DependentResourceAdded(ByVal sender As System.Object, ByVal e As ResourceEventArgs)
        OnDependentResourceAdded(e)
    End Sub

    Private Sub PropertyEditor_DependentResourceRemoved(ByVal sender As System.Object, ByVal e As ResourceNameEventArgs)
        OnDependentResourceRemoved(e)
    End Sub

    Private Sub PropertyEditor_ResourceNeeded(ByVal sender As System.Object, ByVal e As ResourceNeededEventArgs)
        OnResourceNeeded(e)
    End Sub



End Class