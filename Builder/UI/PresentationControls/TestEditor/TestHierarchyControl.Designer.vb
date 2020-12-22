<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestHierarchyControl
    Inherits System.Windows.Forms.UserControl

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
        Dim TestHierarchyGrid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim TestHierarchyGrid_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestHierarchyControl))
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TestHierarchyGrid = New Janus.Windows.GridEX.GridEX()
        Me.TestContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LockedForEditMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddTestPartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddItemsFromCodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetDataSourceMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteTestComponentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveItemUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveItemDownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveSectionUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveSectionDownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestPartUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestPartDownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshItemDataSourceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveExcelFileDialog = New System.Windows.Forms.SaveFileDialog()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TestHierarchyGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TestContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        Me.GridBindingSource.DataSource = GetType(Cito.Tester.ContentModel.AssessmentTest2)
        Me.TestHierarchyGrid.AllowColumnDrag = False
        Me.TestHierarchyGrid.AllowDrop = True
        Me.TestHierarchyGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.TestHierarchyGrid.AutomaticSort = False
        Me.TestHierarchyGrid.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.TestHierarchyGrid.ContextMenuStrip = Me.TestContextMenuStrip
        Me.TestHierarchyGrid.DataSource = Me.GridBindingSource
        resources.ApplyResources(Me.TestHierarchyGrid, "TestHierarchyGrid")
        Me.TestHierarchyGrid.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.TestHierarchyGrid.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TestHierarchyGrid.FocusCellFormatStyle.ForeColor = System.Drawing.Color.White
        Me.TestHierarchyGrid.GridLineColor = System.Drawing.Color.White
        Me.TestHierarchyGrid.GridLines = Janus.Windows.GridEX.GridLines.Both
        Me.TestHierarchyGrid.GroupByBoxVisible = False
        Me.TestHierarchyGrid.GroupRowFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.GroupRowFormatStyle.BackColorGradient = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.HeaderFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Flat
        Me.TestHierarchyGrid.HeaderFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.HeaderFormatStyle.BackColorGradient = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.TestHierarchyGrid.Hierarchical = True
        TestHierarchyGrid_Layout_0.Key = "MultiColumnGrid"
        resources.ApplyResources(TestHierarchyGrid_Layout_0, "TestHierarchyGrid_Layout_0")
        TestHierarchyGrid_Layout_1.DataSource = Me.GridBindingSource
        TestHierarchyGrid_Layout_1.IsCurrentLayout = True
        TestHierarchyGrid_Layout_1.Key = "SingleColumnGrid"
        resources.ApplyResources(TestHierarchyGrid_Layout_1, "TestHierarchyGrid_Layout_1")
        Me.TestHierarchyGrid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {TestHierarchyGrid_Layout_0, TestHierarchyGrid_Layout_1})
        Me.TestHierarchyGrid.Name = "TestHierarchyGrid"
        Me.TestHierarchyGrid.RowFormatStyle.BackColor = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.RowWithErrorsFormatStyle.ForeColor = System.Drawing.Color.Black
        Me.TestHierarchyGrid.SelectedFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TestHierarchyGrid.SelectedFormatStyle.BackColorGradient = System.Drawing.Color.Empty
        Me.TestHierarchyGrid.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.TestHierarchyGrid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.TestHierarchyGrid.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.TestHierarchyGrid.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.TestHierarchyGrid.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Standard
        Me.TestContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LockedForEditMenuItem, Me.ContextMenuToolStripSeparator1, Me.AddTestPartToolStripMenuItem, Me.AddSectionToolStripMenuItem, Me.AddItemToolStripMenuItem, Me.AddItemsFromCodesToolStripMenuItem, Me.SetDataSourceMenuItem, Me.ContextMenuToolStripSeparator, Me.DeleteTestComponentToolStripMenuItem, Me.MoveItemUpMenuItem, Me.MoveItemDownMenuItem, Me.MoveSectionUpMenuItem, Me.MoveSectionDownMenuItem, Me.MoveTestPartUpMenuItem, Me.MoveTestPartDownMenuItem, Me.RefreshItemDataSourceToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem})
        Me.TestContextMenuStrip.Name = "TestContextMenuStrip"
        resources.ApplyResources(Me.TestContextMenuStrip, "TestContextMenuStrip")
        Me.LockedForEditMenuItem.CheckOnClick = True
        resources.ApplyResources(Me.LockedForEditMenuItem, "LockedForEditMenuItem")
        Me.LockedForEditMenuItem.Name = "LockedForEditMenuItem"
        Me.ContextMenuToolStripSeparator1.Name = "ContextMenuToolStripSeparator1"
        resources.ApplyResources(Me.ContextMenuToolStripSeparator1, "ContextMenuToolStripSeparator1")
        resources.ApplyResources(Me.AddTestPartToolStripMenuItem, "AddTestPartToolStripMenuItem")
        Me.AddTestPartToolStripMenuItem.Name = "AddTestPartToolStripMenuItem"
        resources.ApplyResources(Me.AddSectionToolStripMenuItem, "AddSectionToolStripMenuItem")
        Me.AddSectionToolStripMenuItem.Name = "AddSectionToolStripMenuItem"
        resources.ApplyResources(Me.AddItemToolStripMenuItem, "AddItemToolStripMenuItem")
        Me.AddItemToolStripMenuItem.Name = "AddItemToolStripMenuItem"
        resources.ApplyResources(Me.AddItemsFromCodesToolStripMenuItem, "AddItemsFromCodesToolStripMenuItem")
        Me.AddItemsFromCodesToolStripMenuItem.Name = "AddItemsFromCodesToolStripMenuItem"
        Me.SetDataSourceMenuItem.Name = "SetDataSourceMenuItem"
        resources.ApplyResources(Me.SetDataSourceMenuItem, "SetDataSourceMenuItem")
        Me.ContextMenuToolStripSeparator.Name = "ContextMenuToolStripSeparator"
        resources.ApplyResources(Me.ContextMenuToolStripSeparator, "ContextMenuToolStripSeparator")
        resources.ApplyResources(Me.DeleteTestComponentToolStripMenuItem, "DeleteTestComponentToolStripMenuItem")
        Me.DeleteTestComponentToolStripMenuItem.Name = "DeleteTestComponentToolStripMenuItem"
        resources.ApplyResources(Me.MoveItemUpMenuItem, "MoveItemUpMenuItem")
        Me.MoveItemUpMenuItem.Name = "MoveItemUpMenuItem"
        resources.ApplyResources(Me.MoveItemDownMenuItem, "MoveItemDownMenuItem")
        Me.MoveItemDownMenuItem.Name = "MoveItemDownMenuItem"
        resources.ApplyResources(Me.MoveSectionUpMenuItem, "MoveSectionUpMenuItem")
        Me.MoveSectionUpMenuItem.Name = "MoveSectionUpMenuItem"
        resources.ApplyResources(Me.MoveSectionDownMenuItem, "MoveSectionDownMenuItem")
        Me.MoveSectionDownMenuItem.Name = "MoveSectionDownMenuItem"
        resources.ApplyResources(Me.MoveTestPartUpMenuItem, "MoveTestPartUpMenuItem")
        Me.MoveTestPartUpMenuItem.Name = "MoveTestPartUpMenuItem"
        resources.ApplyResources(Me.MoveTestPartDownMenuItem, "MoveTestPartDownMenuItem")
        Me.MoveTestPartDownMenuItem.Name = "MoveTestPartDownMenuItem"
        Me.RefreshItemDataSourceToolStripMenuItem.Image = Global.Questify.Builder.UI.My.Resources.Resources.reload_16x16
        Me.RefreshItemDataSourceToolStripMenuItem.Name = "RefreshItemDataSourceToolStripMenuItem"
        resources.ApplyResources(Me.RefreshItemDataSourceToolStripMenuItem, "RefreshItemDataSourceToolStripMenuItem")
        resources.ApplyResources(Me.CopyToolStripMenuItem, "CopyToolStripMenuItem")
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        resources.ApplyResources(Me.PasteToolStripMenuItem, "PasteToolStripMenuItem")
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.RefreshSectionToolStripMenuItem.Image = Global.Questify.Builder.UI.My.Resources.Resources.app
        Me.RefreshSectionToolStripMenuItem.Name = "RefreshSectionToolStripMenuItem"
        resources.ApplyResources(Me.RefreshSectionToolStripMenuItem, "RefreshSectionToolStripMenuItem")
        Me.SaveExcelFileDialog.DefaultExt = "xls"
        resources.ApplyResources(Me.SaveExcelFileDialog, "SaveExcelFileDialog")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TestHierarchyGrid)
        Me.Name = "TestHierarchyControl"
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TestHierarchyGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TestContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TestContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddTestPartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteTestComponentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveItemUpMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveItemDownMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveSectionUpMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveSectionDownMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestPartUpMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestPartDownMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveExcelFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LockedForEditMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddItemsFromCodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestHierarchyGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents RefreshSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshItemDataSourceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents SetDataSourceMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
