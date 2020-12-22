Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectPauseItemResourceDialog
    Inherits SelectResourceEntityDialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectPauseItemResourceDialog))
        Me.PauseItemGridControl = New Questify.Builder.UI.ItemGrid()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.InstructionsLabel.UseWaitCursor = True
        resources.ApplyResources(Me.OkButton, "OkButton")
        resources.ApplyResources(Me.CloseButton, "CloseButton")
        Me.GridPlaceholderPanel.Controls.Add(Me.PauseItemGridControl)
        resources.ApplyResources(Me.GridPlaceholderPanel, "GridPlaceholderPanel")
        resources.ApplyResources(Me.FooterPanel, "FooterPanel")
        Me.PauseItemGridControl.CustomPropertyColumnsVisible = False
        Me.PauseItemGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.PauseItemGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity)
        resources.ApplyResources(Me.PauseItemGridControl, "PauseItemGridControl")
        Me.PauseItemGridControl.EnableFiltering = False
        Me.PauseItemGridControl.GridContentContextMenuDisabled = False
        Me.PauseItemGridControl.MultiSelect = True
        Me.PauseItemGridControl.Name = "PauseItemGridControl"
        Me.PauseItemGridControl.SearchToolbarVisibility = False
        Me.PauseItemGridControl.SelectedEntity = Nothing
        Me.PauseItemGridControl.UseGridAsItemPicker = True
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CloseButton
        Me.Name = "SelectPauseItemResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PauseItemGridControl As Questify.Builder.UI.ItemGrid
End Class
