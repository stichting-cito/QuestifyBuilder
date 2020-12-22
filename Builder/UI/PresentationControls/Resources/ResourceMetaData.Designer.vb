<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResourceMetaData
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
        Dim VersionLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResourceMetaData))
        Dim CreatedByLabel As System.Windows.Forms.Label
        Dim CreationDateLabel As System.Windows.Forms.Label
        Dim ModifiedByLabel As System.Windows.Forms.Label
        Dim TypeLabel As System.Windows.Forms.Label
        Dim ModifiedDateLabel As System.Windows.Forms.Label
        Dim DescriptionLabel As System.Windows.Forms.Label
        Me.VersionLabel1 = New System.Windows.Forms.Label()
        Me.ResourceMetaDataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TypeLabel1 = New System.Windows.Forms.Label()
        Me.CreatedByLabel1 = New System.Windows.Forms.Label()
        Me.CreationDateLabel1 = New System.Windows.Forms.Label()
        Me.ModifiedByLabel1 = New System.Windows.Forms.Label()
        Me.StateLabel = New System.Windows.Forms.Label()
        Me.ModifiedDateLabel1 = New System.Windows.Forms.Label()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelCopyData = New System.Windows.Forms.Label()
        Me.StateIdComboBox = New System.Windows.Forms.ComboBox()
        Me.StateEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OpenResourcePropertyDialogButton = New System.Windows.Forms.Button()
        Me.LabelOriginalName = New System.Windows.Forms.Label()
        Me.StateIdLabel = New System.Windows.Forms.Label()
        VersionLabel = New System.Windows.Forms.Label()
        CreatedByLabel = New System.Windows.Forms.Label()
        CreationDateLabel = New System.Windows.Forms.Label()
        ModifiedByLabel = New System.Windows.Forms.Label()
        TypeLabel = New System.Windows.Forms.Label()
        ModifiedDateLabel = New System.Windows.Forms.Label()
        DescriptionLabel = New System.Windows.Forms.Label()
        CType(Me.ResourceMetaDataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelMain.SuspendLayout()
        CType(Me.StateEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(VersionLabel, "VersionLabel")
        VersionLabel.Name = "VersionLabel"
        resources.ApplyResources(CreatedByLabel, "CreatedByLabel")
        CreatedByLabel.Name = "CreatedByLabel"
        resources.ApplyResources(CreationDateLabel, "CreationDateLabel")
        CreationDateLabel.Name = "CreationDateLabel"
        resources.ApplyResources(ModifiedByLabel, "ModifiedByLabel")
        ModifiedByLabel.Name = "ModifiedByLabel"
        resources.ApplyResources(TypeLabel, "TypeLabel")
        TypeLabel.Name = "TypeLabel"
        resources.ApplyResources(ModifiedDateLabel, "ModifiedDateLabel")
        ModifiedDateLabel.Name = "ModifiedDateLabel"
        resources.ApplyResources(DescriptionLabel, "DescriptionLabel")
        DescriptionLabel.Name = "DescriptionLabel"
        resources.ApplyResources(Me.VersionLabel1, "VersionLabel1")
        Me.VersionLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "Version", True))
        Me.VersionLabel1.Name = "VersionLabel1"
        Me.ResourceMetaDataBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.Interfaces.IPropertyEntity)
        resources.ApplyResources(Me.TypeLabel1, "TypeLabel1")
        Me.TypeLabel1.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.TypeLabel1, 2)
        Me.TypeLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "ResourceType", True))
        Me.TypeLabel1.Name = "TypeLabel1"
        resources.ApplyResources(Me.CreatedByLabel1, "CreatedByLabel1")
        Me.CreatedByLabel1.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.CreatedByLabel1, 2)
        Me.CreatedByLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "CreatedByFullName", True))
        Me.CreatedByLabel1.Name = "CreatedByLabel1"
        resources.ApplyResources(Me.CreationDateLabel1, "CreationDateLabel1")
        Me.CreationDateLabel1.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.CreationDateLabel1, 2)
        Me.CreationDateLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "CreationDate", True))
        Me.CreationDateLabel1.Name = "CreationDateLabel1"
        resources.ApplyResources(Me.ModifiedByLabel1, "ModifiedByLabel1")
        Me.ModifiedByLabel1.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.ModifiedByLabel1, 2)
        Me.ModifiedByLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "ModifiedByFullName", True))
        Me.ModifiedByLabel1.Name = "ModifiedByLabel1"
        resources.ApplyResources(Me.StateLabel, "StateLabel")
        Me.StateLabel.Name = "StateLabel"
        resources.ApplyResources(Me.ModifiedDateLabel1, "ModifiedDateLabel1")
        Me.ModifiedDateLabel1.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.ModifiedDateLabel1, 2)
        Me.ModifiedDateLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "ModifiedDate", True))
        Me.ModifiedDateLabel1.Name = "ModifiedDateLabel1"
        Me.DescriptionTextBox.AcceptsReturn = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.DescriptionTextBox, 2)
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "Description", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelCopyData, 1, 7)
        Me.TableLayoutPanelMain.Controls.Add(Me.DescriptionTextBox, 1, 8)
        Me.TableLayoutPanelMain.Controls.Add(ModifiedDateLabel, 0, 6)
        Me.TableLayoutPanelMain.Controls.Add(ModifiedByLabel, 0, 5)
        Me.TableLayoutPanelMain.Controls.Add(Me.ModifiedDateLabel1, 1, 6)
        Me.TableLayoutPanelMain.Controls.Add(CreationDateLabel, 0, 4)
        Me.TableLayoutPanelMain.Controls.Add(CreatedByLabel, 0, 3)
        Me.TableLayoutPanelMain.Controls.Add(TypeLabel, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.ModifiedByLabel1, 1, 5)
        Me.TableLayoutPanelMain.Controls.Add(Me.VersionLabel1, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(VersionLabel, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.CreationDateLabel1, 1, 4)
        Me.TableLayoutPanelMain.Controls.Add(Me.CreatedByLabel1, 1, 3)
        Me.TableLayoutPanelMain.Controls.Add(Me.StateLabel, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TypeLabel1, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.StateIdComboBox, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.OpenResourcePropertyDialogButton, 2, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelOriginalName, 0, 7)
        Me.TableLayoutPanelMain.Controls.Add(DescriptionLabel, 0, 8)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        resources.ApplyResources(Me.LabelCopyData, "LabelCopyData")
        Me.LabelCopyData.AutoEllipsis = True
        Me.TableLayoutPanelMain.SetColumnSpan(Me.LabelCopyData, 2)
        Me.LabelCopyData.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMetaDataBindingSource, "CopiedFromString", True))
        Me.LabelCopyData.Name = "LabelCopyData"
        resources.ApplyResources(Me.StateIdComboBox, "StateIdComboBox")
        Me.TableLayoutPanelMain.SetColumnSpan(Me.StateIdComboBox, 2)
        Me.StateIdComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ResourceMetaDataBindingSource, "StateId", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.StateIdComboBox.DataSource = Me.StateEntityBindingSource
        Me.StateIdComboBox.DisplayMember = "Name"
        Me.StateIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StateIdComboBox.FormattingEnabled = True
        Me.StateIdComboBox.Name = "StateIdComboBox"
        Me.StateIdComboBox.ValueMember = "StateId"
        Me.StateEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.StateEntity)
        resources.ApplyResources(Me.OpenResourcePropertyDialogButton, "OpenResourcePropertyDialogButton")
        Me.OpenResourcePropertyDialogButton.Image = Global.Questify.Builder.UI.My.Resources.Resources.SearchToolStripMenuItem_Image
        Me.OpenResourcePropertyDialogButton.Name = "OpenResourcePropertyDialogButton"
        Me.OpenResourcePropertyDialogButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.LabelOriginalName, "LabelOriginalName")
        Me.LabelOriginalName.Name = "LabelOriginalName"
        resources.ApplyResources(Me.StateIdLabel, "StateIdLabel")
        Me.StateIdLabel.Name = "StateIdLabel"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "ResourceMetaData"
        CType(Me.ResourceMetaDataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        CType(Me.StateEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VersionLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents TypeLabel1 As System.Windows.Forms.Label
    Friend WithEvents StateLabel As System.Windows.Forms.Label
    Friend WithEvents ModifiedDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents StateIdComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents StateIdLabel As System.Windows.Forms.Label
    Friend WithEvents StateEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LabelOriginalName As System.Windows.Forms.Label
    Friend WithEvents LabelCopyData As System.Windows.Forms.Label
    Friend WithEvents ResourceMetaDataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OpenResourcePropertyDialogButton As System.Windows.Forms.Button

End Class
