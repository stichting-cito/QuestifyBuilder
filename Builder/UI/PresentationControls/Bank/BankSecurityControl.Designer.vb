<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankSecurityControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankSecurityControl))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UsersGridView = New System.Windows.Forms.DataGridView()
        Me.IconColumn = New System.Windows.Forms.DataGridViewImageColumn()
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FullNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UsersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RemoveUserButton = New System.Windows.Forms.Button()
        Me.AddUserButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RolesList = New Questify.Builder.UI.BankRoleGrid()
        Me.IconsList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.UsersGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UsersGridView, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.UsersGridView.AllowUserToAddRows = False
        Me.UsersGridView.AllowUserToDeleteRows = False
        Me.UsersGridView.AllowUserToResizeColumns = False
        Me.UsersGridView.AllowUserToResizeRows = False
        Me.UsersGridView.AutoGenerateColumns = False
        Me.UsersGridView.BackgroundColor = System.Drawing.SystemColors.Window
        Me.UsersGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.UsersGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.UsersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.UsersGridView.ColumnHeadersVisible = False
        Me.UsersGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IconColumn, Me.IdDataGridViewTextBoxColumn, Me.FullNameDataGridViewTextBoxColumn})
        Me.UsersGridView.DataSource = Me.UsersBindingSource
        resources.ApplyResources(Me.UsersGridView, "UsersGridView")
        Me.UsersGridView.MultiSelect = False
        Me.UsersGridView.Name = "UsersGridView"
        Me.UsersGridView.ReadOnly = True
        Me.UsersGridView.RowHeadersVisible = False
        Me.UsersGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        resources.ApplyResources(Me.IconColumn, "IconColumn")
        Me.IconColumn.Image = CType(resources.GetObject("IconColumn.Image"), System.Drawing.Image)
        Me.IconColumn.Name = "IconColumn"
        Me.IconColumn.ReadOnly = True
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "Id"
        resources.ApplyResources(Me.IdDataGridViewTextBoxColumn, "IdDataGridViewTextBoxColumn")
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        Me.IdDataGridViewTextBoxColumn.ReadOnly = True
        Me.FullNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.FullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName"
        resources.ApplyResources(Me.FullNameDataGridViewTextBoxColumn, "FullNameDataGridViewTextBoxColumn")
        Me.FullNameDataGridViewTextBoxColumn.Name = "FullNameDataGridViewTextBoxColumn"
        Me.FullNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.UsersBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.UserEntity)
        resources.ApplyResources(Me.RemoveUserButton, "RemoveUserButton")
        Me.RemoveUserButton.Name = "RemoveUserButton"
        Me.RemoveUserButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AddUserButton, "AddUserButton")
        Me.AddUserButton.Name = "AddUserButton"
        Me.AddUserButton.UseVisualStyleBackColor = True
        Me.GroupBox1.Controls.Add(Me.RolesList)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.RolesList.BankId = 0
        resources.ApplyResources(Me.RolesList, "RolesList")
        Me.RolesList.Name = "RolesList"
        Me.IconsList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
        resources.ApplyResources(Me.IconsList, "IconsList")
        Me.IconsList.TransparentColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.RemoveUserButton)
        Me.Panel1.Controls.Add(Me.AddUserButton)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "BankSecurityControl"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.UsersGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents IconsList As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UsersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UsersGridView As System.Windows.Forms.DataGridView
    Friend WithEvents RolesList As Questify.Builder.UI.BankRoleGrid
    Friend WithEvents IconColumn As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemoveUserButton As System.Windows.Forms.Button
    Friend WithEvents AddUserButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
