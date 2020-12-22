<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportFormWizard
    Inherits Questify.Builder.UI.WizardBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportFormWizard))
        Me.TabImportMethod = New System.Windows.Forms.TabPage()
        Me.ImportMethodeTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImportMethodCombo = New System.Windows.Forms.ComboBox()
        Me.TabImportOptions = New System.Windows.Forms.TabPage()
        Me.ImportOptionsTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
        Me.PackageImportOptionsDataEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.TabControlMain.SuspendLayout()
        Me.TabImportMethod.SuspendLayout()
        Me.ImportMethodeTabContent.SuspendLayout()
        Me.TabImportOptions.SuspendLayout()
        Me.ImportOptionsTabContent.SuspendLayout()
        CType(Me.PackageImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.TabControlMain.Controls.Add(Me.TabImportMethod)
        Me.TabControlMain.Controls.Add(Me.TabImportOptions)
        Me.TabImportMethod.Controls.Add(Me.ImportMethodeTabContent)
        resources.ApplyResources(Me.TabImportMethod, "TabImportMethod")
        Me.TabImportMethod.Name = "TabImportMethod"
        Me.TabImportMethod.Tag = "ImportMethodeTab"
        resources.ApplyResources(Me.ImportMethodeTabContent, "ImportMethodeTabContent")
        Me.ImportMethodeTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.ImportMethodeTabContent.Controls.Add(Me.Label2)
        Me.ImportMethodeTabContent.Controls.Add(Me.ImportMethodCombo)
        Me.ImportMethodeTabContent.Name = "ImportMethodeTabContent"
        Me.HelpProvider1.SetShowHelp(Me.ImportMethodeTabContent, CType(resources.GetObject("ImportMethodeTabContent.ShowHelp"), Boolean))
        Me.ImportMethodeTabContent.Task = "Task"
        Me.ImportMethodeTabContent.TaskDescription = "Description"
        Me.ImportMethodeTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.ImportMethodeTabContent.TaskPanelBackgroundImage = Nothing
        Me.ImportMethodeTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ImportMethodeTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.HelpProvider1.SetShowHelp(Me.Label2, CType(resources.GetObject("Label2.ShowHelp"), Boolean))
        Me.ImportMethodCombo.DisplayMember = "UserFriendlyName"
        Me.ImportMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ImportMethodCombo.FormattingEnabled = True
        resources.ApplyResources(Me.ImportMethodCombo, "ImportMethodCombo")
        Me.ImportMethodCombo.Name = "ImportMethodCombo"
        Me.HelpProvider1.SetShowHelp(Me.ImportMethodCombo, CType(resources.GetObject("ImportMethodCombo.ShowHelp"), Boolean))
        Me.ImportMethodCombo.ValueMember = "UserFriendlyName"
        Me.TabImportOptions.Controls.Add(Me.ImportOptionsTabContent)
        resources.ApplyResources(Me.TabImportOptions, "TabImportOptions")
        Me.TabImportOptions.Name = "TabImportOptions"
        Me.TabImportOptions.Tag = "ImportOptionsTab"
        resources.ApplyResources(Me.ImportOptionsTabContent, "ImportOptionsTabContent")
        Me.ImportOptionsTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.ImportOptionsTabContent.Controls.Add(Me.OptionsGroupBox)
        Me.ImportOptionsTabContent.Name = "ImportOptionsTabContent"
        Me.HelpProvider1.SetShowHelp(Me.ImportOptionsTabContent, CType(resources.GetObject("ImportOptionsTabContent.ShowHelp"), Boolean))
        Me.ImportOptionsTabContent.Task = "Task"
        Me.ImportOptionsTabContent.TaskDescription = "Description"
        Me.ImportOptionsTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.ImportOptionsTabContent.TaskPanelBackgroundImage = Nothing
        Me.ImportOptionsTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.ImportOptionsTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.OptionsGroupBox, "OptionsGroupBox")
        Me.OptionsGroupBox.Name = "OptionsGroupBox"
        Me.HelpProvider1.SetShowHelp(Me.OptionsGroupBox, CType(resources.GetObject("OptionsGroupBox.ShowHelp"), Boolean))
        Me.OptionsGroupBox.TabStop = False
        Me.PackageImportOptionsDataEntityBindingSource.DataSource = GetType(Questify.Builder.Client.PackageImportOptionsDataEntity)
        resources.ApplyResources(Me, "$this")
        Me.Name = "ImportFormWizard"
        Me.HelpProvider1.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.TabControlMain.ResumeLayout(False)
        Me.TabImportMethod.ResumeLayout(False)
        Me.TabImportMethod.PerformLayout()
        Me.ImportMethodeTabContent.ResumeLayout(False)
        Me.ImportMethodeTabContent.PerformLayout()
        Me.TabImportOptions.ResumeLayout(False)
        Me.TabImportOptions.PerformLayout()
        Me.ImportOptionsTabContent.ResumeLayout(False)
        CType(Me.PackageImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabImportMethod As TabPage
    Friend WithEvents TabImportOptions As TabPage
    Friend WithEvents ImportOptionsTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ImportMethodeTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImportMethodCombo As System.Windows.Forms.ComboBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents PackageImportOptionsDataEntityBindingSource As System.Windows.Forms.BindingSource

End Class
