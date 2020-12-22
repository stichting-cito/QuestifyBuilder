Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GenericResourceEditor
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GenericResourceEditor))
        Me.MetaData = New Questify.Builder.UI.ResourceMetaData()
        Me.ResourceCustomProperties1 = New Questify.Builder.UI.ResourceCustomProperties()
        Me.ResourceEditorControl = New Questify.Builder.UI.GenericResourceEditorControl()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenericResourceEditorMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.GenericResourceEditorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.SaveAsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GenericResourceEditorMenuStrip.SuspendLayout()
        Me.GenericResourceEditorToolStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.MetaData, "MetaData")
        Me.MetaData.Name = "MetaData"
        Me.MetaData.ResourceEntity = Nothing
        resources.ApplyResources(Me.ResourceCustomProperties1, "ResourceCustomProperties1")
        Me.ResourceCustomProperties1.CustomPropertyFilter = Nothing
        Me.ResourceCustomProperties1.CustomPropertyTypeFilter = ResourceTypeEnum.GenericResource
        Me.ResourceCustomProperties1.Name = "ResourceCustomProperties1"
        Me.ResourceCustomProperties1.ResourceEntity = Nothing
        Me.ResourceEditorControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ResourceEditorControl.BankContextIdentifier = Nothing
        Me.ResourceEditorControl.DataSource = Nothing
        resources.ApplyResources(Me.ResourceEditorControl, "ResourceEditorControl")
        Me.ResourceEditorControl.IsEditable = True
        Me.ResourceEditorControl.Name = "ResourceEditorControl"
        Me.ResourceEditorControl.ResourceManager = Nothing
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.SaveCloseToolStripMenuItem, Me.toolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        Me.SaveToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveAs16
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        resources.ApplyResources(Me.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveClose16
        resources.ApplyResources(Me.SaveCloseToolStripMenuItem, "SaveCloseToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Name = "SaveCloseToolStripMenuItem"
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        resources.ApplyResources(Me.toolStripSeparator1, "toolStripSeparator1")
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        resources.ApplyResources(Me.GenericResourceEditorMenuStrip, "GenericResourceEditorMenuStrip")
        Me.GenericResourceEditorMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.GenericResourceEditorMenuStrip.Name = "GenericResourceEditorMenuStrip"
        Me.SaveToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveToolStripButton, "SaveToolStripButton")
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        resources.ApplyResources(Me.GenericResourceEditorToolStrip, "GenericResourceEditorToolStrip")
        Me.GenericResourceEditorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripButton, Me.SaveAsToolStripButton, Me.SaveCloseToolStripButton})
        Me.GenericResourceEditorToolStrip.Name = "GenericResourceEditorToolStrip"
        Me.SaveAsToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveAs16
        resources.ApplyResources(Me.SaveAsToolStripButton, "SaveAsToolStripButton")
        Me.SaveAsToolStripButton.Name = "SaveAsToolStripButton"
        Me.SaveCloseToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.SaveClose16
        resources.ApplyResources(Me.SaveCloseToolStripButton, "SaveCloseToolStripButton")
        Me.SaveCloseToolStripButton.Name = "SaveCloseToolStripButton"
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ResourceEditorControl)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabPage1.Controls.Add(Me.MetaData)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage2.Controls.Add(Me.ResourceCustomProperties1)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.GenericResourceEditorToolStrip)
        Me.Controls.Add(Me.GenericResourceEditorMenuStrip)
        Me.MainMenuStrip = Me.GenericResourceEditorMenuStrip
        Me.Name = "GenericResourceEditor"
        Me.GenericResourceEditorMenuStrip.ResumeLayout(False)
        Me.GenericResourceEditorMenuStrip.PerformLayout()
        Me.GenericResourceEditorToolStrip.ResumeLayout(False)
        Me.GenericResourceEditorToolStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetaData As Questify.Builder.UI.ResourceMetaData
    Friend WithEvents ResourceEditorControl As Questify.Builder.UI.GenericResourceEditorControl
    Friend WithEvents GenericResourceEditorMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenericResourceEditorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ResourceCustomProperties1 As Questify.Builder.UI.ResourceCustomProperties
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCloseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
