Imports Questify.Builder.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Word_TestPartPropertiesEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Word_TestPartPropertiesEditor))
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.DisplaySectionHeaderPageLabel = New System.Windows.Forms.Label()
        Me.DisplaySectionHeaderPageBox = New System.Windows.Forms.CheckBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetailTableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.ControlBindingSource.DataSource = GetType(WordTestPart)
        resources.ApplyResources(Me.DetailTableLayoutPanel, "DetailTableLayoutPanel")
        Me.DetailTableLayoutPanel.Controls.Add(Me.DisplaySectionHeaderPageLabel, 0, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.DisplaySectionHeaderPageBox, 1, 0)
        Me.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel"
        resources.ApplyResources(Me.DisplaySectionHeaderPageLabel, "DisplaySectionHeaderPageLabel")
        Me.DisplaySectionHeaderPageLabel.Name = "DisplaySectionHeaderPageLabel"
        resources.ApplyResources(Me.DisplaySectionHeaderPageBox, "DisplaySectionHeaderPageBox")
        Me.DisplaySectionHeaderPageBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ControlBindingSource, "DisplaySectionHeaderPage", True))
        Me.DisplaySectionHeaderPageBox.Name = "DisplaySectionHeaderPageBox"
        Me.DisplaySectionHeaderPageBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "Word_TestPartPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetailTableLayoutPanel.ResumeLayout(False)
        Me.DetailTableLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents DisplaySectionHeaderPageLabel As System.Windows.Forms.Label
    Friend WithEvents DisplaySectionHeaderPageBox As System.Windows.Forms.CheckBox

End Class
