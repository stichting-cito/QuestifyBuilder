<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResourceParameterEditorControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                releaseImageButton(SelectResourceButton)
                releaseImageButton(EditResourceButton)
                releaseImageButton(DeleteResourceButton)
            End If
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            RemoveHandler _parameter.ResourceNeeded, AddressOf Me.OnResourceNeeded
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Sub releaseImageButton(ByVal b As Button)
        If (b IsNot Nothing AndAlso b.Image IsNot Nothing) Then
            b.Image.Dispose()
        End If
    End Sub


    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResourceParameterEditorControl))
        Me.ParameterEditorToolip = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ResourceParameterTextBox = New System.Windows.Forms.TextBox()
        Me.SelectResourceButton = New System.Windows.Forms.Button()
        Me.DeleteResourceButton = New System.Windows.Forms.Button()
        Me.EditResourceButton = New System.Windows.Forms.Button()
        Me.DimensionsPanel = New System.Windows.Forms.Panel()
        Me.DimensionsTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.WidthLabel = New System.Windows.Forms.Label()
        Me.HeightLabel = New System.Windows.Forms.Label()
        Me.WidthTextBox = New System.Windows.Forms.NumericUpDown()
        Me.HeightTextBox = New System.Windows.Forms.NumericUpDown()
        Me.KeepAspectRatioCheckBox = New System.Windows.Forms.CheckBox()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.DimensionsPanel.SuspendLayout
        Me.DimensionsTableLayoutPanel.SuspendLayout
        CType(Me.WidthTextBox, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.HeightTextBox, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.ResourceParameter)
        Me.ParameterEditorToolip.IsBalloon = true
        Me.ParameterEditorToolip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ResourceParameterTextBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectResourceButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteResourceButton, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.EditResourceButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DimensionsPanel, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.ResourceParameterTextBox, "ResourceParameterTextBox")
        Me.ResourceParameterTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ParameterBindingSource, "Value", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ResourceParameterTextBox.Name = "ResourceParameterTextBox"
        resources.ApplyResources(Me.SelectResourceButton, "SelectResourceButton")
        Me.SelectResourceButton.Name = "SelectResourceButton"
        Me.ParameterEditorToolip.SetToolTip(Me.SelectResourceButton, resources.GetString("SelectResourceButton.ToolTip"))
        Me.SelectResourceButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.DeleteResourceButton, "DeleteResourceButton")
        Me.DeleteResourceButton.Name = "DeleteResourceButton"
        Me.ParameterEditorToolip.SetToolTip(Me.DeleteResourceButton, resources.GetString("DeleteResourceButton.ToolTip"))
        Me.DeleteResourceButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.EditResourceButton, "EditResourceButton")
        Me.EditResourceButton.Name = "EditResourceButton"
        Me.ParameterEditorToolip.SetToolTip(Me.EditResourceButton, resources.GetString("EditResourceButton.ToolTip"))
        Me.EditResourceButton.UseVisualStyleBackColor = true
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
        Me.KeepAspectRatioCheckBox.Checked = true
        Me.KeepAspectRatioCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DimensionsTableLayoutPanel.SetColumnSpan(Me.KeepAspectRatioCheckBox, 2)
        Me.KeepAspectRatioCheckBox.Name = "KeepAspectRatioCheckBox"
        Me.KeepAspectRatioCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ResourceParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.DimensionsPanel.ResumeLayout(false)
        Me.DimensionsTableLayoutPanel.ResumeLayout(false)
        Me.DimensionsTableLayoutPanel.PerformLayout
        CType(Me.WidthTextBox, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.HeightTextBox, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ResourceParameterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SelectResourceButton As System.Windows.Forms.Button
    Friend WithEvents DeleteResourceButton As System.Windows.Forms.Button
    Friend WithEvents EditResourceButton As System.Windows.Forms.Button
    Friend WithEvents ParameterEditorToolip As System.Windows.Forms.ToolTip
    Friend WithEvents DimensionsPanel As Panel
    Friend WithEvents DimensionsTableLayoutPanel As TableLayoutPanel
    Friend WithEvents KeepAspectRatioCheckBox As CheckBox
    Friend WithEvents WidthLabel As Label
    Friend WithEvents HeightLabel As Label
    Friend WithEvents WidthTextBox As NumericUpDown
    Friend WithEvents HeightTextBox As NumericUpDown
End Class
