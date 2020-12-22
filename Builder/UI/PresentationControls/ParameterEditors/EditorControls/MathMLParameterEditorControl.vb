Imports Cito.Tester.ContentModel
Imports System.IO
Imports System.Web
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports System.Linq

Public Class MathMLParameterEditorControl

    Private _mathMLParameter As MathMLParameter
    Private _mathMlEditorPlugin As IMathMlEditorPlugin
    Private _required As Boolean = False
    Private _mathImageWithMathML() As Byte

    Public Sub New(ByVal mathMLParameter As MathMLParameter)
        InitializeComponent()

        _mathMLParameter = mathMLParameter

        DrawImage()
    End Sub


    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Me.Enabled Then
            If String.IsNullOrEmpty(_mathMLParameter.Value) AndAlso _required Then
                result = MandatoryParameterMessage(_mathMLParameter.DesignerSettings.GetSettingValueByKey("label"), _mathMLParameter.DesignerSettings.GetSettingValueByKey("group"))
            End If
        End If
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub


    Private Sub DrawImage()
        Dim mathML As String = _mathMLParameter.Value

        If Not String.IsNullOrEmpty(mathML) Then
            Dim mathImage() As Byte = GetMathMlPlugin().RenderPng(mathML)
            _mathImageWithMathML = MathMLHelper.SetMathMLMetaDataInImage(mathImage, mathML)
            Dim pictureBytes As New MemoryStream(mathImage)
            mathMLPictureBox.Image = Image.FromStream(pictureBytes)
        End If
    End Sub

    Private Sub mathMLPictureBox_Click(sender As Object, e As EventArgs) Handles mathMLPictureBox.Click
        Using formulaDialog As EditMathFormulaDialog = New EditMathFormulaDialog(_mathImageWithMathML, "formula.png", MathMLHelper.GetBaseFont(), False, _mathMLParameter.Value)
            AddHandler formulaDialog.EditFormula, Sub(s, f) OnEditorFormula(f)
            If (Me.ParentForm IsNot Nothing) Then Me.ParentForm.AddOwnedForm(formulaDialog)
            formulaDialog.Location = Me.Location
            formulaDialog.ShowDialog(Me)
        End Using
    End Sub

    Private Sub OnEditorFormula(e As FormulaEventArgs)
        _mathMLParameter.Value = HttpUtility.HtmlDecode(e.MathMlValue)
        DrawImage()
    End Sub

    Private Function GetMathMlPlugin() As IMathMlEditorPlugin
        If _mathMlEditorPlugin Is Nothing Then
            _mathMlEditorPlugin = PluginHelper.MathMlPlugin
            If _mathMlEditorPlugin Is Nothing Then
                MessageBox.Show(My.Resources.MathML_NoPlugin)
            End If
        End If
        Return _mathMlEditorPlugin
    End Function

End Class
