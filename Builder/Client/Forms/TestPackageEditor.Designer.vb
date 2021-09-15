Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestPackageEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestPackageEditor))
        Me.HierarchyControl = New Questify.Builder.UI.TestPackageHierarchyControl()
        Me.MetaDataControl = New Questify.Builder.UI.ResourceMetaData()
        Me.TestReferenceControl = New Questify.Builder.UI.TestReferencePropertyEditorContainer()
        Me.TestSetControl = New Questify.Builder.UI.TestSetPropertyEditorContainer()
        Me.TestPackagePropertiesControl = New Questify.Builder.UI.TestPackagePropertyEditorContainer()
        Me.TestPackageEditorMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeViewTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddNewTestsetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTestPackageComponentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveTestUpInTestsetToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestDownInTestsetToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestsetUpInTestpackageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestsetDownInTestpackageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveButtonsMenuToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveTestUpInTestsetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestDownInTestsetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestSetUpInTestPackageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestsetDownInTestPackageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestPackageEditorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveAsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddTestToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddTestsetToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteTestPackageComponentToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveButtonsToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveTestUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveTestDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveTestSetUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveTestSetDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ExportToExcelToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FormStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.StatusTextLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ResourceCustomProperties1 = New Questify.Builder.UI.ResourceCustomProperties()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TestPackageEditorMenu.SuspendLayout()
        Me.TestPackageEditorToolStrip.SuspendLayout()
        Me.FormStatusStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        Me.HierarchyControl.AllowDrop = True
        resources.ApplyResources(Me.HierarchyControl, "HierarchyControl")
        Me.HierarchyControl.Name = "HierarchyControl"
        Me.HierarchyControl.SelectedComponent = Nothing
        Me.HierarchyControl.TestPackage = Nothing
        Me.HierarchyControl.TestPackageIsNew = False
        resources.ApplyResources(Me.MetaDataControl, "MetaDataControl")
        Me.MetaDataControl.IsNameChangable = False
        Me.MetaDataControl.Name = "MetaDataControl"
        Me.MetaDataControl.ResourceEntity = Nothing
        resources.ApplyResources(Me.TestReferenceControl, "TestReferenceControl")
        Me.TestReferenceControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestReferenceControl.Name = "TestReferenceControl"
        Me.TestReferenceControl.TestPackageEntity = Nothing
        resources.ApplyResources(Me.TestSetControl, "TestSetControl")
        Me.TestSetControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestSetControl.BackColor = System.Drawing.SystemColors.Control
        Me.TestSetControl.Name = "TestSetControl"
        Me.TestSetControl.TestPackageEntity = Nothing
        resources.ApplyResources(Me.TestPackagePropertiesControl, "TestPackagePropertiesControl")
        Me.TestPackagePropertiesControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestPackagePropertiesControl.Name = "TestPackagePropertiesControl"
        Me.TestPackagePropertiesControl.TestPackageEntity = Nothing
        resources.ApplyResources(Me.TestPackageEditorMenu, "TestPackageEditorMenu")
        Me.TestPackageEditorMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolStripSeparator4, Me.EditToolStripMenuItem})
        Me.TestPackageEditorMenu.Name = "TestPackageEditorMenu"
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.SaveCloseToolStripMenuItem, Me.toolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        resources.ApplyResources(Me.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem")
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveCloseToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveCloseToolStripMenuItem, "SaveCloseToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Name = "SaveCloseToolStripMenuItem"
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        resources.ApplyResources(Me.toolStripSeparator2, "toolStripSeparator2")
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeViewTypesToolStripMenuItem, Me.ToolStripSeparator7, Me.AddNewTestsetToolStripMenuItem, Me.AddNewTestToolStripMenuItem, Me.DeleteTestPackageComponentToolStripMenuItem, Me.ToolStripSeparator5, Me.MoveTestUpInTestsetToolStripMenuItem1, Me.MoveTestDownInTestsetToolStripMenuItem1, Me.MoveTestsetUpInTestpackageToolStripMenuItem1, Me.MoveTestsetDownInTestpackageToolStripMenuItem1})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        resources.ApplyResources(Me.ChangeViewTypesToolStripMenuItem, "ChangeViewTypesToolStripMenuItem")
        Me.ChangeViewTypesToolStripMenuItem.Name = "ChangeViewTypesToolStripMenuItem"
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        Me.AddNewTestsetToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.add_favorite_16x16
        Me.AddNewTestsetToolStripMenuItem.Name = "AddNewTestsetToolStripMenuItem"
        resources.ApplyResources(Me.AddNewTestsetToolStripMenuItem, "AddNewTestsetToolStripMenuItem")
        Me.AddNewTestToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.add_favorite_16x16
        Me.AddNewTestToolStripMenuItem.Name = "AddNewTestToolStripMenuItem"
        resources.ApplyResources(Me.AddNewTestToolStripMenuItem, "AddNewTestToolStripMenuItem")
        Me.DeleteTestPackageComponentToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.delete_favorite_16x16
        Me.DeleteTestPackageComponentToolStripMenuItem.Name = "DeleteTestPackageComponentToolStripMenuItem"
        resources.ApplyResources(Me.DeleteTestPackageComponentToolStripMenuItem, "DeleteTestPackageComponentToolStripMenuItem")
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        Me.MoveTestUpInTestsetToolStripMenuItem1.Image = Global.Questify.Builder.Client.My.Resources.Resources.up_16x16
        Me.MoveTestUpInTestsetToolStripMenuItem1.Name = "MoveTestUpInTestsetToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTestUpInTestsetToolStripMenuItem1, "MoveTestUpInTestsetToolStripMenuItem1")
        Me.MoveTestDownInTestsetToolStripMenuItem1.Image = Global.Questify.Builder.Client.My.Resources.Resources.down_16x16
        Me.MoveTestDownInTestsetToolStripMenuItem1.Name = "MoveTestDownInTestsetToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTestDownInTestsetToolStripMenuItem1, "MoveTestDownInTestsetToolStripMenuItem1")
        Me.MoveTestsetUpInTestpackageToolStripMenuItem1.Image = Global.Questify.Builder.Client.My.Resources.Resources.up_16x16
        Me.MoveTestsetUpInTestpackageToolStripMenuItem1.Name = "MoveTestsetUpInTestpackageToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTestsetUpInTestpackageToolStripMenuItem1, "MoveTestsetUpInTestpackageToolStripMenuItem1")
        Me.MoveTestsetDownInTestpackageToolStripMenuItem1.Image = Global.Questify.Builder.Client.My.Resources.Resources.down_16x16
        Me.MoveTestsetDownInTestpackageToolStripMenuItem1.Name = "MoveTestsetDownInTestpackageToolStripMenuItem1"
        resources.ApplyResources(Me.MoveTestsetDownInTestpackageToolStripMenuItem1, "MoveTestsetDownInTestpackageToolStripMenuItem1")
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        Me.MoveButtonsMenuToolStripSeparator.Name = "MoveButtonsMenuToolStripSeparator"
        resources.ApplyResources(Me.MoveButtonsMenuToolStripSeparator, "MoveButtonsMenuToolStripSeparator")
        resources.ApplyResources(Me.MoveTestUpInTestsetToolStripMenuItem, "MoveTestUpInTestsetToolStripMenuItem")
        Me.MoveTestUpInTestsetToolStripMenuItem.Name = "MoveTestUpInTestsetToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestDownInTestsetToolStripMenuItem, "MoveTestDownInTestsetToolStripMenuItem")
        Me.MoveTestDownInTestsetToolStripMenuItem.Name = "MoveTestDownInTestsetToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestSetUpInTestPackageToolStripMenuItem, "MoveTestSetUpInTestPackageToolStripMenuItem")
        Me.MoveTestSetUpInTestPackageToolStripMenuItem.Name = "MoveTestSetUpInTestPackageToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestsetDownInTestPackageToolStripMenuItem, "MoveTestsetDownInTestPackageToolStripMenuItem")
        Me.MoveTestsetDownInTestPackageToolStripMenuItem.Name = "MoveTestsetDownInTestPackageToolStripMenuItem"
        resources.ApplyResources(Me.TestPackageEditorToolStrip, "TestPackageEditorToolStrip")
        Me.TestPackageEditorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripButton, Me.SaveAsToolStripButton, Me.SaveCloseToolStripButton, Me.toolStripSeparator6, Me.AddTestToolStripButton, Me.AddTestsetToolStripButton, Me.ToolStripSeparator1, Me.DeleteTestPackageComponentToolStripButton, Me.MoveButtonsToolStripSeparator, Me.MoveTestUpToolStripButton, Me.MoveTestDownToolStripButton, Me.MoveTestSetUpToolStripButton, Me.MoveTestSetDownToolStripButton, Me.ExportToExcelToolStripButton})
        Me.TestPackageEditorToolStrip.Name = "TestPackageEditorToolStrip"
        resources.ApplyResources(Me.SaveToolStripButton, "SaveToolStripButton")
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        resources.ApplyResources(Me.SaveAsToolStripButton, "SaveAsToolStripButton")
        Me.SaveAsToolStripButton.Name = "SaveAsToolStripButton"
        Me.SaveCloseToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveCloseToolStripButton, "SaveCloseToolStripButton")
        Me.SaveCloseToolStripButton.Name = "SaveCloseToolStripButton"
        Me.toolStripSeparator6.Name = "toolStripSeparator6"
        resources.ApplyResources(Me.toolStripSeparator6, "toolStripSeparator6")
        resources.ApplyResources(Me.AddTestToolStripButton, "AddTestToolStripButton")
        Me.AddTestToolStripButton.Name = "AddTestToolStripButton"
        resources.ApplyResources(Me.AddTestsetToolStripButton, "AddTestsetToolStripButton")
        Me.AddTestsetToolStripButton.Name = "AddTestsetToolStripButton"
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        resources.ApplyResources(Me.DeleteTestPackageComponentToolStripButton, "DeleteTestPackageComponentToolStripButton")
        Me.DeleteTestPackageComponentToolStripButton.Name = "DeleteTestPackageComponentToolStripButton"
        Me.MoveButtonsToolStripSeparator.Name = "MoveButtonsToolStripSeparator"
        resources.ApplyResources(Me.MoveButtonsToolStripSeparator, "MoveButtonsToolStripSeparator")
        resources.ApplyResources(Me.MoveTestUpToolStripButton, "MoveTestUpToolStripButton")
        Me.MoveTestUpToolStripButton.Name = "MoveTestUpToolStripButton"
        resources.ApplyResources(Me.MoveTestDownToolStripButton, "MoveTestDownToolStripButton")
        Me.MoveTestDownToolStripButton.Name = "MoveTestDownToolStripButton"
        resources.ApplyResources(Me.MoveTestSetUpToolStripButton, "MoveTestSetUpToolStripButton")
        Me.MoveTestSetUpToolStripButton.Name = "MoveTestSetUpToolStripButton"
        resources.ApplyResources(Me.MoveTestSetDownToolStripButton, "MoveTestSetDownToolStripButton")
        Me.MoveTestSetDownToolStripButton.Name = "MoveTestSetDownToolStripButton"
        resources.ApplyResources(Me.ExportToExcelToolStripButton, "ExportToExcelToolStripButton")
        Me.ExportToExcelToolStripButton.Name = "ExportToExcelToolStripButton"
        resources.ApplyResources(Me.FormStatusStrip, "FormStatusStrip")
        Me.FormStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusTextLabel, Me.StatusLabel})
        Me.FormStatusStrip.Name = "FormStatusStrip"
        Me.StatusTextLabel.Name = "StatusTextLabel"
        resources.ApplyResources(Me.StatusTextLabel, "StatusTextLabel")
        Me.StatusLabel.Name = "StatusLabel"
        resources.ApplyResources(Me.StatusLabel, "StatusLabel")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Panel1.Controls.Add(Me.HierarchyControl)
        Me.SplitContainer2.Panel2.Controls.Add(Me.TabControl2)
        resources.ApplyResources(Me.TabControl2, "TabControl2")
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabPage4.Controls.Add(Me.MetaDataControl)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage5.Controls.Add(Me.ResourceCustomProperties1)
        resources.ApplyResources(Me.TabPage5, "TabPage5")
        Me.TabPage5.Name = "TabPage5"
        resources.ApplyResources(Me.ResourceCustomProperties1, "ResourceCustomProperties1")
        Me.ResourceCustomProperties1.CustomPropertyFilter = Nothing
        Me.ResourceCustomProperties1.CustomPropertyTypeFilter = Enums.ResourceTypeEnum.TestPackageResource
        Me.ResourceCustomProperties1.Name = "ResourceCustomProperties1"
        Me.ResourceCustomProperties1.ResourceEntity = Nothing
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabPage1.Controls.Add(Me.TestReferenceControl)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage2.Controls.Add(Me.TestSetControl)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage3.Controls.Add(Me.TestPackagePropertiesControl)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.CausesValidation = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TestPackageEditorToolStrip)
        Me.Controls.Add(Me.TestPackageEditorMenu)
        Me.Controls.Add(Me.FormStatusStrip)
        Me.MainMenuStrip = Me.TestPackageEditorMenu
        Me.Name = "TestPackageEditor"
        Me.TestPackageEditorMenu.ResumeLayout(False)
        Me.TestPackageEditorMenu.PerformLayout()
        Me.TestPackageEditorToolStrip.ResumeLayout(False)
        Me.TestPackageEditorToolStrip.PerformLayout()
        Me.FormStatusStrip.ResumeLayout(False)
        Me.FormStatusStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub
    Friend WithEvents HierarchyControl As Questify.Builder.UI.TestPackageHierarchyControl
    Friend WithEvents MetaDataControl As Questify.Builder.UI.ResourceMetaData
    Friend WithEvents TestPackagePropertiesControl As Questify.Builder.UI.TestPackagePropertyEditorContainer
    Friend WithEvents TestPackageEditorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TestPackageEditorMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestReferenceControl As Questify.Builder.UI.TestReferencePropertyEditorContainer
    Friend WithEvents TestSetControl As Questify.Builder.UI.TestSetPropertyEditorContainer
    Friend WithEvents AddTestsetToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddTestToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTestPackageComponentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveButtonsMenuToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveTestUpInTestsetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestDownInTestsetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveButtonsToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveTestUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveTestDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents FormStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusTextLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MoveTestSetUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveTestSetDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveTestSetUpInTestPackageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestsetDownInTestPackageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCloseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveCloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChangeViewTypesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddNewTestsetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveTestUpInTestsetToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestDownInTestsetToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestsetUpInTestpackageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestsetDownInTestpackageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTestPackageComponentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents ResourceCustomProperties1 As UI.ResourceCustomProperties
End Class
