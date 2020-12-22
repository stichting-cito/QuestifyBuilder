<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSectionInsertionGrpEditor
    Inherits SettingsEditorControlBase

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
        Dim InsertionMethodLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestSectionInsertionGrpEditor))
        Dim InsertionModuloLabel As System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.InsertionModuloNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.InsertionMethodComboBox = New System.Windows.Forms.ComboBox()
        InsertionMethodLabel = New System.Windows.Forms.Label()
        InsertionModuloLabel = New System.Windows.Forms.Label()
        CType(Me.SettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.InsertionModuloNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.SettingsBindingSource.DataSource = GetType(Cito.Tester.ContentModel.TestSectionInsertionGrpSettings)
        resources.ApplyResources(InsertionMethodLabel, "InsertionMethodLabel")
        InsertionMethodLabel.Name = "InsertionMethodLabel"
        resources.ApplyResources(InsertionModuloLabel, "InsertionModuloLabel")
        InsertionModuloLabel.Name = "InsertionModuloLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(InsertionModuloLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertionModuloNumericUpDown, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(InsertionMethodLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertionMethodComboBox, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.InsertionModuloNumericUpDown, "InsertionModuloNumericUpDown")
        Me.InsertionModuloNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.SettingsBindingSource, "InsertionModulo", True))
        Me.InsertionModuloNumericUpDown.Name = "InsertionModuloNumericUpDown"
        resources.ApplyResources(Me.InsertionMethodComboBox, "InsertionMethodComboBox")
        Me.InsertionMethodComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SettingsBindingSource, "InsertionMethod", True))
        Me.InsertionMethodComboBox.FormattingEnabled = True
        Me.InsertionMethodComboBox.Name = "InsertionMethodComboBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "TestSectionInsertionGrpEditor"
        CType(Me.SettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.InsertionModuloNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents InsertionModuloNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents InsertionMethodComboBox As System.Windows.Forms.ComboBox

End Class
