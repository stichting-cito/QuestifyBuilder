<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintFormEditor
    Inherits System.Windows.Forms.UserControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintFormEditor))
        Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.WordTemplateLabelLabel = New System.Windows.Forms.Label()
        Me.WordTemplateSourceLabel = New System.Windows.Forms.Label()
        Me.MainTableLayoutPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.MainTableLayoutPanel, "MainTableLayoutPanel")
        Me.MainTableLayoutPanel.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.MainTableLayoutPanel.Controls.Add(Me.AddButton, 2, 2)
        Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Image = My.Resources.Resources.add_icon_16
        Me.AddButton.Name = "AddButton"
        Me.AddButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.WordTemplateLabelLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.WordTemplateSourceLabel, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.WordTemplateLabelLabel, "WordTemplateLabelLabel")
        Me.WordTemplateLabelLabel.Name = "WordTemplateLabelLabel"
        resources.ApplyResources(Me.WordTemplateSourceLabel, "WordTemplateSourceLabel")
        Me.WordTemplateSourceLabel.Name = "WordTemplateSourceLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainTableLayoutPanel)
        Me.Name = "PrintFormEditor"
        Me.MainTableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents WordTemplateLabelLabel As System.Windows.Forms.Label
    Friend WithEvents WordTemplateSourceLabel As System.Windows.Forms.Label

End Class
