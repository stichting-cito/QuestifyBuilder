<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GapTextRichTextParameterEditorControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GapTextRichTextParameterEditorControl))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.NumericUpDownMatchMax = New System.Windows.Forms.NumericUpDown()
        Me.LabelMatchMax = New System.Windows.Forms.Label()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GapTextRichTextParameter)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownMatchMax, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMatchMax, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.NumericUpDownMatchMax.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ParameterBindingSource, "MatchMax", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.NumericUpDownMatchMax, "NumericUpDownMatchMax")
        Me.NumericUpDownMatchMax.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDownMatchMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownMatchMax.Name = "NumericUpDownMatchMax"
        Me.NumericUpDownMatchMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        resources.ApplyResources(Me.LabelMatchMax, "LabelMatchMax")
        Me.LabelMatchMax.Name = "LabelMatchMax"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "GapTextRichTextParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelMatchMax As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownMatchMax As System.Windows.Forms.NumericUpDown

End Class
