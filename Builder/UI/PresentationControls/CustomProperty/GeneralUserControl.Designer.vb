<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneralUserControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralUserControl))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.LabelApplicable = New System.Windows.Forms.Label()
        Me.LabelPublishable = New System.Windows.Forms.Label()
        Me.CheckBoxPublishable = New System.Windows.Forms.CheckBox()
        Me.BindingSourceCustomProperty = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelScorable = New System.Windows.Forms.Label()
        Me.CheckBoxScorable = New System.Windows.Forms.CheckBox()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.ApplicableToMaskControl = New Questify.Builder.UI.ApplicableToMaskControl()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.BindingSourceCustomProperty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.LabelTitle, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelApplicable, 0, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LabelPublishable, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBoxPublishable, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.LabelScorable, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBoxScorable, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxTitle, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.ApplicableToMaskControl, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LabelName, 0, 0)
        Me.ErrorProvider1.SetError(Me.TableLayoutPanel, resources.GetString("TableLayoutPanel.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.TableLayoutPanel, CType(resources.GetObject("TableLayoutPanel.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.TableLayoutPanel, CType(resources.GetObject("TableLayoutPanel.IconPadding"), Integer))
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.ErrorProvider1.SetError(Me.LabelTitle, resources.GetString("LabelTitle.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconPadding"), Integer))
        Me.LabelTitle.Name = "LabelTitle"
        resources.ApplyResources(Me.LabelApplicable, "LabelApplicable")
        Me.ErrorProvider1.SetError(Me.LabelApplicable, resources.GetString("LabelApplicable.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.LabelApplicable, CType(resources.GetObject("LabelApplicable.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.LabelApplicable, CType(resources.GetObject("LabelApplicable.IconPadding"), Integer))
        Me.LabelApplicable.Name = "LabelApplicable"
        resources.ApplyResources(Me.LabelPublishable, "LabelPublishable")
        Me.ErrorProvider1.SetError(Me.LabelPublishable, resources.GetString("LabelPublishable.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.LabelPublishable, CType(resources.GetObject("LabelPublishable.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.LabelPublishable, CType(resources.GetObject("LabelPublishable.IconPadding"), Integer))
        Me.LabelPublishable.Name = "LabelPublishable"
        resources.ApplyResources(Me.CheckBoxPublishable, "CheckBoxPublishable")
        Me.CheckBoxPublishable.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSourceCustomProperty, "Publishable", True))
        Me.CheckBoxPublishable.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.BindingSourceCustomProperty, "Publishable", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ErrorProvider1.SetError(Me.CheckBoxPublishable, resources.GetString("CheckBoxPublishable.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.CheckBoxPublishable, CType(resources.GetObject("CheckBoxPublishable.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.CheckBoxPublishable, CType(resources.GetObject("CheckBoxPublishable.IconPadding"), Integer))
        Me.CheckBoxPublishable.Name = "CheckBoxPublishable"
        Me.CheckBoxPublishable.UseVisualStyleBackColor = True
        Me.BindingSourceCustomProperty.AllowNew = False
        Me.BindingSourceCustomProperty.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity)
        resources.ApplyResources(Me.LabelScorable, "LabelScorable")
        Me.ErrorProvider1.SetError(Me.LabelScorable, resources.GetString("LabelScorable.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.LabelScorable, CType(resources.GetObject("LabelScorable.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.LabelScorable, CType(resources.GetObject("LabelScorable.IconPadding"), Integer))
        Me.LabelScorable.Name = "LabelScorable"
        resources.ApplyResources(Me.CheckBoxScorable, "CheckBoxScorable")
        Me.CheckBoxScorable.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSourceCustomProperty, "Scorable", True))
        Me.CheckBoxScorable.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.BindingSourceCustomProperty, "Scorable", True))
        Me.ErrorProvider1.SetError(Me.CheckBoxScorable, resources.GetString("CheckBoxScorable.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.CheckBoxScorable, CType(resources.GetObject("CheckBoxScorable.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.CheckBoxScorable, CType(resources.GetObject("CheckBoxScorable.IconPadding"), Integer))
        Me.CheckBoxScorable.Name = "CheckBoxScorable"
        Me.CheckBoxScorable.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.TextBoxTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSourceCustomProperty, "Title", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ErrorProvider1.SetError(Me.TextBoxTitle, resources.GetString("TextBoxTitle.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconPadding"), Integer))
        Me.TextBoxTitle.Name = "TextBoxTitle"
        resources.ApplyResources(Me.TextBoxName, "TextBoxName")
        Me.TextBoxName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSourceCustomProperty, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ErrorProvider1.SetError(Me.TextBoxName, resources.GetString("TextBoxName.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconPadding"), Integer))
        Me.TextBoxName.Name = "TextBoxName"
        resources.ApplyResources(Me.ApplicableToMaskControl, "ApplicableToMaskControl")
        Me.ApplicableToMaskControl.DataBindings.Add(New System.Windows.Forms.Binding("Mask", Me.BindingSourceCustomProperty, "ApplicableToMask", True))
        Me.ErrorProvider1.SetError(Me.ApplicableToMaskControl, resources.GetString("ApplicableToMaskControl.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.ApplicableToMaskControl, CType(resources.GetObject("ApplicableToMaskControl.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.ApplicableToMaskControl, CType(resources.GetObject("ApplicableToMaskControl.IconPadding"), Integer))
        Me.ApplicableToMaskControl.Mask = CType(0, Long)
        Me.ApplicableToMaskControl.Name = "ApplicableToMaskControl"
        Me.ApplicableToMaskControl.TabStop = False
        resources.ApplyResources(Me.LabelName, "LabelName")
        Me.ErrorProvider1.SetError(Me.LabelName, resources.GetString("LabelName.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me.LabelName, CType(resources.GetObject("LabelName.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me.LabelName, CType(resources.GetObject("LabelName.IconPadding"), Integer))
        Me.LabelName.Name = "LabelName"
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.DataSource = Me.BindingSourceCustomProperty
        resources.ApplyResources(Me.ErrorProvider1, "ErrorProvider1")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.ErrorProvider1.SetError(Me, resources.GetString("$this.Error"))
        Me.ErrorProvider1.SetIconAlignment(Me, CType(resources.GetObject("$this.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.ErrorProvider1.SetIconPadding(Me, CType(resources.GetObject("$this.IconPadding"), Integer))
        Me.Name = "GeneralUserControl"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.BindingSourceCustomProperty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelName As System.Windows.Forms.Label
    Friend WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents LabelApplicable As System.Windows.Forms.Label
    Friend WithEvents LabelPublishable As System.Windows.Forms.Label
    Friend WithEvents CheckBoxPublishable As System.Windows.Forms.CheckBox
    Friend WithEvents ApplicableToMaskControl As Questify.Builder.UI.ApplicableToMaskControl
    Friend WithEvents BindingSourceCustomProperty As System.Windows.Forms.BindingSource
    Public WithEvents TextBoxTitle As System.Windows.Forms.TextBox
    Public WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents LabelScorable As Label
    Friend WithEvents CheckBoxScorable As CheckBox
End Class
