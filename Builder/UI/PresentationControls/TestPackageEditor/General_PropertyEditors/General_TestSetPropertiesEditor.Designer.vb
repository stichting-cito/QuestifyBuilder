<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class General_TestSetPropertiesEditor
    Inherits TestPackageEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_TestSetPropertiesEditor))
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DetailTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GeneralTestSet)
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Title", true))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.DetailTableLayoutPanel, "DetailTableLayoutPanel")
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleLabel, 0, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleTextBox, 1, 0)
        Me.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "General_TestSetPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.DetailTableLayoutPanel.ResumeLayout(false)
        Me.DetailTableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleLabel As System.Windows.Forms.Label

End Class
