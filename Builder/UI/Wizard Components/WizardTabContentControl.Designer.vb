<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardTabContentControl
    Inherits System.Windows.Forms.UserControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardTabContentControl))
        Me.TaskPanel = New System.Windows.Forms.Panel
        Me.TaskDescriptionLabel = New System.Windows.Forms.Label
        Me.TaskLabel = New System.Windows.Forms.Label
        Me.TaskPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.TaskPanel.AccessibleDescription = Nothing
        Me.TaskPanel.AccessibleName = Nothing
        resources.ApplyResources(Me.TaskPanel, "TaskPanel")
        Me.TaskPanel.BackColor = System.Drawing.Color.White
        Me.TaskPanel.BackgroundImage = Nothing
        Me.TaskPanel.Controls.Add(Me.TaskDescriptionLabel)
        Me.TaskPanel.Controls.Add(Me.TaskLabel)
        Me.TaskPanel.Font = Nothing
        Me.TaskPanel.Name = "TaskPanel"
        Me.TaskDescriptionLabel.AccessibleDescription = Nothing
        Me.TaskDescriptionLabel.AccessibleName = Nothing
        resources.ApplyResources(Me.TaskDescriptionLabel, "TaskDescriptionLabel")
        Me.TaskDescriptionLabel.BackColor = System.Drawing.Color.White
        Me.TaskDescriptionLabel.Font = Nothing
        Me.TaskDescriptionLabel.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TaskDescriptionLabel.Name = "TaskDescriptionLabel"
        Me.TaskLabel.AccessibleDescription = Nothing
        Me.TaskLabel.AccessibleName = Nothing
        resources.ApplyResources(Me.TaskLabel, "TaskLabel")
        Me.TaskLabel.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TaskLabel.Name = "TaskLabel"
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.TaskPanel)
        Me.Font = Nothing
        Me.Name = "WizardTabContentControl"
        Me.TaskPanel.ResumeLayout(False)
        Me.TaskPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TaskPanel As System.Windows.Forms.Panel
    Friend WithEvents TaskDescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents TaskLabel As System.Windows.Forms.Label

End Class
