Imports C1.Win.C1Editor.UICustomization

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogEditCellProperties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogEditCellProperties))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBoxWidthUnit = New System.Windows.Forms.ComboBox()
        Me.TextBoxWidth = New System.Windows.Forms.TextBox()
        Me.RadioButtonFixedWidth = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAutoAdjustWidth = New System.Windows.Forms.RadioButton()
        Me.GroupBoxHeight = New System.Windows.Forms.GroupBox()
        Me.TextBoxHeightUnit = New System.Windows.Forms.TextBox()
        Me.TextBoxHeight = New System.Windows.Forms.TextBox()
        Me.RadioButtonFixedHeight = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAutoAdjustHeight = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxAlignmentVertical = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxAlignmentHorizontal = New System.Windows.Forms.ComboBox()
        Me.ComboBoxApplyTo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxHeight.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.GroupBox1.Controls.Add(Me.ComboBoxWidthUnit)
        Me.GroupBox1.Controls.Add(Me.TextBoxWidth)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFixedWidth)
        Me.GroupBox1.Controls.Add(Me.RadioButtonAutoAdjustWidth)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ComboBoxWidthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.ComboBoxWidthUnit, "ComboBoxWidthUnit")
        Me.ComboBoxWidthUnit.FormattingEnabled = True
        Me.ComboBoxWidthUnit.Items.AddRange(New Object() {resources.GetString("ComboBoxWidthUnit.Items"), resources.GetString("ComboBoxWidthUnit.Items1")})
        Me.ComboBoxWidthUnit.Name = "ComboBoxWidthUnit"
        resources.ApplyResources(Me.TextBoxWidth, "TextBoxWidth")
        Me.TextBoxWidth.Name = "TextBoxWidth"
        resources.ApplyResources(Me.RadioButtonFixedWidth, "RadioButtonFixedWidth")
        Me.RadioButtonFixedWidth.Name = "RadioButtonFixedWidth"
        Me.RadioButtonFixedWidth.TabStop = True
        Me.RadioButtonFixedWidth.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.RadioButtonAutoAdjustWidth, "RadioButtonAutoAdjustWidth")
        Me.RadioButtonAutoAdjustWidth.Name = "RadioButtonAutoAdjustWidth"
        Me.RadioButtonAutoAdjustWidth.TabStop = True
        Me.RadioButtonAutoAdjustWidth.UseVisualStyleBackColor = True
        Me.GroupBoxHeight.Controls.Add(Me.TextBoxHeightUnit)
        Me.GroupBoxHeight.Controls.Add(Me.TextBoxHeight)
        Me.GroupBoxHeight.Controls.Add(Me.RadioButtonFixedHeight)
        Me.GroupBoxHeight.Controls.Add(Me.RadioButtonAutoAdjustHeight)
        resources.ApplyResources(Me.GroupBoxHeight, "GroupBoxHeight")
        Me.GroupBoxHeight.Name = "GroupBoxHeight"
        Me.GroupBoxHeight.TabStop = False
        Me.TextBoxHeightUnit.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        resources.ApplyResources(Me.TextBoxHeightUnit, "TextBoxHeightUnit")
        Me.TextBoxHeightUnit.Name = "TextBoxHeightUnit"
        resources.ApplyResources(Me.TextBoxHeight, "TextBoxHeight")
        Me.TextBoxHeight.Name = "TextBoxHeight"
        resources.ApplyResources(Me.RadioButtonFixedHeight, "RadioButtonFixedHeight")
        Me.RadioButtonFixedHeight.Name = "RadioButtonFixedHeight"
        Me.RadioButtonFixedHeight.TabStop = True
        Me.RadioButtonFixedHeight.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.RadioButtonAutoAdjustHeight, "RadioButtonAutoAdjustHeight")
        Me.RadioButtonAutoAdjustHeight.Name = "RadioButtonAutoAdjustHeight"
        Me.RadioButtonAutoAdjustHeight.TabStop = True
        Me.RadioButtonAutoAdjustHeight.UseVisualStyleBackColor = True
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.ComboBoxAlignmentVertical)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.ComboBoxAlignmentHorizontal)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ComboBoxAlignmentVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAlignmentVertical.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBoxAlignmentVertical, "ComboBoxAlignmentVertical")
        Me.ComboBoxAlignmentVertical.Name = "ComboBoxAlignmentVertical"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ComboBoxAlignmentHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAlignmentHorizontal.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBoxAlignmentHorizontal, "ComboBoxAlignmentHorizontal")
        Me.ComboBoxAlignmentHorizontal.Name = "ComboBoxAlignmentHorizontal"
        Me.ComboBoxApplyTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxApplyTo.FormattingEnabled = True
        Me.ComboBoxApplyTo.Items.AddRange(New Object() {resources.GetString("ComboBoxApplyTo.Items"), resources.GetString("ComboBoxApplyTo.Items1"), resources.GetString("ComboBoxApplyTo.Items2"), resources.GetString("ComboBoxApplyTo.Items3")})
        resources.ApplyResources(Me.ComboBoxApplyTo, "ComboBoxApplyTo")
        Me.ComboBoxApplyTo.Name = "ComboBoxApplyTo"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxApplyTo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBoxHeight)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogEditCellProperties"
        Me.ShowInTaskbar = False
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxHeight.ResumeLayout(False)
        Me.GroupBoxHeight.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonFixedWidth As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAutoAdjustWidth As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxWidth As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxWidthUnit As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBoxHeight As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxHeight As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonFixedHeight As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAutoAdjustHeight As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxHeightUnit As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAlignmentVertical As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAlignmentHorizontal As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxApplyTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
