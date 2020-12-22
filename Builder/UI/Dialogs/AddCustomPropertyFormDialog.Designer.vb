

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddCustomPropertyFormDialog
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddCustomPropertyFormDialog))
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.LabelType = New System.Windows.Forms.Label()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.ComboBoxTypes = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.CodeErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TitleErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CodeErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTitle, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelType, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTitle, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.ComboBoxTypes, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TableLayoutPanel1, 1, 3)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelName, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxName, 1, 0)
        Me.TitleErrorProvider.SetError(Me.TableLayoutPanelMain, resources.GetString("TableLayoutPanelMain.Error"))
        Me.CodeErrorProvider.SetError(Me.TableLayoutPanelMain, resources.GetString("TableLayoutPanelMain.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.TableLayoutPanelMain, CType(resources.GetObject("TableLayoutPanelMain.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.TableLayoutPanelMain, CType(resources.GetObject("TableLayoutPanelMain.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.TableLayoutPanelMain, CType(resources.GetObject("TableLayoutPanelMain.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.TableLayoutPanelMain, CType(resources.GetObject("TableLayoutPanelMain.IconPadding1"), Integer))
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.CodeErrorProvider.SetError(Me.TextBoxTitle, resources.GetString("TextBoxTitle.Error"))
        Me.TitleErrorProvider.SetError(Me.TextBoxTitle, resources.GetString("TextBoxTitle.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.TextBoxTitle, CType(resources.GetObject("TextBoxTitle.IconPadding1"), Integer))
        Me.TextBoxTitle.Name = "TextBoxTitle"
        resources.ApplyResources(Me.LabelType, "LabelType")
        Me.CodeErrorProvider.SetError(Me.LabelType, resources.GetString("LabelType.Error"))
        Me.TitleErrorProvider.SetError(Me.LabelType, resources.GetString("LabelType.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.LabelType, CType(resources.GetObject("LabelType.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.LabelType, CType(resources.GetObject("LabelType.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.LabelType, CType(resources.GetObject("LabelType.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.LabelType, CType(resources.GetObject("LabelType.IconPadding1"), Integer))
        Me.LabelType.Name = "LabelType"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.CodeErrorProvider.SetError(Me.LabelTitle, resources.GetString("LabelTitle.Error"))
        Me.TitleErrorProvider.SetError(Me.LabelTitle, resources.GetString("LabelTitle.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.LabelTitle, CType(resources.GetObject("LabelTitle.IconPadding1"), Integer))
        Me.LabelTitle.Name = "LabelTitle"
        resources.ApplyResources(Me.ComboBoxTypes, "ComboBoxTypes")
        Me.ComboBoxTypes.DisplayMember = "Key"
        Me.ComboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TitleErrorProvider.SetError(Me.ComboBoxTypes, resources.GetString("ComboBoxTypes.Error"))
        Me.CodeErrorProvider.SetError(Me.ComboBoxTypes, resources.GetString("ComboBoxTypes.Error1"))
        Me.ComboBoxTypes.FormattingEnabled = True
        Me.CodeErrorProvider.SetIconAlignment(Me.ComboBoxTypes, CType(resources.GetObject("ComboBoxTypes.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.ComboBoxTypes, CType(resources.GetObject("ComboBoxTypes.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.ComboBoxTypes, CType(resources.GetObject("ComboBoxTypes.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.ComboBoxTypes, CType(resources.GetObject("ComboBoxTypes.IconPadding1"), Integer))
        Me.ComboBoxTypes.Name = "ComboBoxTypes"
        Me.ComboBoxTypes.ValueMember = "Key"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.OkButton, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.CodeErrorProvider.SetError(Me.TableLayoutPanel1, resources.GetString("TableLayoutPanel1.Error"))
        Me.TitleErrorProvider.SetError(Me.TableLayoutPanel1, resources.GetString("TableLayoutPanel1.Error1"))
        Me.TitleErrorProvider.SetIconAlignment(Me.TableLayoutPanel1, CType(resources.GetObject("TableLayoutPanel1.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.CodeErrorProvider.SetIconAlignment(Me.TableLayoutPanel1, CType(resources.GetObject("TableLayoutPanel1.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.TableLayoutPanel1, CType(resources.GetObject("TableLayoutPanel1.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.TableLayoutPanel1, CType(resources.GetObject("TableLayoutPanel1.IconPadding1"), Integer))
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.OkButton, "OkButton")
        Me.CodeErrorProvider.SetError(Me.OkButton, resources.GetString("OkButton.Error"))
        Me.TitleErrorProvider.SetError(Me.OkButton, resources.GetString("OkButton.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.OkButton, CType(resources.GetObject("OkButton.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.OkButton, CType(resources.GetObject("OkButton.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.OkButton, CType(resources.GetObject("OkButton.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.OkButton, CType(resources.GetObject("OkButton.IconPadding1"), Integer))
        Me.OkButton.Name = "OkButton"
        Me.OkButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CodeErrorProvider.SetError(Me.Cancel_Button, resources.GetString("Cancel_Button.Error"))
        Me.TitleErrorProvider.SetError(Me.Cancel_Button, resources.GetString("Cancel_Button.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.Cancel_Button, CType(resources.GetObject("Cancel_Button.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.Cancel_Button, CType(resources.GetObject("Cancel_Button.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.Cancel_Button, CType(resources.GetObject("Cancel_Button.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.Cancel_Button, CType(resources.GetObject("Cancel_Button.IconPadding1"), Integer))
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.LabelName, "LabelName")
        Me.CodeErrorProvider.SetError(Me.LabelName, resources.GetString("LabelName.Error"))
        Me.TitleErrorProvider.SetError(Me.LabelName, resources.GetString("LabelName.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.LabelName, CType(resources.GetObject("LabelName.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.LabelName, CType(resources.GetObject("LabelName.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.LabelName, CType(resources.GetObject("LabelName.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.LabelName, CType(resources.GetObject("LabelName.IconPadding1"), Integer))
        Me.LabelName.Name = "LabelName"
        resources.ApplyResources(Me.TextBoxName, "TextBoxName")
        Me.CodeErrorProvider.SetError(Me.TextBoxName, resources.GetString("TextBoxName.Error"))
        Me.TitleErrorProvider.SetError(Me.TextBoxName, resources.GetString("TextBoxName.Error1"))
        Me.CodeErrorProvider.SetIconAlignment(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconAlignment(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconAlignment1"), System.Windows.Forms.ErrorIconAlignment))
        Me.TitleErrorProvider.SetIconPadding(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconPadding"), Integer))
        Me.CodeErrorProvider.SetIconPadding(Me.TextBoxName, CType(resources.GetObject("TextBoxName.IconPadding1"), Integer))
        Me.TextBoxName.Name = "TextBoxName"
        Me.CodeErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.CodeErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me.CodeErrorProvider, "CodeErrorProvider")
        Me.TitleErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.TitleErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me.TitleErrorProvider, "TitleErrorProvider")
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddCustomPropertyFormDialog"
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.CodeErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TitleErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxTypes As System.Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents CodeErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TitleErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents LabelType As Label
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents TextBoxName As TextBox
End Class
