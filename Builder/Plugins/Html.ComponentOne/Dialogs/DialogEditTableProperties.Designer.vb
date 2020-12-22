Imports C1.Win.C1Editor.UICustomization

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogEditTableProperties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogEditTableProperties))
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.ComboBoxAlignment = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout
        Me.GroupBoxHeight.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.ComboBoxAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAlignment.FormattingEnabled = true
        resources.ApplyResources(Me.ComboBoxAlignment, "ComboBoxAlignment")
        Me.ComboBoxAlignment.Name = "ComboBoxAlignment"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.GroupBox1.Controls.Add(Me.ComboBoxWidthUnit)
        Me.GroupBox1.Controls.Add(Me.TextBoxWidth)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFixedWidth)
        Me.GroupBox1.Controls.Add(Me.RadioButtonAutoAdjustWidth)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = false
        Me.ComboBoxWidthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.ComboBoxWidthUnit, "ComboBoxWidthUnit")
        Me.ComboBoxWidthUnit.FormattingEnabled = true
        Me.ComboBoxWidthUnit.Items.AddRange(New Object() {resources.GetString("ComboBoxWidthUnit.Items"), resources.GetString("ComboBoxWidthUnit.Items1")})
        Me.ComboBoxWidthUnit.Name = "ComboBoxWidthUnit"
        resources.ApplyResources(Me.TextBoxWidth, "TextBoxWidth")
        Me.TextBoxWidth.Name = "TextBoxWidth"
        resources.ApplyResources(Me.RadioButtonFixedWidth, "RadioButtonFixedWidth")
        Me.RadioButtonFixedWidth.Name = "RadioButtonFixedWidth"
        Me.RadioButtonFixedWidth.TabStop = true
        Me.RadioButtonFixedWidth.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.RadioButtonAutoAdjustWidth, "RadioButtonAutoAdjustWidth")
        Me.RadioButtonAutoAdjustWidth.Name = "RadioButtonAutoAdjustWidth"
        Me.RadioButtonAutoAdjustWidth.TabStop = true
        Me.RadioButtonAutoAdjustWidth.UseVisualStyleBackColor = true
        Me.GroupBoxHeight.Controls.Add(Me.TextBoxHeightUnit)
        Me.GroupBoxHeight.Controls.Add(Me.TextBoxHeight)
        Me.GroupBoxHeight.Controls.Add(Me.RadioButtonFixedHeight)
        Me.GroupBoxHeight.Controls.Add(Me.RadioButtonAutoAdjustHeight)
        resources.ApplyResources(Me.GroupBoxHeight, "GroupBoxHeight")
        Me.GroupBoxHeight.Name = "GroupBoxHeight"
        Me.GroupBoxHeight.TabStop = false
        Me.TextBoxHeightUnit.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        resources.ApplyResources(Me.TextBoxHeightUnit, "TextBoxHeightUnit")
        Me.TextBoxHeightUnit.Name = "TextBoxHeightUnit"
        resources.ApplyResources(Me.TextBoxHeight, "TextBoxHeight")
        Me.TextBoxHeight.Name = "TextBoxHeight"
        resources.ApplyResources(Me.RadioButtonFixedHeight, "RadioButtonFixedHeight")
        Me.RadioButtonFixedHeight.Name = "RadioButtonFixedHeight"
        Me.RadioButtonFixedHeight.TabStop = true
        Me.RadioButtonFixedHeight.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.RadioButtonAutoAdjustHeight, "RadioButtonAutoAdjustHeight")
        Me.RadioButtonAutoAdjustHeight.Name = "RadioButtonAutoAdjustHeight"
        Me.RadioButtonAutoAdjustHeight.TabStop = true
        Me.RadioButtonAutoAdjustHeight.UseVisualStyleBackColor = true
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBoxHeight)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxAlignment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "DialogEditTableProperties"
        Me.ShowInTaskbar = false
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBoxHeight.ResumeLayout(false)
        Me.GroupBoxHeight.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ComboBoxAlignment As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OK_Button As Windows.Forms.Button
End Class
