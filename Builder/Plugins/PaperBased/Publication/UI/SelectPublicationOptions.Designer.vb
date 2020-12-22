Namespace Publication.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SelectPublicationOptions
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectPublicationOptions))
            Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
            Me.TableLayoutPanelPublicationOptions = New System.Windows.Forms.TableLayoutPanel()
            Me.PublicationBrowseButton = New System.Windows.Forms.Button()
            Me.PublicationFolderTextBox = New System.Windows.Forms.TextBox()
            Me.PublicationFolderLabel = New System.Windows.Forms.Label()
            Me.PrintformLabel = New System.Windows.Forms.Label()
            Me.PrintFormListBox = New System.Windows.Forms.CheckedListBox()
            Me.TextBoxPackageName = New System.Windows.Forms.TextBox()
            Me.CheckBoxSpecifyPackageName = New System.Windows.Forms.CheckBox()
            Me.PublicationOptionsTabContent = New Questify.Builder.UI.WizardTabContentControl()
            Me.OptionsGroupBox.SuspendLayout
            Me.TableLayoutPanelPublicationOptions.SuspendLayout
            Me.SuspendLayout
            Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OptionsGroupBox.Controls.Add(Me.TableLayoutPanelPublicationOptions)
            Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 85)
            Me.OptionsGroupBox.Name = "OptionsGroupBox"
            Me.OptionsGroupBox.Size = New System.Drawing.Size(682, 253)
            Me.OptionsGroupBox.TabIndex = 8
            Me.OptionsGroupBox.TabStop = false
            Me.TableLayoutPanelPublicationOptions.ColumnCount = 3
            Me.TableLayoutPanelPublicationOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanelPublicationOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanelPublicationOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.PublicationBrowseButton, 2, 0)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.PublicationFolderTextBox, 1, 0)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.PublicationFolderLabel, 0, 0)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.PrintformLabel, 0, 3)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.PrintFormListBox, 1, 3)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.TextBoxPackageName, 1, 2)
            Me.TableLayoutPanelPublicationOptions.Controls.Add(Me.CheckBoxSpecifyPackageName, 1, 1)
            Me.TableLayoutPanelPublicationOptions.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanelPublicationOptions.Location = New System.Drawing.Point(3, 18)
            Me.TableLayoutPanelPublicationOptions.Name = "TableLayoutPanelPublicationOptions"
            Me.TableLayoutPanelPublicationOptions.RowCount = 6
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanelPublicationOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
            Me.TableLayoutPanelPublicationOptions.Size = New System.Drawing.Size(676, 232)
            Me.TableLayoutPanelPublicationOptions.TabIndex = 7
            Me.PublicationBrowseButton.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.PublicationBrowseButton.Image = CType(resources.GetObject("PublicationBrowseButton.Image"), System.Drawing.Image)
            Me.PublicationBrowseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.PublicationBrowseButton.Location = New System.Drawing.Point(641, 3)
            Me.PublicationBrowseButton.Name = "PublicationBrowseButton"
            Me.PublicationBrowseButton.Size = New System.Drawing.Size(26, 23)
            Me.PublicationBrowseButton.TabIndex = 4
            Me.PublicationBrowseButton.UseVisualStyleBackColor = true
            Me.PublicationFolderTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PublicationFolderTextBox.Location = New System.Drawing.Point(130, 3)
            Me.PublicationFolderTextBox.Margin = New System.Windows.Forms.Padding(3, 3, 30, 3)
            Me.PublicationFolderTextBox.Name = "PublicationFolderTextBox"
            Me.PublicationFolderTextBox.Size = New System.Drawing.Size(478, 22)
            Me.PublicationFolderTextBox.TabIndex = 3
            Me.PublicationFolderLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PublicationFolderLabel.AutoSize = true
            Me.PublicationFolderLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.PublicationFolderLabel.Location = New System.Drawing.Point(3, 6)
            Me.PublicationFolderLabel.Name = "PublicationFolderLabel"
            Me.PublicationFolderLabel.Size = New System.Drawing.Size(121, 17)
            Me.PublicationFolderLabel.TabIndex = 3
            Me.PublicationFolderLabel.Text = "Publication folder:"
            Me.PrintformLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrintformLabel.AutoSize = true
            Me.PrintformLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.PrintformLabel.Location = New System.Drawing.Point(3, 84)
            Me.PrintformLabel.Name = "PrintformLabel"
            Me.PrintformLabel.Size = New System.Drawing.Size(121, 17)
            Me.PrintformLabel.TabIndex = 16
            Me.PrintformLabel.Text = "Printform"
            Me.PrintFormListBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PrintFormListBox.BackColor = System.Drawing.SystemColors.Window
            Me.PrintFormListBox.CheckOnClick = true
            Me.PrintFormListBox.FormattingEnabled = true
            Me.PrintFormListBox.Location = New System.Drawing.Point(130, 87)
            Me.PrintFormListBox.Margin = New System.Windows.Forms.Padding(3, 3, 30, 3)
            Me.PrintFormListBox.Name = "PrintFormListBox"
            Me.PrintFormListBox.Size = New System.Drawing.Size(478, 89)
            Me.PrintFormListBox.TabIndex = 7
            Me.TextBoxPackageName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextBoxPackageName.Enabled = false
            Me.TextBoxPackageName.Location = New System.Drawing.Point(157, 59)
            Me.TextBoxPackageName.Margin = New System.Windows.Forms.Padding(30, 3, 30, 3)
            Me.TextBoxPackageName.Name = "TextBoxPackageName"
            Me.TextBoxPackageName.Size = New System.Drawing.Size(451, 22)
            Me.TextBoxPackageName.TabIndex = 6
            Me.CheckBoxSpecifyPackageName.AutoSize = true
            Me.CheckBoxSpecifyPackageName.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.CheckBoxSpecifyPackageName.Location = New System.Drawing.Point(130, 32)
            Me.CheckBoxSpecifyPackageName.Name = "CheckBoxSpecifyPackageName"
            Me.CheckBoxSpecifyPackageName.Size = New System.Drawing.Size(173, 21)
            Me.CheckBoxSpecifyPackageName.TabIndex = 5
            Me.CheckBoxSpecifyPackageName.Text = "Specify package name"
            Me.CheckBoxSpecifyPackageName.UseVisualStyleBackColor = true
            Me.PublicationOptionsTabContent.AutoSize = true
            Me.PublicationOptionsTabContent.BackColor = System.Drawing.SystemColors.Control
            Me.PublicationOptionsTabContent.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PublicationOptionsTabContent.Location = New System.Drawing.Point(0, 0)
            Me.PublicationOptionsTabContent.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.PublicationOptionsTabContent.Name = "PublicationOptionsTabContent"
            Me.PublicationOptionsTabContent.Size = New System.Drawing.Size(703, 409)
            Me.PublicationOptionsTabContent.TabIndex = 7
            Me.PublicationOptionsTabContent.Task = "Task"
            Me.PublicationOptionsTabContent.TaskDescription = "Description"
            Me.PublicationOptionsTabContent.TaskPanelBackColor = System.Drawing.Color.White
            Me.PublicationOptionsTabContent.TaskPanelBackgroundImage = Nothing
            Me.PublicationOptionsTabContent.TaskPanelBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile
            Me.PublicationOptionsTabContent.TaskPanelHeight = 72
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.OptionsGroupBox)
            Me.Controls.Add(Me.PublicationOptionsTabContent)
            Me.Name = "SelectPublicationOptions"
            Me.Size = New System.Drawing.Size(703, 409)
            Me.OptionsGroupBox.ResumeLayout(false)
            Me.TableLayoutPanelPublicationOptions.ResumeLayout(false)
            Me.TableLayoutPanelPublicationOptions.PerformLayout
            Me.ResumeLayout(false)
            Me.PerformLayout

        End Sub

        Friend WithEvents OptionsGroupBox As Windows.Forms.GroupBox
        Friend WithEvents TableLayoutPanelPublicationOptions As Windows.Forms.TableLayoutPanel
        Friend WithEvents PublicationBrowseButton As Windows.Forms.Button
        Friend WithEvents PublicationFolderTextBox As Windows.Forms.TextBox
        Friend WithEvents PublicationFolderLabel As Windows.Forms.Label
        Friend WithEvents PrintformLabel As Windows.Forms.Label
        Friend WithEvents PrintFormListBox As Windows.Forms.CheckedListBox
        Friend WithEvents TextBoxPackageName As Windows.Forms.TextBox
        Friend WithEvents CheckBoxSpecifyPackageName As Windows.Forms.CheckBox
        Friend WithEvents PublicationOptionsTabContent As Questify.Builder.UI.WizardTabContentControl
    End Class
End NameSpace