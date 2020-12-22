
Imports Enums
Imports Questify.Builder.UI.Commanding

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TestEditor_v2
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestEditor_v2))
        Me.LockedForEditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestEditorMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeViewTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddTestpartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemsFromCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTestComponentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveButtonsMenuToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveItemUpInSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveItemDownInSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveSectionUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveSectionDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestpartUpInTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestpartDownInTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateVariantsTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestEditorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveAsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddItemToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddSectionToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddDataSourceToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddTestPartToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddItemsFromCodeToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteTestComponentToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveButtonsToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveItemUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveItemDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveSectionUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveSectionDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveTestPartUpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.MoveTestPartDownToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FormStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.StatusTextLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.HierarchyControl = New Questify.Builder.UI.TestHierarchyControl()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageMetadata = New System.Windows.Forms.TabPage()
        Me.MetaDataControl = New Questify.Builder.UI.ResourceMetaData()
        Me.TabPageCustomProperties = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPageTestPartProperties = New System.Windows.Forms.TabPage()
        Me.TestPartControl = New Questify.Builder.UI.TestPartPropertyEditorContainer()
        Me.TabPageItemReferenceProperties = New System.Windows.Forms.TabPage()
        Me.ItemReferenceControl = New Questify.Builder.UI.ItemReferencePropertyEditorContainer()
        Me.TabPageTestSectionProperties = New System.Windows.Forms.TabPage()
        Me.TestSectionControl = New Questify.Builder.UI.TestSectionPropertyEditorContainer()
        Me.TabPageTestProperties = New System.Windows.Forms.TabPage()
        Me.TestPropertiesControl = New Questify.Builder.UI.AssessmentTestPropertyEditorContainer()
        Me.TabPagePreview = New System.Windows.Forms.TabPage()
        Me.ItemPreviewControl = New Questify.Builder.UI.ItemPreviewContainer()
        Me.ImportantMessageControl = New Questify.Builder.UI.ImportantMessageControl()
        Me.CommandManager1 = New Questify.Builder.UI.Commanding.CommandManager(Me.components)
        Me.ResourceCustomProperties1 = New Questify.Builder.UI.ResourceCustomProperties()
        Me.TestEditorMenu.SuspendLayout()
        Me.TestEditorToolStrip.SuspendLayout()
        Me.FormStatusStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageMetadata.SuspendLayout()
        Me.TabPageCustomProperties.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPageTestPartProperties.SuspendLayout()
        Me.TabPageItemReferenceProperties.SuspendLayout()
        Me.TabPageTestSectionProperties.SuspendLayout()
        Me.TabPageTestProperties.SuspendLayout()
        Me.TabPagePreview.SuspendLayout()
        Me.SuspendLayout()
        Me.LockedForEditToolStripMenuItem.CheckOnClick = True
        resources.ApplyResources(Me.LockedForEditToolStripMenuItem, "LockedForEditToolStripMenuItem")
        Me.LockedForEditToolStripMenuItem.Name = "LockedForEditToolStripMenuItem"
        Me.TestEditorMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem})
        resources.ApplyResources(Me.TestEditorMenu, "TestEditorMenu")
        Me.TestEditorMenu.Name = "TestEditorMenu"
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
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LockedForEditToolStripMenuItem, Me.ChangeViewTypesToolStripMenuItem, Me.ToolStripSeparator3, Me.AddTestpartToolStripMenuItem, Me.AddSectionToolStripMenuItem, Me.AddItemToolStripMenuItem, Me.AddItemsFromCodeToolStripMenuItem, Me.DeleteTestComponentToolStripMenuItem, Me.MoveButtonsMenuToolStripSeparator, Me.MoveItemUpInSectionToolStripMenuItem, Me.MoveItemDownInSectionToolStripMenuItem, Me.MoveSectionUpToolStripMenuItem, Me.MoveSectionDownToolStripMenuItem, Me.MoveTestpartUpInTestToolStripMenuItem, Me.MoveTestpartDownInTestToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        resources.ApplyResources(Me.ChangeViewTypesToolStripMenuItem, "ChangeViewTypesToolStripMenuItem")
        Me.ChangeViewTypesToolStripMenuItem.Name = "ChangeViewTypesToolStripMenuItem"
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        resources.ApplyResources(Me.AddTestpartToolStripMenuItem, "AddTestpartToolStripMenuItem")
        Me.AddTestpartToolStripMenuItem.Name = "AddTestpartToolStripMenuItem"
        resources.ApplyResources(Me.AddSectionToolStripMenuItem, "AddSectionToolStripMenuItem")
        Me.AddSectionToolStripMenuItem.Name = "AddSectionToolStripMenuItem"
        resources.ApplyResources(Me.AddItemToolStripMenuItem, "AddItemToolStripMenuItem")
        Me.AddItemToolStripMenuItem.Name = "AddItemToolStripMenuItem"
        resources.ApplyResources(Me.AddItemsFromCodeToolStripMenuItem, "AddItemsFromCodeToolStripMenuItem")
        Me.AddItemsFromCodeToolStripMenuItem.Name = "AddItemsFromCodeToolStripMenuItem"
        resources.ApplyResources(Me.DeleteTestComponentToolStripMenuItem, "DeleteTestComponentToolStripMenuItem")
        Me.DeleteTestComponentToolStripMenuItem.Name = "DeleteTestComponentToolStripMenuItem"
        Me.MoveButtonsMenuToolStripSeparator.Name = "MoveButtonsMenuToolStripSeparator"
        resources.ApplyResources(Me.MoveButtonsMenuToolStripSeparator, "MoveButtonsMenuToolStripSeparator")
        resources.ApplyResources(Me.MoveItemUpInSectionToolStripMenuItem, "MoveItemUpInSectionToolStripMenuItem")
        Me.MoveItemUpInSectionToolStripMenuItem.Name = "MoveItemUpInSectionToolStripMenuItem"
        resources.ApplyResources(Me.MoveItemDownInSectionToolStripMenuItem, "MoveItemDownInSectionToolStripMenuItem")
        Me.MoveItemDownInSectionToolStripMenuItem.Name = "MoveItemDownInSectionToolStripMenuItem"
        resources.ApplyResources(Me.MoveSectionUpToolStripMenuItem, "MoveSectionUpToolStripMenuItem")
        Me.MoveSectionUpToolStripMenuItem.Name = "MoveSectionUpToolStripMenuItem"
        resources.ApplyResources(Me.MoveSectionDownToolStripMenuItem, "MoveSectionDownToolStripMenuItem")
        Me.MoveSectionDownToolStripMenuItem.Name = "MoveSectionDownToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestpartUpInTestToolStripMenuItem, "MoveTestpartUpInTestToolStripMenuItem")
        Me.MoveTestpartUpInTestToolStripMenuItem.Name = "MoveTestpartUpInTestToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestpartDownInTestToolStripMenuItem, "MoveTestpartDownInTestToolStripMenuItem")
        Me.MoveTestpartDownInTestToolStripMenuItem.Name = "MoveTestpartDownInTestToolStripMenuItem"
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreviewTestToolStripMenuItem, Me.GenerateVariantsTestToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        resources.ApplyResources(Me.ToolsToolStripMenuItem, "ToolsToolStripMenuItem")
        resources.ApplyResources(Me.PreviewTestToolStripMenuItem, "PreviewTestToolStripMenuItem")
        Me.PreviewTestToolStripMenuItem.Name = "PreviewTestToolStripMenuItem"
        Me.GenerateVariantsTestToolStripMenuItem.Name = "GenerateVariantsTestToolStripMenuItem"
        resources.ApplyResources(Me.GenerateVariantsTestToolStripMenuItem, "GenerateVariantsTestToolStripMenuItem")
        Me.TestEditorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripButton, Me.SaveAsToolStripButton, Me.SaveCloseToolStripButton, Me.toolStripSeparator6, Me.AddItemToolStripButton, Me.AddSectionToolStripButton, Me.AddDataSourceToolStripButton, Me.AddTestPartToolStripButton, Me.AddItemsFromCodeToolStripButton, Me.ToolStripSeparator1, Me.DeleteTestComponentToolStripButton, Me.MoveButtonsToolStripSeparator, Me.MoveItemUpToolStripButton, Me.MoveItemDownToolStripButton, Me.MoveSectionUpToolStripButton, Me.MoveSectionDownToolStripButton, Me.MoveTestPartUpToolStripButton, Me.MoveTestPartDownToolStripButton, Me.ToolStripSeparator4, Me.CopyToolStripButton, Me.PasteToolStripButton})
        resources.ApplyResources(Me.TestEditorToolStrip, "TestEditorToolStrip")
        Me.TestEditorToolStrip.Name = "TestEditorToolStrip"
        Me.SaveToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveToolStripButton, "SaveToolStripButton")
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveAsToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveAs16
        resources.ApplyResources(Me.SaveAsToolStripButton, "SaveAsToolStripButton")
        Me.SaveAsToolStripButton.Name = "SaveAsToolStripButton"
        Me.SaveCloseToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveClose16
        resources.ApplyResources(Me.SaveCloseToolStripButton, "SaveCloseToolStripButton")
        Me.SaveCloseToolStripButton.Name = "SaveCloseToolStripButton"
        Me.toolStripSeparator6.Name = "toolStripSeparator6"
        resources.ApplyResources(Me.toolStripSeparator6, "toolStripSeparator6")
        resources.ApplyResources(Me.AddItemToolStripButton, "AddItemToolStripButton")
        Me.AddItemToolStripButton.Name = "AddItemToolStripButton"
        resources.ApplyResources(Me.AddSectionToolStripButton, "AddSectionToolStripButton")
        Me.AddSectionToolStripButton.Name = "AddSectionToolStripButton"
        resources.ApplyResources(Me.AddDataSourceToolStripButton, "AddDataSourceToolStripButton")
        Me.AddDataSourceToolStripButton.Name = "AddDataSourceToolStripButton"
        resources.ApplyResources(Me.AddTestPartToolStripButton, "AddTestPartToolStripButton")
        Me.AddTestPartToolStripButton.Name = "AddTestPartToolStripButton"
        resources.ApplyResources(Me.AddItemsFromCodeToolStripButton, "AddItemsFromCodeToolStripButton")
        Me.AddItemsFromCodeToolStripButton.Name = "AddItemsFromCodeToolStripButton"
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        resources.ApplyResources(Me.DeleteTestComponentToolStripButton, "DeleteTestComponentToolStripButton")
        Me.DeleteTestComponentToolStripButton.Name = "DeleteTestComponentToolStripButton"
        Me.MoveButtonsToolStripSeparator.Name = "MoveButtonsToolStripSeparator"
        resources.ApplyResources(Me.MoveButtonsToolStripSeparator, "MoveButtonsToolStripSeparator")
        resources.ApplyResources(Me.MoveItemUpToolStripButton, "MoveItemUpToolStripButton")
        Me.MoveItemUpToolStripButton.Name = "MoveItemUpToolStripButton"
        resources.ApplyResources(Me.MoveItemDownToolStripButton, "MoveItemDownToolStripButton")
        Me.MoveItemDownToolStripButton.Name = "MoveItemDownToolStripButton"
        resources.ApplyResources(Me.MoveSectionUpToolStripButton, "MoveSectionUpToolStripButton")
        Me.MoveSectionUpToolStripButton.Name = "MoveSectionUpToolStripButton"
        resources.ApplyResources(Me.MoveSectionDownToolStripButton, "MoveSectionDownToolStripButton")
        Me.MoveSectionDownToolStripButton.Name = "MoveSectionDownToolStripButton"
        resources.ApplyResources(Me.MoveTestPartUpToolStripButton, "MoveTestPartUpToolStripButton")
        Me.MoveTestPartUpToolStripButton.Name = "MoveTestPartUpToolStripButton"
        resources.ApplyResources(Me.MoveTestPartDownToolStripButton, "MoveTestPartDownToolStripButton")
        Me.MoveTestPartDownToolStripButton.Name = "MoveTestPartDownToolStripButton"
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        resources.ApplyResources(Me.CopyToolStripButton, "CopyToolStripButton")
        Me.CopyToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.CopyToolStripMenuItem_Image
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        resources.ApplyResources(Me.PasteToolStripButton, "PasteToolStripButton")
        Me.PasteToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.PasteToolStripMenuItem_Image
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        Me.FormStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusTextLabel})
        resources.ApplyResources(Me.FormStatusStrip, "FormStatusStrip")
        Me.FormStatusStrip.Name = "FormStatusStrip"
        Me.StatusTextLabel.Name = "StatusTextLabel"
        resources.ApplyResources(Me.StatusTextLabel, "StatusTextLabel")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl2)
        resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Panel1.Controls.Add(Me.HierarchyControl)
        Me.SplitContainer2.Panel2.Controls.Add(Me.TabControl1)
        Me.HierarchyControl.AllowDrop = True
        Me.HierarchyControl.AssessmentTest = Nothing
        resources.ApplyResources(Me.HierarchyControl, "HierarchyControl")
        Me.HierarchyControl.Name = "HierarchyControl"
        Me.HierarchyControl.SelectedComponent = Nothing
        Me.HierarchyControl.TestIsNew = False
        Me.HierarchyControl.TestIsTemplate = False
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPageMetadata)
        Me.TabControl1.Controls.Add(Me.TabPageCustomProperties)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabPageMetadata.Controls.Add(Me.MetaDataControl)
        resources.ApplyResources(Me.TabPageMetadata, "TabPageMetadata")
        Me.TabPageMetadata.Name = "TabPageMetadata"
        resources.ApplyResources(Me.MetaDataControl, "MetaDataControl")
        Me.MetaDataControl.IsNameChangable = False
        Me.MetaDataControl.Name = "MetaDataControl"
        Me.MetaDataControl.ResourceEntity = Nothing
        Me.TabPageCustomProperties.Controls.Add(Me.ResourceCustomProperties1)
        resources.ApplyResources(Me.TabPageCustomProperties, "TabPageCustomProperties")
        Me.TabPageCustomProperties.Name = "TabPageCustomProperties"
        resources.ApplyResources(Me.TabControl2, "TabControl2")
        Me.TabControl2.Controls.Add(Me.TabPageTestPartProperties)
        Me.TabControl2.Controls.Add(Me.TabPageItemReferenceProperties)
        Me.TabControl2.Controls.Add(Me.TabPageTestSectionProperties)
        Me.TabControl2.Controls.Add(Me.TabPageTestProperties)
        Me.TabControl2.Controls.Add(Me.TabPagePreview)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabPageTestPartProperties.Controls.Add(Me.TestPartControl)
        resources.ApplyResources(Me.TabPageTestPartProperties, "TabPageTestPartProperties")
        Me.TabPageTestPartProperties.Name = "TabPageTestPartProperties"
        resources.ApplyResources(Me.TestPartControl, "TestPartControl")
        Me.TestPartControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestPartControl.Name = "TestPartControl"
        Me.TestPartControl.TestEntity = Nothing
        Me.TabPageItemReferenceProperties.Controls.Add(Me.ItemReferenceControl)
        resources.ApplyResources(Me.TabPageItemReferenceProperties, "TabPageItemReferenceProperties")
        Me.TabPageItemReferenceProperties.Name = "TabPageItemReferenceProperties"
        resources.ApplyResources(Me.ItemReferenceControl, "ItemReferenceControl")
        Me.ItemReferenceControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.ItemReferenceControl.Name = "ItemReferenceControl"
        Me.ItemReferenceControl.TestEntity = Nothing
        Me.TabPageTestSectionProperties.Controls.Add(Me.TestSectionControl)
        resources.ApplyResources(Me.TabPageTestSectionProperties, "TabPageTestSectionProperties")
        Me.TabPageTestSectionProperties.Name = "TabPageTestSectionProperties"
        resources.ApplyResources(Me.TestSectionControl, "TestSectionControl")
        Me.TestSectionControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestSectionControl.Name = "TestSectionControl"
        Me.TestSectionControl.TestEntity = Nothing
        Me.TabPageTestProperties.Controls.Add(Me.TestPropertiesControl)
        resources.ApplyResources(Me.TabPageTestProperties, "TabPageTestProperties")
        Me.TabPageTestProperties.Name = "TabPageTestProperties"
        resources.ApplyResources(Me.TestPropertiesControl, "TestPropertiesControl")
        Me.TestPropertiesControl.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.TestPropertiesControl.Name = "TestPropertiesControl"
        Me.TestPropertiesControl.TestEntity = Nothing
        Me.TabPagePreview.Controls.Add(Me.ItemPreviewControl)
        resources.ApplyResources(Me.TabPagePreview, "TabPagePreview")
        Me.TabPagePreview.Name = "TabPagePreview"
        resources.ApplyResources(Me.ItemPreviewControl, "ItemPreviewControl")
        Me.ItemPreviewControl.FormIsClosing = False
        Me.ItemPreviewControl.Name = "ItemPreviewControl"
        Me.ImportantMessageControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        resources.ApplyResources(Me.ImportantMessageControl, "ImportantMessageControl")
        Me.ImportantMessageControl.Message = Nothing
        Me.ImportantMessageControl.Name = "ImportantMessageControl"
        resources.ApplyResources(Me.ResourceCustomProperties1, "ResourceCustomProperties1")
        Me.ResourceCustomProperties1.CustomPropertyFilter = Nothing
        Me.ResourceCustomProperties1.CustomPropertyTypeFilter = ResourceTypeEnum.AssessmentTestResource
        Me.ResourceCustomProperties1.Name = "ResourceCustomProperties1"
        Me.ResourceCustomProperties1.ResourceEntity = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.CausesValidation = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.FormStatusStrip)
        Me.Controls.Add(Me.ImportantMessageControl)
        Me.Controls.Add(Me.TestEditorToolStrip)
        Me.Controls.Add(Me.TestEditorMenu)
        Me.MainMenuStrip = Me.TestEditorMenu
        Me.Name = "TestEditor_v2"
        Me.TestEditorMenu.ResumeLayout(False)
        Me.TestEditorMenu.PerformLayout()
        Me.TestEditorToolStrip.ResumeLayout(False)
        Me.TestEditorToolStrip.PerformLayout()
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
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageMetadata.ResumeLayout(False)
        Me.TabPageCustomProperties.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPageTestPartProperties.ResumeLayout(False)
        Me.TabPageTestPartProperties.PerformLayout()
        Me.TabPageItemReferenceProperties.ResumeLayout(False)
        Me.TabPageItemReferenceProperties.PerformLayout()
        Me.TabPageTestSectionProperties.ResumeLayout(False)
        Me.TabPageTestSectionProperties.PerformLayout()
        Me.TabPageTestProperties.ResumeLayout(False)
        Me.TabPageTestProperties.PerformLayout()
        Me.TabPagePreview.ResumeLayout(False)
        Me.TabPagePreview.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HierarchyControl As Questify.Builder.UI.TestHierarchyControl
    Friend WithEvents MetaDataControl As Questify.Builder.UI.ResourceMetaData
    Friend WithEvents TestPropertiesControl As Questify.Builder.UI.AssessmentTestPropertyEditorContainer
    Friend WithEvents TestEditorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TestEditorMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemReferenceControl As Questify.Builder.UI.ItemReferencePropertyEditorContainer
    Friend WithEvents TestSectionControl As Questify.Builder.UI.TestSectionPropertyEditorContainer
    Friend WithEvents TestPartControl As Questify.Builder.UI.TestPartPropertyEditorContainer
    Friend WithEvents ItemPreviewControl As Questify.Builder.UI.ItemPreviewContainer
    Friend WithEvents AddTestPartToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddSectionToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddItemToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddTestpartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTestComponentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTestComponentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveButtonsMenuToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveItemUpInSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveItemDownInSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveButtonsToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveItemUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveItemDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateVariantsTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusTextLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MoveSectionUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveSectionDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveTestPartUpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveTestPartDownToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveSectionUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveSectionDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestpartUpInTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestpartDownInTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCloseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveCloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LockedForEditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddItemsFromCodeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddItemsFromCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChangeViewTypesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportantMessageControl As Questify.Builder.UI.ImportantMessageControl
    Friend WithEvents CommandManager1 As CommandManager
    Friend WithEvents AddDataSourceToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CopyToolStripButton As ToolStripButton
    Friend WithEvents PasteToolStripButton As ToolStripButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageMetadata As TabPage
    Friend WithEvents TabPageCustomProperties As TabPage
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPageItemReferenceProperties As TabPage
    Friend WithEvents TabPageTestSectionProperties As TabPage
    Friend WithEvents TabPageTestProperties As TabPage
    Friend WithEvents TabPageTestPartProperties As TabPage
    Friend WithEvents TabPagePreview As TabPage

    Friend WithEvents ResourceCustomProperties1 As UI.ResourceCustomProperties
End Class
