<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApplicationRoleGrid
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApplicationRoleGrid))
        Me.RoleGridView = New System.Windows.Forms.DataGridView
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RoleIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn
        CType(Me.RoleGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.RoleGridView.AllowUserToAddRows = False
        Me.RoleGridView.AllowUserToDeleteRows = False
        Me.RoleGridView.AllowUserToResizeColumns = False
        Me.RoleGridView.AllowUserToResizeRows = False
        Me.RoleGridView.AutoGenerateColumns = False
        Me.RoleGridView.BackgroundColor = System.Drawing.SystemColors.Window
        Me.RoleGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.RoleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RoleGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RoleIdDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.IsInRoleNewValueDataGridViewCheckBoxColumn})
        Me.RoleGridView.DataSource = Me.GridBindingSource
        resources.ApplyResources(Me.RoleGridView, "RoleGridView")
        Me.RoleGridView.MultiSelect = False
        Me.RoleGridView.Name = "RoleGridView"
        Me.RoleGridView.RowHeadersVisible = False
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.RoleGridView.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.RoleGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridBindingSource.DataSource = GetType(Questify.Builder.UI.RoleGridRowEntity)
        Me.RoleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId"
        resources.ApplyResources(Me.RoleIdDataGridViewTextBoxColumn, "RoleIdDataGridViewTextBoxColumn")
        Me.RoleIdDataGridViewTextBoxColumn.Name = "RoleIdDataGridViewTextBoxColumn"
        Me.RoleIdDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        resources.ApplyResources(Me.NameDataGridViewTextBoxColumn, "NameDataGridViewTextBoxColumn")
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.DataPropertyName = "IsInRoleNewValue"
        resources.ApplyResources(Me.IsInRoleNewValueDataGridViewCheckBoxColumn, "IsInRoleNewValueDataGridViewCheckBoxColumn")
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.Name = "IsInRoleNewValueDataGridViewCheckBoxColumn"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RoleGridView)
        Me.Name = "ApplicationRoleGrid"
        CType(Me.RoleGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RoleGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoleIdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsInRoleNewValueDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn

End Class
