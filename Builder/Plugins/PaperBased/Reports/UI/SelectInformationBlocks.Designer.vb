<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectInformationBlocks
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                If _cbtExtraOptions IsNot Nothing Then
                    _cbtExtraOptions.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectInformationBlocks))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PageOrientationLandscapeCheckbox = New System.Windows.Forms.CheckBox()
        Me.OptionValidatorWordExportBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemOnNewPageCheckBox = New System.Windows.Forms.CheckBox()
        Me.TargetSpecificOptionsPanel = New System.Windows.Forms.Panel()
        Me.SelectTargetLabel = New System.Windows.Forms.Label()
        Me.ItemInformationCheckBox = New System.Windows.Forms.CheckBox()
        Me.ItemSettingsCheckBox = New System.Windows.Forms.CheckBox()
        Me.CustomPropertyCheckBox = New System.Windows.Forms.CheckBox()
        Me.ItemReferenceCheckBox = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.ItemSolutionCheckBox = New System.Windows.Forms.CheckBox()
        Me.ItemContentCheckBox = New System.Windows.Forms.CheckBox()
        Me.AddChoiceAlternativesCheckbox = New System.Windows.Forms.CheckBox()
        Me.AnalysesFilesGroupBox = New System.Windows.Forms.GroupBox()
        Me.AttachedFilesCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.SelectTargetComboBox = New System.Windows.Forms.ComboBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.OptionValidatorWordExportBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.AnalysesFilesGroupBox.SuspendLayout
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.PageOrientationLandscapeCheckbox, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemOnNewPageCheckBox, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.TargetSpecificOptionsPanel, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectTargetLabel, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemInformationCheckBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemSettingsCheckBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CustomPropertyCheckBox, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemReferenceCheckBox, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox4, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemSolutionCheckBox, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemContentCheckBox, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.AddChoiceAlternativesCheckbox, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.AnalysesFilesGroupBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectTargetComboBox, 1, 10)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.PageOrientationLandscapeCheckbox, "PageOrientationLandscapeCheckbox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.PageOrientationLandscapeCheckbox, 2)
        Me.PageOrientationLandscapeCheckbox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "PageOrientationLandscape", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.PageOrientationLandscapeCheckbox.Name = "PageOrientationLandscapeCheckbox"
        Me.PageOrientationLandscapeCheckbox.UseVisualStyleBackColor = true
        Me.OptionValidatorWordExportBindingSource.DataSource = GetType(Questify.Builder.Plugins.PaperBased.OptionValidatorWordExport)
        resources.ApplyResources(Me.ItemOnNewPageCheckBox, "ItemOnNewPageCheckBox")
        Me.TableLayoutPanel1.SetColumnSpan(Me.ItemOnNewPageCheckBox, 2)
        Me.ItemOnNewPageCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ItemOnNewPage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemOnNewPageCheckBox.Name = "ItemOnNewPageCheckBox"
        Me.ItemOnNewPageCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.TargetSpecificOptionsPanel, "TargetSpecificOptionsPanel")
        Me.TableLayoutPanel1.SetColumnSpan(Me.TargetSpecificOptionsPanel, 2)
        Me.TargetSpecificOptionsPanel.Name = "TargetSpecificOptionsPanel"
        resources.ApplyResources(Me.SelectTargetLabel, "SelectTargetLabel")
        Me.SelectTargetLabel.Name = "SelectTargetLabel"
        resources.ApplyResources(Me.ItemInformationCheckBox, "ItemInformationCheckBox")
        Me.ItemInformationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldItemInformationBeAddedToTheReport", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemInformationCheckBox.Name = "ItemInformationCheckBox"
        Me.ItemInformationCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.ItemSettingsCheckBox, "ItemSettingsCheckBox")
        Me.ItemSettingsCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldShowPreprocessorRules", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemSettingsCheckBox.Name = "ItemSettingsCheckBox"
        Me.ItemSettingsCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.CustomPropertyCheckBox, "CustomPropertyCheckBox")
        Me.CustomPropertyCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldItemCustomPropertiesBeAddedToTheReport", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CustomPropertyCheckBox.Name = "CustomPropertyCheckBox"
        Me.CustomPropertyCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.ItemReferenceCheckBox, "ItemReferenceCheckBox")
        Me.ItemReferenceCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldReferencesBeAddedToTheReport", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemReferenceCheckBox.Name = "ItemReferenceCheckBox"
        Me.ItemReferenceCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.CheckBox4, "CheckBox4")
        Me.CheckBox4.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldDependenciesBeAddedToTheReport", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.ItemSolutionCheckBox, "ItemSolutionCheckBox")
        Me.ItemSolutionCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldItemSolutionBeAddedToTheReport", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemSolutionCheckBox.Name = "ItemSolutionCheckBox"
        Me.ItemSolutionCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.ItemContentCheckBox, "ItemContentCheckBox")
        Me.ItemContentCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldAddItemContent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemContentCheckBox.Name = "ItemContentCheckBox"
        Me.ItemContentCheckBox.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.AddChoiceAlternativesCheckbox, "AddChoiceAlternativesCheckbox")
        Me.AddChoiceAlternativesCheckbox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OptionValidatorWordExportBindingSource, "ShouldAddChoiceAlternatives", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.AddChoiceAlternativesCheckbox.Name = "AddChoiceAlternativesCheckbox"
        Me.AddChoiceAlternativesCheckbox.UseVisualStyleBackColor = true
        Me.AnalysesFilesGroupBox.Controls.Add(Me.AttachedFilesCheckedListBox)
        resources.ApplyResources(Me.AnalysesFilesGroupBox, "AnalysesFilesGroupBox")
        Me.AnalysesFilesGroupBox.Name = "AnalysesFilesGroupBox"
        Me.TableLayoutPanel1.SetRowSpan(Me.AnalysesFilesGroupBox, 7)
        Me.AnalysesFilesGroupBox.TabStop = false
        Me.AttachedFilesCheckedListBox.CheckOnClick = true
        resources.ApplyResources(Me.AttachedFilesCheckedListBox, "AttachedFilesCheckedListBox")
        Me.AttachedFilesCheckedListBox.FormattingEnabled = true
        Me.AttachedFilesCheckedListBox.Name = "AttachedFilesCheckedListBox"
        resources.ApplyResources(Me.SelectTargetComboBox, "SelectTargetComboBox")
        Me.SelectTargetComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.OptionValidatorWordExportBindingSource, "SelectedHandler", true))
        Me.SelectTargetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SelectTargetComboBox.FormattingEnabled = true
        Me.SelectTargetComboBox.Name = "SelectTargetComboBox"
        Me.ErrorProvider.ContainerControl = Me
        Me.ErrorProvider.DataSource = Me.OptionValidatorWordExportBindingSource
        resources.ApplyResources(Me.GroupBox, "GroupBox")
        Me.GroupBox.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.TabStop = false
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox)
        Me.Name = "SelectInformationBlocks"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.OptionValidatorWordExportBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.AnalysesFilesGroupBox.ResumeLayout(false)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox.ResumeLayout(false)
        Me.GroupBox.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ItemInformationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents OptionValidatorWordExportBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CustomPropertyCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ItemReferenceCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents ItemContentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AnalysesFilesGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AttachedFilesCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents ItemSolutionCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ItemSettingsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AddChoiceAlternativesCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents SelectTargetLabel As Windows.Forms.Label
    Friend WithEvents SelectTargetComboBox As Windows.Forms.ComboBox
    Friend WithEvents PageOrientationLandscapeCheckbox As Windows.Forms.CheckBox
    Friend WithEvents ItemOnNewPageCheckBox As Windows.Forms.CheckBox
    Friend WithEvents TargetSpecificOptionsPanel As Windows.Forms.Panel
End Class
