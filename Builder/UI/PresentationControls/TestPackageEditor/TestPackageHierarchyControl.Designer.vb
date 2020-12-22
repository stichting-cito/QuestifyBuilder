<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestPackageHierarchyControl
    Inherits System.Windows.Forms.UserControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestPackageHierarchyControl))
        Dim TestPackageHierarchyGrid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim TestPackageHierarchyGrid_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TestPackageContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddTestsetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteTestsetComponentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestDownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestSetUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveTestSetDownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertTestBeforeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshSectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveExcelFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.TestPackageHierarchyGrid = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TestPackageContextMenuStrip.SuspendLayout()
        CType(Me.TestPackageHierarchyGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.GridBindingSource.DataSource = GetType(Cito.Tester.ContentModel.TestPackage)
        Me.TestPackageContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddTestsetToolStripMenuItem, Me.AddTestToolStripMenuItem, Me.ContextMenuToolStripSeparator, Me.DeleteTestsetComponentToolStripMenuItem, Me.MoveTestUpMenuItem, Me.MoveTestDownMenuItem, Me.MoveTestSetUpMenuItem, Me.MoveTestSetDownMenuItem, Me.InsertTestBeforeToolStripMenuItem})
        Me.TestPackageContextMenuStrip.Name = "TestContextMenuStrip"
        resources.ApplyResources(Me.TestPackageContextMenuStrip, "TestPackageContextMenuStrip")
        resources.ApplyResources(Me.AddTestsetToolStripMenuItem, "AddTestsetToolStripMenuItem")
        Me.AddTestsetToolStripMenuItem.Name = "AddTestsetToolStripMenuItem"
        resources.ApplyResources(Me.AddTestToolStripMenuItem, "AddTestToolStripMenuItem")
        Me.AddTestToolStripMenuItem.Name = "AddTestToolStripMenuItem"
        Me.ContextMenuToolStripSeparator.Name = "ContextMenuToolStripSeparator"
        resources.ApplyResources(Me.ContextMenuToolStripSeparator, "ContextMenuToolStripSeparator")
        resources.ApplyResources(Me.DeleteTestsetComponentToolStripMenuItem, "DeleteTestsetComponentToolStripMenuItem")
        Me.DeleteTestsetComponentToolStripMenuItem.Name = "DeleteTestsetComponentToolStripMenuItem"
        resources.ApplyResources(Me.MoveTestUpMenuItem, "MoveTestUpMenuItem")
        Me.MoveTestUpMenuItem.Name = "MoveTestUpMenuItem"
        resources.ApplyResources(Me.MoveTestDownMenuItem, "MoveTestDownMenuItem")
        Me.MoveTestDownMenuItem.Name = "MoveTestDownMenuItem"
        resources.ApplyResources(Me.MoveTestSetUpMenuItem, "MoveTestSetUpMenuItem")
        Me.MoveTestSetUpMenuItem.Name = "MoveTestSetUpMenuItem"
        resources.ApplyResources(Me.MoveTestSetDownMenuItem, "MoveTestSetDownMenuItem")
        Me.MoveTestSetDownMenuItem.Name = "MoveTestSetDownMenuItem"
        resources.ApplyResources(Me.InsertTestBeforeToolStripMenuItem, "InsertTestBeforeToolStripMenuItem")
        Me.InsertTestBeforeToolStripMenuItem.Name = "InsertTestBeforeToolStripMenuItem"
        Me.RefreshSectionToolStripMenuItem.Image = Global.Questify.Builder.UI.My.Resources.Resources.app
        Me.RefreshSectionToolStripMenuItem.Name = "RefreshSectionToolStripMenuItem"
        resources.ApplyResources(Me.RefreshSectionToolStripMenuItem, "RefreshSectionToolStripMenuItem")
        Me.SaveExcelFileDialog.DefaultExt = "xls"
        resources.ApplyResources(Me.SaveExcelFileDialog, "SaveExcelFileDialog")
        Me.TestPackageHierarchyGrid.AllowColumnDrag = False
        Me.TestPackageHierarchyGrid.AllowDrop = True
        Me.TestPackageHierarchyGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.TestPackageHierarchyGrid.AutomaticSort = False
        Me.TestPackageHierarchyGrid.ColumnAutoResize = True
        Me.TestPackageHierarchyGrid.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.TestPackageHierarchyGrid.ContextMenuStrip = Me.TestPackageContextMenuStrip
        Me.TestPackageHierarchyGrid.DataSource = Me.GridBindingSource
        resources.ApplyResources(Me.TestPackageHierarchyGrid, "TestPackageHierarchyGrid")
        Me.TestPackageHierarchyGrid.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.TestPackageHierarchyGrid.FocusCellFormatStyle.BackColor = System.Drawing.Color.Navy
        Me.TestPackageHierarchyGrid.FocusCellFormatStyle.ForeColor = System.Drawing.Color.Black
        Me.TestPackageHierarchyGrid.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.TestPackageHierarchyGrid.GroupByBoxVisible = False
        Me.TestPackageHierarchyGrid.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.TestPackageHierarchyGrid.Hierarchical = True
        TestPackageHierarchyGrid_Layout_0.Key = "MultiColumnGrid"
        resources.ApplyResources(TestPackageHierarchyGrid_Layout_0, "TestPackageHierarchyGrid_Layout_0")
        TestPackageHierarchyGrid_Layout_1.DataSource = Me.GridBindingSource
        TestPackageHierarchyGrid_Layout_1.IsCurrentLayout = True
        TestPackageHierarchyGrid_Layout_1.Key = "SingleColumnGrid"
        resources.ApplyResources(TestPackageHierarchyGrid_Layout_1, "TestPackageHierarchyGrid_Layout_1")
        Me.TestPackageHierarchyGrid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {TestPackageHierarchyGrid_Layout_0, TestPackageHierarchyGrid_Layout_1})
        Me.TestPackageHierarchyGrid.Name = "TestPackageHierarchyGrid"
        Me.TestPackageHierarchyGrid.RowWithErrorsFormatStyle.ForeColor = System.Drawing.Color.Black
        Me.TestPackageHierarchyGrid.SelectedFormatStyle.BackColor = System.Drawing.Color.Navy
        Me.TestPackageHierarchyGrid.SelectedFormatStyle.ForeColor = System.Drawing.Color.Black
        Me.TestPackageHierarchyGrid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.TestPackageHierarchyGrid.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.TestPackageHierarchyGrid.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.TestPackageHierarchyGrid.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TestPackageHierarchyGrid)
        Me.Name = "TestPackageHierarchyControl"
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TestPackageContextMenuStrip.ResumeLayout(False)
        CType(Me.TestPackageHierarchyGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TestPackageContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddTestsetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteTestsetComponentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestUpMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestDownMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestSetUpMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTestSetDownMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveExcelFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents InsertTestBeforeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshSectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestPackageHierarchyGrid As Janus.Windows.GridEX.GridEX

End Class
