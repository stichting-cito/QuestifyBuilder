<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XhtmlReferenceDialog
    Inherits DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XhtmlReferenceDialog))
        Dim ReferenceGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ReferenceGrid = New Janus.Windows.GridEX.GridEX()
        Me.XhtmlReferenceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ContentPanel.SuspendLayout()
        CType(Me.ReferenceGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XhtmlReferenceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.ContentPanel.Controls.Add(Me.ReferenceGrid)
        resources.ApplyResources(Me.ReferenceGrid, "ReferenceGrid")
        Me.ReferenceGrid.AllowColumnDrag = False
        Me.ReferenceGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.ReferenceGrid.ColumnAutoResize = True
        Me.ReferenceGrid.DataSource = Me.XhtmlReferenceBindingSource
        resources.ApplyResources(ReferenceGrid_DesignTimeLayout, "ReferenceGrid_DesignTimeLayout")
        Me.ReferenceGrid.DesignTimeLayout = ReferenceGrid_DesignTimeLayout
        Me.ReferenceGrid.ExpandableCards = False
        Me.ReferenceGrid.ExpandableGroups = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.ReferenceGrid.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.ReferenceGrid.GroupByBoxVisible = False
        Me.ReferenceGrid.Name = "ReferenceGrid"
        Me.XhtmlReferenceBindingSource.DataSource = GetType(Cito.Tester.Common.XhtmlReference)
        resources.ApplyResources(Me, "$this")
        Me.Name = "XhtmlReferenceDialog"
        Me.ContentPanel.ResumeLayout(False)
        CType(Me.ReferenceGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XhtmlReferenceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReferenceGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents XhtmlReferenceBindingSource As System.Windows.Forms.BindingSource

End Class
