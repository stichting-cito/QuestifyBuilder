<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddTemplateDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddTemplateDialog))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ItemTypeLabel = New System.Windows.Forms.Label()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.CodeTextBox = New System.Windows.Forms.TextBox()
        Me.ItemTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.DataErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DataErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.ItemTypeLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.DescriptionTextBox, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TitleTextBox, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.CodeTextBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ItemTypeComboBox, 1, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        resources.ApplyResources(Me.ItemTypeLabel, "ItemTypeLabel")
        Me.ItemTypeLabel.Name = "ItemTypeLabel"
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.CodeTextBox, "CodeTextBox")
        Me.CodeTextBox.Name = "CodeTextBox"
        resources.ApplyResources(Me.ItemTypeComboBox, "ItemTypeComboBox")
        Me.ItemTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItemTypeComboBox.FormattingEnabled = True
        Me.ItemTypeComboBox.Name = "ItemTypeComboBox"
        Me.DataErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.DataErrorProvider.ContainerControl = Me
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddTemplateDialog"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DataErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ItemTypeLabel As System.Windows.Forms.Label
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ItemTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DataErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button

End Class
