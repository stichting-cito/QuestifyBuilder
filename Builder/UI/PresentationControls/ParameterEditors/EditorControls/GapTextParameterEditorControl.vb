Imports System.Text.RegularExpressions
Imports System.Web
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Exceptions


Public Class GapTextParameterEditorControl
    Inherits ResizableParameterEditorControlBase(Of GapTextParameter)


    Private ReadOnly _validationRegEx As String
    Private ReadOnly _validationRegExMessage As String



    Public Property GapTextParameter As GapTextParameter
        Get
            Return _parameter
        End Get
        Set
            _parameter = Value
            ParameterBindingSource.DataSource = _parameter
        End Set
    End Property



    Public Overrides Function ResourceUsedInThisParameter(resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Not Enabled Then
            Return result
        End If

        If String.IsNullOrEmpty(_parameter.Value) AndAlso _required Then
            result = MandatoryParameterMessage(_parameter.DesignerSettings.GetSettingValueByKey("label"), _parameter.DesignerSettings.GetSettingValueByKey("group"))
        End If

        If Not String.IsNullOrEmpty(_validationRegEx) Then
            Dim regExEngineInstance As New Regex(_validationRegEx)
            Dim m As Match = regExEngineInstance.Match(_parameter.Value)
            If Not m.Success Then
                result = _validationRegExMessage
            End If
        End If

        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub



    Private Sub TextBoxBindingFormatter(sender As Object, e As ConvertEventArgs)
        e.Value = HttpUtility.HtmlDecode(DirectCast(e.Value, String))
    End Sub

    Private Sub TextBoxBindingParser(sender As Object, e As ConvertEventArgs)
        e.Value = HttpUtility.HtmlEncode(DirectCast(e.Value, String))
    End Sub

    Protected Overrides Sub DimensionEditing(enable As Boolean)
        WidthTextBox.ReadOnly = Not enable
        HeightTextBox.ReadOnly = Not enable
        KeepAspectRatioCheckBox.Enabled = enable
    End Sub

    Private Sub InitControls()
        SetDimensionVisibility()
        DimensionEditing(EnableEditDimensions)
        SetDimensionValues(HeightTextBox, WidthTextBox)
    End Sub



    Public Overloads Sub SetDimensionVisibility()
        SetDimensionVisibility(DimensionsPanel, 121, 52)
    End Sub



    Private Sub GapTextParameterEditorControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim textBoxBinding As Binding = GapTextParameterTextBox.DataBindings.Item("Text")
        AddHandler textBoxBinding.Format, AddressOf TextBoxBindingFormatter
        AddHandler textBoxBinding.Parse, AddressOf TextBoxBindingParser

        If DesignMode Then
            Return
        End If

        InitControls()
    End Sub

    Private Sub GapTextParameterTextBox_TextChanged(sender As Object, e As EventArgs) Handles GapTextParameterTextBox.TextChanged
        InitControls()
    End Sub

    Private Sub WidthTextBox_TextChanged(sender As Object, e As EventArgs) Handles WidthTextBox.TextChanged
        HandleWidthTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub

    Private Sub HeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles HeightTextBox.TextChanged
        HandleHeightTextBoxChanged(KeepAspectRatioCheckBox.Checked, HeightTextBox, WidthTextBox)
    End Sub



    Public Sub New(parent As ParameterSetsEditor, gapTextParameter As GapTextParameter)
        InitializeComponent()

        EditorParent = parent
        Me.GapTextParameter = gapTextParameter

        If _parameter.Height > 0 Then
            HeightValue = _parameter.Height
        Else
            HeightValue = 15
        End If
        If _parameter.Width > 0 Then
            WidthValue = _parameter.Width
        Else
            WidthValue = 100
        End If

        Try
            Dim requiredSettingValue As String = _parameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then
                _required = Boolean.Parse(requiredSettingValue)
            End If

            Dim validationRegExSettingValue As String = _parameter.DesignerSettings.GetSettingValueByKey("validationRegEx")
            If Not String.IsNullOrEmpty(validationRegExSettingValue) Then
                _validationRegEx = validationRegExSettingValue
            Else
                _validationRegEx = String.Empty
            End If

            Dim validationRegExMessageSettingValue As String = _parameter.DesignerSettings.GetSettingValueByKey("validationRegExMessage")
            If Not String.IsNullOrEmpty(validationRegExMessageSettingValue) Then
                _validationRegExMessage = validationRegExMessageSettingValue
            Else
                _validationRegExMessage = "Text is not in the correct format."
            End If

        Catch ex As Exception
            Throw New AppLogicException($"Error parsing designer settings for parameter '{_parameter.Name}'.")
        End Try

    End Sub


End Class
