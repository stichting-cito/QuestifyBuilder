<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserBankRoleViewer
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
        Me.components = New System.ComponentModel.Container
        Dim UserBankRoleGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserBankRoleViewer))
        Me.UserBankRoleGrid = New Janus.Windows.GridEX.GridEX
        Me.BankRoleGridRowEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.UserBankRoleGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BankRoleGridRowEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.UserBankRoleGrid.ColumnAutoResize = True
        Me.UserBankRoleGrid.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.UserBankRoleGrid.DataSource = Me.BankRoleGridRowEntityBindingSource
        resources.ApplyResources(UserBankRoleGrid_DesignTimeLayout, "UserBankRoleGrid_DesignTimeLayout")
        Me.UserBankRoleGrid.DesignTimeLayout = UserBankRoleGrid_DesignTimeLayout
        resources.ApplyResources(Me.UserBankRoleGrid, "UserBankRoleGrid")
        Me.UserBankRoleGrid.ExpandableCards = False
        Me.UserBankRoleGrid.ExpandableGroups = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.UserBankRoleGrid.GridLines = Janus.Windows.GridEX.GridLines.None
        Me.UserBankRoleGrid.GroupByBoxVisible = False
        Me.UserBankRoleGrid.Hierarchical = True
        Me.UserBankRoleGrid.Name = "UserBankRoleGrid"
        Me.UserBankRoleGrid.SelectOnExpand = False
        Me.UserBankRoleGrid.TreeLineColor = System.Drawing.SystemColors.ControlText
        Me.BankRoleGridRowEntityBindingSource.DataSource = GetType(Questify.Builder.UI.BankRoleGridRowEntity)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UserBankRoleGrid)
        Me.Name = "UserBankRoleViewer"
        CType(Me.UserBankRoleGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BankRoleGridRowEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UserBankRoleGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents BankRoleGridRowEntityBindingSource As System.Windows.Forms.BindingSource

End Class
