<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTarget
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Me.CbtExtraOptions.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTarget))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OptionValidatorImageExportBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TargetSpecificOptionsPanel = New System.Windows.Forms.Panel()
        Me.CbtExtraOptions = New CBTExtraOptions()
        Me.ChooseTargetPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectTargetComboBox = New System.Windows.Forms.ComboBox()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.OptionValidatorImageExportBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TargetSpecificOptionsPanel.SuspendLayout
        Me.ChooseTargetPanel.SuspendLayout
        Me.TableLayoutPanel3.SuspendLayout
        Me.SuspendLayout
        Me.ErrorProvider.ContainerControl = Me
        Me.ErrorProvider.DataSource = Me.OptionValidatorImageExportBindingSource
        Me.OptionValidatorImageExportBindingSource.DataSource = GetType(Questify.Builder.Logic.OptionValidatorImageExport)
        Me.TargetSpecificOptionsPanel.Controls.Add(Me.CbtExtraOptions)
        resources.ApplyResources(Me.TargetSpecificOptionsPanel, "TargetSpecificOptionsPanel")
        Me.TargetSpecificOptionsPanel.Name = "TargetSpecificOptionsPanel"
        resources.ApplyResources(Me.CbtExtraOptions, "CbtExtraOptions")
        Me.CbtExtraOptions.Name = "CbtExtraOptions"
        Me.ChooseTargetPanel.Controls.Add(Me.TableLayoutPanel3)
        resources.ApplyResources(Me.ChooseTargetPanel, "ChooseTargetPanel")
        Me.ChooseTargetPanel.Name = "ChooseTargetPanel"
        resources.ApplyResources(Me.TableLayoutPanel3, "TableLayoutPanel3")
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.SelectTargetComboBox, 1, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.SelectTargetComboBox, "SelectTargetComboBox")
        Me.SelectTargetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectTargetComboBox.FormattingEnabled = true
        Me.SelectTargetComboBox.Name = "SelectTargetComboBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TargetSpecificOptionsPanel)
        Me.Controls.Add(Me.ChooseTargetPanel)
        Me.Name = "SelectTarget"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.OptionValidatorImageExportBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TargetSpecificOptionsPanel.ResumeLayout(false)
        Me.TargetSpecificOptionsPanel.PerformLayout
        Me.ChooseTargetPanel.ResumeLayout(false)
        Me.TableLayoutPanel3.ResumeLayout(false)
        Me.TableLayoutPanel3.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents OptionValidatorImageExportBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents ChooseTargetPanel As System.Windows.Forms.Panel
    Friend WithEvents TargetSpecificOptionsPanel As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectTargetComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CbtExtraOptions As CBTExtraOptions

End Class
