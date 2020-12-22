Imports System.Text
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class PlainTextParameterEditorControl


    Private _plainTextParameter As Cito.Tester.ContentModel.PlainTextParameter
    Private ReadOnly _required As Boolean
    Private ReadOnly _validationRegEx As String
    Private ReadOnly _validationRegExMessage As String




    Public Property PlainTextParameter() As Cito.Tester.ContentModel.PlainTextParameter
        Get
            Return _plainTextParameter
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.PlainTextParameter)
            _plainTextParameter = value
            ParameterBindingSource.DataSource = _plainTextParameter
        End Set
    End Property



    Private _startingHeight As Integer = 24



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Me.Enabled Then
            If String.IsNullOrEmpty(_plainTextParameter.Value) AndAlso _required Then
                result = MandatoryParameterMessage(_plainTextParameter.DesignerSettings.GetSettingValueByKey("label"), _plainTextParameter.DesignerSettings.GetSettingValueByKey("group"))
            End If

            If Not String.IsNullOrEmpty(_validationRegEx) Then
                Dim regExEngineInstance As New RegularExpressions.Regex(_validationRegEx)
                Dim m As RegularExpressions.Match = regExEngineInstance.Match(_plainTextParameter.Value)
                If Not m.Success Then
                    result = _validationRegExMessage
                End If
            End If
        End If
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub



    Private Sub TextBoxBindingFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = System.Web.HttpUtility.HtmlDecode(DirectCast(e.Value, String))
    End Sub

    Private Sub TextBoxBindingParser(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = System.Web.HttpUtility.HtmlEncode(DirectCast(e.Value, String))
    End Sub



    Private Sub PlainTextParameterEditorControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim textBoxBinding As Binding = PlainTextParameterTextBox.DataBindings.Item("Text")
        AddHandler textBoxBinding.Format, AddressOf TextBoxBindingFormatter
        AddHandler textBoxBinding.Parse, AddressOf TextBoxBindingParser
    End Sub




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal plainTextParameter As Cito.Tester.ContentModel.PlainTextParameter)
        InitializeComponent()

        Me.EditorParent = parent
        Me.PlainTextParameter = plainTextParameter

        Try
            Dim requiredSettingValue As String = _plainTextParameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

            Dim validationRegExSettingValue As String = _plainTextParameter.DesignerSettings.GetSettingValueByKey("validationRegEx")
            If Not String.IsNullOrEmpty(validationRegExSettingValue) Then _validationRegEx = validationRegExSettingValue Else _validationRegEx = String.Empty

            Dim validationRegExMessageSettingValue As String = _plainTextParameter.DesignerSettings.GetSettingValueByKey("validationRegExMessage")
            If Not String.IsNullOrEmpty(validationRegExMessageSettingValue) Then _validationRegExMessage = validationRegExMessageSettingValue Else _validationRegExMessage = "Text is not in the correct format."

            Dim isMultiLineValue As String = _plainTextParameter.DesignerSettings.GetSettingValueByKey("multiline")
            Dim isMultiLine As Boolean
            If Not String.IsNullOrWhiteSpace(isMultiLineValue) AndAlso Boolean.TryParse(isMultiLineValue, isMultiLine) Then
                PlainTextParameterTextBox.Multiline = isMultiLine
                PlainTextParameterTextBox.AcceptsReturn = isMultiLine
                _startingHeight = Me.Height
                Me.Height = 4 * _startingHeight
            Else
                PlainTextParameterTextBox.Multiline = False
                PlainTextParameterTextBox.AcceptsReturn = False
                Me.Height = _startingHeight
            End If

        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _plainTextParameter.Name))
        End Try

    End Sub


End Class
