<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CBTExtraOptions
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CBTExtraOptions))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DimensionComboBox = New System.Windows.Forms.ComboBox()
        Me.OptionValidatorWordExportBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.OptionValidatorWordExportBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.DimensionComboBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.DimensionComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.OptionValidatorWordExportBindingSource, "Size", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.DimensionComboBox, "DimensionComboBox")
        Me.DimensionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DimensionComboBox.FormattingEnabled = true
        Me.DimensionComboBox.Name = "DimensionComboBox"
        Me.OptionValidatorWordExportBindingSource.DataSource = GetType(Questify.Builder.Logic.OptionValidatorExportBase)
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ErrorProvider.ContainerControl = Me
        Me.ErrorProvider.DataSource = Me.OptionValidatorWordExportBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CBTExtraOptions"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.OptionValidatorWordExportBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DimensionComboBox As System.Windows.Forms.ComboBox
    Public WithEvents OptionValidatorWordExportBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider

End Class
