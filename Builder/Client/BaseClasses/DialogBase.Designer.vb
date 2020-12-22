<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogBase
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogBase))
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.DialogOkButton = New System.Windows.Forms.Button()
        Me.DialogCancelButton = New System.Windows.Forms.Button()
        Me.BottomPanel.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.FillPanel, "FillPanel")
        Me.FillPanel.Name = "FillPanel"
        Me.BottomPanel.Controls.Add(Me.DialogOkButton)
        Me.BottomPanel.Controls.Add(Me.DialogCancelButton)
        resources.ApplyResources(Me.BottomPanel, "BottomPanel")
        Me.BottomPanel.Name = "BottomPanel"
        Me.DialogOkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.DialogOkButton, "DialogOkButton")
        Me.DialogOkButton.Name = "DialogOkButton"
        Me.DialogOkButton.UseVisualStyleBackColor = true
        Me.DialogCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.DialogCancelButton, "DialogCancelButton")
        Me.DialogCancelButton.Name = "DialogCancelButton"
        Me.DialogCancelButton.UseVisualStyleBackColor = true
        Me.AcceptButton = Me.DialogOkButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.DialogCancelButton
        Me.Controls.Add(Me.FillPanel)
        Me.Controls.Add(Me.BottomPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "DialogBase"
        Me.BottomPanel.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents BottomPanel As System.Windows.Forms.Panel
    Protected WithEvents FillPanel As System.Windows.Forms.Panel
    Protected WithEvents DialogOkButton As System.Windows.Forms.Button
    Protected WithEvents DialogCancelButton As System.Windows.Forms.Button
End Class
