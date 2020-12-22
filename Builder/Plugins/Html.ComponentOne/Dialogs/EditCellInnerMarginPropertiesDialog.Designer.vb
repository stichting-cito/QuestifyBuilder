Imports C1.Win.C1Editor.UICustomization

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditCellInnerMarginPropertiesDialog
    Inherits System.Windows.Forms.Form
    Implements ICellItemDialog

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditCellInnerMarginPropertiesDialog))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxApplyTo = New System.Windows.Forms.ComboBox()
        Me.MaskedTextBoxMarginTop = New System.Windows.Forms.MaskedTextBox()
        Me.LabelMarginTop = New System.Windows.Forms.Label()
        Me.LabelMarginBottom = New System.Windows.Forms.Label()
        Me.LabelMarginLeft = New System.Windows.Forms.Label()
        Me.LabelMarginRight = New System.Windows.Forms.Label()
        Me.MaskedTextBoxMarginBottom = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBoxMarginLeft = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBoxMarginRight = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ComboBoxApplyTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxApplyTo.FormattingEnabled = True
        Me.ComboBoxApplyTo.Items.AddRange(New Object() {resources.GetString("ComboBoxApplyTo.Items"), resources.GetString("ComboBoxApplyTo.Items1"), resources.GetString("ComboBoxApplyTo.Items2"), resources.GetString("ComboBoxApplyTo.Items3")})
        resources.ApplyResources(Me.ComboBoxApplyTo, "ComboBoxApplyTo")
        Me.ComboBoxApplyTo.Name = "ComboBoxApplyTo"
        resources.ApplyResources(Me.MaskedTextBoxMarginTop, "MaskedTextBoxMarginTop")
        Me.MaskedTextBoxMarginTop.Name = "MaskedTextBoxMarginTop"
        resources.ApplyResources(Me.LabelMarginTop, "LabelMarginTop")
        Me.LabelMarginTop.Name = "LabelMarginTop"
        resources.ApplyResources(Me.LabelMarginBottom, "LabelMarginBottom")
        Me.LabelMarginBottom.Name = "LabelMarginBottom"
        resources.ApplyResources(Me.LabelMarginLeft, "LabelMarginLeft")
        Me.LabelMarginLeft.Name = "LabelMarginLeft"
        resources.ApplyResources(Me.LabelMarginRight, "LabelMarginRight")
        Me.LabelMarginRight.Name = "LabelMarginRight"
        resources.ApplyResources(Me.MaskedTextBoxMarginBottom, "MaskedTextBoxMarginBottom")
        Me.MaskedTextBoxMarginBottom.Name = "MaskedTextBoxMarginBottom"
        resources.ApplyResources(Me.MaskedTextBoxMarginLeft, "MaskedTextBoxMarginLeft")
        Me.MaskedTextBoxMarginLeft.Name = "MaskedTextBoxMarginLeft"
        resources.ApplyResources(Me.MaskedTextBoxMarginRight, "MaskedTextBoxMarginRight")
        Me.MaskedTextBoxMarginRight.Name = "MaskedTextBoxMarginRight"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MaskedTextBoxMarginRight)
        Me.Controls.Add(Me.MaskedTextBoxMarginLeft)
        Me.Controls.Add(Me.MaskedTextBoxMarginBottom)
        Me.Controls.Add(Me.LabelMarginRight)
        Me.Controls.Add(Me.LabelMarginLeft)
        Me.Controls.Add(Me.LabelMarginBottom)
        Me.Controls.Add(Me.LabelMarginTop)
        Me.Controls.Add(Me.MaskedTextBoxMarginTop)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxApplyTo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditCellInnerMarginPropertiesDialog"
        Me.ShowInTaskbar = False
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxApplyTo As System.Windows.Forms.ComboBox
    Friend WithEvents MaskedTextBoxMarginTop As System.Windows.Forms.MaskedTextBox
    Friend WithEvents LabelMarginTop As System.Windows.Forms.Label
    Friend WithEvents LabelMarginBottom As System.Windows.Forms.Label
    Friend WithEvents LabelMarginLeft As System.Windows.Forms.Label
    Friend WithEvents LabelMarginRight As System.Windows.Forms.Label
    Friend WithEvents MaskedTextBoxMarginBottom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBoxMarginLeft As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBoxMarginRight As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
