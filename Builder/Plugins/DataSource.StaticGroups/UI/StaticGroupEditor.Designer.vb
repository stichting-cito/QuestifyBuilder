<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StaticGroupEditor


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StaticGroupEditor))
        Dim ItemInGroupGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ContainerPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ItemInGroupGrid = New Janus.Windows.GridEX.GridEX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemsFromCodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StaticGroupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ContainerPanel.SuspendLayout()
        CType(Me.ItemInGroupGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.StaticGroupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContainerPanel, "ContainerPanel")
        Me.ContainerPanel.Controls.Add(Me.ItemInGroupGrid, 0, 0)
        Me.ContainerPanel.Name = "ContainerPanel"
        Me.ItemInGroupGrid.AllowColumnDrag = False
        Me.ItemInGroupGrid.AllowDrop = True
        Me.ItemInGroupGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.ItemInGroupGrid.AlternatingColors = True
        Me.ItemInGroupGrid.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ItemInGroupGrid.AutomaticSort = False
        Me.ItemInGroupGrid.ColumnSetHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.ContainerPanel.SetColumnSpan(Me.ItemInGroupGrid, 2)
        Me.ItemInGroupGrid.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ItemInGroupGrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.ItemInGroupGrid.DataSource = Me.StaticGroupBindingSource
        resources.ApplyResources(ItemInGroupGrid_DesignTimeLayout, "ItemInGroupGrid_DesignTimeLayout")
        Me.ItemInGroupGrid.DesignTimeLayout = ItemInGroupGrid_DesignTimeLayout
        resources.ApplyResources(Me.ItemInGroupGrid, "ItemInGroupGrid")
        Me.ItemInGroupGrid.ExpandableGroups = Janus.Windows.GridEX.InheritableBoolean.[Default]
        Me.ItemInGroupGrid.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.ItemInGroupGrid.FocusCellFormatStyle.ForeColor = System.Drawing.Color.White
        Me.ItemInGroupGrid.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.ItemInGroupGrid.GroupByBoxVisible = False
        Me.ItemInGroupGrid.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.ItemInGroupGrid.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.ItemInGroupGrid.Hierarchical = True
        Me.ItemInGroupGrid.Name = "ItemInGroupGrid"
        Me.ItemInGroupGrid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.ItemInGroupGrid.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToolStripMenuItem, Me.AddItemToolStripMenuItem, Me.AddItemsFromCodesToolStripMenuItem, Me.CreateGroupToolStripMenuItem, Me.MoveUpToolStripMenuItem, Me.MoveDownToolStripMenuItem, Me.RemoveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        resources.ApplyResources(Me.AddToolStripMenuItem, "AddToolStripMenuItem")
        Me.AddToolStripMenuItem.Tag = "AddGroups"
        Me.AddItemToolStripMenuItem.Name = "AddItemToolStripMenuItem"
        resources.ApplyResources(Me.AddItemToolStripMenuItem, "AddItemToolStripMenuItem")
        Me.AddItemToolStripMenuItem.Tag = "Items"
        Me.AddItemsFromCodesToolStripMenuItem.Name = "AddItemsFromCodesToolStripMenuItem"
        resources.ApplyResources(Me.AddItemsFromCodesToolStripMenuItem, "AddItemsFromCodesToolStripMenuItem")
        Me.AddItemsFromCodesToolStripMenuItem.Tag = "Items"
        Me.CreateGroupToolStripMenuItem.Name = "CreateGroupToolStripMenuItem"
        resources.ApplyResources(Me.CreateGroupToolStripMenuItem, "CreateGroupToolStripMenuItem")
        Me.CreateGroupToolStripMenuItem.Tag = "CreateGroups"
        Me.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem"
        resources.ApplyResources(Me.MoveUpToolStripMenuItem, "MoveUpToolStripMenuItem")
        Me.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem"
        resources.ApplyResources(Me.MoveDownToolStripMenuItem, "MoveDownToolStripMenuItem")
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        resources.ApplyResources(Me.RemoveToolStripMenuItem, "RemoveToolStripMenuItem")
        Me.StaticGroupBindingSource.DataSource = GetType(Questify.Builder.Plugins.DataSource.StaticGroups.Entities.StaticGroupEntry)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ContainerPanel)
        Me.Name = "StaticGroupEditor"
        Me.ContainerPanel.ResumeLayout(False)
        CType(Me.ItemInGroupGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.StaticGroupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContainerPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StaticGroupBindingSource As System.Windows.Forms.BindingSource
    Private WithEvents ItemInGroupGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddItemToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddItemsFromCodesToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
