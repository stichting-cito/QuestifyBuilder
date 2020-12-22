<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeStructureGrid
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TreeStructureGrid))
        Me.ListViewData = New System.Windows.Forms.ListView()
        Me.ColumnHeaderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        Me.ListViewData.AllowColumnReorder = True
        Me.ListViewData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderName, Me.ColumnHeaderTitle})
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
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListViewData)
        Me.Name = "TreeStructureGrid"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListViewData As ListView
    Friend WithEvents ColumnHeaderName As ColumnHeader
    Friend WithEvents ColumnHeaderTitle As ColumnHeader
End Class
