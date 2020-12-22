<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParameterCollectionEditorControl
    Inherits CollectionEditorControlBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParameterCollectionEditorControl))
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ParamCountTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ItemCountLabel = New System.Windows.Forms.Label()
        Me.nrOfItemsComboBox = New System.Windows.Forms.ComboBox()
        Me.parameterLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.MainLayoutPanel.SuspendLayout
        Me.ParamCountTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.MainLayoutPanel, "MainLayoutPanel")
        Me.MainLayoutPanel.Controls.Add(Me.ParamCountTableLayoutPanel, 0, 0)
        Me.MainLayoutPanel.Controls.Add(Me.parameterLayoutPanel, 0, 1)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        resources.ApplyResources(Me.ParamCountTableLayoutPanel, "ParamCountTableLayoutPanel")
        Me.ParamCountTableLayoutPanel.Controls.Add(Me.ItemCountLabel, 1, 0)
        Me.ParamCountTableLayoutPanel.Controls.Add(Me.nrOfItemsComboBox, 0, 0)
        Me.ParamCountTableLayoutPanel.Name = "ParamCountTableLayoutPanel"
        resources.ApplyResources(Me.ItemCountLabel, "ItemCountLabel")
        Me.ItemCountLabel.Name = "ItemCountLabel"
        resources.ApplyResources(Me.nrOfItemsComboBox, "nrOfItemsComboBox")
        Me.nrOfItemsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.nrOfItemsComboBox.FormattingEnabled = true
        Me.nrOfItemsComboBox.Name = "nrOfItemsComboBox"
        resources.ApplyResources(Me.parameterLayoutPanel, "parameterLayoutPanel")
        Me.parameterLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.parameterLayoutPanel.Name = "parameterLayoutPanel"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.Name = "ParameterCollectionEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.MainLayoutPanel.ResumeLayout(false)
        Me.MainLayoutPanel.PerformLayout
        Me.ParamCountTableLayoutPanel.ResumeLayout(false)
        Me.ParamCountTableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents parameterLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ParamCountTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ItemCountLabel As System.Windows.Forms.Label
    Friend WithEvents nrOfItemsComboBox As System.Windows.Forms.ComboBox

End Class
