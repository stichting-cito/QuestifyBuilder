<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GridBase
    Inherits ActionCommandSupportBaseControl

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
        Dim GridControl_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GridBase))
        Me.GridControl = New Janus.Windows.GridEX.GridEX()
        Me.ReportsContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilteringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilteringToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditSecondMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HarmonizeDependentItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PublishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultiEditToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MultiEditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.FastSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResourceMoveToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveResourceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToggleVisibilitySeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToggleVisibilityMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertiesSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SaveExcelFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.CollapseGroupsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReportsContextMenuStrip.SuspendLayout()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.GridControl.AllowColumnDrag = False
        Me.GridControl.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridControl.AlternatingColors = True
        Me.GridControl.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GridControl.ContextMenuStrip = Me.ReportsContextMenuStrip
        Me.GridControl.DataSource = Me.GridBindingSource
        resources.ApplyResources(GridControl_DesignTimeLayout, "GridControl_DesignTimeLayout")
        Me.GridControl.DesignTimeLayout = GridControl_DesignTimeLayout
        resources.ApplyResources(Me.GridControl, "GridControl")
        Me.GridControl.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.GridControl.FocusCellFormatStyle.ForeColor = System.Drawing.Color.White
        Me.GridControl.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.GridControl.GroupByBoxVisible = False
        Me.GridControl.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.GridControl.Name = "GridControl"
        Me.GridControl.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridControl.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.ReportsContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectColumnsToolStripMenuItem, Me.GroupingToolStripMenuItem, Me.CollapseGroupsToolStripMenuItem, Me.FilteringToolStripMenuItem, Me.FilteringToolStripSeparator, Me.EditToolStripMenuItem, Me.EditSecondMenuItem, Me.DeleteToolStripMenuItem, Me.HarmonizeDependentItemsToolStripMenuItem, Me.ExportSeparator, Me.PreviewToolStripMenuItem, Me.PublishToolStripMenuItem, Me.ExportToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.MultiEditToolStripSeparator, Me.MultiEditToolStripMenuItem, Me.SearchSeparator, Me.FastSearchToolStripMenuItem, Me.ResourceMoveToolStripSeparator, Me.MoveResourceToolStripMenuItem, Me.ToggleVisibilitySeparator, Me.ToggleVisibilityMenuItem, Me.PropertiesSeparator, Me.PropertiesToolStripMenuItem})
        Me.ReportsContextMenuStrip.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.ReportsContextMenuStrip, "ReportsContextMenuStrip")
        Me.SelectColumnsToolStripMenuItem.Name = "SelectColumnsToolStripMenuItem"
        resources.ApplyResources(Me.SelectColumnsToolStripMenuItem, "SelectColumnsToolStripMenuItem")
        Me.GroupingToolStripMenuItem.Name = "GroupingToolStripMenuItem"
        resources.ApplyResources(Me.GroupingToolStripMenuItem, "GroupingToolStripMenuItem")
        Me.FilteringToolStripMenuItem.Name = "FilteringToolStripMenuItem"
        resources.ApplyResources(Me.FilteringToolStripMenuItem, "FilteringToolStripMenuItem")
        Me.FilteringToolStripSeparator.Name = "FilteringToolStripSeparator"
        resources.ApplyResources(Me.FilteringToolStripSeparator, "FilteringToolStripSeparator")
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditSecondMenuItem.Name = "EditSecondMenuItem"
        resources.ApplyResources(Me.EditSecondMenuItem, "EditSecondMenuItem")
        resources.ApplyResources(Me.DeleteToolStripMenuItem, "DeleteToolStripMenuItem")
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        resources.ApplyResources(Me.HarmonizeDependentItemsToolStripMenuItem, "HarmonizeDependentItemsToolStripMenuItem")
        Me.HarmonizeDependentItemsToolStripMenuItem.Name = "HarmonizeDependentItemsToolStripMenuItem"
        Me.ExportSeparator.Name = "ExportSeparator"
        resources.ApplyResources(Me.ExportSeparator, "ExportSeparator")
        resources.ApplyResources(Me.PreviewToolStripMenuItem, "PreviewToolStripMenuItem")
        Me.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem"
        resources.ApplyResources(Me.PublishToolStripMenuItem, "PublishToolStripMenuItem")
        Me.PublishToolStripMenuItem.Name = "PublishToolStripMenuItem"
        resources.ApplyResources(Me.ExportToolStripMenuItem, "ExportToolStripMenuItem")
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        resources.ApplyResources(Me.ReportsToolStripMenuItem, "ReportsToolStripMenuItem")
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.MultiEditToolStripSeparator.Name = "MultiEditToolStripSeparator"
        resources.ApplyResources(Me.MultiEditToolStripSeparator, "MultiEditToolStripSeparator")
        Me.MultiEditToolStripMenuItem.Name = "MultiEditToolStripMenuItem"
        resources.ApplyResources(Me.MultiEditToolStripMenuItem, "MultiEditToolStripMenuItem")
        Me.SearchSeparator.Name = "SearchSeparator"
        resources.ApplyResources(Me.SearchSeparator, "SearchSeparator")
        resources.ApplyResources(Me.FastSearchToolStripMenuItem, "FastSearchToolStripMenuItem")
        Me.FastSearchToolStripMenuItem.Name = "FastSearchToolStripMenuItem"
        Me.ResourceMoveToolStripSeparator.Name = "ResourceMoveToolStripSeparator"
        resources.ApplyResources(Me.ResourceMoveToolStripSeparator, "ResourceMoveToolStripSeparator")
        Me.MoveResourceToolStripMenuItem.Name = "MoveResourceToolStripMenuItem"
        resources.ApplyResources(Me.MoveResourceToolStripMenuItem, "MoveResourceToolStripMenuItem")
        Me.ToggleVisibilitySeparator.Name = "ToggleVisibilitySeparator"
        resources.ApplyResources(Me.ToggleVisibilitySeparator, "ToggleVisibilitySeparator")
        Me.ToggleVisibilityMenuItem.Name = "ToggleVisibilityMenuItem"
        resources.ApplyResources(Me.ToggleVisibilityMenuItem, "ToggleVisibilityMenuItem")
        Me.PropertiesSeparator.Name = "PropertiesSeparator"
        resources.ApplyResources(Me.PropertiesSeparator, "PropertiesSeparator")
        resources.ApplyResources(Me.PropertiesToolStripMenuItem, "PropertiesToolStripMenuItem")
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.SaveExcelFileDialog.DefaultExt = "xls"
        resources.ApplyResources(Me.SaveExcelFileDialog, "SaveExcelFileDialog")
        Me.CollapseGroupsToolStripMenuItem.Name = "CollapseGroupsToolStripMenuItem"
        resources.ApplyResources(Me.CollapseGroupsToolStripMenuItem, "CollapseGroupsToolStripMenuItem")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridControl)
        Me.Name = "GridBase"
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ReportsContextMenuStrip.ResumeLayout(False)
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ReportsContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectColumnsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PublishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertiesSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveExcelFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilteringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FastSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FilteringToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultiEditToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MultiEditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HarmonizeDependentItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResourceMoveToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveResourceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditSecondMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToggleVisibilitySeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToggleVisibilityMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseGroupsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents GridControl As Janus.Windows.GridEX.GridEX

End Class
