<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTestPreviewDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTestPreviewDialog))
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SelectTestPanel = New System.Windows.Forms.Panel()
        Me.SelectTestComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectPreviewMethodPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.SelectTestPreviewLabel = New System.Windows.Forms.Label()
        Me.SelectTestPreviewComboBox = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.SelectTestPanel.SuspendLayout()
        Me.SelectPreviewMethodPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.ButtonCancel, "ButtonCancel")
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.ButtonOK, "ButtonOK")
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        Me.Panel1.Controls.Add(Me.ButtonOK)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.SelectTestPanel, "SelectTestPanel")
        Me.SelectTestPanel.Controls.Add(Me.SelectTestComboBox)
        Me.SelectTestPanel.Controls.Add(Me.Label1)
        Me.SelectTestPanel.Name = "SelectTestPanel"
        resources.ApplyResources(Me.SelectTestComboBox, "SelectTestComboBox")
        Me.SelectTestComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectTestComboBox.FormattingEnabled = True
        Me.SelectTestComboBox.Name = "SelectTestComboBox"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.SelectPreviewMethodPanel.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.SelectPreviewMethodPanel, "SelectPreviewMethodPanel")
        Me.SelectPreviewMethodPanel.Name = "SelectPreviewMethodPanel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.SelectTestPreviewLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectTestPreviewComboBox, 1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.SelectTestPreviewLabel, "SelectTestPreviewLabel")
        Me.SelectTestPreviewLabel.Name = "SelectTestPreviewLabel"
        resources.ApplyResources(Me.SelectTestPreviewComboBox, "SelectTestPreviewComboBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.SelectTestPreviewComboBox, 2)
        Me.SelectTestPreviewComboBox.DisplayMember = "UserFriendlyName"
        Me.SelectTestPreviewComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectTestPreviewComboBox.FormattingEnabled = True
        Me.SelectTestPreviewComboBox.Name = "SelectTestPreviewComboBox"
        Me.SelectTestPreviewComboBox.ValueMember = "UserFriendlyName"
        Me.AcceptButton = Me.ButtonOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ControlBox = False
        Me.Controls.Add(Me.SelectPreviewMethodPanel)
        Me.Controls.Add(Me.SelectTestPanel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "SelectTestPreviewDialog"
        Me.Panel1.ResumeLayout(False)
        Me.SelectTestPanel.ResumeLayout(False)
        Me.SelectTestPanel.PerformLayout()
        Me.SelectPreviewMethodPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SelectTestPanel As System.Windows.Forms.Panel
    Friend WithEvents SelectTestComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectPreviewMethodPanel As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SelectTestPreviewLabel As System.Windows.Forms.Label
    Friend WithEvents SelectTestPreviewComboBox As System.Windows.Forms.ComboBox
End Class
