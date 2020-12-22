<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectResourceEntityDialogBase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectResourceEntityDialogBase))
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.GridPlaceholderPanel = New System.Windows.Forms.Panel()
        Me.InstructionsLabel = New System.Windows.Forms.Label()
        Me.GridBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.FooterPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.HeaderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.Name = "CloseButton"
        resources.ApplyResources(Me.OkButton, "OkButton")
        Me.OkButton.Name = "OkButton"
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        Me.GridPlaceholderPanel.Name = "GridPlaceholderPanel"
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.Name = "InstructionsLabel"
        Me.GridBackgroundWorker.WorkerReportsProgress = True
        Me.GridBackgroundWorker.WorkerSupportsCancellation = True
        Me.HeaderPanel.Controls.Add(Me.InstructionsLabel)
        resources.ApplyResources(Me.HeaderPanel, "HeaderPanel")
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.FooterPanel.Controls.Add(Me.OkButton)
        Me.FooterPanel.Controls.Add(Me.CloseButton)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Name = "FooterPanel"
        Me.Panel1.Controls.Add(Me.GridPlaceholderPanel)
        Me.Panel1.Controls.Add(Me.FooterPanel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.MinimizeBox = False
        Me.Name = "SelectResourceEntityDialogBase"
        Me.ShowInTaskbar = False
        Me.HeaderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents InstructionsLabel As System.Windows.Forms.Label
    Protected WithEvents GridBackgroundWorker As System.ComponentModel.BackgroundWorker
    Protected WithEvents OkButton As System.Windows.Forms.Button
    Protected WithEvents CloseButton As System.Windows.Forms.Button
    Protected WithEvents GridPlaceholderPanel As System.Windows.Forms.Panel
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents FooterPanel As System.Windows.Forms.Panel

End Class
