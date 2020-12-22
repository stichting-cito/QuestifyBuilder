<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewUserDialog
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewUserDialog))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.FullNameLabel = New System.Windows.Forms.Label()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.FullNameTextBox = New System.Windows.Forms.TextBox()
        Me.UserNameTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.IsActiveLabel = New System.Windows.Forms.Label()
        Me.IsActiveCheckBox = New System.Windows.Forms.CheckBox()
        Me.UserErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UserEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.UserErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.UserEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.FullNameLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.UserNameLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.FullNameTextBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.UserNameTextBox, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.PasswordLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.PasswordTextBox, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.IsActiveLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.IsActiveCheckBox, 1, 2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.FullNameLabel, "FullNameLabel")
        Me.FullNameLabel.Name = "FullNameLabel"
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.Name = "UserNameLabel"
        resources.ApplyResources(Me.FullNameTextBox, "FullNameTextBox")
        Me.FullNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "FullName", True))
        Me.FullNameTextBox.Name = "FullNameTextBox"
        resources.ApplyResources(Me.UserNameTextBox, "UserNameTextBox")
        Me.UserNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "UserName", True))
        Me.UserNameTextBox.Name = "UserNameTextBox"
        resources.ApplyResources(Me.PasswordLabel, "PasswordLabel")
        Me.PasswordLabel.Name = "PasswordLabel"
        resources.ApplyResources(Me.PasswordTextBox, "PasswordTextBox")
        Me.PasswordTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.UserEntityBindingSource, "Password", True))
        Me.PasswordTextBox.Name = "PasswordTextBox"
        resources.ApplyResources(Me.IsActiveLabel, "IsActiveLabel")
        Me.IsActiveLabel.Name = "IsActiveLabel"
        resources.ApplyResources(Me.IsActiveCheckBox, "IsActiveCheckBox")
        Me.IsActiveCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.UserEntityBindingSource, "Active", True))
        Me.IsActiveCheckBox.Name = "IsActiveCheckBox"
        Me.IsActiveCheckBox.UseVisualStyleBackColor = True
        Me.UserErrorProvider.ContainerControl = Me
        Me.UserErrorProvider.DataSource = Me.UserEntityBindingSource
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.UserEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.UserEntity)
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewUserDialog"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.UserErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.UserEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FullNameLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents FullNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UserErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents IsActiveLabel As System.Windows.Forms.Label
    Friend WithEvents IsActiveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
