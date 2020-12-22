Imports Cito.Tester.ContentModel
Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.IO
Imports System.Text.RegularExpressions
Imports Cito.Tester.Common
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories

Public Class GapImageParameterEditorControl


    Private ReadOnly _resourceEntity As ResourceEntity
    Private _gapImageParameter As GapImageParameter
    Private ReadOnly _required As Boolean
    Private ReadOnly _validationRegEx As String
    Private ReadOnly _validationRegExMessage As String
    Private _resourceParamControl As ResourceParameterEditorControl
    Private _textParamControl As GapTextParameterEditorControl
    Private ReadOnly _textParameter As New GapTextParameter()
    Private _duringConstruction As Boolean = True
    Private _currentSource As String



    Public Property GapImageParameter() As GapImageParameter
        Get
            Return _gapImageParameter
        End Get
        Set(ByVal value As GapImageParameter)
            _gapImageParameter = value
            ParameterBindingSource.DataSource = _gapImageParameter
        End Set
    End Property



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Enabled Then
            If _required AndAlso ((_gapImageParameter.ContentType = GapImageParameter.GapImageParameterContentType.Text AndAlso String.IsNullOrEmpty(_gapImageParameter.EnteredText)) OrElse (Not _gapImageParameter.ContentType = GapImageParameter.GapImageParameterContentType.Text AndAlso String.IsNullOrEmpty(_gapImageParameter.Value))) Then
                result = MandatoryParameterMessage(_gapImageParameter.DesignerSettings.GetSettingValueByKey("label"), _gapImageParameter.DesignerSettings.GetSettingValueByKey("group"))
            End If

            If Not String.IsNullOrEmpty(_validationRegEx) Then
                Dim regExEngineInstance As New Regex(_validationRegEx)
                Dim m As Match = regExEngineInstance.Match(_gapImageParameter.Value)
                If Not m.Success Then
                    result = _validationRegExMessage
                End If
            End If
        End If
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub





    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal gapImageParameter As GapImageParameter, ByVal resourceEntity As ResourceEntity, ByVal resourceManager As ResourceManagerBase)
        InitializeComponent()

        EditorParent = parent
        Me.GapImageParameter = gapImageParameter
        Me.ResourceManager = resourceManager

        _resourceEntity = resourceEntity

        InitControls()
        AddSpecificControl()

        AddHandler _textParameter.PropertyChanged, AddressOf TextualValueChanged

        Try
            Dim requiredSettingValue As String = _gapImageParameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

            Dim validationRegExSettingValue As String = _gapImageParameter.DesignerSettings.GetSettingValueByKey("validationRegEx")
            If Not String.IsNullOrEmpty(validationRegExSettingValue) Then _validationRegEx = validationRegExSettingValue Else _validationRegEx = String.Empty

            Dim validationRegExMessageSettingValue As String = _gapImageParameter.DesignerSettings.GetSettingValueByKey("validationRegExMessage")
            If Not String.IsNullOrEmpty(validationRegExMessageSettingValue) Then _validationRegExMessage = validationRegExMessageSettingValue Else _validationRegExMessage = "Text is not in the correct format."

        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _gapImageParameter.Name))
        End Try

        _duringConstruction = False
    End Sub


    Private Sub TextualValueChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        If e.PropertyName.ToLower = "value" Then
            GapImageParameter.EnteredText = _textParameter.Value
        ElseIf e.PropertyName.ToLower = "width" Then
            GapImageParameter.Width = _textParameter.Width
        ElseIf e.PropertyName.ToLower = "height" Then
            GapImageParameter.Height = _textParameter.Height
        End If
    End Sub

    Private Sub InitControls()
        _resourceParamControl = New ResourceParameterEditorControl(_parent, GapImageParameter, _resourceEntity, ResourceManager)
        _resourceParamControl.ForceShowDimensions = True
        _resourceParamControl.EnableEditDimensions = True
        _resourceParamControl.Dock = DockStyle.Fill
        AddHandler _resourceParamControl.EditResource, AddressOf EditResourceHandler
        AddHandler _resourceParamControl.AddingResource, AddressOf Adding_Resource

        _textParameter.Value = GapImageParameter.EnteredText
        _textParameter.Height = GapImageParameter.Height
        _textParameter.Width = GapImageParameter.Width
        _textParamControl = New GapTextParameterEditorControl(_parent, _textParameter)
        _textParamControl.Dock = DockStyle.Fill
        _textParamControl.ForceShowDimensions = True
        _textParamControl.EnableEditDimensions = True
        _textParamControl.NumericUpDownMatchMax.Visible = False
        _textParamControl.LabelMatchMax.Visible = False
        comboxImageTextOrFormula.SelectedIndex = CInt(GapImageParameter.ContentType)
    End Sub

    Private Sub Adding_Resource(sender As Object, e As EventArgs)
        If _currentSource <> _gapImageParameter.Value Then
            _currentSource = _gapImageParameter.Value
        End If
    End Sub

    Private Sub AddSpecificControl()
        SuspendLayout()

        ParamControlPanel.Controls.Clear()
        Dim containerPadding = New Padding(0)

        If IsTextMode Then
            _textParamControl.SetDimensionVisibility()
            ParamControlPanel.Controls.Add(_textParamControl)
        Else
            _resourceParamControl.SetDimensionVisibility()
            ParamControlPanel.Controls.Add(_resourceParamControl)
            If IsFormulaMode Then
                _resourceParamControl.SelectResourceButton.Visible = False
                _resourceParamControl.EditResourceButton.Visible = True
            Else
                _resourceParamControl.SelectResourceButton.Visible = True
                _resourceParamControl.EditResourceButton.Visible = False
            End If
        End If
        ParamControlPanel.Padding = containerPadding
        ResumeLayout()
    End Sub

    Private ReadOnly Property IsTextMode As Boolean
        Get
            Return (GapImageParameter.ContentType = GapImageParameter.GapImageParameterContentType.Text)
        End Get
    End Property

    Private ReadOnly Property IsFormulaMode As Boolean
        Get
            Return (GapImageParameter.ContentType = GapImageParameter.GapImageParameterContentType.FormulaImage)
        End Get
    End Property


    Private Sub comboxImageTextOrFormula_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboxImageTextOrFormula.SelectedIndexChanged
        GapImageParameter.ContentType = DirectCast(comboxImageTextOrFormula.SelectedIndex, GapImageParameter.GapImageParameterContentType)
        If Not _duringConstruction Then
            GapImageParameter.Value = String.Empty
            GapImageParameter.EnteredText = String.Empty
            _textParameter.Value = String.Empty
        End If
        AddSpecificControl()
    End Sub

    Private Sub EditResourceHandler(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        If IsFormulaMode Then

            Using formulaDialog As EditMathFormulaDialog = New EditMathFormulaDialog(ReadMathMLResource(e.ResourceName), e.ResourceName, Font, False)
                AddHandler formulaDialog.EditFormula, Sub(s, ea) OnEditorFormula(ea)
                If (Me.ParentForm IsNot Nothing) Then Me.ParentForm.AddOwnedForm(formulaDialog)
                formulaDialog.Location = Me.Location
                formulaDialog.ShowDialog(Me)
            End Using
            Me.Focus()
        End If
    End Sub

    Private Sub OnEditorFormula(formulaArgs As FormulaEventArgs)
        Dim currentImageUri As Uri = Nothing
        If Not String.IsNullOrEmpty(formulaArgs.NewImageName) Then
            currentImageUri = New Uri(Path.Combine(TempStorageHelper.GetTempStoragePath(), formulaArgs.NewImageName))
        End If
        Dim imageFileNameUri As Uri = MathMLHelper.StoreMathMLImageInTempStore(formulaArgs.Image, currentImageUri)
        GapImageParameter.Value = MathMLHelper.StoreMathMLImageAsGenericResource(imageFileNameUri, _resourceEntity.BankId)
        If _resourceParamControl IsNot Nothing AndAlso _resourceEntity IsNot Nothing Then
            _resourceParamControl.SetNewDimensions(DtoFactory.Generic.Get(_resourceEntity.BankId, GapImageParameter.Value))
        End If
    End Sub

    Private Function ReadMathMLResource(ByVal resourceName As String) As Byte()
        If Not String.IsNullOrEmpty(resourceName) Then
            Dim ImageGenericResource As GenericResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByNameWithOption(_resourceEntity.BankId, resourceName, New ResourceRequestDTO()), GenericResourceEntity)
            If ImageGenericResource IsNot Nothing Then
                ImageGenericResource.ResourceData = ResourceFactory.Instance.GetResourceData(ImageGenericResource)

                Using memStream As New MemoryStream(ImageGenericResource.ResourceData.BinData)
                    Return memStream.ToArray()
                End Using
            End If
        End If

        Return Nothing
    End Function

    Private Sub GapImageParameterEditorControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Height = 150
    End Sub
End Class
