<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GapTextParameterEditorControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GapTextParameterEditorControl))
        Me.GapTextParameterTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DimensionsPanel = New System.Windows.Forms.Panel()
        Me.DimensionsTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.WidthLabel = New System.Windows.Forms.Label()
        Me.HeightLabel = New System.Windows.Forms.Label()
        Me.WidthTextBox = New System.Windows.Forms.NumericUpDown()
        Me.HeightTextBox = New System.Windows.Forms.NumericUpDown()
        Me.KeepAspectRatioCheckBox = New System.Windows.Forms.CheckBox()
        Me.LabelMatchMax = New System.Windows.Forms.Label()
        Me.NumericUpDownMatchMax = New System.Windows.Forms.NumericUpDown()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.DimensionsPanel.SuspendLayout()
        Me.DimensionsTableLayoutPanel.SuspendLayout()
        CType(Me.WidthTextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeightTextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GapTextParameter)
        resources.ApplyResources(Me.GapTextParameterTextBox, "GapTextParameterTextBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.GapTextParameterTextBox, 2)
        Me.GapTextParameterTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ParameterBindingSource, "Value", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.GapTextParameterTextBox.Name = "GapTextParameterTextBox"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownMatchMax, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMatchMax, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.DimensionsPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GapTextParameterTextBox, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.SetColumnSpan(Me.DimensionsPanel, 2)
        Me.DimensionsPanel.Controls.Add(Me.DimensionsTableLayoutPanel)
        resources.ApplyResources(Me.DimensionsPanel, "DimensionsPanel")
        Me.DimensionsPanel.Name = "DimensionsPanel"
        resources.ApplyResources(Me.DimensionsTableLayoutPanel, "DimensionsTableLayoutPanel")
        Me.DimensionsTableLayoutPanel.Controls.Add(Me.WidthLabel, 0, 0)
        Me.DimensionsTableLayoutPanel.Controls.Add(Me.HeightLabel, 0, 1)
        Me.DimensionsTableLayoutPanel.Controls.Add(Me.WidthTextBox, 1, 0)
        Me.DimensionsTableLayoutPanel.Controls.Add(Me.HeightTextBox, 1, 1)
        Me.DimensionsTableLayoutPanel.Controls.Add(Me.KeepAspectRatioCheckBox, 0, 2)
        Me.DimensionsTableLayoutPanel.Name = "DimensionsTableLayoutPanel"
        resources.ApplyResources(Me.WidthLabel, "WidthLabel")
        Me.WidthLabel.Name = "WidthLabel"
        resources.ApplyResources(Me.HeightLabel, "HeightLabel")
        Me.HeightLabel.Name = "HeightLabel"
        resources.ApplyResources(Me.WidthTextBox, "WidthTextBox")
        Me.WidthTextBox.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.WidthTextBox.Name = "WidthTextBox"
        resources.ApplyResources(Me.HeightTextBox, "HeightTextBox")
        Me.HeightTextBox.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.HeightTextBox.Name = "HeightTextBox"
        resources.ApplyResources(Me.KeepAspectRatioCheckBox, "KeepAspectRatioCheckBox")
        Me.DimensionsTableLayoutPanel.SetColumnSpan(Me.KeepAspectRatioCheckBox, 2)
        Me.KeepAspectRatioCheckBox.Name = "KeepAspectRatioCheckBox"
        Me.KeepAspectRatioCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.LabelMatchMax, "LabelMatchMax")
        Me.LabelMatchMax.Name = "LabelMatchMax"
        Me.NumericUpDownMatchMax.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ParameterBindingSource, "MatchMax", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.NumericUpDownMatchMax, "NumericUpDownMatchMax")
        Me.NumericUpDownMatchMax.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDownMatchMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownMatchMax.Name = "NumericUpDownMatchMax"
        Me.NumericUpDownMatchMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "GapTextParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.DimensionsPanel.ResumeLayout(False)
        Me.DimensionsTableLayoutPanel.ResumeLayout(False)
        Me.DimensionsTableLayoutPanel.PerformLayout()
        CType(Me.WidthTextBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeightTextBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GapTextParameterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DimensionsPanel As Panel
    Friend WithEvents DimensionsTableLayoutPanel As TableLayoutPanel
    Friend WithEvents WidthLabel As Label
    Friend WithEvents HeightLabel As Label
    Friend WithEvents WidthTextBox As NumericUpDown
    Friend WithEvents HeightTextBox As NumericUpDown
    Friend WithEvents KeepAspectRatioCheckBox As CheckBox
    Friend WithEvents NumericUpDownMatchMax As NumericUpDown
    Friend WithEvents LabelMatchMax As Label
End Class
