<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeValueDetailsUserControl
    Inherits CustomPropertyUserControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TreeValueDetailsUserControl))
        Me.TreeCustomBankPropertyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.TreeStructureGrid = New Questify.Builder.UI.TreeStructureGrid()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.NameErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TitleErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.TreeCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelMain.SuspendLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.TreeCustomBankPropertyBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.TreeStructurePartCustomBankPropertyEntity)
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTitle, 2, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTitle, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.TreeStructureGrid, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelName, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxName, 2, 0)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.TextBoxTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TreeCustomBankPropertyBindingSource, "Title", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.TextBoxTitle.Name = "TextBoxTitle"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.LabelTitle.Name = "LabelTitle"
        resources.ApplyResources(Me.TreeStructureGrid, "TreeStructureGrid")
        Me.TreeStructureGrid.Name = "TreeStructureGrid"
        Me.TableLayoutPanelMain.SetRowSpan(Me.TreeStructureGrid, 3)
        resources.ApplyResources(Me.LabelName, "LabelName")
        Me.LabelName.Name = "LabelName"
        Me.TextBoxName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TreeCustomBankPropertyBindingSource, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
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
        Me.Name = "TreeValueDetailsUserControl"
        CType(Me.TreeCustomBankPropertyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        CType(Me.NameErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeStructureGrid As Questify.Builder.UI.TreeStructureGrid
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TreeCustomBankPropertyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NameErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TitleErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents TextBoxName As TextBox
End Class
