<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeDataSourceCodeDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangeDataSourceCodeDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.headerLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrentNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NewNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ReferencedTestsCountTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.headerLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrentNameTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.NewNameTextBox, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ReferencedTestsCountTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCancel, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonOK, 1, 6)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.headerLabel, "headerLabel")
        Me.TableLayoutPanel1.SetColumnSpan(Me.headerLabel, 3)
        Me.headerLabel.Name = "headerLabel"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.CurrentNameTextBox, "CurrentNameTextBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.CurrentNameTextBox, 2)
        Me.CurrentNameTextBox.Name = "CurrentNameTextBox"
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        resources.ApplyResources(Me.NewNameTextBox, "NewNameTextBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.NewNameTextBox, 2)
        Me.NewNameTextBox.Name = "NewNameTextBox"
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        resources.ApplyResources(Me.ReferencedTestsCountTextBox, "ReferencedTestsCountTextBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.ReferencedTestsCountTextBox, 2)
        Me.ReferencedTestsCountTextBox.Name = "ReferencedTestsCountTextBox"
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.ButtonCancel, "ButtonCancel")
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ButtonOK, "ButtonOK")
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        Me.AcceptButton = Me.ButtonOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ChangeDataSourceCodeDialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents headerLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CurrentNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NewNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ReferencedTestsCountTextBox As System.Windows.Forms.TextBox
End Class
