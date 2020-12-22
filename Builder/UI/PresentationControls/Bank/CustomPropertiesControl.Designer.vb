<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomPropertiesControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim NameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomPropertiesControl))
        Dim TitleLabel As System.Windows.Forms.Label
        Dim PublishableLabel As System.Windows.Forms.Label
        Dim AvailablePropertiesGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim ListValuesGridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.applicableToLabel = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AvailablePropertiesGrid = New Janus.Windows.GridEX.GridEX
        Me.DetailsGroupBox = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ListValuesLabel = New System.Windows.Forms.Label
        Me.PublishableCheckBox = New System.Windows.Forms.CheckBox
        Me.ListValuesGridEX1 = New Janus.Windows.GridEX.GridEX
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.TitleTextBox = New System.Windows.Forms.TextBox
        Me.DeleteValueButton = New System.Windows.Forms.Button
        Me.AddValueButton = New System.Windows.Forms.Button
        Me.AddButton = New System.Windows.Forms.Button
        Me.DeleteButton = New System.Windows.Forms.Button
        Me.CustomPropertyContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FreeValueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListValueSingleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListValueMultiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomPropertyCollectionErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ListValuesErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CustomPropertyCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ApplicableToMaskControl1 = New Cito.TestBuilder.UI.ApplicableToMaskControl
        NameLabel = New System.Windows.Forms.Label
        TitleLabel = New System.Windows.Forms.Label
        PublishableLabel = New System.Windows.Forms.Label
        CType(Me.AvailablePropertiesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DetailsGroupBox.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ListValuesGridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CustomPropertyContextMenu.SuspendLayout()
        CType(Me.CustomPropertyCollectionErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListValuesErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomPropertyCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NameLabel
        '
        resources.ApplyResources(NameLabel, "NameLabel")
        NameLabel.Name = "NameLabel"
        '
        'TitleLabel
        '
        resources.ApplyResources(TitleLabel, "TitleLabel")
        TitleLabel.Name = "TitleLabel"
        '
        'PublishableLabel
        '
        resources.ApplyResources(PublishableLabel, "PublishableLabel")
        PublishableLabel.Name = "PublishableLabel"
        '
        'applicableToLabel
        '
        resources.ApplyResources(Me.applicableToLabel, "applicableToLabel")
        Me.applicableToLabel.Name = "applicableToLabel"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'AvailablePropertiesGrid
        '
        Me.AvailablePropertiesGrid.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        resources.ApplyResources(Me.AvailablePropertiesGrid, "AvailablePropertiesGrid")
        Me.AvailablePropertiesGrid.ColumnAutoResize = True
        Me.AvailablePropertiesGrid.DataSource = Me.CustomPropertyCollectionBindingSource
        resources.ApplyResources(AvailablePropertiesGrid_DesignTimeLayout, "AvailablePropertiesGrid_DesignTimeLayout")
        Me.AvailablePropertiesGrid.DesignTimeLayout = AvailablePropertiesGrid_DesignTimeLayout
        Me.AvailablePropertiesGrid.GroupByBoxVisible = False
        Me.AvailablePropertiesGrid.Name = "AvailablePropertiesGrid"
        '
        'DetailsGroupBox
        '
        resources.ApplyResources(Me.DetailsGroupBox, "DetailsGroupBox")
        Me.DetailsGroupBox.Controls.Add(Me.TableLayoutPanel1)
        Me.DetailsGroupBox.Controls.Add(Me.ApplicableToMaskControl1)
        Me.DetailsGroupBox.Controls.Add(Me.DeleteValueButton)
        Me.DetailsGroupBox.Controls.Add(Me.AddValueButton)
        Me.DetailsGroupBox.Controls.Add(Me.applicableToLabel)
        Me.DetailsGroupBox.Name = "DetailsGroupBox"
        Me.DetailsGroupBox.TabStop = False
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ListValuesLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.PublishableCheckBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ListValuesGridEX1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(PublishableLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(TitleLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(NameLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TitleTextBox, 1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'ListValuesLabel
        '
        resources.ApplyResources(Me.ListValuesLabel, "ListValuesLabel")
        Me.ListValuesLabel.Name = "ListValuesLabel"
        '
        'PublishableCheckBox
        '
        resources.ApplyResources(Me.PublishableCheckBox, "PublishableCheckBox")
        Me.PublishableCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.CustomPropertyCollectionBindingSource, "Publishable", True))
        Me.PublishableCheckBox.Name = "PublishableCheckBox"
        '
        'ListValuesGridEX1
        '
        Me.ListValuesGridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        resources.ApplyResources(Me.ListValuesGridEX1, "ListValuesGridEX1")
        Me.ListValuesGridEX1.ColumnAutoResize = True
        resources.ApplyResources(ListValuesGridEX1_DesignTimeLayout, "ListValuesGridEX1_DesignTimeLayout")
        Me.ListValuesGridEX1.DesignTimeLayout = ListValuesGridEX1_DesignTimeLayout
        Me.ListValuesGridEX1.GroupByBoxVisible = False
        Me.ListValuesGridEX1.Name = "ListValuesGridEX1"
        '
        'NameTextBox
        '
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CustomPropertyCollectionBindingSource, "Name", True))
        Me.NameTextBox.Name = "NameTextBox"
        '
        'TitleTextBox
        '
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CustomPropertyCollectionBindingSource, "Title", True))
        Me.TitleTextBox.Name = "TitleTextBox"
        '
        'DeleteValueButton
        '
        resources.ApplyResources(Me.DeleteValueButton, "DeleteValueButton")
        Me.DeleteValueButton.Name = "DeleteValueButton"
        Me.DeleteValueButton.UseVisualStyleBackColor = True
        '
        'AddValueButton
        '
        resources.ApplyResources(Me.AddValueButton, "AddValueButton")
        Me.AddValueButton.Name = "AddValueButton"
        Me.AddValueButton.UseVisualStyleBackColor = True
        '
        'AddButton
        '
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Name = "AddButton"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        resources.ApplyResources(Me.DeleteButton, "DeleteButton")
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'CustomPropertyContextMenu
        '
        Me.CustomPropertyContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FreeValueToolStripMenuItem, Me.ListValueSingleToolStripMenuItem, Me.ListValueMultiToolStripMenuItem})
        Me.CustomPropertyContextMenu.Name = "ContextMenuStrip1"
        Me.CustomPropertyContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.CustomPropertyContextMenu.ShowImageMargin = False
        resources.ApplyResources(Me.CustomPropertyContextMenu, "CustomPropertyContextMenu")
        '
        'FreeValueToolStripMenuItem
        '
        Me.FreeValueToolStripMenuItem.Name = "FreeValueToolStripMenuItem"
        resources.ApplyResources(Me.FreeValueToolStripMenuItem, "FreeValueToolStripMenuItem")
        '
        'ListValueSingleToolStripMenuItem
        '
        Me.ListValueSingleToolStripMenuItem.Name = "ListValueSingleToolStripMenuItem"
        resources.ApplyResources(Me.ListValueSingleToolStripMenuItem, "ListValueSingleToolStripMenuItem")
        '
        'ListValueMultiToolStripMenuItem
        '
        Me.ListValueMultiToolStripMenuItem.Name = "ListValueMultiToolStripMenuItem"
        resources.ApplyResources(Me.ListValueMultiToolStripMenuItem, "ListValueMultiToolStripMenuItem")
        '
        'CustomPropertyCollectionErrorProvider
        '
        Me.CustomPropertyCollectionErrorProvider.ContainerControl = Me
        Me.CustomPropertyCollectionErrorProvider.DataSource = Me.CustomPropertyCollectionBindingSource
        '
        'ListValuesErrorProvider
        '
        Me.ListValuesErrorProvider.ContainerControl = Me
        '
        'CustomPropertyCollectionBindingSource
        '
        Me.CustomPropertyCollectionBindingSource.DataSource = GetType(Cito.TestBuilder.ContentModel.EntityClasses.CustomBankPropertyEntity)
        '
        'ApplicableToMaskControl1
        '
        resources.ApplyResources(Me.ApplicableToMaskControl1, "ApplicableToMaskControl1")
        Me.ApplicableToMaskControl1.DataBindings.Add(New System.Windows.Forms.Binding("Mask", Me.CustomPropertyCollectionBindingSource, "ApplicableToMask", True))
        Me.ApplicableToMaskControl1.Mask = CType(0, Long)
        Me.ApplicableToMaskControl1.Name = "ApplicableToMaskControl1"
        Me.ApplicableToMaskControl1.TabStop = False
        '
        'CustomPropertiesControl
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.AvailablePropertiesGrid)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.DetailsGroupBox)
        Me.Controls.Add(Me.AddButton)
        Me.Controls.Add(Me.Label1)
        Me.Name = "CustomPropertiesControl"
        CType(Me.AvailablePropertiesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DetailsGroupBox.ResumeLayout(False)
        Me.DetailsGroupBox.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ListValuesGridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CustomPropertyContextMenu.ResumeLayout(False)
        CType(Me.CustomPropertyCollectionErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListValuesErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomPropertyCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents applicableToLabel As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AvailablePropertiesGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents DetailsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents CustomPropertyContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FreeValueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListValueSingleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListValueMultiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CustomPropertyCollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DeleteValueButton As System.Windows.Forms.Button
    Friend WithEvents AddValueButton As System.Windows.Forms.Button
    Friend WithEvents ListValuesGridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents CustomPropertyCollectionErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents ListValuesErrorProvider As System.Windows.Forms.ErrorProvider
    'Friend WithEvents ApplicableToMaskControl1 As System.Windows.Forms.Panel
    Friend WithEvents ApplicableToMaskControl1 As ApplicableToMaskControl
    Friend WithEvents ListValuesLabel As System.Windows.Forms.Label
    Friend WithEvents PublishableCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
