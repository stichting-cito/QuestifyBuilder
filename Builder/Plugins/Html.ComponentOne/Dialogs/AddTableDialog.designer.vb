Imports C1.Win.C1Editor.UICustomization

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddTableDialog
    Inherits System.Windows.Forms.Form
    Implements ITableItemDialog

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddTableDialog))
        Me.TableLayoutPanelSelectCells = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTableDimensions = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanelSelectCells, "TableLayoutPanelSelectCells")
        Me.TableLayoutPanelSelectCells.Name = "TableLayoutPanelSelectCells"
        resources.ApplyResources(Me.LabelTableDimensions, "LabelTableDimensions")
        Me.LabelTableDimensions.Name = "LabelTableDimensions"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.Controls.Add(Me.LabelTableDimensions)
        Me.Controls.Add(Me.TableLayoutPanelSelectCells)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddTableDialog"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanelSelectCells As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelTableDimensions As System.Windows.Forms.Label

End Class
