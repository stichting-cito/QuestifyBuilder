Imports System.ComponentModel
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic

Public Class AssessmentTestPropertyEditorContainer

    Sub New()
        InitializeComponent()
    End Sub


    <Description("This event will be raised when a dependent resource is added in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event DependentResourceAdded As EventHandler(Of ResourceEventArgs)

    <Description("This event will be raised when a dependent resource is removed in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)

    <Description("This event will be raised when the control needs a resource from the bank."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)



    Public Sub InitializePropertyEditorsAndSetDataSource(ByVal testModel As AssessmentTest2, ByVal viewTypes As List(Of String))
        Me.SuspendLayout()

        For Each propertyEditorBaseRef As TestEditorControlBase In Me.CurrentPropertyEditorControls
            Dim currentPropertyEditor As IAssessmentTestPropertyEditor = DirectCast(propertyEditorBaseRef, IAssessmentTestPropertyEditor)
            RemoveHandler propertyEditorBaseRef.DataChanged, AddressOf PropertyEditor_DataChanged
            RemoveHandler currentPropertyEditor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            RemoveHandler currentPropertyEditor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            RemoveHandler currentPropertyEditor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            RemoveHandler currentPropertyEditor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest
            currentPropertyEditor.TestEntity = Nothing

            propertyEditorBaseRef.Dispose()
        Next
        Me.Controls.Clear()
        Me.CurrentPropertyEditorControls.Clear()

        Dim genericPropertyEditor As New General_AssessmentTestPropertiesEditor()
        InitializePropertyEditor(genericPropertyEditor, testModel)
        genericPropertyEditor.ToggleCodeField(Me.TestEntity.IsNew)

        For Each viewType In viewTypes
            Dim editor As IAssessmentTestPropertyEditor = GetPropertyEditorBasedInViewType(viewType)
            If editor IsNot Nothing Then
                InitializePropertyEditor(editor, testModel)
            End If
        Next

        Me.ResumeLayout()
    End Sub

    Public Sub ToggleCodeField(ByVal enabled As Boolean)
        For Each editor As TestEditorControlBase In Me.CurrentPropertyEditorControls
            If TypeOf editor Is General_AssessmentTestPropertiesEditor Then
                DirectCast(editor, General_AssessmentTestPropertiesEditor).ToggleCodeField(enabled)
                Exit For
            End If
        Next
    End Sub

    Protected Sub OnDependentResourceAdded(ByVal e As ResourceEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Protected Sub OnDependentResourceRemoved(ByVal e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub

    Protected Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    Private Function GetPropertyEditorBasedInViewType(ByVal viewType As String) As IAssessmentTestPropertyEditor
        Dim plugin = IoCHelper.GetInstances(Of ITestEditorPlugin).FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Return Nothing
        End If

        Return plugin.GetTestPropertiesEditor()
    End Function

    Private Sub InitializePropertyEditor(ByVal editor As IAssessmentTestPropertyEditor, ByVal dataSource As AssessmentTest2)
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