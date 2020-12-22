<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PropertyFilterEditor
    Inherits FilterEditorBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PropertyFilterEditor))
        Me.EditorTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ValueLabel = New System.Windows.Forms.Label()
        Me.PropertyLabel = New System.Windows.Forms.Label()
        Me.ResourcePropertyDefinitionComboBox = New System.Windows.Forms.ComboBox()
        Me.ResourcePropertyFilterPredicateBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ResourcePropertyDefinitionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperatorLabel = New System.Windows.Forms.Label()
        Me.OpComboBox = New System.Windows.Forms.ComboBox()
        Me.ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ResourcePropertyValuePanel = New System.Windows.Forms.Panel()
        Me.ResourcePropertyFreeValueTextBox = New System.Windows.Forms.TextBox()
        Me.ResourcePropertyListValueDefinitionComboBox = New System.Windows.Forms.ComboBox()
        Me.ResourcePropertyListValueDefinitionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CaptionLabel = New System.Windows.Forms.Label()
        Me.EditorTableLayoutPanel.SuspendLayout()
        CType(Me.ResourcePropertyFilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcePropertyDefinitionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ResourcePropertyValuePanel.SuspendLayout()
        CType(Me.ResourcePropertyListValueDefinitionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.EditorTableLayoutPanel, "EditorTableLayoutPanel")
        Me.EditorTableLayoutPanel.Controls.Add(Me.ValueLabel, 0, 2)
        Me.EditorTableLayoutPanel.Controls.Add(Me.PropertyLabel, 0, 0)
        Me.EditorTableLayoutPanel.Controls.Add(Me.ResourcePropertyDefinitionComboBox, 1, 0)
        Me.EditorTableLayoutPanel.Controls.Add(Me.OperatorLabel, 0, 1)
        Me.EditorTableLayoutPanel.Controls.Add(Me.OpComboBox, 1, 1)
        Me.EditorTableLayoutPanel.Controls.Add(Me.ResourcePropertyValuePanel, 1, 2)
        Me.EditorTableLayoutPanel.Name = "EditorTableLayoutPanel"
        resources.ApplyResources(Me.ValueLabel, "ValueLabel")
        Me.ValueLabel.Name = "ValueLabel"
        resources.ApplyResources(Me.PropertyLabel, "PropertyLabel")
        Me.PropertyLabel.Name = "PropertyLabel"
        Me.ResourcePropertyDefinitionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.ResourcePropertyDefinitionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ResourcePropertyDefinitionComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ResourcePropertyFilterPredicateBindingSource, "PropertyKey", True))
        Me.ResourcePropertyDefinitionComboBox.DataSource = Me.ResourcePropertyDefinitionBindingSource
        Me.ResourcePropertyDefinitionComboBox.DisplayMember = "Title"
        resources.ApplyResources(Me.ResourcePropertyDefinitionComboBox, "ResourcePropertyDefinitionComboBox")
        Me.ResourcePropertyDefinitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ResourcePropertyDefinitionComboBox.DropDownWidth = 200
        Me.ResourcePropertyDefinitionComboBox.Name = "ResourcePropertyDefinitionComboBox"
        Me.ResourcePropertyDefinitionComboBox.ValueMember = "Key"
        Me.ResourcePropertyFilterPredicateBindingSource.DataSource = GetType(ResourcePropertyFilterPredicate)
        Me.ResourcePropertyDefinitionBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.ResourceProperties.ResourcePropertyDefinition)
        resources.ApplyResources(Me.OperatorLabel, "OperatorLabel")
        Me.OperatorLabel.Name = "OperatorLabel"
        resources.ApplyResources(Me.OpComboBox, "OpComboBox")
        Me.OpComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.ResourcePropertyFilterPredicateBindingSource, "Op", True))
        Me.OpComboBox.DataSource = Me.ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource
        Me.OpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OpComboBox.FormattingEnabled = True
        Me.OpComboBox.Name = "OpComboBox"
        Me.ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource.DataSource = GetType(ResourcePropertyFilterPredicate.FilterOperatorEnum)
        resources.ApplyResources(Me.ResourcePropertyValuePanel, "ResourcePropertyValuePanel")
        Me.ResourcePropertyValuePanel.Controls.Add(Me.ResourcePropertyFreeValueTextBox)
        Me.ResourcePropertyValuePanel.Controls.Add(Me.ResourcePropertyListValueDefinitionComboBox)
        Me.ResourcePropertyValuePanel.Name = "ResourcePropertyValuePanel"
        Me.ResourcePropertyFreeValueTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourcePropertyFilterPredicateBindingSource, "Value", True))
        resources.ApplyResources(Me.ResourcePropertyFreeValueTextBox, "ResourcePropertyFreeValueTextBox")
        Me.ResourcePropertyFreeValueTextBox.Name = "ResourcePropertyFreeValueTextBox"
        Me.ResourcePropertyListValueDefinitionComboBox.DisplayMember = "Title"
        resources.ApplyResources(Me.ResourcePropertyListValueDefinitionComboBox, "ResourcePropertyListValueDefinitionComboBox")
        Me.ResourcePropertyListValueDefinitionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ResourcePropertyListValueDefinitionComboBox.FormattingEnabled = True
        Me.ResourcePropertyListValueDefinitionComboBox.Name = "ResourcePropertyListValueDefinitionComboBox"
        Me.ResourcePropertyListValueDefinitionBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.ResourceProperties.ResourcePropertyListValueDefinition)
        resources.ApplyResources(Me.CaptionLabel, "CaptionLabel")
        Me.CaptionLabel.Name = "CaptionLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.EditorTableLayoutPanel)
        Me.Controls.Add(Me.CaptionLabel)
        Me.Name = "PropertyFilterEditor"
        Me.EditorTableLayoutPanel.ResumeLayout(False)
        Me.EditorTableLayoutPanel.PerformLayout()
        CType(Me.ResourcePropertyFilterPredicateBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcePropertyDefinitionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResourcePropertyValuePanel.ResumeLayout(False)
        Me.ResourcePropertyValuePanel.PerformLayout()
        CType(Me.ResourcePropertyListValueDefinitionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EditorTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ResourcePropertyValuePanel As System.Windows.Forms.Panel
    Friend WithEvents ResourcePropertyFreeValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ResourcePropertyListValueDefinitionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PropertyLabel As System.Windows.Forms.Label
    Friend WithEvents ResourcePropertyDefinitionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents OpComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ValueLabel As System.Windows.Forms.Label
    Friend WithEvents OperatorLabel As System.Windows.Forms.Label
    Friend WithEvents CaptionLabel As System.Windows.Forms.Label
    Friend WithEvents ResourcePropertyDefinitionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourcePropertyListValueDefinitionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourcePropertyFilterPredicateBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourcePropertyFilterPredicate_FilterOperatorEnumBindingSource As System.Windows.Forms.BindingSource

End Class
