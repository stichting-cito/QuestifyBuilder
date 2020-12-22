<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SimpleSearchControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleSearchControl))
        Me.SearchForTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TestComboBox = New System.Windows.Forms.ComboBox()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OptionsMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.chkIncludeSubBanks = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.SearchForTextBox, "SearchForTextBox")
        Me.SearchForTextBox.Name = "SearchForTextBox"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.TestComboBox, "TestComboBox")
        Me.TestComboBox.DisplayMember = "TestName"
        Me.TestComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TestComboBox.FormattingEnabled = true
        Me.TestComboBox.Name = "TestComboBox"
        Me.TestComboBox.ValueMember = "ResourceId"
        resources.ApplyResources(Me.SearchButton, "SearchButton")
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TestComboBox, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SearchForTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OptionsMenuStrip, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SearchButton, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ClearButton, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.chkIncludeSubBanks, 4, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.OptionsMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        resources.ApplyResources(Me.OptionsMenuStrip, "OptionsMenuStrip")
        Me.OptionsMenuStrip.Name = "OptionsMenuStrip"
        resources.ApplyResources(Me.ClearButton, "ClearButton")
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.chkIncludeSubBanks, "chkIncludeSubBanks")
        Me.chkIncludeSubBanks.Name = "chkIncludeSubBanks"
        Me.chkIncludeSubBanks.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "SimpleSearchControl"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents SearchForTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TestComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OptionsMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents chkIncludeSubBanks As System.Windows.Forms.CheckBox

End Class
