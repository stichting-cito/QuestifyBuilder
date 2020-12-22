<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemInTestFilterEditor
    Inherits FilterEditorBase

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
        Dim AssessmentTestNameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemInTestFilterEditor))
        Me.ItemInTestFilterEditorTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AssessmentTestNameTextBox = New System.Windows.Forms.TextBox()
        Me.SelectTestButton = New System.Windows.Forms.Button()
        Me.CaptionLabel = New System.Windows.Forms.Label()
        AssessmentTestNameLabel = New System.Windows.Forms.Label()
        Me.ItemInTestFilterEditorTableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(AssessmentTestNameLabel, "AssessmentTestNameLabel")
        AssessmentTestNameLabel.Name = "AssessmentTestNameLabel"
        resources.ApplyResources(Me.ItemInTestFilterEditorTableLayoutPanel, "ItemInTestFilterEditorTableLayoutPanel")
        Me.ItemInTestFilterEditorTableLayoutPanel.Controls.Add(AssessmentTestNameLabel, 0, 0)
        Me.ItemInTestFilterEditorTableLayoutPanel.Controls.Add(Me.AssessmentTestNameTextBox, 1, 0)
        Me.ItemInTestFilterEditorTableLayoutPanel.Controls.Add(Me.SelectTestButton, 2, 0)
        Me.ItemInTestFilterEditorTableLayoutPanel.Name = "ItemInTestFilterEditorTableLayoutPanel"
        resources.ApplyResources(Me.AssessmentTestNameTextBox, "AssessmentTestNameTextBox")
        Me.AssessmentTestNameTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.AssessmentTestNameTextBox.Name = "AssessmentTestNameTextBox"
        Me.AssessmentTestNameTextBox.ReadOnly = True
        resources.ApplyResources(Me.SelectTestButton, "SelectTestButton")
        Me.SelectTestButton.Name = "SelectTestButton"
        Me.SelectTestButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.CaptionLabel, "CaptionLabel")
        Me.CaptionLabel.Name = "CaptionLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ItemInTestFilterEditorTableLayoutPanel)
        Me.Controls.Add(Me.CaptionLabel)
        Me.Name = "ItemInTestFilterEditor"
        Me.ItemInTestFilterEditorTableLayoutPanel.ResumeLayout(False)
        Me.ItemInTestFilterEditorTableLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CaptionLabel As System.Windows.Forms.Label
    Friend WithEvents SelectTestButton As System.Windows.Forms.Button
    Friend WithEvents ItemInTestFilterEditorTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AssessmentTestNameTextBox As System.Windows.Forms.TextBox

End Class
