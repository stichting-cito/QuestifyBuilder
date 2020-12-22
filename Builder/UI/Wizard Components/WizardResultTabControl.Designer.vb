<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardResultTabControl
    Inherits WizardTabContentControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        If _linkedLabel IsNot Nothing Then
            For Each link As LinkLabel In _linkedLabel
                RemoveHandler link.LinkClicked, AddressOf LinkLabel_Click
            Next
        End If
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardResultTabControl))
        Me.ResultTextBox = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ResultTextBox, "ResultTextBox")
        Me.ResultTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.ResultTextBox.Name = "ResultTextBox"
        Me.ResultTextBox.ReadOnly = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ResultTextBox)
        Me.Name = "WizardResultTabControl"
        Me.Controls.SetChildIndex(Me.ResultTextBox, 0)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ResultTextBox As System.Windows.Forms.RichTextBox

End Class
