<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddGenericResourceWizardForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddGenericResourceWizardForm))
        Dim GenericGridEX_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.TabPageControl1 = New TabPage
        Me.SelectMethodTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TemplatedRadioButton = New System.Windows.Forms.RadioButton()
        Me.ImportRadioButton = New System.Windows.Forms.RadioButton()
        Me.FileSelectionTabPageControl = New TabPage
        Me.FileSelectionTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.SelectedFileInfoTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPageControl2 = New TabPage
        Me.SelectTemplateResourceTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TemplateTextBox = New System.Windows.Forms.TextBox()
        Me.SelectTemplateButton = New System.Windows.Forms.Button()
        Me.SelectedTabPageControl = New TabPage
        Me.SelectedFilesTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.GenericGridEX = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.SelectFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ExportOptionControlBase1 = New Questify.Builder.Client.ExportOptionControlBase()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageControl1.SuspendLayout()
        Me.SelectMethodTabContent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.FileSelectionTabPageControl.SuspendLayout()
        Me.FileSelectionTabContent.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabPageControl2.SuspendLayout()
        Me.SelectTemplateResourceTabContent.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SelectedTabPageControl.SuspendLayout()
        Me.SelectedFilesTabContent.SuspendLayout()
        CType(Me.GenericGridEX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.TabControlMain.Controls.Add(Me.TabPageControl1)
        Me.TabControlMain.Controls.Add(Me.TabPageControl2)
        Me.TabControlMain.Controls.Add(Me.FileSelectionTabPageControl)
        Me.TabControlMain.Controls.Add(Me.SelectedTabPageControl)
        resources.ApplyResources(Me.TabControlMain, "TabControlMain")
        TabPageControl1.Tag = "SelectImportMethod"
        FileSelectionTabPageControl.Tag = "FileSelectionTab"
        TabPageControl2.Tag = "SelectTemplateTab"
        SelectedTabPageControl.Tag = "SelectedFilesTab"
        TabPageControl1.Controls.Add(Me.SelectMethodTabContent)
        resources.ApplyResources(Me.TabPageControl1, "UltraTabPageControl1")
        TabPageControl1.Name = "UltraTabPageControl1"
        resources.ApplyResources(Me.SelectMethodTabContent, "SelectMethodTabContent")
        Me.SelectMethodTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.SelectMethodTabContent.Controls.Add(Me.Panel1)
        Me.SelectMethodTabContent.Name = "SelectMethodTabContent"
        Me.SelectMethodTabContent.Task = "What would you like to do"
        Me.SelectMethodTabContent.TaskDescription = "Description"
        Me.SelectMethodTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectMethodTabContent.TaskPanelBackgroundImage = Nothing
        Me.SelectMethodTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectMethodTabContent.TaskPanelHeight = 72
        Me.Panel1.Controls.Add(Me.TemplatedRadioButton)
        Me.Panel1.Controls.Add(Me.ImportRadioButton)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.TemplatedRadioButton, "TemplatedRadioButton")
        Me.TemplatedRadioButton.Name = "TemplatedRadioButton"
        Me.TemplatedRadioButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ImportRadioButton, "ImportRadioButton")
        Me.ImportRadioButton.Checked = True
        Me.ImportRadioButton.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.ImportRadioButton.Name = "ImportRadioButton"
        Me.ImportRadioButton.TabStop = True
        Me.ImportRadioButton.UseVisualStyleBackColor = True
        Me.FileSelectionTabPageControl.Controls.Add(Me.FileSelectionTabContent)
        resources.ApplyResources(Me.FileSelectionTabPageControl, "FileSelectionTabPageControl")
        Me.FileSelectionTabPageControl.Name = "FileSelectionTab"
        resources.ApplyResources(Me.FileSelectionTabContent, "FileSelectionTabContent")
        Me.FileSelectionTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.FileSelectionTabContent.Controls.Add(Me.SelectedFileInfoTextBox)
        Me.FileSelectionTabContent.Controls.Add(Me.TableLayoutPanel1)
        Me.FileSelectionTabContent.Name = "FileSelectionTabContent"
        Me.FileSelectionTabContent.Task = "Task"
        Me.FileSelectionTabContent.TaskDescription = "Description"
        Me.FileSelectionTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.FileSelectionTabContent.TaskPanelBackgroundImage = Nothing
        Me.FileSelectionTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.FileSelectionTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.SelectedFileInfoTextBox, "SelectedFileInfoTextBox")
        Me.SelectedFileInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectedFileInfoTextBox.Name = "SelectedFileInfoTextBox"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.BrowseButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.BrowseButton, "BrowseButton")
        Me.BrowseButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.TestToolStripMenuItem_Image
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.TabPageControl2.Controls.Add(Me.SelectTemplateResourceTabContent)
        resources.ApplyResources(Me.TabPageControl2, "UltraTabPageControl2")
        Me.TabPageControl2.Name = "SelectTemplateTab"
        resources.ApplyResources(Me.SelectTemplateResourceTabContent, "SelectTemplateResourceTabContent")
        Me.SelectTemplateResourceTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.SelectTemplateResourceTabContent.Controls.Add(Me.TableLayoutPanel2)
        Me.SelectTemplateResourceTabContent.Name = "SelectTemplateResourceTabContent"
        Me.SelectTemplateResourceTabContent.Task = "Select Template Resource"
        Me.SelectTemplateResourceTabContent.TaskDescription = "Select resource that will be used as a template"
        Me.SelectTemplateResourceTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectTemplateResourceTabContent.TaskPanelBackgroundImage = Nothing
        Me.SelectTemplateResourceTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectTemplateResourceTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TemplateTextBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.SelectTemplateButton, 2, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        resources.ApplyResources(Me.TemplateTextBox, "TemplateTextBox")
        Me.TemplateTextBox.Name = "TemplateTextBox"
        Me.TemplateTextBox.ReadOnly = True
        resources.ApplyResources(Me.SelectTemplateButton, "SelectTemplateButton")
        Me.SelectTemplateButton.Name = "SelectTemplateButton"
        Me.SelectTemplateButton.UseVisualStyleBackColor = True
        Me.SelectedTabPageControl.Controls.Add(Me.SelectedFilesTabContent)
        resources.ApplyResources(Me.SelectedTabPageControl, "SelectedTabPageControl")
        Me.SelectedTabPageControl.Name = "SelectedFilesTab"
        resources.ApplyResources(Me.SelectedFilesTabContent, "SelectedFilesTabContent")
        Me.SelectedFilesTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.SelectedFilesTabContent.Controls.Add(Me.GenericGridEX)
        Me.SelectedFilesTabContent.Controls.Add(Me.Label1)
        Me.SelectedFilesTabContent.Controls.Add(Me.DeleteButton)
        Me.SelectedFilesTabContent.Name = "SelectedFilesTabContent"
        Me.SelectedFilesTabContent.Task = "Task"
        Me.SelectedFilesTabContent.TaskDescription = "Description"
        Me.SelectedFilesTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectedFilesTabContent.TaskPanelBackgroundImage = Nothing
        Me.SelectedFilesTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectedFilesTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.GenericGridEX, "GenericGridEX")
        Me.GenericGridEX.BoundMode = Janus.Windows.GridEX.BoundMode.Unbound
        resources.ApplyResources(GenericGridEX_DesignTimeLayout, "GenericGridEX_DesignTimeLayout")
        Me.GenericGridEX.DesignTimeLayout = GenericGridEX_DesignTimeLayout
        Me.GenericGridEX.GroupByBoxVisible = False
        Me.GenericGridEX.Name = "GenericGridEX"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.DeleteButton, "DeleteButton")
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.SelectFileDialog, "SelectFileDialog")
        Me.SelectFileDialog.Multiselect = True
        resources.ApplyResources(Me.ExportOptionControlBase1, "ExportOptionControlBase1")
        Me.ExportOptionControlBase1.Name = "ExportOptionControlBase1"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ExportOptionControlBase1)
        Me.Name = "AddGenericResourceWizardForm"
        Me.ProgressMinimumStepsRequired = 3
        Me.Controls.SetChildIndex(Me.ExportOptionControlBase1, 0)
        Me.Controls.SetChildIndex(Me.TabControlMain, 0)
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageControl1.ResumeLayout(False)
        Me.TabPageControl1.PerformLayout()
        Me.SelectMethodTabContent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.FileSelectionTabPageControl.ResumeLayout(False)
        Me.FileSelectionTabPageControl.PerformLayout()
        Me.FileSelectionTabContent.ResumeLayout(False)
        Me.FileSelectionTabContent.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TabPageControl2.ResumeLayout(False)
        Me.TabPageControl2.PerformLayout()
        Me.SelectTemplateResourceTabContent.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.SelectedTabPageControl.ResumeLayout(False)
        Me.SelectedTabPageControl.PerformLayout()
        Me.SelectedFilesTabContent.ResumeLayout(False)
        Me.SelectedFilesTabContent.PerformLayout()
        CType(Me.GenericGridEX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SelectedTabPageControl As TabPage
    Friend WithEvents SelectedFilesTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents FileSelectionTabPageControl As TabPage
    Friend WithEvents FileSelectionTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents SelectFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GenericGridEX As Janus.Windows.GridEX.GridEX
    Friend WithEvents SelectedFileInfoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabPageControl1 As TabPage
    Friend WithEvents TabPageControl2 As TabPage
    Friend WithEvents SelectTemplateResourceTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SelectTemplateButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TemplateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExportOptionControlBase1 As Questify.Builder.Client.ExportOptionControlBase
    Friend WithEvents SelectMethodTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TemplatedRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ImportRadioButton As System.Windows.Forms.RadioButton

End Class
