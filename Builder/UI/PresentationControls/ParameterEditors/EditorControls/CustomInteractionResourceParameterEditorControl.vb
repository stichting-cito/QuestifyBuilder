Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic

Public Class CustomInteractionResourceParameterEditorControl
    Inherits ResizableParameterEditorControlBase(Of CustomInteractionResourceParameter)


    Public Event ScorableChanged As EventHandler(Of EventArgs)



    Private _parameterVisibilityDetermined As Boolean = False
    Private ReadOnly _ciDimensionsHelper As CustomInterActionDimensionsHelper


    Public Property EditResourceButtonVisible As Boolean
        Get
            Return EditResourceButton.Visible
        End Get
        Set(value As Boolean)
            EditResourceButton.Visible = value
        End Set
    End Property

    Public Property DeleteResourceButtonVisible As Boolean
        Get
            Return DeleteResourceButton.Visible
        End Get
        Set(value As Boolean)
            DeleteResourceButton.Visible = value
        End Set
    End Property

    Public Property ResourceParameter() As CustomInteractionResourceParameter
        Get
            Return _parameter
        End Get
        Set(ByVal value As CustomInteractionResourceParameter)
            _parameter = value
            ParameterBindingSource.DataSource = _parameter

            If _parameter IsNot Nothing Then
                Dim labelName As String = _parameter.DesignerSettings.GetSettingValueByKey("label")
                ParameterEditorTooltip.ToolTipTitle = labelName
            Else
                ParameterEditorTooltip.ToolTipTitle = String.Empty
            End If
        End Set
    End Property




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal resourceParameter As CustomInteractionResourceParameter, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase)
        Me.New()

        Me.ResourceParameter = resourceParameter
        AddHandler _parameter.ResourceNeeded, AddressOf OnResourceNeeded
        Initialize(parent, resourceManager, resourceEntity)

        _ciDimensionsHelper = New CustomInterActionDimensionsHelper(_resourceEntity, _parameter)
    End Sub

    Public Sub New()
        InitializeComponent()

    End Sub



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        If resource Is Nothing Then
            Throw New ArgumentNullException("resource")
        End If

        Return (_parameter.Value.Equals(resource.Name))
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty

        If String.IsNullOrEmpty(_parameter.Value) AndAlso _required AndAlso Enabled Then
            result = My.Resources.ThisParameterMustBeFilledPleaseSelectAMediaObject
        End If

        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
        OnRemovingResource(New ResourceNameEventArgs(_parameter.Value))
    End Sub



    Private Sub SelectResourceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectResourceButton.Click
        Using dialog As New SelectMediaResourceDialog(_resourceEntity.BankId, _contextIdentifier, ResourceManager)
            AddHandler dialog.ResourcesLoaded, AddressOf SelectMediaResourceDialog_DialogResourcesLoaded
            AddHandler dialog.ResourceNeeded, AddressOf SelectMediaResourceDialog_ResourceNeeded

            dialog.Filter = _filter
            dialog.CanPickFiles = True

            Dim previousResourceName = _parameter.Value
            If dialog.ShowDialog() = DialogResult.OK AndAlso dialog.SelectedEntity.name <> _parameter.Value Then
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.resourceId) Then
                    OnRemovingResource(New ResourceNameEventArgs(_parameter.Value))

                    _parameter.Value = dialog.SelectedEntity.name
                    _parameterVisibilityDetermined = False
                    InitControls()

                    Dim fileName As String = _ciDimensionsHelper.SaveCustomInteractionResourceToTempLocation(dialog.SelectedEntity.ResourceId)

                    ScorableCheckBox.Enabled = _ciDimensionsHelper.GetCiPropertiesAndShouldEnableScoring(fileName)

                    _parameter = _ciDimensionsHelper.CiResourceParameter

                    OnAddingResource(New ResourceNameEventArgs(_parameter.Value))
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                _parameter.Value = previousResourceName
            End If

            RemoveHandler dialog.ResourcesLoaded, AddressOf SelectMediaResourceDialog_DialogResourcesLoaded
            RemoveHandler dialog.ResourceNeeded, AddressOf SelectMediaResourceDialog_ResourceNeeded
        End Using
    End Sub

    Public Sub SetParameterVisibility()
        _sizeChanging = True
        Dim validateCi = _ciDimensionsHelper.ValidateCustomInteractionResourceIsCiOrPci()
        DimensionsPanel.Visible = Not String.IsNullOrEmpty(_parameter.Value) AndAlso (ForceShowDimensions OrElse Not validateCi)
        ScorableCheckBox.Enabled = Not validateCi
        _sizeChanging = False
        _parameterVisibilityDetermined = True
    End Sub

    Private Sub DeleteResourceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteResourceButton.Click
        OnRemovingResource(New ResourceNameEventArgs(_parameter.Value))
        ResourceParameter.Value = String.Empty
        _parameterVisibilityDetermined = False
        InitControls()
    End Sub

    Private Sub EditResourceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditResourceButton.Click
        OnEditResource(New ResourceNameEventArgs(_parameter.Value))
        RefreshResourceData()
    End Sub

    Private Sub SelectMediaResourceDialog_DialogResourcesLoaded(ByVal sender As Object, ByVal e As ResourceEventArgs)
        If String.IsNullOrEmpty(_parameter.Value) Then Return

        Dim dependentResource As DependentResourceEntity = _resourceEntity.GetDependentResourceByName(_parameter.Value)

        If dependentResource Is Nothing Then
            e.Resource = Nothing
            ResourceParameter.Value = String.Empty
        Else
            e.Resource = DtoFactory.Generic.Get(dependentResource.DependentResource.ResourceId)
            RefreshResourceData()
        End If
    End Sub

    Private Sub SelectMediaResourceDialog_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        OnResourceNeeded(Me, e)
    End Sub

    Protected Overrides Sub DimensionEditing(enable As Boolean)
        WidthTextBox.ReadOnly = Not enable
        HeightTextBox.ReadOnly = Not enable
        KeepAspectRatioCheckBox.Enabled = enable
    End Sub



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If (_parameter IsNot Nothing) Then RemoveHandler _parameter.ResourceNeeded, AddressOf OnResourceNeeded
        ParameterEditorTooltip.Dispose()
    End Sub

    Private Sub WidthTextBox_TextChanged(sender As Object, e As EventArgs) Handles WidthTextBox.TextChanged
        HandleWidthTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub

    Private Sub HeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles HeightTextBox.TextChanged
        HandleHeightTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub

    Private Sub InitControls()
        If Not _parameterVisibilityDetermined Then SetParameterVisibility()
        DimensionEditing(EnableEditDimensions)
        SetDimensionValues(HeightTextBox, WidthTextBox)
    End Sub

    Private Sub CustomInteractionResourceParameterEditorControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not DesignMode Then
            InitControls()
        End If
    End Sub

    Private Sub ScorableCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ScorableCheckBox.CheckedChanged
        RaiseEvent ScorableChanged(ScorableCheckBox, e)
    End Sub
End Class
