<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MultiSelectItemEditWizardForm
    Inherits Questify.Builder.UI.WizardBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiSelectItemEditWizardForm))
        Dim AvailableParametersGrid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim AvailablePropertiesGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim MetaDataGrid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.TabPageControl1 = New System.Windows.Forms.TabPage
        Me.SelectEditTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.TableLayoutPanelSelectEdit = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelCustomProperties = New System.Windows.Forms.Label()
        Me.AvailableParametersGrid = New Janus.Windows.GridEX.GridEX()
        Me.LabelParameters = New System.Windows.Forms.Label()
        Me.AvailablePropertiesGrid = New Janus.Windows.GridEX.GridEX()
        Me.MetaDataGrid = New Janus.Windows.GridEX.GridEX()
        Me.LabelMetaDataProperties = New System.Windows.Forms.Label()
        Me.TabPageControl2 = New System.Windows.Forms.TabPage
        Me.OverwriteTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.OverwriteExistingCPListMultiValuesCheckbox = New System.Windows.Forms.CheckBox()
        Me.FileSelectionTabPageControl = New System.Windows.Forms.TabPage
        Me.EditTabContent = New Questify.Builder.UI.WizardTabContentControl()
        Me.PanelEditPE = New System.Windows.Forms.Panel()
        Me.paramSetsEditor = New Questify.Builder.UI.ParameterSetsEditor()
        Me.TableLayoutPanelEditCP = New System.Windows.Forms.TableLayoutPanel()
        Me.resourceCustomPropsEditor = New Questify.Builder.UI.ResourceCustomProperties()
        Me.TableLayoutPanelEditMD = New System.Windows.Forms.TableLayoutPanel()
        Me.ResourceMetaDataMultiEditInstance = New Questify.Builder.UI.ResourceMetaDataMultiEdit()
        Me.ResourceCustomProperties1 = New Questify.Builder.UI.ResourceCustomProperties()
        Me.CustomPropertyCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ExportOptionControlBase1 = New Questify.Builder.Client.ExportOptionControlBase()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageControl1.SuspendLayout()
        Me.SelectEditTabContent.SuspendLayout()
        Me.TableLayoutPanelSelectEdit.SuspendLayout()
        CType(Me.AvailableParametersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AvailablePropertiesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MetaDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageControl2.SuspendLayout()
        Me.OverwriteTabContent.SuspendLayout()
        Me.FileSelectionTabPageControl.SuspendLayout()
        Me.EditTabContent.SuspendLayout()
        Me.PanelEditPE.SuspendLayout()
        Me.TableLayoutPanelEditCP.SuspendLayout()
        Me.TableLayoutPanelEditMD.SuspendLayout()
        CType(Me.CustomPropertyCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        Me.TabControlMain.Controls.Add(Me.TabPageControl1)
        Me.TabControlMain.Controls.Add(Me.TabPageControl2)
        Me.TabControlMain.Controls.Add(Me.FileSelectionTabPageControl)
        Me.TabPageControl1.Controls.Add(Me.SelectEditTabContent)
        resources.ApplyResources(Me.TabPageControl1, "TabPageControl1")
        Me.TabPageControl1.Name = "TabPageControl1"
        Me.TabPageControl1.Tag = "SelectEdit"
        resources.ApplyResources(Me.SelectEditTabContent, "SelectEditTabContent")
        Me.SelectEditTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.SelectEditTabContent.Controls.Add(Me.TableLayoutPanelSelectEdit)
        Me.SelectEditTabContent.Name = "SelectEditTabContent"
        Me.SelectEditTabContent.Task = "Task"
        Me.SelectEditTabContent.TaskDescription = "Description"
        Me.SelectEditTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.SelectEditTabContent.TaskPanelBackgroundImage = Nothing
        Me.SelectEditTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.SelectEditTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.TableLayoutPanelSelectEdit, "TableLayoutPanelSelectEdit")
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.LabelCustomProperties, 1, 0)
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.AvailableParametersGrid, 0, 1)
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.LabelParameters, 0, 0)
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.AvailablePropertiesGrid, 1, 1)
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.MetaDataGrid, 1, 3)
        Me.TableLayoutPanelSelectEdit.Controls.Add(Me.LabelMetaDataProperties, 1, 2)
        Me.TableLayoutPanelSelectEdit.Name = "TableLayoutPanelSelectEdit"
        resources.ApplyResources(Me.LabelCustomProperties, "LabelCustomProperties")
        Me.LabelCustomProperties.Name = "LabelCustomProperties"
        Me.AvailableParametersGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        resources.ApplyResources(Me.AvailableParametersGrid, "AvailableParametersGrid")
        Me.AvailableParametersGrid.ColumnAutoResize = True
        Me.AvailableParametersGrid.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.AvailableParametersGrid.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.AvailableParametersGrid.GroupByBoxVisible = False
        AvailableParametersGrid_Layout_0.IsCurrentLayout = True
        AvailableParametersGrid_Layout_0.Key = "tmp"
        resources.ApplyResources(AvailableParametersGrid_Layout_0, "AvailableParametersGrid_Layout_0")
        Me.AvailableParametersGrid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {AvailableParametersGrid_Layout_0})
        Me.AvailableParametersGrid.Name = "AvailableParametersGrid"
        Me.TableLayoutPanelSelectEdit.SetRowSpan(Me.AvailableParametersGrid, 3)
        resources.ApplyResources(Me.LabelParameters, "LabelParameters")
        Me.LabelParameters.Name = "LabelParameters"
        Me.AvailablePropertiesGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        resources.ApplyResources(Me.AvailablePropertiesGrid, "AvailablePropertiesGrid")
        Me.AvailablePropertiesGrid.ColumnAutoResize = True
        resources.ApplyResources(AvailablePropertiesGrid_DesignTimeLayout, "AvailablePropertiesGrid_DesignTimeLayout")
        Me.AvailablePropertiesGrid.DesignTimeLayout = AvailablePropertiesGrid_DesignTimeLayout
        Me.AvailablePropertiesGrid.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.AvailablePropertiesGrid.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.AvailablePropertiesGrid.GroupByBoxVisible = False
        Me.AvailablePropertiesGrid.Name = "AvailablePropertiesGrid"
        Me.MetaDataGrid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        resources.ApplyResources(Me.MetaDataGrid, "MetaDataGrid")
        Me.MetaDataGrid.ColumnAutoResize = True
        Me.MetaDataGrid.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.MetaDataGrid.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.MetaDataGrid.GroupByBoxVisible = False
        MetaDataGrid_Layout_0.IsCurrentLayout = True
        MetaDataGrid_Layout_0.Key = "tmp"
        resources.ApplyResources(MetaDataGrid_Layout_0, "MetaDataGrid_Layout_0")
        Me.MetaDataGrid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {MetaDataGrid_Layout_0})
        Me.MetaDataGrid.Name = "MetaDataGrid"
        Me.TableLayoutPanelSelectEdit.SetRowSpan(Me.MetaDataGrid, 2)
        resources.ApplyResources(Me.LabelMetaDataProperties, "LabelMetaDataProperties")
        Me.LabelMetaDataProperties.Name = "LabelMetaDataProperties"
        Me.TabPageControl2.Controls.Add(Me.OverwriteTabContent)
        resources.ApplyResources(Me.TabPageControl2, "TabPageControl2")
        Me.TabPageControl2.Name = "TabPageControl2"
        Me.TabPageControl2.Tag = "Overwrite"
        resources.ApplyResources(Me.OverwriteTabContent, "OverwriteTabContent")
        Me.OverwriteTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.OverwriteTabContent.Controls.Add(Me.OverwriteExistingCPListMultiValuesCheckbox)
        Me.OverwriteTabContent.Name = "OverwriteTabContent"
        Me.OverwriteTabContent.Task = "Task"
        Me.OverwriteTabContent.TaskDescription = "Description"
        Me.OverwriteTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.OverwriteTabContent.TaskPanelBackgroundImage = Nothing
        Me.OverwriteTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.OverwriteTabContent.TaskPanelHeight = 72
        resources.ApplyResources(Me.OverwriteExistingCPListMultiValuesCheckbox, "OverwriteExistingCPListMultiValuesCheckbox")
        Me.OverwriteExistingCPListMultiValuesCheckbox.Name = "OverwriteExistingCPListMultiValuesCheckbox"
        Me.OverwriteExistingCPListMultiValuesCheckbox.UseVisualStyleBackColor = True
        Me.FileSelectionTabPageControl.Controls.Add(Me.EditTabContent)
        resources.ApplyResources(Me.FileSelectionTabPageControl, "FileSelectionTabPageControl")
        Me.FileSelectionTabPageControl.Name = "FileSelectionTabPageControl"
        Me.FileSelectionTabPageControl.Tag = "Edit"
        resources.ApplyResources(Me.EditTabContent, "EditTabContent")
        Me.EditTabContent.BackColor = System.Drawing.SystemColors.Control
        Me.EditTabContent.Controls.Add(Me.PanelEditPE)
        Me.EditTabContent.Controls.Add(Me.TableLayoutPanelEditCP)
        Me.EditTabContent.Controls.Add(Me.TableLayoutPanelEditMD)
        Me.EditTabContent.Name = "EditTabContent"
        Me.EditTabContent.Task = "Task"
        Me.EditTabContent.TaskDescription = "Description"
        Me.EditTabContent.TaskPanelBackColor = System.Drawing.Color.White
        Me.EditTabContent.TaskPanelBackgroundImage = Nothing
        Me.EditTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
        Me.EditTabContent.TaskPanelHeight = 72
        Me.PanelEditPE.Controls.Add(Me.paramSetsEditor)
        resources.ApplyResources(Me.PanelEditPE, "PanelEditPE")
        Me.PanelEditPE.Name = "PanelEditPE"
        resources.ApplyResources(Me.paramSetsEditor, "paramSetsEditor")
        Me.paramSetsEditor.ContextIdentifierForEditors = Nothing
        Me.paramSetsEditor.FilterParameter = Nothing
        Me.paramSetsEditor.HasLoadedOldItemLayoutTemplate = False
        Me.paramSetsEditor.Name = "paramSetsEditor"
        Me.paramSetsEditor.ParameterSets = Nothing
        Me.paramSetsEditor.ResourceEntity = Nothing
        Me.paramSetsEditor.ResourceManager = Nothing
        Me.paramSetsEditor.ShouldSort = False
        Me.paramSetsEditor.Solution = Nothing
        resources.ApplyResources(Me.TableLayoutPanelEditCP, "TableLayoutPanelEditCP")
        Me.TableLayoutPanelEditCP.Controls.Add(Me.resourceCustomPropsEditor, 1, 0)
        Me.TableLayoutPanelEditCP.Name = "TableLayoutPanelEditCP"
        resources.ApplyResources(Me.resourceCustomPropsEditor, "resourceCustomPropsEditor")
        Me.resourceCustomPropsEditor.CustomPropertyFilter = Nothing
        Me.resourceCustomPropsEditor.Name = "resourceCustomPropsEditor"
        Me.resourceCustomPropsEditor.ResourceEntity = Nothing
        resources.ApplyResources(Me.TableLayoutPanelEditMD, "TableLayoutPanelEditMD")
        Me.TableLayoutPanelEditMD.Controls.Add(Me.ResourceMetaDataMultiEditInstance, 0, 0)
        Me.TableLayoutPanelEditMD.Controls.Add(Me.ResourceCustomProperties1, 1, 0)
        Me.TableLayoutPanelEditMD.Name = "TableLayoutPanelEditMD"
        Me.ResourceMetaDataMultiEditInstance.DisplayField = Questify.Builder.UI.ResourceMetaDataMultiEdit.FieldName.State
        resources.ApplyResources(Me.ResourceMetaDataMultiEditInstance, "ResourceMetaDataMultiEditInstance")
        Me.ResourceMetaDataMultiEditInstance.Name = "ResourceMetaDataMultiEditInstance"
        Me.ResourceMetaDataMultiEditInstance.ResourceEntity = Nothing
        resources.ApplyResources(Me.ResourceCustomProperties1, "ResourceCustomProperties1")
        Me.ResourceCustomProperties1.CustomPropertyFilter = Nothing
        Me.ResourceCustomProperties1.Name = "ResourceCustomProperties1"
        Me.ResourceCustomProperties1.ResourceEntity = Nothing
        Me.CustomPropertyCollectionBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        resources.ApplyResources(Me.ExportOptionControlBase1, "ExportOptionControlBase1")
        Me.ExportOptionControlBase1.Name = "ExportOptionControlBase1"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ExportOptionControlBase1)
        Me.Name = "MultiSelectItemEditWizardForm"
        Me.ProgressMinimumStepsRequired = 3
        Me.Controls.SetChildIndex(Me.ExportOptionControlBase1, 0)
        Me.Controls.SetChildIndex(Me.TabControlMain, 0)
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageControl1.ResumeLayout(False)
        Me.TabPageControl1.PerformLayout()
        Me.SelectEditTabContent.ResumeLayout(False)
        Me.TableLayoutPanelSelectEdit.ResumeLayout(False)
        Me.TableLayoutPanelSelectEdit.PerformLayout()
        CType(Me.AvailableParametersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AvailablePropertiesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MetaDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageControl2.ResumeLayout(False)
        Me.TabPageControl2.PerformLayout()
        Me.OverwriteTabContent.ResumeLayout(False)
        Me.OverwriteTabContent.PerformLayout()
        Me.FileSelectionTabPageControl.ResumeLayout(False)
        Me.FileSelectionTabPageControl.PerformLayout()
        Me.EditTabContent.ResumeLayout(False)
        Me.PanelEditPE.ResumeLayout(False)
        Me.PanelEditPE.PerformLayout()
        Me.TableLayoutPanelEditCP.ResumeLayout(False)
        Me.TableLayoutPanelEditMD.ResumeLayout(False)
        CType(Me.CustomPropertyCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FileSelectionTabPageControl As System.Windows.Forms.TabPage
    Friend WithEvents EditTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents TabPageControl1 As System.Windows.Forms.TabPage
    Friend WithEvents SelectEditTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents ExportOptionControlBase1 As Questify.Builder.Client.ExportOptionControlBase
    Friend WithEvents TableLayoutPanelSelectEdit As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CustomPropertyCollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AvailableParametersGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents AvailablePropertiesGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents LabelCustomProperties As System.Windows.Forms.Label
    Friend WithEvents LabelParameters As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanelEditCP As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents resourceCustomPropsEditor As Questify.Builder.UI.ResourceCustomProperties
    Friend WithEvents PanelEditPE As System.Windows.Forms.Panel
    Friend WithEvents paramSetsEditor As Questify.Builder.UI.ParameterSetsEditor
    Friend WithEvents MetaDataGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents LabelMetaDataProperties As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanelEditMD As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ResourceCustomProperties1 As Questify.Builder.UI.ResourceCustomProperties
    Friend WithEvents ResourceMetaDataMultiEditInstance As Questify.Builder.UI.ResourceMetaDataMultiEdit
    Friend WithEvents TabPageControl2 As System.Windows.Forms.TabPage
    Friend WithEvents OverwriteTabContent As Questify.Builder.UI.WizardTabContentControl
    Friend WithEvents OverwriteExistingCPListMultiValuesCheckbox As System.Windows.Forms.CheckBox

End Class
