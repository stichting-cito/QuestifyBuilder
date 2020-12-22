<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConceptValueDetailsUserControl
    Inherits CustomPropertyUserControlBase

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConceptValueDetailsUserControl))
        Me.ConceptCustomBankPropertyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.LabelType = New System.Windows.Forms.Label()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.ComboBoxTypes = New System.Windows.Forms.ComboBox()
        Me.ConceptStructureGrid = New Questify.Builder.UI.ConceptStructureGrid()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.NameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TitleErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ConceptCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelMain.SuspendLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ConceptCustomBankPropertyBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructurePartCustomBankPropertyEntity)
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTitle, 2, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelType, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTitle, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.ComboBoxTypes, 2, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.ConceptStructureGrid, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelName, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxName, 2, 0)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.TextBoxTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConceptCustomBankPropertyBindingSource, "Title", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.TextBoxTitle.Name = "TextBoxTitle"
        resources.ApplyResources(Me.LabelType, "LabelType")
        Me.LabelType.Name = "LabelType"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.LabelTitle.Name = "LabelTitle"
        Me.ComboBoxTypes.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ConceptCustomBankPropertyBindingSource, "ConceptType", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ComboBoxTypes.DisplayMember = "Value"
        resources.ApplyResources(Me.ComboBoxTypes, "ComboBoxTypes")
        Me.ComboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTypes.FormattingEnabled = True
        Me.ComboBoxTypes.Name = "ComboBoxTypes"
        Me.ComboBoxTypes.ValueMember = "Key"
        resources.ApplyResources(Me.ConceptStructureGrid, "ConceptStructureGrid")
        Me.ConceptStructureGrid.Name = "ConceptStructureGrid"
        Me.TableLayoutPanelMain.SetRowSpan(Me.ConceptStructureGrid, 4)
        resources.ApplyResources(Me.LabelName, "LabelName")
        Me.LabelName.Name = "LabelName"
        Me.TextBoxName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConceptCustomBankPropertyBindingSource, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.TextBoxName, "TextBoxName")
        Me.TextBoxName.Name = "TextBoxName"
        Me.NameErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.NameErrorProvider.ContainerControl = Me
        Me.TitleErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.TitleErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "ConceptValueDetailsUserControl"
        CType(Me.ConceptCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxTypes As System.Windows.Forms.ComboBox
    Friend WithEvents ConceptStructureGrid As Questify.Builder.UI.ConceptStructureGrid
    Friend WithEvents ConceptCustomBankPropertyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TitleErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelType As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents TextBoxName As TextBox
End Class
