<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Chromium_ItemPreviewer
    Inherits ItemPreviewerBase


    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Chromium_ItemPreviewer))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanelOperatorButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.ComboBoxDimensions = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenPackageButton = New System.Windows.Forms.Button()
        Me.TestMonitorButton = New System.Windows.Forms.Button()
        Me.PreviewerButton = New System.Windows.Forms.Button()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.borderPanel = New System.Windows.Forms.Panel()
        Me.pleaseWaitLabel = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.TableLayoutPanelOperatorButtons.SuspendLayout
        Me.borderPanel.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanelOperatorButtons)
        resources.ApplyResources(Me.SplitContainer1.Panel2, "SplitContainer1.Panel2")
        Me.SplitContainer1.Panel2.Controls.Add(Me.borderPanel)
        resources.ApplyResources(Me.TableLayoutPanelOperatorButtons, "TableLayoutPanelOperatorButtons")
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.ComboBoxDimensions, 6, 0)
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.Label1, 5, 0)
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.OpenPackageButton, 3, 0)
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.TestMonitorButton, 0, 0)
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.PreviewerButton, 2, 0)
        Me.TableLayoutPanelOperatorButtons.Controls.Add(Me.TextBoxUrl, 4, 0)
        Me.TableLayoutPanelOperatorButtons.Name = "TableLayoutPanelOperatorButtons"
        resources.ApplyResources(Me.ComboBoxDimensions, "ComboBoxDimensions")
        Me.ComboBoxDimensions.DisplayMember = "0"
        Me.ComboBoxDimensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDimensions.DropDownWidth = 94
        Me.ComboBoxDimensions.FormattingEnabled = true
        Me.ComboBoxDimensions.Name = "ComboBoxDimensions"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.OpenPackageButton.Image = Global.Questify.Builder.UI.My.Resources.Resources.FolderOpen_16x16_72
        resources.ApplyResources(Me.OpenPackageButton, "OpenPackageButton")
        Me.OpenPackageButton.Name = "OpenPackageButton"
        Me.OpenPackageButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.TestMonitorButton, "TestMonitorButton")
        Me.TestMonitorButton.Name = "TestMonitorButton"
        Me.TestMonitorButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.PreviewerButton, "PreviewerButton")
        Me.PreviewerButton.Name = "PreviewerButton"
        Me.PreviewerButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.TextBoxUrl, "TextBoxUrl")
        Me.TextBoxUrl.Name = "TextBoxUrl"
        resources.ApplyResources(Me.borderPanel, "borderPanel")
        Me.borderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.borderPanel.CausesValidation = false
        Me.borderPanel.Controls.Add(Me.pleaseWaitLabel)
        Me.borderPanel.Name = "borderPanel"
        resources.ApplyResources(Me.pleaseWaitLabel, "pleaseWaitLabel")
        Me.pleaseWaitLabel.Name = "pleaseWaitLabel"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Chromium_ItemPreviewer"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.TableLayoutPanelOperatorButtons.ResumeLayout(false)
        Me.TableLayoutPanelOperatorButtons.PerformLayout
        Me.borderPanel.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanelOperatorButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxDimensions As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents borderPanel As System.Windows.Forms.Panel
    Friend WithEvents pleaseWaitLabel As System.Windows.Forms.Label
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents OpenPackageButton As Button
    Friend WithEvents TestMonitorButton As Button
    Friend WithEvents PreviewerButton As Button
End Class
