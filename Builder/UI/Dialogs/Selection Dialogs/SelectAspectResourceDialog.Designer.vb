Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectAspectResourceDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectAspectResourceDialog))
        Me.AspectGrid = New Questify.Builder.UI.AspectGrid()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.GridPlaceholderPanel.Controls.Add(Me.AspectGrid)
        Me.AspectGrid.CustomPropertyColumnsVisible = False
        Me.AspectGrid.CustomPropertyFilter = ResourceTypeEnum.None
        Me.AspectGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity)
        resources.ApplyResources(Me.AspectGrid, "AspectGrid")
        Me.AspectGrid.EnableFiltering = False
        Me.AspectGrid.GridContentContextMenuDisabled = False
        Me.AspectGrid.Name = "AspectGrid"
        Me.AspectGrid.SelectedEntity = Nothing
        Me.AspectGrid.UseGridAsItemPicker = True
        Me.AcceptButton = Me.OkButton
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CloseButton
        Me.Name = "SelectAspectResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AspectGrid As Questify.Builder.UI.AspectGrid

End Class
