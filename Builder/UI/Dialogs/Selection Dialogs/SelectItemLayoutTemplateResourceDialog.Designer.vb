Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectItemLayoutTemplateResourceDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectItemLayoutTemplateResourceDialog))
        Me.ItemLayoutTemplateGridControl = New Questify.Builder.UI.ItemLayoutTemplateGrid()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        resources.ApplyResources(Me.OkButton, "OkButton")
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.GridPlaceholderPanel.Controls.Add(Me.ItemLayoutTemplateGridControl)
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.ItemLayoutTemplateGridControl.CustomPropertyColumnsVisible = False
        Me.ItemLayoutTemplateGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.ItemLayoutTemplateGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity)
        resources.ApplyResources(Me.ItemLayoutTemplateGridControl, "ItemLayoutTemplateGridControl")
        Me.ItemLayoutTemplateGridControl.EnableFiltering = False
        Me.ItemLayoutTemplateGridControl.GridContentContextMenuDisabled = False
        Me.ItemLayoutTemplateGridControl.Name = "ItemLayoutTemplateGridControl"
        Me.ItemLayoutTemplateGridControl.SelectedEntity = Nothing
        Me.ItemLayoutTemplateGridControl.UseGridAsItemPicker = True
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.CloseButton
        Me.Name = "SelectItemLayoutTemplateResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ItemLayoutTemplateGridControl As Questify.Builder.UI.ItemLayoutTemplateGrid

End Class
