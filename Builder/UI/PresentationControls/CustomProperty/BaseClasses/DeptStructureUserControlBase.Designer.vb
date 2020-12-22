<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeptStructureUserControlBase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeptStructureUserControlBase))
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.ComboBoxDep = New System.Windows.Forms.ComboBox()
        Me.LabelDep = New System.Windows.Forms.Label()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.ComboBoxDep, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelDep, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.ButtonRemove, 2, 0)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.ComboBoxDep.DisplayMember = "ConceptStructureId"
        resources.ApplyResources(Me.ComboBoxDep, "ComboBoxDep")
        Me.ComboBoxDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDep.FormattingEnabled = True
        Me.ComboBoxDep.Name = "ComboBoxDep"
        Me.ComboBoxDep.ValueMember = "ConceptStructureId"
        resources.ApplyResources(Me.LabelDep, "LabelDep")
        Me.LabelDep.Name = "LabelDep"
        resources.ApplyResources(Me.ButtonRemove, "ButtonRemove")
        Me.ButtonRemove.Image = Global.Questify.Builder.UI.My.Resources.Resources.remove16
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "DeptStructureUserControlBase"
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxDep As System.Windows.Forms.ComboBox
    Friend WithEvents LabelDep As System.Windows.Forms.Label
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button

End Class
