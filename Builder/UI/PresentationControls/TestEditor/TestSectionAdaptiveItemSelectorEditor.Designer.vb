<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestSectionAdaptiveItemSelectorEditor
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
        Dim OTDSourceLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestSectionAdaptiveItemSelectorEditor))
        Dim MinTimeLabel As System.Windows.Forms.Label
        Dim MaxTimeLabel As System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MaxTimeNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.MinTimeNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.OTDSourceTextBox = New System.Windows.Forms.TextBox()
        Me.OpenSelectGenericResourceDialog = New System.Windows.Forms.Button()
        Me.DeleteGenericResourceButton = New System.Windows.Forms.Button()
        OTDSourceLabel = New System.Windows.Forms.Label()
        MinTimeLabel = New System.Windows.Forms.Label()
        MaxTimeLabel = New System.Windows.Forms.Label()
        CType(Me.SettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.MaxTimeNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MinTimeNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.SettingsBindingSource.DataSource = GetType(Cito.Tester.ContentModel.TestSectionAdaptiveItemSelectorSettings)
        resources.ApplyResources(OTDSourceLabel, "OTDSourceLabel")
        OTDSourceLabel.Name = "OTDSourceLabel"
        resources.ApplyResources(MinTimeLabel, "MinTimeLabel")
        MinTimeLabel.Name = "MinTimeLabel"
        resources.ApplyResources(MaxTimeLabel, "MaxTimeLabel")
        MaxTimeLabel.Name = "MaxTimeLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(MaxTimeLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MaxTimeNumericUpDown, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(MinTimeLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.MinTimeNumericUpDown, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(OTDSourceLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.OTDSourceTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.OpenSelectGenericResourceDialog, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteGenericResourceButton, 3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.MaxTimeNumericUpDown, "MaxTimeNumericUpDown")
        Me.MaxTimeNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.SettingsBindingSource, "MaxTime", True))
        Me.MaxTimeNumericUpDown.Name = "MaxTimeNumericUpDown"
        resources.ApplyResources(Me.MinTimeNumericUpDown, "MinTimeNumericUpDown")
        Me.MinTimeNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.SettingsBindingSource, "MinTime", True))
        Me.MinTimeNumericUpDown.Name = "MinTimeNumericUpDown"
        resources.ApplyResources(Me.OTDSourceTextBox, "OTDSourceTextBox")
        Me.OTDSourceTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SettingsBindingSource, "OTDSource", True))
        Me.OTDSourceTextBox.Name = "OTDSourceTextBox"
        resources.ApplyResources(Me.OpenSelectGenericResourceDialog, "OpenSelectGenericResourceDialog")
        Me.OpenSelectGenericResourceDialog.Name = "OpenSelectGenericResourceDialog"
        Me.OpenSelectGenericResourceDialog.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DeleteGenericResourceButton, "DeleteGenericResourceButton")
        Me.DeleteGenericResourceButton.Name = "DeleteGenericResourceButton"
        Me.DeleteGenericResourceButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "TestSectionAdaptiveItemSelectorEditor"
        CType(Me.SettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.MaxTimeNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MinTimeNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MaxTimeNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents MinTimeNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents OTDSourceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OpenSelectGenericResourceDialog As System.Windows.Forms.Button
    Friend WithEvents DeleteGenericResourceButton As System.Windows.Forms.Button

End Class
