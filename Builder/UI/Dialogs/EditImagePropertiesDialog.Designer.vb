<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditImagePropertiesDialog
    Inherits Questify.Builder.UI.DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditImagePropertiesDialog))
        Me.GroupBoxImageSize = New System.Windows.Forms.GroupBox()
        Me.LabelOrg = New System.Windows.Forms.Label()
        Me.LabelCurrent = New System.Windows.Forms.Label()
        Me.LabelOriginalAspectRatio = New System.Windows.Forms.Label()
        Me.LabelAspectRatio = New System.Windows.Forms.Label()
        Me.LabelCurrentAspectRatio = New System.Windows.Forms.Label()
        Me.NumericUpDownWidth = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownHeight = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownPerc = New System.Windows.Forms.NumericUpDown()
        Me.ButtonReset = New System.Windows.Forms.Button()
        Me.LabelPercentageSign = New System.Windows.Forms.Label()
        Me.LabelPercentage = New System.Windows.Forms.Label()
        Me.LabelHeightInCm = New System.Windows.Forms.Label()
        Me.LabelHeightPixels = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelWidthPixels = New System.Windows.Forms.Label()
        Me.LabelWidthInCm = New System.Windows.Forms.Label()
        Me.LabelImageWidth = New System.Windows.Forms.Label()
        Me.GroupBoxGeneral = New System.Windows.Forms.GroupBox()
        Me.CheckBoxApplyBorder = New System.Windows.Forms.CheckBox()
        Me.TextBoxAlternativeText = New System.Windows.Forms.TextBox()
        Me.LabelAlternativeText = New System.Windows.Forms.Label()
        Me.ContentPanel.SuspendLayout()
        Me.GroupBoxImageSize.SuspendLayout()
        CType(Me.NumericUpDownWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownPerc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxGeneral.SuspendLayout()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.GroupBoxGeneral)
        Me.ContentPanel.Controls.Add(Me.GroupBoxImageSize)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.GroupBoxImageSize.Controls.Add(Me.LabelOrg)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelCurrent)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelOriginalAspectRatio)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelAspectRatio)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelCurrentAspectRatio)
        Me.GroupBoxImageSize.Controls.Add(Me.NumericUpDownWidth)
        Me.GroupBoxImageSize.Controls.Add(Me.NumericUpDownHeight)
        Me.GroupBoxImageSize.Controls.Add(Me.NumericUpDownPerc)
        Me.GroupBoxImageSize.Controls.Add(Me.ButtonReset)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelPercentageSign)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelPercentage)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelHeightInCm)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelHeightPixels)
        Me.GroupBoxImageSize.Controls.Add(Me.Label1)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelWidthPixels)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelWidthInCm)
        Me.GroupBoxImageSize.Controls.Add(Me.LabelImageWidth)
        resources.ApplyResources(Me.GroupBoxImageSize, "GroupBoxImageSize")
        Me.GroupBoxImageSize.Name = "GroupBoxImageSize"
        Me.GroupBoxImageSize.TabStop = False
        resources.ApplyResources(Me.LabelOrg, "LabelOrg")
        Me.LabelOrg.Name = "LabelOrg"
        resources.ApplyResources(Me.LabelCurrent, "LabelCurrent")
        Me.LabelCurrent.Name = "LabelCurrent"
        resources.ApplyResources(Me.LabelOriginalAspectRatio, "LabelOriginalAspectRatio")
        Me.LabelOriginalAspectRatio.Name = "LabelOriginalAspectRatio"
        resources.ApplyResources(Me.LabelAspectRatio, "LabelAspectRatio")
        Me.LabelAspectRatio.Name = "LabelAspectRatio"
        resources.ApplyResources(Me.LabelCurrentAspectRatio, "LabelCurrentAspectRatio")
        Me.LabelCurrentAspectRatio.Name = "LabelCurrentAspectRatio"
        resources.ApplyResources(Me.NumericUpDownWidth, "NumericUpDownWidth")
        Me.NumericUpDownWidth.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.NumericUpDownWidth.Name = "NumericUpDownWidth"
        resources.ApplyResources(Me.NumericUpDownHeight, "NumericUpDownHeight")
        Me.NumericUpDownHeight.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.NumericUpDownHeight.Name = "NumericUpDownHeight"
        Me.NumericUpDownPerc.DecimalPlaces = 1
        resources.ApplyResources(Me.NumericUpDownPerc, "NumericUpDownPerc")
        Me.NumericUpDownPerc.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDownPerc.Name = "NumericUpDownPerc"
        resources.ApplyResources(Me.ButtonReset, "ButtonReset")
        Me.ButtonReset.Name = "ButtonReset"
        Me.ButtonReset.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.LabelPercentageSign, "LabelPercentageSign")
        Me.LabelPercentageSign.Name = "LabelPercentageSign"
        resources.ApplyResources(Me.LabelPercentage, "LabelPercentage")
        Me.LabelPercentage.Name = "LabelPercentage"
        resources.ApplyResources(Me.LabelHeightInCm, "LabelHeightInCm")
        Me.LabelHeightInCm.Name = "LabelHeightInCm"
        resources.ApplyResources(Me.LabelHeightPixels, "LabelHeightPixels")
        Me.LabelHeightPixels.Name = "LabelHeightPixels"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.LabelWidthPixels, "LabelWidthPixels")
        Me.LabelWidthPixels.Name = "LabelWidthPixels"
        resources.ApplyResources(Me.LabelWidthInCm, "LabelWidthInCm")
        Me.LabelWidthInCm.Name = "LabelWidthInCm"
        resources.ApplyResources(Me.LabelImageWidth, "LabelImageWidth")
        Me.LabelImageWidth.Name = "LabelImageWidth"
        Me.GroupBoxGeneral.Controls.Add(Me.CheckBoxApplyBorder)
        Me.GroupBoxGeneral.Controls.Add(Me.TextBoxAlternativeText)
        Me.GroupBoxGeneral.Controls.Add(Me.LabelAlternativeText)
        resources.ApplyResources(Me.GroupBoxGeneral, "GroupBoxGeneral")
        Me.GroupBoxGeneral.Name = "GroupBoxGeneral"
        Me.GroupBoxGeneral.TabStop = False
        resources.ApplyResources(Me.CheckBoxApplyBorder, "CheckBoxApplyBorder")
        Me.CheckBoxApplyBorder.Name = "CheckBoxApplyBorder"
        Me.CheckBoxApplyBorder.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TextBoxAlternativeText, "TextBoxAlternativeText")
        Me.TextBoxAlternativeText.Name = "TextBoxAlternativeText"
        resources.ApplyResources(Me.LabelAlternativeText, "LabelAlternativeText")
        Me.LabelAlternativeText.Name = "LabelAlternativeText"
        Me.AcceptButton = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Nothing
        Me.Name = "EditImagePropertiesDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.GroupBoxImageSize.ResumeLayout(False)
        Me.GroupBoxImageSize.PerformLayout()
        CType(Me.NumericUpDownWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownPerc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxGeneral.ResumeLayout(False)
        Me.GroupBoxGeneral.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxImageSize As System.Windows.Forms.GroupBox
    Friend WithEvents LabelWidthPixels As System.Windows.Forms.Label
    Friend WithEvents LabelWidthInCm As System.Windows.Forms.Label
    Friend WithEvents LabelImageWidth As System.Windows.Forms.Label
    Friend WithEvents ButtonReset As System.Windows.Forms.Button
    Friend WithEvents LabelPercentageSign As System.Windows.Forms.Label
    Friend WithEvents LabelPercentage As System.Windows.Forms.Label
    Friend WithEvents LabelHeightInCm As System.Windows.Forms.Label
    Friend WithEvents LabelHeightPixels As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxApplyBorder As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxAlternativeText As System.Windows.Forms.TextBox
    Friend WithEvents LabelAlternativeText As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownPerc As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents LabelAspectRatio As System.Windows.Forms.Label
    Friend WithEvents LabelCurrentAspectRatio As System.Windows.Forms.Label
    Friend WithEvents LabelOrg As System.Windows.Forms.Label
    Friend WithEvents LabelCurrent As System.Windows.Forms.Label
    Friend WithEvents LabelOriginalAspectRatio As System.Windows.Forms.Label

End Class
