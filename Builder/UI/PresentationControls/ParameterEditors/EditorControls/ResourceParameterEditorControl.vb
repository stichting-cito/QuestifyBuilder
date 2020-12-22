Imports System.ComponentModel
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ResourceParameterEditorControl
    Inherits ResizableParameterEditorControlBase(Of ResourceParameter)

    Private _dimensionsRetrieved As Boolean = False

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

    <Browsable(True)>
    <Category("Behavior")>
    Public Property IsSVGFreeFormResource As Boolean

    Public Property ResourceParameter() As ResourceParameter
        Get
            Return _parameter
        End Get
        Set(ByVal value As ResourceParameter)
            _parameter = value
            ParameterBindingSource.DataSource = _parameter

            If _parameter IsNot Nothing Then
                Dim labelName As String = _parameter.DesignerSettings.GetSettingValueByKey("label")
                ParameterEditorToolip.ToolTipTitle = labelName
            Else
                ParameterEditorToolip.ToolTipTitle = String.Empty
            End If
        End Set
    End Property




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal resourceParameter As ResourceParameter, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase)
        Me.New()

        Me.ResourceParameter = resourceParameter
        AddHandler _parameter.ResourceNeeded, AddressOf OnResourceNeeded

        Initialize(parent, resourceManager, resourceEntity)
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

        If String.IsNullOrEmpty(_parameter.Value) AndAlso _required AndAlso Me.Enabled Then
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
            dialog.CanPickFiles = Not (TypeOf _parameter Is XhtmlResourceParameter)

            Dim previousResourceName = _parameter.Value
            If dialog.ShowDialog() = DialogResult.OK AndAlso dialog.SelectedEntity.name <> _parameter.Value Then
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.resourceId) Then
                    OnRemovingResource(New ResourceNameEventArgs(_parameter.Value))

                    _parameter.Value = dialog.SelectedEntity.name
                    _dimensionsRetrieved = False
                    SetNewDimensions(dialog.SelectedEntity)
                    InitControls()
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

    Public Overloads Sub SetDimensionVisibility()
        _sizeChanging = True
        SetDimensionVisibility(DimensionsPanel, 100, 26)
        _sizeChanging = False
    End Sub

    Public Sub SetNewDimensions(genericResource As GenericResourceDto)
        If genericResource IsNot Nothing AndAlso genericResource.Height.HasValue AndAlso genericResource.Width.HasValue Then
            WidthValue = genericResource.Width.Value
            HeightValue = genericResource.Height.Value
            ResourceParameter.Width = WidthValue.Value
            ResourceParameter.Height = HeightValue.Value
        Else
            ResourceParameter.Width = 0
            ResourceParameter.Height = 0
            ResourceParameter.EditSize = False
        End If
        _dimensionsRetrieved = True
    End Sub

    Private Sub RetrieveDimensions()
        If _parameter IsNot Nothing AndAlso Not String.IsNullOrEmpty(_parameter.Value) Then
            Dim resource = ResourceFactory.Instance.GetResourceByNameWithOption(_resourceEntity.BankId, _parameter.Value, New ResourceRequestDTO())
            If resource IsNot Nothing AndAlso TypeOf resource Is GenericResourceEntity Then
                Dim genericResource = DirectCast(resource, GenericResourceEntity)
                WidthValue = genericResource.Width
                HeightValue = genericResource.Height
                KeepAspectRatioCheckBox.Checked = WidthValue.HasValue AndAlso HeightValue.HasValue
                KeepAspectRatioCheckBox.Visible = WidthValue.HasValue AndAlso HeightValue.HasValue
            Else
                KeepAspectRatioCheckBox.Checked = False
                KeepAspectRatioCheckBox.Visible = False
            End If
        End If
        _dimensionsRetrieved = True
    End Sub

    Private Sub DeleteResourceButton_Click(sender As Object, e As EventArgs) Handles DeleteResourceButton.Click
        OnRemovingResource(New ResourceNameEventArgs(_parameter.Value))
        ResourceParameter.Value = String.Empty
    End Sub

    Private Sub EditResourceButton_Click(sender As Object, e As EventArgs) Handles EditResourceButton.Click
        OnEditResource(New ResourceNameEventArgs(_parameter.Value))
        RefreshResourceData()
    End Sub

    Private Sub SelectMediaResourceDialog_DialogResourcesLoaded(sender As Object, e As ResourceEventArgs)
        If Not String.IsNullOrEmpty(_parameter.Value) Then
            Dim dependentResource As DependentResourceEntity = _resourceEntity.GetDependentResourceByName(_parameter.Value)

            If dependentResource Is Nothing Then
                e.Resource = Nothing
                ResourceParameter.Value = String.Empty
            Else
                e.Resource = DtoFactory.Generic.Get(dependentResource.DependentResource.ResourceId)
                RefreshResourceData()
            End If
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
        ParameterEditorToolip.Dispose()
    End Sub

    Private Sub WidthTextBox_TextChanged(sender As Object, e As EventArgs) Handles WidthTextBox.TextChanged
        HandleWidthTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub

    Private Sub HeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles HeightTextBox.TextChanged
        HandleHeightTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub

    Protected Sub InitControls()
        If Not _dimensionsRetrieved Then RetrieveDimensions()
        SetDimensionVisibility()
        SetDimensionValues(HeightTextBox, WidthTextBox)
    End Sub

    Private Sub ResourceParameterEditorControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not DesignMode Then
            InitControls()
        End If
    End Sub

End Class
