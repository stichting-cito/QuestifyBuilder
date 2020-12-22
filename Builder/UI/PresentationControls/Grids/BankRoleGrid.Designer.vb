<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankRoleGrid
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankRoleGrid))
        Me.RoleGridView = New System.Windows.Forms.DataGridView
        Me.GridBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RoleIdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NameTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.RoleGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RoleIdDataGridViewTextBoxColumn, Me.NameTextBoxColumn, Me.IsInRoleNewValueDataGridViewCheckBoxColumn})
        Me.RoleGridView.DataSource = Me.GridBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RoleGridView.DefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.RoleGridView, "RoleGridView")
        Me.RoleGridView.MultiSelect = False
        Me.RoleGridView.Name = "RoleGridView"
        Me.RoleGridView.RowHeadersVisible = False
        Me.RoleGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridBindingSource.DataSource = GetType(Questify.Builder.UI.RoleGridRowEntity)
        Me.RoleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId"
        resources.ApplyResources(Me.RoleIdDataGridViewTextBoxColumn, "RoleIdDataGridViewTextBoxColumn")
        Me.RoleIdDataGridViewTextBoxColumn.Name = "RoleIdDataGridViewTextBoxColumn"
        Me.RoleIdDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NameTextBoxColumn.DataPropertyName = "Name"
        resources.ApplyResources(Me.NameTextBoxColumn, "NameTextBoxColumn")
        Me.NameTextBoxColumn.Name = "NameTextBoxColumn"
        Me.NameTextBoxColumn.ReadOnly = True
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.DataPropertyName = "IsInRoleNewValue"
        resources.ApplyResources(Me.IsInRoleNewValueDataGridViewCheckBoxColumn, "IsInRoleNewValueDataGridViewCheckBoxColumn")
        Me.IsInRoleNewValueDataGridViewCheckBoxColumn.Name = "IsInRoleNewValueDataGridViewCheckBoxColumn"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RoleGridView)
        Me.Name = "BankRoleGrid"
        CType(Me.RoleGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RoleGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GridBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoleIdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsInRoleNewValueDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn

End Class
