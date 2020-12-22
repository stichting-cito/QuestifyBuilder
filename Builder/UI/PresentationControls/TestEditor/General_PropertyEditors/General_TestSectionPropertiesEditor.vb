Imports System.ComponentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Factories

Public Class General_TestSectionPropertiesEditor
    Implements ITestSectionPartPropertyEditor

    Const Inclusion As String = "inclusion"
    Const Normal As String = "normal"
    Const Seeding As String = "seeding"


    Private Const NO_MAX_PICKS_VALUE As Integer = -1

    Private _testSection As GeneralTestSection
    Private _testSectionModel As TestSection2
    Private _itemDataSourcesAlreadyAdded As List(Of String)



    <Description("This event will be raised when a dependent resource is added in this control."),
    Category("TestSectionPropertiesEditor events")>
    Public Event DependentResourceAdded As EventHandler(Of ResourceEventArgs) Implements ITestSectionPartPropertyEditor.DependentResourceAdded

    <Description("This event will be raised when a dependent resource is removed in this control."),
    Category("TestSectionPropertiesEditor events")>
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs) Implements ITestSectionPartPropertyEditor.DependentResourceRemoved

    <Description("This event will be raised when this control needs the 'select resource' dialog."),
    Category("SectionLogicSettingsControl events")>
    Public Event ResourceDialogRequest As EventHandler(Of ResourceDialogRequestEventArgs) Implements ITestSectionPartPropertyEditor.ResourceDialogRequest

    <Description("This event will be raised when the control needs a resource from the bank."),
    Category("TestSectionPropertiesEditor events")>
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs) Implements ITestSectionPartPropertyEditor.ResourceNeeded

    <Description("This event will be raised when a dependent resource is changed in a section logic settings control."),
    Category("TestSectionPropertiesEditor events")>
    Public Event SectionItemDatasourceDependentResourceChanged As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs) Implements ITestSectionPartPropertyEditor.SectionItemDatasourceDependentResourceChanged



    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.General_TestSectionPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Property TestSection As TestSection2 Implements ITestSectionPartPropertyEditor.CurrentDataSource
        Get
            Return _testSectionModel
        End Get
        Set
            _testSectionModel = Value

            If _testSectionModel IsNot Nothing Then
                _testSection = New GeneralTestSection(_testSectionModel)
            End If

            ControlBindingSource.DataSource = _testSection
            ShowHideManyOutputSettingsControl()
        End Set
    End Property



    Public Overrides Sub HandleTestDesignPermissionChange(permission As TestDesignPermission)
        For Each ctrl As Control In DetailTableLayoutPanel.Controls
            If permission = TestDesignPermission.Minimal Then
                ctrl.Enabled = ctrl Is TitleLabel OrElse ctrl Is TitleTextBox
            Else
                If Not (ctrl Is ItemDataSourceTextBox OrElse ctrl Is ModuleHrefTextBox OrElse ctrl Is DriverHrefTextBox) Then
                    ctrl.Enabled = True
                End If
            End If
        Next
    End Sub

    Protected Sub OnDependentResourceAdded(ByVal e As ResourceEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Protected Sub OnDependentResourceRemoved(e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub

    Protected Sub OnResourceDialogRequest(e As ResourceDialogRequestEventArgs)
        RaiseEvent ResourceDialogRequest(Me, e)
    End Sub

    Protected Sub OnResourceNeeded(e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub

    Protected Sub OnSectionItemDatasourceDependentResourceChanged(e As SectionLogicSettingsDependencyChangedEventArgs)
        RaiseEvent SectionItemDatasourceDependentResourceChanged(Me, e)
    End Sub

    Private Sub RemoveItemDatasourceButton_Click(sender As Object, e As EventArgs) Handles RemoveItemDatasourceButton.Click
        OnDependentResourceRemoved(New ResourceNameEventArgs(TestSection.ItemDataSource))
        ResetItemDataSource()
        OnSectionItemDatasourceDependentResourceChanged(New SectionLogicSettingsDependencyChangedEventArgs(TestSection.ItemDataSource))
    End Sub

    Public Sub ResetItemDataSource()
        TestSection.ItemDataSource = String.Empty
        ItemDataSourceTextBox.Text = String.Empty
        TestSection.ItemDataSourceBehaviour = Nothing
        ControlBindingSource.ResetBindings(True)
        ShowHideManyOutputSettingsControl()
    End Sub

    Private Sub SelectItemDatasourceButton_Click(sender As Object, e As EventArgs) Handles SelectItemDatasourceButton.Click
        Dim dialog As New SelectDataSourceResourceDialog(TestEntity.BankId, _itemDataSourcesAlreadyAdded, False, Normal, Inclusion, Seeding)
        dialog.AllowMultiSelect = False

        Select Case dialog.ShowDialog()
            Case DialogResult.OK
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.ResourceId) Then
                    OnDependentResourceRemoved(New ResourceNameEventArgs(ItemDataSourceTextBox.Text))
                    _testSectionModel.ItemDataSource = dialog.SelectedEntity.Name

                    Dim dataSourceBehaviour As DataSourceBehaviourEnum
                    If [Enum].TryParse(Of DataSourceBehaviourEnum)(dialog.SelectedEntity.DataSourceType, True, dataSourceBehaviour) Then
                        _testSectionModel.ItemDataSourceBehaviour = dataSourceBehaviour
                        ShowHideManyOutputSettingsControl()
                    End If

                    OnDependentResourceAdded(New ResourceEventArgs(dialog.SelectedEntity))
                    OnSectionItemDatasourceDependentResourceChanged(New SectionLogicSettingsDependencyChangedEventArgs(Me.TestSection.ItemDataSource))
                    ControlBindingSource.ResetCurrentItem()
                    ValidateChildren()

                    OnCommandExecuteRequest(New CommandExecuteRequestEventArgs(TestEditorCommands.RefreshDataSourceInSection))
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
        End Select
    End Sub

    Private Sub SelectAdaptiveModuleButton_Click(sender As Object, e As EventArgs) Handles SelectAdaptiveModuleButton.Click
        Using dialog As New SelectMediaResourceDialog(Me.TestEntity.BankId)
            Dim result As DialogResult = dialog.ShowDialog()

            If result = DialogResult.OK Then
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.ResourceId) Then
                    If Not String.IsNullOrEmpty(Me.ModuleHrefTextBox.Text) Then
                        OnDependentResourceRemoved(New ResourceNameEventArgs(Me.ModuleHrefTextBox.Text))
                    End If
                    Me.ModuleHrefTextBox.Text = dialog.SelectedEntity.Name
                    OnDependentResourceAdded(New ResourceEventArgs(dialog.SelectedEntity))
                    Me.ValidateChildren()
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End Using
    End Sub

    Private Sub SelectAdaptiveDriver_Click(sender As Object, e As EventArgs) Handles SelectAdaptiveDriver.Click
        Using dialog As New SelectMediaResourceDialog(Me.TestEntity.BankId)
            Dim result As DialogResult = dialog.ShowDialog()

            If result = DialogResult.OK Then
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.ResourceId) Then
                    If Not String.IsNullOrEmpty(Me.DriverHrefTextBox.Text) Then
                        OnDependentResourceRemoved(New ResourceNameEventArgs(Me.DriverHrefTextBox.Text))
                    End If
                    Me.DriverHrefTextBox.Text = dialog.SelectedEntity.Name
                    OnDependentResourceAdded(New ResourceEventArgs(dialog.SelectedEntity))
                    Me.ValidateChildren()
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End Using
    End Sub

    Private Sub RemoveAdaptiveModuleButton_Click(sender As System.Object, e As System.EventArgs) Handles RemoveAdaptiveModuleButton.Click
        OnDependentResourceRemoved(New ResourceNameEventArgs(Me._testSection.ModuleHref))
        Me.ModuleHrefTextBox.Text = String.Empty
    End Sub

    Private Sub RemoveAdaptiveDriverButton_Click(sender As Object, e As EventArgs) Handles RemoveAdaptiveDriverButton.Click
        OnDependentResourceRemoved(New ResourceNameEventArgs(Me._testSection.DriverHref))
        Me.DriverHrefTextBox.Text = String.Empty
    End Sub

    Private Sub ShowHideManyOutputSettingsControl()
        Dim blnShow = False
        If _testSectionModel IsNot Nothing AndAlso Not String.IsNullOrEmpty(_testSectionModel.ItemDataSource) Then
            Dim itemDataSource As ItemDataSource = GetItemDataSource()
            If itemDataSource IsNot Nothing AndAlso TypeOf itemDataSource Is ItemDataSourceManyOutput Then
                blnShow = True
            End If
        End If
        If Not blnShow Then
            WeightTextbox.Value = 0
        Else
            WeightTextbox.Value = If(_testSectionModel.ItemDataSourceBehaviour = DataSourceBehaviourEnum.Seeding, 0, 1)
        End If
        WeightTextbox.Visible = blnShow
        WeightLabel.Visible = blnShow
    End Sub

    Public Function GetItemDataSource() As ItemDataSource
        Dim res = ResourceFactory.Instance.GetResourceByNameWithOption(TestEntity.BankId, _testSectionModel.ItemDataSource, New ResourceRequestDTO())
        If res IsNot Nothing AndAlso TypeOf res Is DataSourceResourceEntity Then
            Dim entity = DirectCast(res, DataSourceResourceEntity)
            Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(entity)
            If settings IsNot Nothing Then
                Dim dataSource = TryCast(settings.CreateGetDataSource(), ItemDataSource)
                If dataSource IsNot Nothing Then
                    Return dataSource
                End If
            End If
        End If
        Return Nothing
    End Function


    Public Sub New(itemDataSourcesAlreadyAdded As List(Of String))
        InitializeComponent()

        _itemDataSourcesAlreadyAdded = itemDataSourcesAlreadyAdded
    End Sub

End Class