Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic

Public Class ItemReferencePropertyEditorContainer

    Sub New()
        InitializeComponent()
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

    Private Sub PropertyEditor_CommandExecuteRequest(ByVal sender As Object, ByVal e As CommandExecuteRequestEventArgs)
        OnCommandExecuteRequest(e)
    End Sub

    Public Sub InitializePropertyEditorsAndSetDataSource(ByVal itemReferenceModel As ItemReference2, ByVal viewTypes As List(Of String))
        Me.SuspendLayout()

        For Each propertyEditorBaseRef As TestEditorControlBase In Me.CurrentPropertyEditorControls
            Dim currentPropertyEditor As IItemReferencePropertyEditor = DirectCast(propertyEditorBaseRef, IItemReferencePropertyEditor)
            RemoveHandler currentPropertyEditor.DataChanged, AddressOf PropertyEditor_DataChanged
            RemoveHandler currentPropertyEditor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            RemoveHandler currentPropertyEditor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            RemoveHandler currentPropertyEditor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            RemoveHandler currentPropertyEditor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest
            currentPropertyEditor.TestEntity = Nothing

            propertyEditorBaseRef.Dispose()
        Next
        Me.Controls.Clear()
        Me.CurrentPropertyEditorControls.Clear()

        Dim genericPropertyEditor As New General_ItemReferencePropertiesEditor()
        InitializePropertyEditor(genericPropertyEditor, itemReferenceModel)

        For Each viewType In viewTypes
            Dim editor As IItemReferencePropertyEditor = GetPropertyEditorBasedInViewType(viewType)
            If editor IsNot Nothing Then
                InitializePropertyEditor(editor, itemReferenceModel)
            End If
        Next

        Me.ResumeLayout()
    End Sub

    Private Function GetPropertyEditorBasedInViewType(ByVal viewType As String) As IItemReferencePropertyEditor
        Dim plugin = IoCHelper.GetInstances(Of ITestEditorPlugin).FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Return Nothing
        End If

        Return plugin.GetItemReferencePropertyEditor()
    End Function

    Private Sub InitializePropertyEditor(ByVal editor As IItemReferencePropertyEditor, ByVal dataSource As ItemReference2)
        If editor IsNot Nothing Then
            AddHandler editor.DataChanged, AddressOf PropertyEditor_DataChanged
            AddHandler editor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            AddHandler editor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            AddHandler editor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            AddHandler editor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest

            editor.TestEntity = Me.TestEntity
            editor.CurrentDataSource = dataSource

            Me.CreateFrameForPropertyEditorAndAddToContainer(DirectCast(editor, TestEditorControlBase), editor.FrameTitle)
            Me.CurrentPropertyEditorControls.Add(DirectCast(editor, TestEditorControlBase))
        End If
    End Sub

End Class