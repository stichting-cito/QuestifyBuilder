<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordChangeDialog
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.PolicyLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.RepeatPasswordLabel = New System.Windows.Forms.Label()
        Me.RepeatPasswordTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.NewPasswordLabel = New System.Windows.Forms.Label()
        Me.IntroLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout
        Me.TableLayoutPanel2.SuspendLayout
        Me.TableLayoutPanel3.SuspendLayout
        Me.SuspendLayout
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(463, 529)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 619!))
        Me.TableLayoutPanel2.Controls.Add(Me.PolicyLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.IntroLabel, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(24, 12)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(619, 480)
        Me.TableLayoutPanel2.TabIndex = 17
        Me.PolicyLabel.AutoSize = true
        Me.PolicyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold)
        Me.PolicyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PolicyLabel.Location = New System.Drawing.Point(4, 265)
        Me.PolicyLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PolicyLabel.Name = "PolicyLabel"
        Me.PolicyLabel.Size = New System.Drawing.Size(0, 17)
        Me.PolicyLabel.TabIndex = 20
        Me.TableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5!))
        Me.TableLayoutPanel3.Controls.Add(Me.RepeatPasswordLabel, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.RepeatPasswordTextBox, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.PasswordTextBox, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.NewPasswordLabel, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 107)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(613, 115)
        Me.TableLayoutPanel3.TabIndex = 19
        Me.RepeatPasswordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RepeatPasswordLabel.AutoSize = true
        Me.RepeatPasswordLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RepeatPasswordLabel.Location = New System.Drawing.Point(4, 81)
        Me.RepeatPasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.RepeatPasswordLabel.Name = "RepeatPasswordLabel"
        Me.RepeatPasswordLabel.Size = New System.Drawing.Size(150, 17)
        Me.RepeatPasswordLabel.TabIndex = 18
        Me.RepeatPasswordLabel.Text = "Repeat New Password"
        Me.RepeatPasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.RepeatPasswordTextBox.Location = New System.Drawing.Point(309, 79)
        Me.RepeatPasswordTextBox.Name = "RepeatPasswordTextBox"
        Me.RepeatPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.RepeatPasswordTextBox.Size = New System.Drawing.Size(269, 22)
        Me.RepeatPasswordTextBox.TabIndex = 20
        Me.PasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.PasswordTextBox.Location = New System.Drawing.Point(309, 19)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(269, 22)
        Me.PasswordTextBox.TabIndex = 16
        Me.NewPasswordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.NewPasswordLabel.AutoSize = true
        Me.NewPasswordLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NewPasswordLabel.Location = New System.Drawing.Point(4, 21)
        Me.NewPasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.NewPasswordLabel.Name = "NewPasswordLabel"
        Me.NewPasswordLabel.Size = New System.Drawing.Size(99, 17)
        Me.NewPasswordLabel.TabIndex = 19
        Me.NewPasswordLabel.Text = "New password"
        Me.IntroLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.IntroLabel.AutoSize = true
        Me.IntroLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.IntroLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.IntroLabel.Location = New System.Drawing.Point(4, 24)
        Me.IntroLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.IntroLabel.Name = "IntroLabel"
        Me.IntroLabel.Size = New System.Drawing.Size(325, 17)
        Me.IntroLabel.TabIndex = 18
        Me.IntroLabel.Text = "Please change your password in order to continue"
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(674, 580)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "PasswordChangeDialog"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PasswordChange"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel2.ResumeLayout(false)
        Me.TableLayoutPanel2.PerformLayout
        Me.TableLayoutPanel3.ResumeLayout(false)
        Me.TableLayoutPanel3.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents PolicyLabel As Label
    Friend WithEvents IntroLabel As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents RepeatPasswordLabel As Label
    Friend WithEvents RepeatPasswordTextBox As TextBox
    Friend WithEvents PasswordTextBox As TextBox
    Friend WithEvents NewPasswordLabel As Label
End Class
