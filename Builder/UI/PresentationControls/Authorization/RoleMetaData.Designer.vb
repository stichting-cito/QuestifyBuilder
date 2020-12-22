<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoleMetaData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RoleMetaData))
        Me.UserDetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.ModificationDateLabel = New System.Windows.Forms.Label
        Me.RoleEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ModifiedByLabel = New System.Windows.Forms.Label
        Me.CreationDateLabel = New System.Windows.Forms.Label
        Me.CreatedByLabel = New System.Windows.Forms.Label
        Me.DescriptionLabel1 = New System.Windows.Forms.Label
        Me.CreatedByLabel1 = New System.Windows.Forms.Label
        Me.CreationDateLabel1 = New System.Windows.Forms.Label
        Me.ModifiedByLabel1 = New System.Windows.Forms.Label
        Me.ModificationDateLabel1 = New System.Windows.Forms.Label
        Me.NameLabel1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.NameLabel = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox
        Me.DescriptionLabel = New System.Windows.Forms.Label
        Me.UserDetailTableLayoutPanel.SuspendLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.UserDetailTableLayoutPanel, "UserDetailTableLayoutPanel")
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModificationDateLabel, 1, 5)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModifiedByLabel, 1, 4)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreationDateLabel, 1, 3)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreatedByLabel, 1, 2)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.DescriptionLabel1, 0, 1)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreatedByLabel1, 0, 2)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreationDateLabel1, 0, 3)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModifiedByLabel1, 0, 4)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModificationDateLabel1, 0, 5)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.NameLabel1, 0, 0)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.Panel1, 1, 0)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.Panel2, 1, 1)
        Me.UserDetailTableLayoutPanel.Name = "UserDetailTableLayoutPanel"
        resources.ApplyResources(Me.ModificationDateLabel, "ModificationDateLabel")
        Me.ModificationDateLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "ModifiedDate", True))
        Me.ModificationDateLabel.Name = "ModificationDateLabel"
        Me.RoleEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.RoleEntity)
        resources.ApplyResources(Me.ModifiedByLabel, "ModifiedByLabel")
        Me.ModifiedByLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "ModifiedByFullName", True))
        Me.ModifiedByLabel.Name = "ModifiedByLabel"
        resources.ApplyResources(Me.CreationDateLabel, "CreationDateLabel")
        Me.CreationDateLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "CreationDate", True))
        Me.CreationDateLabel.Name = "CreationDateLabel"
        resources.ApplyResources(Me.CreatedByLabel, "CreatedByLabel")
        Me.CreatedByLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "CreatedByFullName", True))
        Me.CreatedByLabel.Name = "CreatedByLabel"
        resources.ApplyResources(Me.DescriptionLabel1, "DescriptionLabel1")
        Me.DescriptionLabel1.Name = "DescriptionLabel1"
        resources.ApplyResources(Me.CreatedByLabel1, "CreatedByLabel1")
        Me.CreatedByLabel1.Name = "CreatedByLabel1"
        resources.ApplyResources(Me.CreationDateLabel1, "CreationDateLabel1")
        Me.CreationDateLabel1.Name = "CreationDateLabel1"
        resources.ApplyResources(Me.ModifiedByLabel1, "ModifiedByLabel1")
        Me.ModifiedByLabel1.Name = "ModifiedByLabel1"
        resources.ApplyResources(Me.ModificationDateLabel1, "ModificationDateLabel1")
        Me.ModificationDateLabel1.Name = "ModificationDateLabel1"
        resources.ApplyResources(Me.NameLabel1, "NameLabel1")
        Me.NameLabel1.Name = "NameLabel1"
        Me.Panel1.Controls.Add(Me.NameTextBox)
        Me.Panel1.Controls.Add(Me.NameLabel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Name", True))
        Me.NameTextBox.Name = "NameTextBox"
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.NameLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Name", True))
        Me.NameLabel.Name = "NameLabel"
        Me.Panel2.Controls.Add(Me.DescriptionTextBox)
        Me.Panel2.Controls.Add(Me.DescriptionLabel)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Description", True))
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.RoleEntityBindingSource, "Description", True))
        Me.DescriptionLabel.Name = "DescriptionLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UserDetailTableLayoutPanel)
        Me.Name = "RoleMetaData"
        Me.UserDetailTableLayoutPanel.ResumeLayout(False)
        Me.UserDetailTableLayoutPanel.PerformLayout()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UserDetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ModificationDateLabel As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel As System.Windows.Forms.Label
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents NameLabel1 As System.Windows.Forms.Label
    Friend WithEvents DescriptionLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModificationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents RoleEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox

End Class
