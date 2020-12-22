Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectMediaResourceDialog
    Inherits SelectResourceEntityDialogBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectMediaResourceDialog))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MediaGridControl = New Questify.Builder.UI.MediaGrid()
        Me.PreviewGroupBox = New System.Windows.Forms.GroupBox()
        Me.GenericResourceViewer = New Questify.Builder.UI.GenericResourceEditorControl()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.PreviewGroupBox.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        resources.ApplyResources(Me.OkButton, "OkButton")
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.GridPlaceholderPanel.Controls.Add(Me.SplitContainer1)
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        Me.FooterPanel.Controls.Add(Me.NewButton)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.FooterPanel.Controls.SetChildIndex(Me.NewButton, 0)
        Me.FooterPanel.Controls.SetChildIndex(Me.CloseButton, 0)
        Me.FooterPanel.Controls.SetChildIndex(Me.OkButton, 0)
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Panel1.Controls.Add(Me.MediaGridControl)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PreviewGroupBox)
        Me.MediaGridControl.CustomPropertyColumnsVisible = False
        Me.MediaGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.MediaGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity)
        resources.ApplyResources(Me.MediaGridControl, "MediaGridControl")
        Me.MediaGridControl.EnableFiltering = False
        Me.MediaGridControl.GridContentContextMenuDisabled = False
        Me.MediaGridControl.Name = "MediaGridControl"
        Me.MediaGridControl.SelectedEntity = Nothing
        Me.MediaGridControl.UseGridAsItemPicker = True
        Me.PreviewGroupBox.Controls.Add(Me.GenericResourceViewer)
        resources.ApplyResources(Me.PreviewGroupBox, "PreviewGroupBox")
        Me.PreviewGroupBox.Name = "PreviewGroupBox"
        Me.PreviewGroupBox.TabStop = False
        Me.GenericResourceViewer.BankContextIdentifier = Nothing
        Me.GenericResourceViewer.DataSource = Nothing
        resources.ApplyResources(Me.GenericResourceViewer, "GenericResourceViewer")
        Me.GenericResourceViewer.Name = "GenericResourceViewer"
        Me.GenericResourceViewer.ResourceManager = Nothing
        resources.ApplyResources(Me.NewButton, "NewButton")
        Me.NewButton.Name = "NewButton"
        Me.NewButton.UseVisualStyleBackColor = True
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.CloseButton
        Me.Name = "SelectMediaResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.PreviewGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MediaGridControl As Questify.Builder.UI.MediaGrid
    Friend WithEvents PreviewGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents GenericResourceViewer As Questify.Builder.UI.GenericResourceEditorControl
    Friend WithEvents NewButton As System.Windows.Forms.Button

End Class
