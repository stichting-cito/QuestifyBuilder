Imports Questify.Builder.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Word_TestSectionPropertiesEditor
    Inherits TestEditorControlBase

    <System.Diagnostics.DebuggerNonUserCode()>
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Word_TestSectionPropertiesEditor))
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.IsSampleSectionLabel = New System.Windows.Forms.Label()
        Me.IsSampleSectionCheckBox = New System.Windows.Forms.CheckBox()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DetailTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Questify.Builder.Plugins.PaperBased.WordTestSection)
        resources.ApplyResources(Me.DetailTableLayoutPanel, "DetailTableLayoutPanel")
        Me.DetailTableLayoutPanel.Controls.Add(Me.IsSampleSectionLabel, 0, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.IsSampleSectionCheckBox, 1, 0)
        Me.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel"
        resources.ApplyResources(Me.IsSampleSectionLabel, "IsSampleSectionLabel")
        Me.IsSampleSectionLabel.Name = "IsSampleSectionLabel"
        resources.ApplyResources(Me.IsSampleSectionCheckBox, "IsSampleSectionCheckBox")
        Me.IsSampleSectionCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ControlBindingSource, "IsSampleSection", true))
        Me.IsSampleSectionCheckBox.Name = "IsSampleSectionCheckBox"
        Me.IsSampleSectionCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "Word_TestSectionPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.DetailTableLayoutPanel.ResumeLayout(false)
        Me.DetailTableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents DetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents IsSampleSectionLabel As System.Windows.Forms.Label
    Friend WithEvents IsSampleSectionCheckBox As System.Windows.Forms.CheckBox

End Class
