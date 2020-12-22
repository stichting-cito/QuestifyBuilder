
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic

Public Class TestSectionPropertyEditorContainer

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

    Private Sub PropertyEditor_ResourceDialogRequest(ByVal sender As Object, ByVal e As ResourceDialogRequestEventArgs)
        OnResourceDialogRequest(e)
    End Sub

    Private Sub PropertyEditor_ResourceNeeded(ByVal sender As System.Object, ByVal e As ResourceNeededEventArgs)
        OnResourceNeeded(e)
    End Sub

    Private Sub PropertyEditor_SectionItemDatasourceDependentResourceChanged(ByVal sender As Object, ByVal e As SectionLogicSettingsDependencyChangedEventArgs)
        OnSectionItemDatasourceDependentResourceChanged(e)
    End Sub

    Private Sub PropertyEditor_CommandExecuteRequest(ByVal sender As Object, ByVal e As CommandExecuteRequestEventArgs)
        OnCommandExecuteRequest(e)
    End Sub



    Protected Sub OnDependentResourceAdded(ByVal e As ResourceEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Protected Sub OnDependentResourceRemoved(ByVal e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub

    Protected Sub OnResourceDialogRequest(ByVal e As ResourceDialogRequestEventArgs)
        RaiseEvent ResourceDialogRequest(Me, e)
    End Sub

    Protected Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    Protected Sub OnSectionItemDatasourceDependentResourceChanged(ByVal e As SectionLogicSettingsDependencyChangedEventArgs)
        RaiseEvent SectionItemDatasourceDependentResourceChanged(Me, e)
    End Sub



    Public Sub InitializePropertyEditorsAndSetDataSource(ByVal testSectionModel As TestSection2, ByVal viewTypes As List(Of String), itemDataSourcesAlreadyAdded As List(Of String))
        Me.SuspendLayout()

        For Each propertyEditorBaseRef As TestEditorControlBase In Me.CurrentPropertyEditorControls
            Dim currentPropertyEditor As ITestSectionPartPropertyEditor = DirectCast(propertyEditorBaseRef, ITestSectionPartPropertyEditor)
            RemoveHandler currentPropertyEditor.DataChanged, AddressOf PropertyEditor_DataChanged
            RemoveHandler currentPropertyEditor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            RemoveHandler currentPropertyEditor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            RemoveHandler currentPropertyEditor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            RemoveHandler currentPropertyEditor.ResourceDialogRequest, AddressOf PropertyEditor_ResourceDialogRequest
            RemoveHandler currentPropertyEditor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest
            RemoveHandler currentPropertyEditor.SectionItemDatasourceDependentResourceChanged, AddressOf PropertyEditor_SectionItemDatasourceDependentResourceChanged
            currentPropertyEditor.TestEntity = Nothing

            propertyEditorBaseRef.Dispose()
        Next
        Me.Controls.Clear()
        Me.CurrentPropertyEditorControls.Clear()

        Dim genericPropertyEditor As New General_TestSectionPropertiesEditor(itemDataSourcesAlreadyAdded)
        InitializePropertyEditor(genericPropertyEditor, testSectionModel)

        For Each viewType In viewTypes
            Dim editor As ITestSectionPartPropertyEditor = GetPropertyEditorBasedInViewType(viewType)
            If editor IsNot Nothing Then
                InitializePropertyEditor(editor, testSectionModel)
            End If
        Next

        Me.ResumeLayout()
    End Sub

    Private Function GetPropertyEditorBasedInViewType(ByVal viewType As String) As ITestSectionPartPropertyEditor
        Dim plugin = IoCHelper.GetInstances(Of ITestEditorPlugin).FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Return Nothing
        End If

        Return plugin.GetTestSectionPropertyEditor()
    End Function

    Private Sub InitializePropertyEditor(ByVal editor As ITestSectionPartPropertyEditor, ByVal dataSource As TestSection2)
        If editor IsNot Nothing Then
            AddHandler editor.DataChanged, AddressOf PropertyEditor_DataChanged
            AddHandler editor.DependentResourceAdded, AddressOf PropertyEditor_DependentResourceAdded
            AddHandler editor.DependentResourceRemoved, AddressOf PropertyEditor_DependentResourceRemoved
            AddHandler editor.ResourceNeeded, AddressOf PropertyEditor_ResourceNeeded
            AddHandler editor.ResourceDialogRequest, AddressOf PropertyEditor_ResourceDialogRequest
            AddHandler editor.CommandExecuteRequest, AddressOf PropertyEditor_CommandExecuteRequest
            AddHandler editor.SectionItemDatasourceDependentResourceChanged, AddressOf PropertyEditor_SectionItemDatasourceDependentResourceChanged

            editor.TestEntity = Me.TestEntity
            editor.CurrentDataSource = dataSource

            Me.CreateFrameForPropertyEditorAndAddToContainer(DirectCast(editor, TestEditorControlBase), editor.FrameTitle)
            Me.CurrentPropertyEditorControls.Add(DirectCast(editor, TestEditorControlBase))
        End If
    End Sub


    Public Event DependentResourceAdded As EventHandler(Of ResourceEventArgs)
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)
    Public Event ResourceDialogRequest As EventHandler(Of ResourceDialogRequestEventArgs)
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Public Event SectionItemDatasourceDependentResourceChanged As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs)
End Class