Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTestTemplateResourceDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTestTemplateResourceDialog))
        Me.TestTemplateGridControl = New Questify.Builder.UI.TestGrid()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        resources.ApplyResources(Me.OkButton, "OkButton")
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        Me.GridPlaceholderPanel.Controls.Add(Me.TestTemplateGridControl)
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        resources.ApplyResources(Me.TestTemplateGridControl, "TestTemplateGridControl")
        Me.TestTemplateGridControl.CustomPropertyColumnsVisible = False
        Me.TestTemplateGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.TestTemplateGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity)
        Me.TestTemplateGridControl.EnableFiltering = False
        Me.TestTemplateGridControl.GridContentContextMenuDisabled = False
        Me.TestTemplateGridControl.Name = "TestTemplateGridControl"
        Me.TestTemplateGridControl.SelectedEntity = Nothing
        Me.TestTemplateGridControl.UseGridAsItemPicker = True
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.CloseButton
        Me.Name = "SelectTestTemplateResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TestTemplateGridControl As Questify.Builder.UI.TestGrid

End Class
