<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddItemsFromCodeDialog
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddItemsFromCodeDialog))
        Me.ItemCodeTextBox = New System.Windows.Forms.TextBox()
        Me.FooterPanel = New System.Windows.Forms.Panel()
        Me.AddItemsButton = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ItemCodeTextBox, "ItemCodeTextBox")
        Me.ItemCodeTextBox.Name = "ItemCodeTextBox"
        Me.FooterPanel.Controls.Add(Me.AddItemsButton)
        Me.FooterPanel.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Name = "FooterPanel"
        resources.ApplyResources(Me.AddItemsButton, "AddItemsButton")
        Me.AddItemsButton.Name = "AddItemsButton"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FooterPanel)
        Me.Controls.Add(Me.ItemCodeTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimizeBox = False
        Me.Name = "AddItemsFromCodeDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ItemCodeTextBox As System.Windows.Forms.TextBox
    Protected WithEvents FooterPanel As System.Windows.Forms.Panel
    Friend WithEvents AddItemsButton As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
