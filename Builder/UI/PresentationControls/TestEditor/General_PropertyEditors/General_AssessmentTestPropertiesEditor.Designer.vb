<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class General_AssessmentTestPropertiesEditor
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
        Dim IdentifierLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_AssessmentTestPropertiesEditor))
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.ChangeCodeButton = New System.Windows.Forms.Button()
        Me.CodeTextBox = New System.Windows.Forms.TextBox()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        IdentifierLabel = New System.Windows.Forms.Label()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.DetailTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GeneralAssessmentTest)
        resources.ApplyResources(IdentifierLabel, "IdentifierLabel")
        IdentifierLabel.Name = "IdentifierLabel"
        resources.ApplyResources(Me.DetailTableLayoutPanel, "DetailTableLayoutPanel")
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleLabel, 0, 1)
        Me.DetailTableLayoutPanel.Controls.Add(IdentifierLabel, 0, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.ChangeCodeButton, 3, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.CodeTextBox, 1, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleTextBox, 1, 1)
        Me.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel"
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.ChangeCodeButton, "ChangeCodeButton")
        Me.ChangeCodeButton.Name = "ChangeCodeButton"
        Me.ChangeCodeButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.CodeTextBox, "CodeTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.CodeTextBox, 2)
        Me.CodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Identifier", true))
        Me.CodeTextBox.Name = "CodeTextBox"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.TitleTextBox, 2)
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Title", true))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "General_AssessmentTestPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.DetailTableLayoutPanel.ResumeLayout(false)
        Me.DetailTableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents DetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ChangeCodeButton As System.Windows.Forms.Button
    Friend WithEvents CodeTextBox As TextBox
End Class
