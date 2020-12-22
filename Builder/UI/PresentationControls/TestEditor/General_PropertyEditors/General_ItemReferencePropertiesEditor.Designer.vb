Imports Cito.Tester.ContentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class General_ItemReferencePropertiesEditor
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
        Dim TitleLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_ItemReferencePropertiesEditor))
        Dim ActiveLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Me.WeightLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.ItemFunctionalTypeCombobox = New System.Windows.Forms.ComboBox()
        Me.ActiveCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.IsAnchorItemCheckBox = New System.Windows.Forms.CheckBox()
        Me.WeightTextBox = New System.Windows.Forms.TextBox()
        TitleLabel = New System.Windows.Forms.Label()
        ActiveLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        Me.ControlBindingSource.DataSource = GetType(ItemReference2)
        resources.ApplyResources(TitleLabel, "TitleLabel")
        TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(ActiveLabel, "ActiveLabel")
        ActiveLabel.Name = "ActiveLabel"
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        resources.ApplyResources(Me.WeightLabel, "WeightLabel")
        Me.WeightLabel.Name = "WeightLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(TitleLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TitleTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ItemFunctionalTypeCombobox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ActiveCheckBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(ActiveLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.IsAnchorItemCheckBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.WeightLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.WeightTextBox, 1, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Title", True))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.ItemFunctionalTypeCombobox, "ItemFunctionalTypeCombobox")
        Me.ItemFunctionalTypeCombobox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ControlBindingSource, "ItemFunctionalType", True))
        Me.ItemFunctionalTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItemFunctionalTypeCombobox.FormattingEnabled = True
        Me.ItemFunctionalTypeCombobox.Name = "ItemFunctionalTypeCombobox"
        resources.ApplyResources(Me.ActiveCheckBox, "ActiveCheckBox")
        Me.ActiveCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.ControlBindingSource, "Active", True))
        Me.ActiveCheckBox.Name = "ActiveCheckBox"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.IsAnchorItemCheckBox, "IsAnchorItemCheckBox")
        Me.IsAnchorItemCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.ControlBindingSource, "IsAnchorItem", True))
        Me.IsAnchorItemCheckBox.Name = "IsAnchorItemCheckBox"
        Me.IsAnchorItemCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.WeightTextBox, "WeightTextBox")
        Me.WeightTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Weight", True))
        Me.WeightTextBox.Name = "WeightTextBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "General_ItemReferencePropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ActiveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ItemFunctionalTypeCombobox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents IsAnchorItemCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WeightTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WeightLabel As System.Windows.Forms.Label

End Class
