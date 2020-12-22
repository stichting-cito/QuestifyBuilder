<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserMetaData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserMetaData))
        Me.UserDetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.FullNameLabel1 = New System.Windows.Forms.Label()
        Me.IsActiveLabel1 = New System.Windows.Forms.Label()
        Me.IsActiveCheckBox = New System.Windows.Forms.CheckBox()
        Me.UserEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UserNameLabel1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UserNameTextBox = New System.Windows.Forms.TextBox()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FullNameTextBox = New System.Windows.Forms.TextBox()
        Me.FullNameLabel = New System.Windows.Forms.Label()
        Me.CreatedByLabel1 = New System.Windows.Forms.Label()
        Me.CreationDateLabel1 = New System.Windows.Forms.Label()
        Me.ModificationDateLabel1 = New System.Windows.Forms.Label()
        Me.ModifiedByLabel1 = New System.Windows.Forms.Label()
        Me.CreatedByLabel = New System.Windows.Forms.Label()
        Me.CreationDateLabel = New System.Windows.Forms.Label()
        Me.ModificationDateLabel = New System.Windows.Forms.Label()
        Me.ModifiedByLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.AuthenticationTypeLabel1 = New System.Windows.Forms.Label()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.AuthenticationTypeLabel = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.UserDetailTableLayoutPanel.SuspendLayout
        CType(Me.UserEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel1.SuspendLayout
        Me.Panel2.SuspendLayout
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        resources.ApplyResources(Me.UserDetailTableLayoutPanel, "UserDetailTableLayoutPanel")
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.FullNameLabel1, 0, 1)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.IsActiveLabel1, 0, 2)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.IsActiveCheckBox, 1, 2)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.UserNameLabel1, 0, 0)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.Panel1, 1, 0)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.Panel2, 1, 1)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreatedByLabel1, 0, 5)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreationDateLabel1, 0, 6)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModificationDateLabel1, 0, 8)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModifiedByLabel1, 0, 7)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreatedByLabel, 1, 5)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.CreationDateLabel, 1, 6)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModificationDateLabel, 1, 8)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.ModifiedByLabel, 1, 7)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.PasswordLabel, 0, 4)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.AuthenticationTypeLabel1, 0, 3)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.PasswordTextBox, 1, 4)
        Me.UserDetailTableLayoutPanel.Controls.Add(Me.AuthenticationTypeLabel, 1, 3)
        Me.UserDetailTableLayoutPanel.Name = "UserDetailTableLayoutPanel"
        resources.ApplyResources(Me.FullNameLabel1, "FullNameLabel1")
        Me.FullNameLabel1.Name = "FullNameLabel1"
        resources.ApplyResources(Me.IsActiveLabel1, "IsActiveLabel1")
        Me.IsActiveLabel1.Name = "IsActiveLabel1"
        resources.ApplyResources(Me.IsActiveCheckBox, "IsActiveCheckBox")
        Me.IsActiveCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.UserEntityBindingSource, "Active", true))
        Me.IsActiveCheckBox.Name = "IsActiveCheckBox"
        Me.IsActiveCheckBox.UseVisualStyleBackColor = true
        Me.UserEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.UserEntity)
        resources.ApplyResources(Me.UserNameLabel1, "UserNameLabel1")
        Me.UserNameLabel1.Name = "UserNameLabel1"
        Me.Panel1.Controls.Add(Me.UserNameTextBox)
        Me.Panel1.Controls.Add(Me.UserNameLabel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.UserNameTextBox, "UserNameTextBox")
        Me.UserNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "UserName", true))
        Me.UserNameTextBox.Name = "UserNameTextBox"
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "UserName", true))
        Me.UserNameLabel.Name = "UserNameLabel"
        Me.Panel2.Controls.Add(Me.FullNameTextBox)
        Me.Panel2.Controls.Add(Me.FullNameLabel)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        resources.ApplyResources(Me.FullNameTextBox, "FullNameTextBox")
        Me.FullNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "FullName", true))
        Me.FullNameTextBox.Name = "FullNameTextBox"
        resources.ApplyResources(Me.FullNameLabel, "FullNameLabel")
        Me.FullNameLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "FullName", true))
        Me.FullNameLabel.Name = "FullNameLabel"
        resources.ApplyResources(Me.CreatedByLabel1, "CreatedByLabel1")
        Me.CreatedByLabel1.Name = "CreatedByLabel1"
        resources.ApplyResources(Me.CreationDateLabel1, "CreationDateLabel1")
        Me.CreationDateLabel1.Name = "CreationDateLabel1"
        resources.ApplyResources(Me.ModificationDateLabel1, "ModificationDateLabel1")
        Me.ModificationDateLabel1.Name = "ModificationDateLabel1"
        resources.ApplyResources(Me.ModifiedByLabel1, "ModifiedByLabel1")
        Me.ModifiedByLabel1.Name = "ModifiedByLabel1"
        resources.ApplyResources(Me.CreatedByLabel, "CreatedByLabel")
        Me.CreatedByLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "CreatedByFullName", true))
        Me.CreatedByLabel.Name = "CreatedByLabel"
        resources.ApplyResources(Me.CreationDateLabel, "CreationDateLabel")
        Me.CreationDateLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "CreationDate", true))
        Me.CreationDateLabel.Name = "CreationDateLabel"
        resources.ApplyResources(Me.ModificationDateLabel, "ModificationDateLabel")
        Me.ModificationDateLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "ModifiedDate", true))
        Me.ModificationDateLabel.Name = "ModificationDateLabel"
        resources.ApplyResources(Me.ModifiedByLabel, "ModifiedByLabel")
        Me.ModifiedByLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "ModifiedByFullName", true))
        Me.ModifiedByLabel.Name = "ModifiedByLabel"
        resources.ApplyResources(Me.PasswordLabel, "PasswordLabel")
        Me.PasswordLabel.Name = "PasswordLabel"
        resources.ApplyResources(Me.AuthenticationTypeLabel1, "AuthenticationTypeLabel1")
        Me.AuthenticationTypeLabel1.Name = "AuthenticationTypeLabel1"
        resources.ApplyResources(Me.PasswordTextBox, "PasswordTextBox")
        Me.PasswordTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "Password", true))
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.UseSystemPasswordChar = true
        resources.ApplyResources(Me.AuthenticationTypeLabel, "AuthenticationTypeLabel")
        Me.AuthenticationTypeLabel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "AuthenticationType", true))
        Me.AuthenticationTypeLabel.Name = "AuthenticationTypeLabel"
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.DataSource = Me.UserEntityBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UserDetailTableLayoutPanel)
        Me.Name = "UserMetaData"
        Me.UserDetailTableLayoutPanel.ResumeLayout(false)
        Me.UserDetailTableLayoutPanel.PerformLayout
        CType(Me.UserEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.Panel2.ResumeLayout(false)
        Me.Panel2.PerformLayout
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents UserDetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ModificationDateLabel As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel As System.Windows.Forms.Label
    Friend WithEvents FullNameLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel1 As System.Windows.Forms.Label
    Friend WithEvents FullNameLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModificationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents IsActiveLabel1 As System.Windows.Forms.Label
    Friend WithEvents IsActiveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents UserEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents FullNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AuthenticationTypeLabel1 As System.Windows.Forms.Label
    Friend WithEvents AuthenticationTypeLabel As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider

End Class
