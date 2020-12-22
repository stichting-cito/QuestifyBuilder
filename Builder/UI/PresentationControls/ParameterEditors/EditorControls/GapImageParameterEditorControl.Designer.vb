<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GapImageParameterEditorControl
    Inherits ParameterEditorControlBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                RemoveHandler _textParameter.PropertyChanged, AddressOf TextualValueChanged
                RemoveHandler _resourceParamControl.EditResource, AddressOf EditResourceHandler
                RemoveHandler _resourceParamControl.AddingResource, AddressOf Adding_Resource
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GapImageParameterEditorControl))
        Me.GapImageParameterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelMatchMax = New System.Windows.Forms.Label()
        Me.NumericUpDownMatchMax = New System.Windows.Forms.NumericUpDown()
        Me.ParamControlPanel = New System.Windows.Forms.Panel()
        Me.comboxImageTextOrFormula = New System.Windows.Forms.ComboBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GapImageParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GapImageParameter)
        Me.GapImageParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GapImageParameter)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMatchMax, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownMatchMax, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ParamControlPanel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.comboxImageTextOrFormula, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.LabelMatchMax, "LabelMatchMax")
        Me.LabelMatchMax.Name = "LabelMatchMax"
        Me.NumericUpDownMatchMax.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ParameterBindingSource, "MatchMax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.NumericUpDownMatchMax, "NumericUpDownMatchMax")
        Me.NumericUpDownMatchMax.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownMatchMax.Name = "NumericUpDownMatchMax"
        Me.NumericUpDownMatchMax.Value = New Decimal(New Integer() {1, 0, 0, 0})
        resources.ApplyResources(Me.ParamControlPanel, "ParamControlPanel")
        Me.ParamControlPanel.Name = "ParamControlPanel"
        Me.comboxImageTextOrFormula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboxImageTextOrFormula.FormattingEnabled = true
        resources.ApplyResources(Me.comboxImageTextOrFormula, "comboxImageTextOrFormula")
        Me.comboxImageTextOrFormula.Items.AddRange(New Object() {resources.GetString("comboxImageTextOrFormula.Items"), resources.GetString("comboxImageTextOrFormula.Items1"), resources.GetString("comboxImageTextOrFormula.Items2")})
        Me.comboxImageTextOrFormula.Name = "comboxImageTextOrFormula"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "GapImageParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GapImageParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.NumericUpDownMatchMax, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub


    Friend WithEvents LabelMatchMax As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents NumericUpDownMatchMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents ParamControlPanel As System.Windows.Forms.Panel
    Friend WithEvents GapImageParameterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents comboxImageTextOrFormula As System.Windows.Forms.ComboBox

End Class
