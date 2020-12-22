Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueryEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QueryEditor))
        Me.QuerySplitContainer = New System.Windows.Forms.SplitContainer()
        Me.FilterContainerTreeView = New System.Windows.Forms.TreeView()
        Me.FilterContainerTreeContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FiltersContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AndToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AndToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItemInTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QueryEditorCommandManager = New CommandManager(Me.components)
        Me.QuerySplitContainer.Panel1.SuspendLayout()
        Me.QuerySplitContainer.SuspendLayout()
        Me.FilterContainerTreeContextMenuStrip.SuspendLayout()
        Me.FiltersContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.QuerySplitContainer, "QuerySplitContainer")
        Me.QuerySplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.QuerySplitContainer.Name = "QuerySplitContainer"
        resources.ApplyResources(Me.QuerySplitContainer.Panel1, "QuerySplitContainer.Panel1")
        Me.QuerySplitContainer.Panel1.Controls.Add(Me.FilterContainerTreeView)
        resources.ApplyResources(Me.QuerySplitContainer.Panel2, "QuerySplitContainer.Panel2")
        resources.ApplyResources(Me.FilterContainerTreeView, "FilterContainerTreeView")
        Me.FilterContainerTreeView.FullRowSelect = True
        Me.FilterContainerTreeView.HideSelection = False
        Me.FilterContainerTreeView.Name = "FilterContainerTreeView"
        Me.FilterContainerTreeView.ShowPlusMinus = False
        resources.ApplyResources(Me.FilterContainerTreeContextMenuStrip, "FilterContainerTreeContextMenuStrip")
        Me.FilterContainerTreeContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetToolStripMenuItem, Me.RemoveToolStripMenuItem})
        Me.FilterContainerTreeContextMenuStrip.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.SetToolStripMenuItem, "SetToolStripMenuItem")
        Me.SetToolStripMenuItem.DropDown = Me.FiltersContextMenuStrip
        Me.SetToolStripMenuItem.Name = "SetToolStripMenuItem"
        resources.ApplyResources(Me.FiltersContextMenuStrip, "FiltersContextMenuStrip")
        Me.FiltersContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AndToolStripMenuItem, Me.PropertyToolStripMenuItem, Me.ItemInTestToolStripMenuItem})
        Me.FiltersContextMenuStrip.Name = "FiltersContextMenuStrip"
        resources.ApplyResources(Me.AndToolStripMenuItem, "AndToolStripMenuItem")
        Me.AndToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AndToolStripMenuItem1, Me.OrToolStripMenuItem1, Me.NotToolStripMenuItem1})
        Me.AndToolStripMenuItem.Name = "AndToolStripMenuItem"
        resources.ApplyResources(Me.AndToolStripMenuItem1, "AndToolStripMenuItem1")
        Me.AndToolStripMenuItem1.Name = "AndToolStripMenuItem1"
        resources.ApplyResources(Me.OrToolStripMenuItem1, "OrToolStripMenuItem1")
        Me.OrToolStripMenuItem1.Name = "OrToolStripMenuItem1"
        resources.ApplyResources(Me.NotToolStripMenuItem1, "NotToolStripMenuItem1")
        Me.NotToolStripMenuItem1.Name = "NotToolStripMenuItem1"
        resources.ApplyResources(Me.PropertyToolStripMenuItem, "PropertyToolStripMenuItem")
        Me.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem"
        resources.ApplyResources(Me.ItemInTestToolStripMenuItem, "ItemInTestToolStripMenuItem")
        Me.ItemInTestToolStripMenuItem.Name = "ItemInTestToolStripMenuItem"
        resources.ApplyResources(Me.RemoveToolStripMenuItem, "RemoveToolStripMenuItem")
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.QuerySplitContainer)
        Me.Name = "QueryEditor"
        Me.QuerySplitContainer.Panel1.ResumeLayout(False)
        Me.QuerySplitContainer.ResumeLayout(False)
        Me.FilterContainerTreeContextMenuStrip.ResumeLayout(False)
        Me.FiltersContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QuerySplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents FilterContainerTreeView As System.Windows.Forms.TreeView
    Friend WithEvents FilterContainerTreeContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FiltersContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AndToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AndToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemInTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueryEditorCommandManager As Commanding.CommandManager

End Class
