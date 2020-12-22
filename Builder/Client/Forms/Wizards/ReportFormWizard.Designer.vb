<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportFormWizard
    Inherits Questify.Builder.UI.WizardBase


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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportFormWizard))
        Me.ExtraOptionWizardTabContentControl = New TabPage
        Me.SelectReportHandlerWizardTabContentControl = New Questify.Builder.UI.WizardTabContentControl()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.SelectReportComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InitialiseProgressWizardTabPageControl = New TabPage
        Me.InitialiseProgressTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.InitialiseProgressLabel = New System.Windows.Forms.Label()
        Me.InitialiseProgressBar = New System.Windows.Forms.ProgressBar()
        Me.ExportLocationWizardTabContentControl = New TabPage
        Me.ExtraOptionsWizardTabContentControl = New Questify.Builder.UI.WizardTabContentControl()
        Me.WrapExtraOptionUIPanel = New System.Windows.Forms.Panel()
        Me.ExportProgressWizardTabContentControl = New TabPage
        Me.SelectExportPathWizardTabContentControl = New Questify.Builder.UI.WizardTabContentControl()
        Me.WrapSelectLocationUIPanel = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl4 = New TabPage
        Me.SelectReportErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TabControlMain.SuspendLayout()
        Me.ExtraOptionWizardTabContentControl.SuspendLayout()
        Me.SelectReportHandlerWizardTabContentControl.SuspendLayout()
        Me.InitialiseProgressWizardTabPageControl.SuspendLayout()
        Me.InitialiseProgressTabContent.SuspendLayout()
        Me.ExportLocationWizardTabContentControl.SuspendLayout()
        Me.ExtraOptionsWizardTabContentControl.SuspendLayout()
        Me.ExportProgressWizardTabContentControl.SuspendLayout()
        Me.SelectExportPathWizardTabContentControl.SuspendLayout()
        CType(Me.SelectReportErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.TabControlMain.Controls.Add(Me.ExtraOptionWizardTabContentControl)
        Me.TabControlMain.Controls.Add(Me.InitialiseProgressWizardTabPageControl)
        Me.TabControlMain.Controls.Add(Me.ExportLocationWizardTabContentControl)
        Me.TabControlMain.Controls.Add(Me.ExportProgressWizardTabContentControl)
        Me.TabControlMain.Controls.Add(Me.UltraTabPageControl4)
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        ExtraOptionWizardTabContentControl.Tag = "SelectHandlerTab"
        ExtraOptionWizardTabContentControl.Name = "SelectHandlerTab"

        InitialiseProgressWizardTabPageControl.Tag = "InitialiseProgressTab"
        InitialiseProgressWizardTabPageControl.Name = "InitialiseProgressTab"

        ExportLocationWizardTabContentControl.Tag = "ExtraOptionsTab"
        ExportLocationWizardTabContentControl.Name = "ExtraOptionsTab"

        ExportProgressWizardTabContentControl.Tag = "SelectExportLocationTab"
        ExportProgressWizardTabContentControl.Name = "SelectExportLocationTab"

        Me.ExtraOptionWizardTabContentControl.Controls.Add(Me.SelectReportHandlerWizardTabContentControl)
        resources.ApplyResources(Me.ExtraOptionWizardTabContentControl, "ExtraOptionWizardTabContentControl")
        resources.ApplyResources(Me.SelectReportHandlerWizardTabContentControl, "SelectReportHandlerWizardTabContentControl")
        Me.SelectReportHandlerWizardTabContentControl.BackColor = System.Drawing.SystemColors.Control
        Me.SelectReportHandlerWizardTabContentControl.Controls.Add(Me.DescriptionLabel)
        Me.SelectReportHandlerWizardTabContentControl.Controls.Add(Me.SelectReportComboBox)
        Me.SelectReportHandlerWizardTabContentControl.Controls.Add(Me.Label1)
        Me.SelectReportHandlerWizardTabContentControl.Name = "SelectReportHandlerWizardTabContentControl"
        Me.SelectReportHandlerWizardTabContentControl.Task = "Task"
        Me.SelectReportHandlerWizardTabContentControl.TaskDescription = "Description"
        Me.SelectReportHandlerWizardTabContentControl.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectReportHandlerWizardTabContentControl.TaskPanelBackgroundImage = Nothing
        Me.SelectReportHandlerWizardTabContentControl.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectReportHandlerWizardTabContentControl.TaskPanelHeight = 72
        Me.DescriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.SelectReportComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectReportComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.SelectReportComboBox, "SelectReportComboBox")
        Me.SelectReportComboBox.Name = "SelectReportComboBox"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.InitialiseProgressWizardTabPageControl.Controls.Add(Me.InitialiseProgressTabContent)
        resources.ApplyResources(Me.InitialiseProgressWizardTabPageControl, "InitialiseProgressWizardTabPageControl")
        resources.ApplyResources(Me.InitialiseProgressTabContent, "InitialiseProgressTabContent")
        Me.InitialiseProgressTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.InitialiseProgressTabContent.Controls.Add(Me.InitialiseProgressLabel)
        Me.InitialiseProgressTabContent.Controls.Add(Me.InitialiseProgressBar)
        Me.InitialiseProgressTabContent.Name = "InitialiseProgressTabContent"
        Me.InitialiseProgressTabContent.Task = "Task"
        Me.InitialiseProgressTabContent.TaskDescription = "Description"
        Me.InitialiseProgressTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.InitialiseProgressTabContent.TaskPanelBackgroundImage = Nothing
        Me.InitialiseProgressTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.InitialiseProgressTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.InitialiseProgressLabel, "InitialiseProgressLabel")
        Me.InitialiseProgressLabel.Name = "InitialiseProgressLabel"
        resources.ApplyResources(Me.InitialiseProgressBar, "InitialiseProgressBar")
        Me.InitialiseProgressBar.Name = "InitialiseProgressBar"
        Me.ExportLocationWizardTabContentControl.Controls.Add(Me.ExtraOptionsWizardTabContentControl)
        resources.ApplyResources(Me.ExportLocationWizardTabContentControl, "ExportLocationWizardTabContentControl")
        resources.ApplyResources(Me.ExtraOptionsWizardTabContentControl, "ExtraOptionsWizardTabContentControl")
        Me.ExtraOptionsWizardTabContentControl.BackColor = System.Drawing.SystemColors.Control
        Me.ExtraOptionsWizardTabContentControl.Controls.Add(Me.WrapExtraOptionUIPanel)
        Me.ExtraOptionsWizardTabContentControl.Name = "ExtraOptionsWizardTabContentControl"
        Me.ExtraOptionsWizardTabContentControl.Task = "Task"
        Me.ExtraOptionsWizardTabContentControl.TaskDescription = "Description"
        Me.ExtraOptionsWizardTabContentControl.TaskPanelBackColor = System.Drawing.Color.White
        Me.ExtraOptionsWizardTabContentControl.TaskPanelBackgroundImage = Nothing
        Me.ExtraOptionsWizardTabContentControl.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ExtraOptionsWizardTabContentControl.TaskPanelHeight = 72
        resources.ApplyResources(Me.WrapExtraOptionUIPanel, "WrapExtraOptionUIPanel")
        Me.WrapExtraOptionUIPanel.Name = "WrapExtraOptionUIPanel"
        Me.ExportProgressWizardTabContentControl.Controls.Add(Me.SelectExportPathWizardTabContentControl)
        resources.ApplyResources(Me.ExportProgressWizardTabContentControl, "ExportProgressWizardTabContentControl")
        resources.ApplyResources(Me.SelectExportPathWizardTabContentControl, "SelectExportPathWizardTabContentControl")
        Me.SelectExportPathWizardTabContentControl.BackColor = System.Drawing.SystemColors.Control
        Me.SelectExportPathWizardTabContentControl.Controls.Add(Me.WrapSelectLocationUIPanel)
        Me.SelectExportPathWizardTabContentControl.Name = "SelectExportPathWizardTabContentControl"
        Me.SelectExportPathWizardTabContentControl.Task = "Task"
        Me.SelectExportPathWizardTabContentControl.TaskDescription = "Description"
        Me.SelectExportPathWizardTabContentControl.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectExportPathWizardTabContentControl.TaskPanelBackgroundImage = Nothing
        Me.SelectExportPathWizardTabContentControl.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectExportPathWizardTabContentControl.TaskPanelHeight = 72
        resources.ApplyResources(Me.WrapSelectLocationUIPanel, "WrapSelectLocationUIPanel")
        Me.WrapSelectLocationUIPanel.Name = "WrapSelectLocationUIPanel"
        resources.ApplyResources(Me.UltraTabPageControl4, "UltraTabPageControl4")
        Me.SelectReportErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "ReportFormWizard"
        Me.TabControlMain.ResumeLayout(False)
        Me.ExtraOptionWizardTabContentControl.ResumeLayout(False)
        Me.ExtraOptionWizardTabContentControl.PerformLayout()
        Me.SelectReportHandlerWizardTabContentControl.ResumeLayout(False)
        Me.SelectReportHandlerWizardTabContentControl.PerformLayout()
        Me.InitialiseProgressWizardTabPageControl.ResumeLayout(False)
        Me.InitialiseProgressWizardTabPageControl.PerformLayout()
        Me.InitialiseProgressTabContent.ResumeLayout(False)
        Me.InitialiseProgressTabContent.PerformLayout()
        Me.ExportLocationWizardTabContentControl.ResumeLayout(False)
        Me.ExportLocationWizardTabContentControl.PerformLayout()
        Me.ExtraOptionsWizardTabContentControl.ResumeLayout(False)
        Me.ExportProgressWizardTabContentControl.ResumeLayout(False)
        Me.ExportProgressWizardTabContentControl.PerformLayout()
        Me.SelectExportPathWizardTabContentControl.ResumeLayout(False)
        CType(Me.SelectReportErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents InitialiseProgressWizardTabPageControl As TabPage
    Friend WithEvents ExtraOptionWizardTabContentControl As TabPage
    Friend WithEvents ExportLocationWizardTabContentControl As TabPage
    Friend WithEvents ExportProgressWizardTabContentControl As TabPage
    Friend WithEvents UltraTabPageControl4 As TabPage
    Friend WithEvents InitialiseProgressTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents InitialiseProgressLabel As System.Windows.Forms.Label
    Friend WithEvents InitialiseProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents SelectReportHandlerWizardTabContentControl As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectReportComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ExtraOptionsWizardTabContentControl As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents SelectExportPathWizardTabContentControl As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents WrapExtraOptionUIPanel As System.Windows.Forms.Panel
    Friend WithEvents WrapSelectLocationUIPanel As System.Windows.Forms.Panel
    Friend WithEvents SelectReportErrorProvider As System.Windows.Forms.ErrorProvider
End Class
