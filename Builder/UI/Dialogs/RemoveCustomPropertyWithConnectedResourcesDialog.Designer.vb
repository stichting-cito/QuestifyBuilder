<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RemoveCustomPropertyWithConnectedResourcesDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RemoveCustomPropertyWithConnectedResourcesDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.DummyColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.YesButton = New System.Windows.Forms.Button()
        Me.NoButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ListView1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.YesButton, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.NoButton, 1, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.ListView1, "ListView1")
        Me.ListView1.AutoArrange = False
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.DummyColumnHeader})
        Me.TableLayoutPanel1.SetColumnSpan(Me.ListView1, 2)
        Me.ListView1.GridLines = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowGroups = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        resources.ApplyResources(Me.DummyColumnHeader, "DummyColumnHeader")
        resources.ApplyResources(Me.Label1, "Label1")
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Name = "Label1"
        Me.YesButton.DialogResult = System.Windows.Forms.DialogResult.Yes
        resources.ApplyResources(Me.YesButton, "YesButton")
        Me.YesButton.Name = "YesButton"
        Me.YesButton.UseVisualStyleBackColor = True
        Me.NoButton.DialogResult = System.Windows.Forms.DialogResult.No
        resources.ApplyResources(Me.NoButton, "NoButton")
        Me.NoButton.Name = "NoButton"
        Me.NoButton.UseVisualStyleBackColor = True
        Me.AcceptButton = Me.YesButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.NoButton
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RemoveCustomPropertyWithConnectedResourcesDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents YesButton As System.Windows.Forms.Button
    Friend WithEvents NoButton As System.Windows.Forms.Button
    Friend WithEvents DummyColumnHeader As System.Windows.Forms.ColumnHeader
End Class
