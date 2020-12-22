<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewRoleDialog
    Inherits Questify.Builder.UI.DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewRoleDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.RoleEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.ContentPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NameLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Description", True))
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        Me.RoleEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.RoleEntity)
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.NameLabel.Name = "NameLabel"
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.Name = "DescriptionLabel"
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Name", True))
        Me.NameTextBox.Name = "NameTextBox"
        resources.ApplyResources(Me, "$this")
        Me.Name = "NewRoleDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RoleEntityBindingSource As System.Windows.Forms.BindingSource

End Class
