<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class General_TestSectionPropertiesEditor
    Inherits TestEditorControlBase

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
        Dim ItemDatasourceLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_TestSectionPropertiesEditor))
        Me.WeightLabel = New System.Windows.Forms.Label()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DetailTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.WeightTextbox = New System.Windows.Forms.NumericUpDown()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.ItemDataSourceTextBox = New System.Windows.Forms.TextBox()
        Me.SelectItemDatasourceButton = New System.Windows.Forms.Button()
        Me.RemoveItemDatasourceButton = New System.Windows.Forms.Button()
        Me.AdaptiveModuleLabel = New System.Windows.Forms.Label()
        Me.AdaptiveControlFileLabel = New System.Windows.Forms.Label()
        Me.ModuleHrefTextBox = New System.Windows.Forms.TextBox()
        Me.DriverHrefTextBox = New System.Windows.Forms.TextBox()
        Me.SelectAdaptiveModuleButton = New System.Windows.Forms.Button()
        Me.SelectAdaptiveDriver = New System.Windows.Forms.Button()
        Me.RemoveAdaptiveModuleButton = New System.Windows.Forms.Button()
        Me.RemoveAdaptiveDriverButton = New System.Windows.Forms.Button()
        ItemDatasourceLabel = New System.Windows.Forms.Label()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetailTableLayoutPanel.SuspendLayout()
        CType(Me.WeightTextbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GeneralTestSection)
        resources.ApplyResources(ItemDatasourceLabel, "ItemDatasourceLabel")
        ItemDatasourceLabel.Name = "ItemDatasourceLabel"
        resources.ApplyResources(Me.WeightLabel, "WeightLabel")
        Me.WeightLabel.Name = "WeightLabel"
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.DetailTableLayoutPanel, "DetailTableLayoutPanel")
        Me.DetailTableLayoutPanel.Controls.Add(Me.WeightTextbox, 1, 3)
        Me.DetailTableLayoutPanel.Controls.Add(Me.WeightLabel, 0, 3)
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleLabel, 0, 0)
        Me.DetailTableLayoutPanel.Controls.Add(Me.TitleTextBox, 1, 0)
        Me.DetailTableLayoutPanel.Controls.Add(ItemDatasourceLabel, 0, 2)
        Me.DetailTableLayoutPanel.Controls.Add(Me.ItemDataSourceTextBox, 1, 2)
        Me.DetailTableLayoutPanel.Controls.Add(Me.SelectItemDatasourceButton, 3, 2)
        Me.DetailTableLayoutPanel.Controls.Add(Me.RemoveItemDatasourceButton, 4, 2)
        Me.DetailTableLayoutPanel.Controls.Add(Me.AdaptiveModuleLabel, 0, 4)
        Me.DetailTableLayoutPanel.Controls.Add(Me.AdaptiveControlFileLabel, 0, 5)
        Me.DetailTableLayoutPanel.Controls.Add(Me.ModuleHrefTextBox, 1, 4)
        Me.DetailTableLayoutPanel.Controls.Add(Me.DriverHrefTextBox, 1, 5)
        Me.DetailTableLayoutPanel.Controls.Add(Me.SelectAdaptiveModuleButton, 3, 4)
        Me.DetailTableLayoutPanel.Controls.Add(Me.SelectAdaptiveDriver, 3, 5)
        Me.DetailTableLayoutPanel.Controls.Add(Me.RemoveAdaptiveModuleButton, 4, 4)
        Me.DetailTableLayoutPanel.Controls.Add(Me.RemoveAdaptiveDriverButton, 4, 5)
        Me.DetailTableLayoutPanel.Name = "DetailTableLayoutPanel"
        resources.ApplyResources(Me.WeightTextbox, "WeightTextbox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.WeightTextbox, 2)
        Me.WeightTextbox.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ControlBindingSource, "ItemWeightForVariantTests", True))
        Me.WeightTextbox.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.WeightTextbox.Name = "WeightTextbox"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.TitleTextBox, 2)
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Title", True))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.ItemDataSourceTextBox, "ItemDataSourceTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.ItemDataSourceTextBox, 2)
        Me.ItemDataSourceTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "ItemDataSource", True))
        Me.ItemDataSourceTextBox.Name = "ItemDataSourceTextBox"
        resources.ApplyResources(Me.SelectItemDatasourceButton, "SelectItemDatasourceButton")
        Me.SelectItemDatasourceButton.Name = "SelectItemDatasourceButton"
        Me.SelectItemDatasourceButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.RemoveItemDatasourceButton, "RemoveItemDatasourceButton")
        Me.RemoveItemDatasourceButton.Name = "RemoveItemDatasourceButton"
        Me.RemoveItemDatasourceButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AdaptiveModuleLabel, "AdaptiveModuleLabel")
        Me.AdaptiveModuleLabel.Name = "AdaptiveModuleLabel"
        resources.ApplyResources(Me.AdaptiveControlFileLabel, "AdaptiveControlFileLabel")
        Me.AdaptiveControlFileLabel.Name = "AdaptiveControlFileLabel"
        resources.ApplyResources(Me.ModuleHrefTextBox, "ModuleHrefTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.ModuleHrefTextBox, 2)
        Me.ModuleHrefTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "ModuleHref", True))
        Me.ModuleHrefTextBox.Name = "ModuleHrefTextBox"
        resources.ApplyResources(Me.DriverHrefTextBox, "DriverHrefTextBox")
        Me.DetailTableLayoutPanel.SetColumnSpan(Me.DriverHrefTextBox, 2)
        Me.DriverHrefTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "DriverHref", True))
        Me.DriverHrefTextBox.Name = "DriverHrefTextBox"
        resources.ApplyResources(Me.SelectAdaptiveModuleButton, "SelectAdaptiveModuleButton")
        Me.SelectAdaptiveModuleButton.Name = "SelectAdaptiveModuleButton"
        Me.SelectAdaptiveModuleButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.SelectAdaptiveDriver, "SelectAdaptiveDriver")
        Me.SelectAdaptiveDriver.Name = "SelectAdaptiveDriver"
        Me.SelectAdaptiveDriver.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.RemoveAdaptiveModuleButton, "RemoveAdaptiveModuleButton")
        Me.RemoveAdaptiveModuleButton.Name = "RemoveAdaptiveModuleButton"
        Me.RemoveAdaptiveModuleButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.RemoveAdaptiveDriverButton, "RemoveAdaptiveDriverButton")
        Me.RemoveAdaptiveDriverButton.Name = "RemoveAdaptiveDriverButton"
        Me.RemoveAdaptiveDriverButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DetailTableLayoutPanel)
        Me.Name = "General_TestSectionPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.DetailTableLayoutPanel.ResumeLayout(false)
        Me.DetailTableLayoutPanel.PerformLayout
        CType(Me.WeightTextbox, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents DetailTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents SelectItemDatasourceButton As System.Windows.Forms.Button
    Friend WithEvents RemoveItemDatasourceButton As System.Windows.Forms.Button
    Friend WithEvents ItemDataSourceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WeightTextbox As System.Windows.Forms.NumericUpDown
    Friend WithEvents WeightLabel As Label
    Friend WithEvents AdaptiveModuleLabel As Label
    Friend WithEvents AdaptiveControlFileLabel As Label
    Friend WithEvents DriverHrefTextBox As TextBox
    Friend WithEvents ModuleHrefTextBox As TextBox
    Friend WithEvents SelectAdaptiveModuleButton As Button
    Friend WithEvents SelectAdaptiveDriver As Button
    Friend WithEvents RemoveAdaptiveModuleButton As Button
    Friend WithEvents RemoveAdaptiveDriverButton As Button
End Class
