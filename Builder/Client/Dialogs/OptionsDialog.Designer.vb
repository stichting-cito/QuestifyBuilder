<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsDialog
    Inherits Questify.Builder.Client.DialogBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsDialog))
        Me.availableLanguagesList = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrentLanguageTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.FillPanel.Controls.Add(Me.availableLanguagesList)
        Me.FillPanel.Controls.Add(Me.Label2)
        Me.FillPanel.Controls.Add(Me.CurrentLanguageTextBox)
        Me.FillPanel.Controls.Add(Me.Label1)
        resources.ApplyResources(Me.FillPanel, "FillPanel")
        resources.ApplyResources(Me.DialogOkButton, "DialogOkButton")
        resources.ApplyResources(Me.DialogCancelButton, "DialogCancelButton")
        Me.availableLanguagesList.DisplayMember = "CultureName"
        Me.availableLanguagesList.FormattingEnabled = True
        resources.ApplyResources(Me.availableLanguagesList, "availableLanguagesList")
        Me.availableLanguagesList.Name = "availableLanguagesList"
        Me.availableLanguagesList.ValueMember = "CultureIdentifier"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.CurrentLanguageTextBox, "CurrentLanguageTextBox")
        Me.CurrentLanguageTextBox.Name = "CurrentLanguageTextBox"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me, "$this")
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsDialog"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents availableLanguagesList As ListBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CurrentLanguageTextBox As TextBox
    Friend WithEvents Label1 As Label
End Class
