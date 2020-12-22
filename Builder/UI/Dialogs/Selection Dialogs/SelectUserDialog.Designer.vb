<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectUserDialog
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
        Dim AddUserButton As System.Windows.Forms.Button
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectUserDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TheGrid = New Questify.Builder.UI.UserGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NoAddButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        AddUserButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(AddUserButton, "AddUserButton")
        AddUserButton.Name = "AddUserButton"
        AddUserButton.UseVisualStyleBackColor = True
        AddHandler AddUserButton.Click, AddressOf Me.AddUserButton_Click
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TheGrid, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.SetColumnSpan(Me.TheGrid, 2)
        Me.TheGrid.DataSource = Nothing
        resources.ApplyResources(Me.TheGrid, "TheGrid")
        Me.TheGrid.Name = "TheGrid"
        Me.TheGrid.SelectedUser = Nothing
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.NoAddButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.NoAddButton, "NoAddButton")
        Me.NoAddButton.Name = "NoAddButton"
        Me.NoAddButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Panel1.Controls.Add(AddUserButton)
        Me.Panel1.Controls.Add(Me.NoAddButton)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.AcceptButton = AddUserButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.NoAddButton
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "SelectUserDialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents NoAddButton As System.Windows.Forms.Button
    Friend WithEvents TheGrid As Questify.Builder.UI.UserGrid
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
