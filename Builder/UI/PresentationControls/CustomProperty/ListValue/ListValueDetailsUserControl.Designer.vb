<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListValueDetailsUserControl
    Inherits CustomPropertyUserControlBase

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListValueDetailsUserControl))
        Me.ListCustomBankPropertyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.ListValueDetailsGrid = New Questify.Builder.UI.ListValueDetailsGrid()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.NameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TitleErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ListCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ListCustomBankPropertyBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ListCustomBankPropertyEntity)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxTitle, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelTitle, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ListValueDetailsGrid, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelName, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxName, 2, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TextBoxTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ListCustomBankPropertyBindingSource, "Title", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.TextBoxTitle.Name = "TextBoxTitle"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.LabelTitle.Name = "LabelTitle"
        resources.ApplyResources(Me.ListValueDetailsGrid, "ListValueDetailsGrid")
        Me.ListValueDetailsGrid.Name = "ListValueDetailsGrid"
        Me.TableLayoutPanel1.SetRowSpan(Me.ListValueDetailsGrid, 3)
        resources.ApplyResources(Me.LabelName, "LabelName")
        Me.LabelName.Name = "LabelName"
        Me.TextBoxName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ListCustomBankPropertyBindingSource, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.TextBoxName, "TextBoxName")
        Me.TextBoxName.Name = "TextBoxName"
        Me.NameErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.NameErrorProvider.ContainerControl = Me
        Me.TitleErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.TitleErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ListValueDetailsUserControl"
        CType(Me.ListCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ListValueDetailsGrid As Questify.Builder.UI.ListValueDetailsGrid
    Friend WithEvents ListCustomBankPropertyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TitleErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents TextBoxName As TextBox
End Class
