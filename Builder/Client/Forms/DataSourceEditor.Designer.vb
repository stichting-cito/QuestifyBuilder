Imports Enums
Imports Questify.Builder.UI.Commanding

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataSourceEditor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataSourceEditor))
        Me.ResourceIdentifierEditorInstance = New Questify.Builder.UI.ResourceIdentifierEditor()
        Me.MetaDataControl = New Questify.Builder.UI.ResourceMetaData()
        Me.ResourceCustomProperties1 = New Questify.Builder.UI.ResourceCustomProperties()
        Me.DataSourceSettingsEditorInstance = New Questify.Builder.UI.DataSourceSettingsEditor()
        Me.DataSourcePreviewInstance = New Questify.Builder.UI.ItemDataSourcePreview()
        Me.DataSourceReportsInstance = New Questify.Builder.UI.DataSourceReportContainerControl()
        Me.FormStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.StatusTextLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSourceEditorToolStrip = New System.Windows.Forms.ToolStrip()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveAsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSourceEditorMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.SelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandManager1 = New Questify.Builder.UI.Commanding.CommandManager(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.FormStatusStrip.SuspendLayout()
        Me.DataSourceEditorToolStrip.SuspendLayout()
        Me.DataSourceEditorMenuStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ResourceIdentifierEditorInstance, "ResourceIdentifierEditorInstance")
        Me.ResourceIdentifierEditorInstance.Name = "ResourceIdentifierEditorInstance"
        Me.ResourceIdentifierEditorInstance.ResourceEntity = Nothing
        resources.ApplyResources(Me.MetaDataControl, "MetaDataControl")
        Me.MetaDataControl.IsNameChangable = False
        Me.MetaDataControl.Name = "MetaDataControl"
        Me.MetaDataControl.ResourceEntity = Nothing
        resources.ApplyResources(Me.ResourceCustomProperties1, "ResourceCustomProperties1")
        Me.ResourceCustomProperties1.CustomPropertyFilter = Nothing
        Me.ResourceCustomProperties1.CustomPropertyTypeFilter = ResourceTypeEnum.ItemResource
        Me.ResourceCustomProperties1.Name = "ResourceCustomProperties1"
        Me.ResourceCustomProperties1.ResourceEntity = Nothing
        resources.ApplyResources(Me.DataSourceSettingsEditorInstance, "DataSourceSettingsEditorInstance")
        Me.DataSourceSettingsEditorInstance.Name = "DataSourceSettingsEditorInstance"
        resources.ApplyResources(Me.DataSourcePreviewInstance, "DataSourcePreviewInstance")
        Me.DataSourcePreviewInstance.Name = "DataSourcePreviewInstance"
        resources.ApplyResources(Me.DataSourceReportsInstance, "DataSourceReportsInstance")
        Me.DataSourceReportsInstance.Name = "DataSourceReportsInstance"
        resources.ApplyResources(Me.FormStatusStrip, "FormStatusStrip")
        Me.FormStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusTextLabel})
        Me.FormStatusStrip.Name = "FormStatusStrip"
        Me.StatusTextLabel.Name = "StatusTextLabel"
        resources.ApplyResources(Me.StatusTextLabel, "StatusTextLabel")
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.ExitToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.close_16x16
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        resources.ApplyResources(Me.DataSourceEditorToolStrip, "DataSourceEditorToolStrip")
        Me.DataSourceEditorToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripButton, Me.SaveAsToolStripButton, Me.SaveCloseToolStripButton})
        Me.DataSourceEditorToolStrip.Name = "DataSourceEditorToolStrip"
        Me.SaveToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save
        resources.ApplyResources(Me.SaveToolStripButton, "SaveToolStripButton")
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveAsToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save
        resources.ApplyResources(Me.SaveAsToolStripButton, "SaveAsToolStripButton")
        Me.SaveAsToolStripButton.Name = "SaveAsToolStripButton"
        Me.SaveCloseToolStripButton.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveCloseToolStripButton, "SaveCloseToolStripButton")
        Me.SaveCloseToolStripButton.Name = "SaveCloseToolStripButton"
        Me.SaveCloseToolStripMenuItem.Image = Global.Questify.Builder.Client.My.Resources.Resources.Save16
        resources.ApplyResources(Me.SaveCloseToolStripMenuItem, "SaveCloseToolStripMenuItem")
        Me.SaveCloseToolStripMenuItem.Name = "SaveCloseToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        resources.ApplyResources(Me.SaveAsToolStripMenuItem, "SaveAsToolStripMenuItem")
        resources.ApplyResources(Me.SaveToolStripMenuItem, "SaveToolStripMenuItem")
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.SaveCloseToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        resources.ApplyResources(Me.DataSourceEditorMenuStrip, "DataSourceEditorMenuStrip")
        Me.DataSourceEditorMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SelectionToolStripMenuItem})
        Me.DataSourceEditorMenuStrip.Name = "DataSourceEditorMenuStrip"
        Me.SelectionToolStripMenuItem.Name = "SelectionToolStripMenuItem"
        resources.ApplyResources(Me.SelectionToolStripMenuItem, "SelectionToolStripMenuItem")
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl2)
        resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Panel1.Controls.Add(Me.ResourceIdentifierEditorInstance)
        Me.SplitContainer2.Panel2.Controls.Add(Me.TabControl1)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabPage1.Controls.Add(Me.MetaDataControl)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage2.Controls.Add(Me.ResourceCustomProperties1)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        resources.ApplyResources(Me.TabControl2, "TabControl2")
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabPage3.Controls.Add(Me.DataSourceSettingsEditorInstance)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage4.Controls.Add(Me.DataSourcePreviewInstance)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage5.Controls.Add(Me.DataSourceReportsInstance)
        resources.ApplyResources(Me.TabPage5, "TabPage5")
        Me.TabPage5.Name = "TabPage5"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DataSourceEditorToolStrip)
        Me.Controls.Add(Me.DataSourceEditorMenuStrip)
        Me.Controls.Add(Me.FormStatusStrip)
        Me.Name = "DataSourceEditor"
        Me.FormStatusStrip.ResumeLayout(False)
        Me.FormStatusStrip.PerformLayout()
        Me.DataSourceEditorToolStrip.ResumeLayout(False)
        Me.DataSourceEditorToolStrip.PerformLayout()
        Me.DataSourceEditorMenuStrip.ResumeLayout(False)
        Me.DataSourceEditorMenuStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataSourceSettingsEditorInstance As Questify.Builder.UI.DataSourceSettingsEditor
    Friend WithEvents DataSourceReportsInstance As Questify.Builder.UI.DataSourceReportContainerControl
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataSourceEditorToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveCloseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveCloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResourceCustomProperties1 As Questify.Builder.UI.ResourceCustomProperties
    Friend WithEvents MetaDataControl As Questify.Builder.UI.ResourceMetaData
    Friend WithEvents ResourceIdentifierEditorInstance As Questify.Builder.UI.ResourceIdentifierEditor
    Friend WithEvents StatusTextLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DataSourcePreviewInstance As Questify.Builder.UI.ItemDataSourcePreview
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataSourceEditorMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FormStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents CommandManager1 As CommandManager
    Friend WithEvents SelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
End Class
