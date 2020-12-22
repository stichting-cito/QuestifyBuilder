<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardWelcomeTabControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardWelcomeTabControl))
        Me.LeftPanel = New System.Windows.Forms.Panel()
        Me.RightPanel = New System.Windows.Forms.Panel()
        Me.HideWelcomeTabCheckBox = New System.Windows.Forms.CheckBox()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LeftPanel.SuspendLayout()
        Me.RightPanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.LeftPanel.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.LeftPanel, "LeftPanel")
        Me.LeftPanel.Controls.Add(Me.PictureBox1)
        Me.LeftPanel.Name = "LeftPanel"
        Me.RightPanel.BackColor = System.Drawing.Color.White
        Me.RightPanel.Controls.Add(Me.HideWelcomeTabCheckBox)
        Me.RightPanel.Controls.Add(Me.DescriptionLabel)
        Me.RightPanel.Controls.Add(Me.TitleLabel)
        resources.ApplyResources(Me.RightPanel, "RightPanel")
        Me.RightPanel.Name = "RightPanel"
        resources.ApplyResources(Me.HideWelcomeTabCheckBox, "HideWelcomeTabCheckBox")
        Me.HideWelcomeTabCheckBox.Name = "HideWelcomeTabCheckBox"
        Me.HideWelcomeTabCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.Name = "DescriptionLabel"
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Image = Global.Questify.Builder.UI.My.Resources.Resources.Questify
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RightPanel)
        Me.Controls.Add(Me.LeftPanel)
        Me.Name = "WizardWelcomeTabControl"
        Me.LeftPanel.ResumeLayout(False)
        Me.RightPanel.ResumeLayout(False)
        Me.RightPanel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents TitleLabel As System.Windows.Forms.Label
    Private WithEvents DescriptionLabel As System.Windows.Forms.Label
    Private WithEvents LeftPanel As System.Windows.Forms.Panel
    Private WithEvents RightPanel As System.Windows.Forms.Panel
    Protected WithEvents HideWelcomeTabCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
