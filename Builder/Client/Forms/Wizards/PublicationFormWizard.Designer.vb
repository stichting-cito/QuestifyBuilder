<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PublicationFormWizard
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PublicationFormWizard))
        Me.TabPageValidateAdaptive = New System.Windows.Forms.TabPage()
        Me.ValidationWizardTabContentControl = New Questify.Builder.UI.WizardTabContentControl()
        Me.PublicationvalidateTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ValidatorProgressLabel = New System.Windows.Forms.Label()
        Me.ProgressBarValidation = New System.Windows.Forms.ProgressBar()
        Me.ValidationTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.PublicationMethodeTabPageControl = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PublicationMethodeTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.PublicationMethodCombo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BackgroundWorkerPublication = New System.ComponentModel.BackgroundWorker()
        Me.OptionsErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PublicationOptionsTab = New System.Windows.Forms.TabPage()
        Me.TabControlMain.SuspendLayout
        Me.TabPageValidateAdaptive.SuspendLayout
        Me.ValidationWizardTabContentControl.SuspendLayout
        Me.PublicationvalidateTableLayoutPanel.SuspendLayout
        Me.PublicationMethodeTabPageControl.SuspendLayout
        Me.PublicationMethodeTabContent.SuspendLayout
        CType(Me.OptionsErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.TabControlMain.Controls.Add(Me.TabPageValidateAdaptive)
        Me.TabControlMain.Controls.Add(Me.PublicationMethodeTabPageControl)
        Me.TabControlMain.Controls.Add(Me.PublicationOptionsTab)
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        Me.TabPageValidateAdaptive.Controls.Add(Me.ValidationWizardTabContentControl)
        resources.ApplyResources(Me.TabPageValidateAdaptive, "TabPageValidateAdaptive")
        Me.TabPageValidateAdaptive.Name = "TabPageValidateAdaptive"
        Me.TabPageValidateAdaptive.Tag = "ValidationTab"
        resources.ApplyResources(Me.ValidationWizardTabContentControl, "ValidationWizardTabContentControl")
        Me.ValidationWizardTabContentControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ValidationWizardTabContentControl.Controls.Add(Me.PublicationvalidateTableLayoutPanel)
        Me.ValidationWizardTabContentControl.Name = "ValidationWizardTabContentControl"
        Me.ValidationWizardTabContentControl.Task = "Task"
        Me.ValidationWizardTabContentControl.TaskDescription = "Description"
        Me.ValidationWizardTabContentControl.TaskPanelBackColor = System.Drawing.Color.White
        Me.ValidationWizardTabContentControl.TaskPanelBackgroundImage = Nothing
        Me.ValidationWizardTabContentControl.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ValidationWizardTabContentControl.TaskPanelHeight = 72
        resources.ApplyResources(Me.PublicationvalidateTableLayoutPanel, "PublicationvalidateTableLayoutPanel")
        Me.PublicationvalidateTableLayoutPanel.BackColor = System.Drawing.SystemColors.Control
        Me.PublicationvalidateTableLayoutPanel.Controls.Add(Me.ValidatorProgressLabel, 0, 1)
        Me.PublicationvalidateTableLayoutPanel.Controls.Add(Me.ProgressBarValidation, 0, 2)
        Me.PublicationvalidateTableLayoutPanel.Controls.Add(Me.ValidationTableLayoutPanel, 0, 0)
        Me.PublicationvalidateTableLayoutPanel.Name = "PublicationvalidateTableLayoutPanel"
        resources.ApplyResources(Me.ValidatorProgressLabel, "ValidatorProgressLabel")
        Me.ValidatorProgressLabel.Name = "ValidatorProgressLabel"
        resources.ApplyResources(Me.ProgressBarValidation, "ProgressBarValidation")
        Me.ProgressBarValidation.Name = "ProgressBarValidation"
        resources.ApplyResources(Me.ValidationTableLayoutPanel, "ValidationTableLayoutPanel")
        Me.ValidationTableLayoutPanel.Name = "ValidationTableLayoutPanel"
        Me.PublicationMethodeTabPageControl.Controls.Add(Me.Label2)
        Me.PublicationMethodeTabPageControl.Controls.Add(Me.PublicationMethodeTabContent)
        resources.ApplyResources(Me.PublicationMethodeTabPageControl, "PublicationMethodeTabPageControl")
        Me.PublicationMethodeTabPageControl.Name = "PublicationMethodeTabPageControl"
        Me.PublicationMethodeTabPageControl.Tag = "PublicationMethodeTab"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.PublicationMethodeTabContent, "PublicationMethodeTabContent")
        Me.PublicationMethodeTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.PublicationMethodeTabContent.Controls.Add(Me.PublicationMethodCombo)
        Me.PublicationMethodeTabContent.Name = "PublicationMethodeTabContent"
        Me.PublicationMethodeTabContent.Task = "Task"
        Me.PublicationMethodeTabContent.TaskDescription = "Description"
        Me.PublicationMethodeTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.PublicationMethodeTabContent.TaskPanelBackgroundImage = Nothing
        Me.PublicationMethodeTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.PublicationMethodeTabContent.TaskPanelHeight = 72
        Me.PublicationMethodCombo.DisplayMember = "UserFriendlyName"
        Me.PublicationMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PublicationMethodCombo.FormattingEnabled = true
        resources.ApplyResources(Me.PublicationMethodCombo, "PublicationMethodCombo")
        Me.PublicationMethodCombo.Name = "PublicationMethodCombo"
        Me.PublicationMethodCombo.ValueMember = "Type"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.BackgroundWorkerPublication.WorkerReportsProgress = true
        Me.OptionsErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.OptionsErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me.PublicationOptionsTab, "PublicationOptionsTab")
        Me.PublicationOptionsTab.Name = "PublicationOptionsTab"
        Me.PublicationOptionsTab.Tag = "PublicationOptionsTab"
        Me.PublicationOptionsTab.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "PublicationFormWizard"
        Me.TabControlMain.ResumeLayout(false)
        Me.TabPageValidateAdaptive.ResumeLayout(false)
        Me.TabPageValidateAdaptive.PerformLayout
        Me.ValidationWizardTabContentControl.ResumeLayout(false)
        Me.ValidationWizardTabContentControl.PerformLayout
        Me.PublicationvalidateTableLayoutPanel.ResumeLayout(false)
        Me.PublicationvalidateTableLayoutPanel.PerformLayout
        Me.PublicationMethodeTabPageControl.ResumeLayout(false)
        Me.PublicationMethodeTabPageControl.PerformLayout
        Me.PublicationMethodeTabContent.ResumeLayout(false)
        CType(Me.OptionsErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents PublicationMethodeTabPageControl As System.Windows.Forms.TabPage
    Friend WithEvents PublicationMethodeTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PublicationMethodCombo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabPageValidateAdaptive As System.Windows.Forms.TabPage
    Friend WithEvents ValidationWizardTabContentControl As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents ValidatorProgressLabel As System.Windows.Forms.Label
    Friend WithEvents PublicationvalidateTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ProgressBarValidation As System.Windows.Forms.ProgressBar
    Friend WithEvents ValidationTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BackgroundWorkerPublication As System.ComponentModel.BackgroundWorker
    Friend WithEvents OptionsErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents PublicationOptionsTab As TabPage
End Class
