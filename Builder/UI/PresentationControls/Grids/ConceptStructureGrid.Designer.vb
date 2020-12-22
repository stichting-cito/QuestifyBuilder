<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConceptStructureGrid
    Inherits System.Windows.Forms.UserControl

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConceptStructureGrid))
        Me.ListViewData = New System.Windows.Forms.ListView()
        Me.ColumnHeaderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderConceptType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        Me.ListViewData.AllowColumnReorder = True
        Me.ListViewData.AllowDrop = True
        Me.ListViewData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderName, Me.ColumnHeaderTitle, Me.ColumnHeaderConceptType})
        resources.ApplyResources(Me.ListViewData, "ListViewData")
        Me.ListViewData.FullRowSelect = True
        Me.ListViewData.GridLines = True
        Me.ListViewData.MultiSelect = False
        Me.ListViewData.Name = "ListViewData"
        Me.ListViewData.ShowItemToolTips = True
        Me.ListViewData.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListViewData.UseCompatibleStateImageBehavior = False
        Me.ListViewData.View = System.Windows.Forms.View.Details
        resources.ApplyResources(Me.ColumnHeaderName, "ColumnHeaderName")
        resources.ApplyResources(Me.ColumnHeaderTitle, "ColumnHeaderTitle")
        resources.ApplyResources(Me.ColumnHeaderConceptType, "ColumnHeaderConceptType")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListViewData)
        Me.Name = "ConceptStructureGrid"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListViewData As ListView
    Friend WithEvents ColumnHeaderName As ColumnHeader
    Friend WithEvents ColumnHeaderTitle As ColumnHeader
    Friend WithEvents ColumnHeaderConceptType As ColumnHeader
End Class
