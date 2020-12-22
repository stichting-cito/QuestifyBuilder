Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectItemResourceDialog
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectItemResourceDialog))
        Me.DialogSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.ItemGridControl = New Questify.Builder.UI.ItemGrid()
        Me.Previewer = New Questify.Builder.UI.ItemPreviewContainer()
        Me.GridBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.InstructionsLabel = New System.Windows.Forms.Label()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.FooterPanel = New System.Windows.Forms.Panel()
        Me.chkPreviewEnabled = New System.Windows.Forms.CheckBox()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GridPlaceholderPanel = New System.Windows.Forms.Panel()
        Me.DialogSplitContainer.Panel1.SuspendLayout()
        Me.DialogSplitContainer.Panel2.SuspendLayout()
        Me.DialogSplitContainer.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.DialogSplitContainer, "DialogSplitContainer")
        Me.DialogSplitContainer.Name = "DialogSplitContainer"
        Me.DialogSplitContainer.Panel1.Controls.Add(Me.ItemGridControl)
        Me.DialogSplitContainer.Panel2.Controls.Add(Me.Previewer)
        Me.ItemGridControl.CustomPropertyColumnsVisible = False
        Me.ItemGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.ItemGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity)
        resources.ApplyResources(Me.ItemGridControl, "ItemGridControl")
        Me.ItemGridControl.EnableFiltering = False
        Me.ItemGridControl.GridContentContextMenuDisabled = False
        Me.ItemGridControl.Name = "ItemGridControl"
        Me.ItemGridControl.SearchToolbarVisibility = False
        Me.ItemGridControl.SelectedEntity = Nothing
        Me.ItemGridControl.UseGridAsItemPicker = False
        resources.ApplyResources(Me.Previewer, "Previewer")
        Me.Previewer.Name = "Previewer"

        Me.GridBackgroundWorker.WorkerReportsProgress = True
        Me.GridBackgroundWorker.WorkerSupportsCancellation = True
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.Name = "InstructionsLabel"
        Me.HeaderPanel.Controls.Add(Me.InstructionsLabel)
        resources.ApplyResources(Me.HeaderPanel, "HeaderPanel")
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.FooterPanel.Controls.Add(Me.chkPreviewEnabled)
        Me.FooterPanel.Controls.Add(Me.AddButton)
        Me.FooterPanel.Controls.Add(Me.CloseButton)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Name = "FooterPanel"
        resources.ApplyResources(Me.chkPreviewEnabled, "chkPreviewEnabled")
        Me.chkPreviewEnabled.Name = "chkPreviewEnabled"
        Me.chkPreviewEnabled.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AddButton, "AddButton")
        Me.AddButton.Name = "AddButton"
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.CloseButton.Name = "CloseButton"
        Me.Panel1.Controls.Add(Me.GridPlaceholderPanel)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.GridPlaceholderPanel.Controls.Add(Me.DialogSplitContainer)
        Me.GridPlaceholderPanel.Controls.Add(Me.FooterPanel)
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        Me.GridPlaceholderPanel.Name = "GridPlaceholderPanel"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Name = "SelectItemResourceDialog"
        Me.DialogSplitContainer.Panel1.ResumeLayout(False)
        Me.DialogSplitContainer.Panel2.ResumeLayout(False)
        Me.DialogSplitContainer.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.FooterPanel.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents GridBackgroundWorker As System.ComponentModel.BackgroundWorker
    Protected WithEvents InstructionsLabel As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Protected WithEvents FooterPanel As System.Windows.Forms.Panel
    Protected WithEvents AddButton As System.Windows.Forms.Button
    Protected WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents GridPlaceholderPanel As System.Windows.Forms.Panel
    Friend WithEvents DialogSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents ItemGridControl As Questify.Builder.UI.ItemGrid
    Friend WithEvents Previewer As Questify.Builder.UI.ItemPreviewContainer
    Friend WithEvents chkPreviewEnabled As System.Windows.Forms.CheckBox


End Class
