<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class General_TestPartPropertiesEditor
    Inherits TestEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_TestPartPropertiesEditor))
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DetailTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GeneralTestPart)
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.TitleTextBox, 2)
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
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "General_TestPartPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.DetailTableLayoutPanel.ResumeLayout(false)
        Me.DetailTableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)

    End Sub

    Friend WithEvents TitleTextBox As TextBox
    Friend WithEvents DetailTableLayoutPanel As TableLayoutPanel
    Friend WithEvents TitleLabel As Label
End Class
