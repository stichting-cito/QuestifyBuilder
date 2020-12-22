<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HotTextScoringParameterEditorControl
    Inherits ParameterEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HotTextScoringParameterEditorControl))
        Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelHotText = New System.Windows.Forms.Label()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainTableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.HotTextScoringParameter)
        resources.ApplyResources(Me.MainTableLayoutPanel, "MainTableLayoutPanel")
        Me.MainTableLayoutPanel.Controls.Add(Me.LabelHotText, 0, 0)
        Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
        resources.ApplyResources(Me.LabelHotText, "LabelHotText")
        Me.LabelHotText.Name = "LabelHotText"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainTableLayoutPanel)
        Me.Name = "HotTextScoringParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainTableLayoutPanel.ResumeLayout(False)
        Me.MainTableLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelHotText As System.Windows.Forms.Label

End Class
