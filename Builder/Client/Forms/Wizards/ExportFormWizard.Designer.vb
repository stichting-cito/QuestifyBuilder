<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportFormWizard
    Inherits Questify.Builder.UI.WizardBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportFormWizard))
        Me.TabPageControl1 = New TabPage
        Me.ExportMethodeTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExportMethodCombo = New System.Windows.Forms.ComboBox()
        Me.TabPageControl2 = New TabPage
        Me.ExportOptionsTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
        Me.BackgroundWorkerExport = New System.ComponentModel.BackgroundWorker()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageControl1.SuspendLayout()
        Me.ExportMethodeTabContent.SuspendLayout()
        Me.TabPageControl2.SuspendLayout()
        Me.ExportOptionsTabContent.SuspendLayout()
        Me.SuspendLayout()
        Me.TabControlMain.Controls.Add(Me.TabPageControl1)
        Me.TabControlMain.Controls.Add(Me.TabPageControl2)
        TabPageControl1.Tag = "ExportMethodeTab"
        TabPageControl2.Tag = "ExportOptionsTab"
        Me.TabPageControl1.Controls.Add(Me.ExportMethodeTabContent)
        resources.ApplyResources(Me.TabPageControl1, "UltraTabPageControl1")
        Me.TabPageControl1.Name = "UltraTabPageControl1"
        resources.ApplyResources(Me.ExportMethodeTabContent, "ExportMethodeTabContent")
        Me.ExportMethodeTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.ExportMethodeTabContent.Controls.Add(Me.Label2)
        Me.ExportMethodeTabContent.Controls.Add(Me.ExportMethodCombo)
        Me.ExportMethodeTabContent.Name = "ExportMethodeTabContent"
        Me.ExportMethodeTabContent.Task = "Task"
        Me.ExportMethodeTabContent.TaskDescription = "Description"
        Me.ExportMethodeTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.ExportMethodeTabContent.TaskPanelBackgroundImage = Nothing
        Me.ExportMethodeTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ExportMethodeTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ExportMethodCombo.DisplayMember = "UserFriendlyName"
        Me.ExportMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ExportMethodCombo.FormattingEnabled = True
        resources.ApplyResources(Me.ExportMethodCombo, "ExportMethodCombo")
        Me.ExportMethodCombo.Name = "ExportMethodCombo"
        Me.ExportMethodCombo.ValueMember = "UserFriendlyName"
        Me.TabPageControl2.Controls.Add(Me.ExportOptionsTabContent)
        resources.ApplyResources(Me.TabPageControl2, "UltraTabPageControl2")
        Me.TabPageControl2.Name = "UltraTabPageControl2"
        resources.ApplyResources(Me.ExportOptionsTabContent, "ExportOptionsTabContent")
        Me.ExportOptionsTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.ExportOptionsTabContent.Controls.Add(Me.OptionsGroupBox)
        Me.ExportOptionsTabContent.Name = "ExportOptionsTabContent"
        Me.ExportOptionsTabContent.Task = "Task"
        Me.ExportOptionsTabContent.TaskDescription = "Description"
        Me.ExportOptionsTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.ExportOptionsTabContent.TaskPanelBackgroundImage = Nothing
        Me.ExportOptionsTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ExportOptionsTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.OptionsGroupBox, "OptionsGroupBox")
        Me.OptionsGroupBox.Name = "OptionsGroupBox"
        Me.OptionsGroupBox.TabStop = False
        Me.BackgroundWorkerExport.WorkerReportsProgress = True
        Me.BackgroundWorkerExport.WorkerSupportsCancellation = True
        resources.ApplyResources(Me, "$this")
        Me.Name = "ExportFormWizard"
        Me.SupportsCancelation = True
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageControl1.ResumeLayout(False)
        Me.TabPageControl1.PerformLayout()
        Me.ExportMethodeTabContent.ResumeLayout(False)
        Me.ExportMethodeTabContent.PerformLayout()
        Me.TabPageControl2.ResumeLayout(False)
        Me.TabPageControl2.PerformLayout()
        Me.ExportOptionsTabContent.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPageControl1 As TabPage
    Friend WithEvents TabPageControl2 As TabPage
    Friend WithEvents ExportMethodeTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExportMethodCombo As System.Windows.Forms.ComboBox
    Friend WithEvents ExportOptionsTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorkerExport As System.ComponentModel.BackgroundWorker

End Class
