Imports Questify.Builder.Logic.Service.Interfaces.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddSymbolsDialog
    Inherits System.Windows.Forms.Form
    Implements IAddSymbolDialog


    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddSymbolsDialog))
        Me.ComboBoxSymbolGroup = New System.Windows.Forms.ComboBox()
        Me.PanelSpecialSymbols = New System.Windows.Forms.Panel()
        Me.PanelSelectedSymbol = New System.Windows.Forms.Panel()
        Me.LabelSymbolName = New System.Windows.Forms.Label()
        Me.labelSymbolText = New System.Windows.Forms.Label()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.InsertSymbolButton = New System.Windows.Forms.Button()
        Me.FooterPanel = New System.Windows.Forms.Panel()
        Me.PanelSelectedSymbol.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.ComboBoxSymbolGroup.DisplayMember = "Name"
        Me.ComboBoxSymbolGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSymbolGroup.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBoxSymbolGroup, "ComboBoxSymbolGroup")
        Me.ComboBoxSymbolGroup.Name = "ComboBoxSymbolGroup"
        Me.ComboBoxSymbolGroup.ValueMember = "SymbolList"
        resources.ApplyResources(Me.PanelSpecialSymbols, "PanelSpecialSymbols")
        Me.PanelSpecialSymbols.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSpecialSymbols.CausesValidation = False
        Me.PanelSpecialSymbols.Name = "PanelSpecialSymbols"
        Me.PanelSelectedSymbol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSelectedSymbol.Controls.Add(Me.LabelSymbolName)
        Me.PanelSelectedSymbol.Controls.Add(Me.labelSymbolText)
        resources.ApplyResources(Me.PanelSelectedSymbol, "PanelSelectedSymbol")
        Me.PanelSelectedSymbol.Name = "PanelSelectedSymbol"
        Me.LabelSymbolName.AutoEllipsis = True
        resources.ApplyResources(Me.LabelSymbolName, "LabelSymbolName")
        Me.LabelSymbolName.Name = "LabelSymbolName"
        resources.ApplyResources(Me.labelSymbolText, "labelSymbolText")
        Me.labelSymbolText.Name = "labelSymbolText"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        resources.ApplyResources(Me.InsertSymbolButton, "InsertSymbolButton")
        Me.InsertSymbolButton.Name = "InsertSymbolButton"
        Me.FooterPanel.Controls.Add(Me.InsertSymbolButton)
        Me.FooterPanel.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Name = "FooterPanel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.CausesValidation = False
        Me.Controls.Add(Me.FooterPanel)
        Me.Controls.Add(Me.PanelSelectedSymbol)
        Me.Controls.Add(Me.PanelSpecialSymbols)
        Me.Controls.Add(Me.ComboBoxSymbolGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddSymbolsDialog"
        Me.ShowInTaskbar = False
        Me.PanelSelectedSymbol.ResumeLayout(False)
        Me.PanelSelectedSymbol.PerformLayout()
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBoxSymbolGroup As System.Windows.Forms.ComboBox
    Friend WithEvents PanelSpecialSymbols As System.Windows.Forms.Panel
    Friend WithEvents PanelSelectedSymbol As System.Windows.Forms.Panel
    Friend WithEvents LabelSymbolName As System.Windows.Forms.Label
    Friend WithEvents labelSymbolText As System.Windows.Forms.Label
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents InsertSymbolButton As System.Windows.Forms.Button
    Protected WithEvents FooterPanel As System.Windows.Forms.Panel
End Class
