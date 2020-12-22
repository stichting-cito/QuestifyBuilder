<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WizardBase
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardBase))
        Me.FooterTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonsTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.ProcessButton = New System.Windows.Forms.Button()
        Me.PreviousButton = New System.Windows.Forms.Button()
        Me.NextButton = New System.Windows.Forms.Button()
        Me.LineLabel = New System.Windows.Forms.Label()
        Me.HelpBtn = New System.Windows.Forms.Button()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.FooterTableLayoutPanel.SuspendLayout()
        Me.ButtonsTableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.FooterTableLayoutPanel, "FooterTableLayoutPanel")
        Me.FooterTableLayoutPanel.CausesValidation = False
        Me.FooterTableLayoutPanel.Controls.Add(Me.ButtonsTableLayoutPanel, 1, 1)
        Me.FooterTableLayoutPanel.Controls.Add(Me.LineLabel, 0, 0)
        Me.FooterTableLayoutPanel.Controls.Add(Me.HelpBtn, 0, 1)
        Me.FooterTableLayoutPanel.Name = "FooterTableLayoutPanel"
        resources.ApplyResources(Me.ButtonsTableLayoutPanel, "ButtonsTableLayoutPanel")
        Me.ButtonsTableLayoutPanel.CausesValidation = False
        Me.ButtonsTableLayoutPanel.Controls.Add(Me.CancelBtn, 3, 0)
        Me.ButtonsTableLayoutPanel.Controls.Add(Me.ProcessButton, 2, 0)
        Me.ButtonsTableLayoutPanel.Controls.Add(Me.PreviousButton, 0, 0)
        Me.ButtonsTableLayoutPanel.Controls.Add(Me.NextButton, 1, 0)
        Me.ButtonsTableLayoutPanel.Name = "ButtonsTableLayoutPanel"
        resources.ApplyResources(Me.CancelBtn, "CancelBtn")
        Me.CancelBtn.CausesValidation = False
        Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ProcessButton, "ProcessButton")
        Me.ProcessButton.Name = "ProcessButton"
        Me.ProcessButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.PreviousButton, "PreviousButton")
        Me.PreviousButton.CausesValidation = False
        Me.PreviousButton.Name = "PreviousButton"
        Me.PreviousButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.NextButton, "NextButton")
        Me.NextButton.Name = "NextButton"
        Me.NextButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.LineLabel, "LineLabel")
        Me.LineLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FooterTableLayoutPanel.SetColumnSpan(Me.LineLabel, 2)
        Me.LineLabel.Name = "LineLabel"
        resources.ApplyResources(Me.HelpBtn, "HelpBtn")
        Me.HelpBtn.CausesValidation = False
        Me.HelpBtn.Name = "HelpBtn"
        Me.HelpBtn.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.FooterTableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "WizardBase"
        Me.FooterTableLayoutPanel.ResumeLayout(False)
        Me.ButtonsTableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents WelcomeTabContent As Questify.Builder.UI.WizardWelcomeTabControl
    Protected WithEvents WelcomeTabPageControl As System.Windows.Forms.TabPage
    Protected WithEvents ProcessTabPageControl As System.Windows.Forms.TabPage
    Protected WithEvents ProcessTabContent As Questify.Builder.UI.WizardProcessTabControl
    Protected WithEvents ResultTabPageControl As System.Windows.Forms.TabPage
    Protected WithEvents ResultTabContent As Questify.Builder.UI.WizardResultTabControl
    Protected WithEvents OverviewTabPageControl As System.Windows.Forms.TabPage
    Protected WithEvents OverviewTabContent As Questify.Builder.UI.WizardOverviewTabControl
    Friend WithEvents FooterTableLayoutPanel As TableLayoutPanel
    Friend WithEvents ButtonsTableLayoutPanel As TableLayoutPanel
    Protected WithEvents CancelBtn As Button
    Protected WithEvents ProcessButton As Button
    Protected WithEvents PreviousButton As Button
    Protected WithEvents NextButton As Button
    Friend WithEvents LineLabel As Label
    Protected WithEvents HelpBtn As Button
    Protected WithEvents TabControlMain As TabControl
End Class
