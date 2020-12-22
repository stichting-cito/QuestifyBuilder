<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResolveValidationErrorDialog
    Inherits Questify.Builder.Client.DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResolveValidationErrorDialog))
        Me.RetryFixRadioButton = New System.Windows.Forms.RadioButton()
        Me.RetryIgnoreRadioButton = New System.Windows.Forms.RadioButton()
        Me.InclusionMessageLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ExclusionMessageLabel = New System.Windows.Forms.Label()
        Me.GenericMessageLabel = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.FillPanel, "FillPanel")
        Me.FillPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.FillPanel.Controls.Add(Me.ExclusionMessageLabel)
        Me.FillPanel.Controls.Add(Me.InclusionMessageLabel)
        Me.FillPanel.Controls.Add(Me.GenericMessageLabel)
        resources.ApplyResources(Me.DialogOkButton, "DialogOkButton")
        resources.ApplyResources(Me.DialogCancelButton, "DialogCancelButton")
        resources.ApplyResources(Me.RetryFixRadioButton, "RetryFixRadioButton")
        Me.RetryFixRadioButton.Checked = true
        Me.RetryFixRadioButton.Name = "RetryFixRadioButton"
        Me.RetryFixRadioButton.TabStop = true
        Me.RetryFixRadioButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.RetryIgnoreRadioButton, "RetryIgnoreRadioButton")
        Me.RetryIgnoreRadioButton.Name = "RetryIgnoreRadioButton"
        Me.RetryIgnoreRadioButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.InclusionMessageLabel, "InclusionMessageLabel")
        Me.InclusionMessageLabel.Name = "InclusionMessageLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.RetryFixRadioButton, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RetryIgnoreRadioButton, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.ExclusionMessageLabel, "ExclusionMessageLabel")
        Me.ExclusionMessageLabel.Name = "ExclusionMessageLabel"
        resources.ApplyResources(Me.GenericMessageLabel, "GenericMessageLabel")
        Me.GenericMessageLabel.Name = "GenericMessageLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "ResolveValidationErrorDialog"
        Me.FillPanel.ResumeLayout(false)
        Me.FillPanel.PerformLayout
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents RetryFixRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents InclusionMessageLabel As System.Windows.Forms.Label
    Friend WithEvents RetryIgnoreRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExclusionMessageLabel As System.Windows.Forms.Label
    Friend WithEvents GenericMessageLabel As System.Windows.Forms.Label
End Class
