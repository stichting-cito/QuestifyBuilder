<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResourceMetaDataMultiEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResourceMetaDataMultiEdit))
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.ResourceMetaDataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StateEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StateIdLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.StateIdComboBox = New System.Windows.Forms.ComboBox()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.StateLabel = New System.Windows.Forms.Label()
        CType(Me.ResourceMetaDataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.StateEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanelMain.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.ResourceMetaDataBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity)
        Me.StateEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.StateEntity)
        resources.ApplyResources(Me.StateIdLabel, "StateIdLabel")
        Me.StateIdLabel.Name = "StateIdLabel"
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.StateIdComboBox, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.DescriptionTextBox, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.DescriptionLabel, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.StateLabel, 0, 0)
        Me.TableLayoutPanelMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.StateIdComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ResourceMetaDataBindingSource, "StateId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StateIdComboBox.DataSource = Me.StateEntityBindingSource
        Me.StateIdComboBox.DisplayMember = "Name"
        resources.ApplyResources(Me.StateIdComboBox, "StateIdComboBox")
        Me.StateIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StateIdComboBox.FormattingEnabled = true
        Me.StateIdComboBox.Name = "StateIdComboBox"
        Me.StateIdComboBox.ValueMember = "StateId"
        Me.DescriptionTextBox.AcceptsReturn = true
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "Description", true))
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        resources.ApplyResources(Me.StateLabel, "StateLabel")
        Me.StateLabel.Name = "StateLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "ResourceMetaDataMultiEdit"
        CType(Me.ResourceMetaDataBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.StateEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanelMain.ResumeLayout(false)
        Me.TableLayoutPanelMain.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents ResourceMetaDataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StateIdLabel As System.Windows.Forms.Label
    Friend WithEvents StateEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents StateIdComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents StateLabel As System.Windows.Forms.Label

End Class
