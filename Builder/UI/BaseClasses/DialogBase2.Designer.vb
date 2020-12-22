<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogBase2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogBase2))
        Me.OkDialogButton = New System.Windows.Forms.Button()
        Me.CancelDialogButton = New System.Windows.Forms.Button()
        Me.ContentPlaceHolderPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.InstructionsLabel = New System.Windows.Forms.Label()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.ContentPlaceHolderPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.OkDialogButton, "OkDialogButton")
        Me.OkDialogButton.Name = "OkDialogButton"
        Me.OkDialogButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.CancelDialogButton, "CancelDialogButton")
        Me.CancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelDialogButton.Name = "CancelDialogButton"
        Me.CancelDialogButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ContentPlaceHolderPanel, "ContentPlaceHolderPanel")
        Me.ContentPlaceHolderPanel.Controls.Add(Me.Panel1)
        Me.ContentPlaceHolderPanel.Name = "ContentPlaceHolderPanel"
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.OkDialogButton)
        Me.Panel1.Controls.Add(Me.CancelDialogButton)
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.Name = "InstructionsLabel"
        resources.ApplyResources(Me.HeaderPanel, "HeaderPanel")
        Me.HeaderPanel.BackColor = System.Drawing.SystemColors.Info
        Me.HeaderPanel.Controls.Add(Me.InstructionsLabel)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.AcceptButton = Me.OkDialogButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelDialogButton
        Me.Controls.Add(Me.ContentPlaceHolderPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Name = "DialogBase2"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.ContentPlaceHolderPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OkDialogButton As System.Windows.Forms.Button
    Friend WithEvents CancelDialogButton As System.Windows.Forms.Button
    Protected WithEvents ContentPlaceHolderPanel As System.Windows.Forms.Panel
    Protected WithEvents InstructionsLabel As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
