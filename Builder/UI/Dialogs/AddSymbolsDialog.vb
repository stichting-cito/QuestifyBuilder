Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions.Symbols
Imports Questify.Builder.Logic.Service.Interfaces.UI

Public Class AddSymbolsDialog
    Implements IAddSymbolDialog

    Public Event SpecialSymbolPicked As EventHandler(Of SpecialSymbolEventArgs) Implements IAddSymbolDialog.SpecialSymbolPicked
    Public Event DialogClosed As EventHandler(Of EventArgs) Implements IAddSymbolDialog.DialogClosed

    Public Sub DisposeFromInterface() Implements IAddSymbolDialog.Dispose
        Dispose(True)
    End Sub

    Public Function IsDisposedFromInterface() As Boolean Implements IAddSymbolDialog.IsDisposed
        Return IsDisposed
    End Function

    Public Sub ShowFromInterface(ByVal ownerForm As IWin32Window, ByVal windowLocation As Point) Implements IAddSymbolDialog.Show
        Location = windowLocation

        If Not Application.OpenForms.OfType(Of AddSymbolsDialog).Any() Then
            Init()
            Show(ownerForm)
        End If

        TopMost = True
        Focus()
    End Sub

    Private Sub Init()
        For Each symbolGrp As SpecialSymbolGroup In SpecialSymbolGroup.Symbols
            ComboBoxSymbolGroup.Items.Add(symbolGrp)
        Next

        PanelSpecialSymbols.Font = New Font(PanelSpecialSymbols.Font.FontFamily.Name, 16, FontStyle.Regular, GraphicsUnit.Pixel, PanelSpecialSymbols.Font.GdiCharSet)
        PanelSpecialSymbols.AutoSize = False

        ComboBoxSymbolGroup.SelectedIndex = 0
        ComboBoxSymbolGroup.Enabled = (ComboBoxSymbolGroup.Items.Count > 1)
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub RenderSelectedLabel(ByVal selectedLabel As Control)

        If Not labelSymbolText.Tag Is Nothing Then
            Dim prevLabel As Label = DirectCast(labelSymbolText.Tag, Label)
            prevLabel.BackColor = Color.White
        End If

        Dim specSymbol As SpecialSymbol = DirectCast(selectedLabel.Tag, SpecialSymbol)

        labelSymbolText.Tag = selectedLabel
        labelSymbolText.Text = specSymbol.UnicodeString
        LabelSymbolName.Text = specSymbol.SymbolName
        selectedLabel.BackColor = Color.Aqua
    End Sub

    Private Sub SymbolLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RenderSelectedLabel(DirectCast(sender, Control))

        InsertSymbolButton.Enabled = True
    End Sub

    Private Sub SymbolLabel_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim senderControl As Control = DirectCast(sender, Control)
        Dim specSymbol As SpecialSymbol = DirectCast(senderControl.Tag, SpecialSymbol)

        RaiseEvent SpecialSymbolPicked(Me, New SpecialSymbolEventArgs(specSymbol.HtmlUnicodeDecimalCode, specSymbol.UnicodeString))
    End Sub

    Private Sub ComboBoxSymbolGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxSymbolGroup.SelectedIndexChanged
        InsertSymbolButton.Enabled = False

        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim labelControls As New List(Of Control)
        Dim symbols = DirectCast(ComboBoxSymbolGroup.SelectedItem, SpecialSymbolGroup).SymbolList

        For Each specSymbol As SpecialSymbol In symbols

            Dim SymbolLabel As New Label With {
                .Dock = DockStyle.None,
                .Anchor = AnchorStyles.None,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Width = 28,
                .Height = 24
            }

            SymbolLabel.Top = row * (SymbolLabel.Height + 2)
            SymbolLabel.Left = col * (SymbolLabel.Width + 2)
            SymbolLabel.BackColor = Color.White
            SymbolLabel.Text = specSymbol.UnicodeString()
            SymbolLabel.Tag = specSymbol

            labelControls.Add(SymbolLabel)

            AddHandler SymbolLabel.Click, AddressOf SymbolLabel_Click
            AddHandler SymbolLabel.DoubleClick, AddressOf SymbolLabel_DoubleClick

            col += 1
            If (col Mod 14) = 0 Then
                col = 0
                row += 1
            End If
        Next

        PanelSpecialSymbols.Controls.Clear()
        PanelSpecialSymbols.Controls.AddRange(labelControls.ToArray())
    End Sub

    Private Sub InsertSymbolButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertSymbolButton.Click
        If labelSymbolText.Tag IsNot Nothing Then
            Dim symbolLabel As Label = DirectCast(labelSymbolText.Tag, Label)
            Dim specSymbol As SpecialSymbol = DirectCast(symbolLabel.Tag, SpecialSymbol)

            RaiseEvent SpecialSymbolPicked(Me, New SpecialSymbolEventArgs(specSymbol.HtmlUnicodeDecimalCode, specSymbol.UnicodeString))
        End If
    End Sub

    Private Sub AddSymbolsDialog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        RaiseEvent DialogClosed(Me, e)
    End Sub
End Class