Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers
Imports Questify.Builder.Logic.Service.Interfaces.UI

Public Class XhtmlDialogs
    Public Shared Sub OpenSymbolDialog(owner As IWin32Window, selection As ISelection, location As Point)
        Dim addSymbolsDialog As IAddSymbolDialog = New AddSymbolsDialog()
        AddHandler addSymbolsDialog.SpecialSymbolPicked, Sub(sender, e) selection.Text = e.UnicodeValue(0)
        addSymbolsDialog.Show(owner, location)
    End Sub

    Public Shared Sub OpenMathFormulaDialog(owner As IWin32Window, handler As HtmlFormulaHandler, src As String, fnt As Font, location As Point, parnt As Form)
        Dim mathMLImage As Byte() = handler.ReadMathMLImage()
        Dim mathMLAttribute As String = handler.ReadMathMlAttribute()
        using formulaDialog = New EditMathFormulaDialog(mathMlImage, src, fnt, False, mathMlAttribute)
            AddHandler formulaDialog.EditFormula, Sub(sender, e) OnEditorFormula(e, handler)
            If parnt IsNot Nothing Then
                parnt.AddOwnedForm(formulaDialog)
            End If
            formulaDialog.Location = location
            formulaDialog.ShowDialog(owner)
        End Using
    End Sub

    Private Shared Sub OnEditorFormula(formulaArgs As FormulaEventArgs, handler As HtmlFormulaHandler)
        handler.EditMathMlFormula(formulaArgs.Image, formulaArgs.NewImageName, formulaArgs.VerticalAlignValue, formulaArgs.MathMlValue)
    End Sub
End Class
