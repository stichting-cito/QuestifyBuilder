Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectDataSourceResourceDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectDataSourceResourceDialog))
        Me.DataSourceGridControl = New Questify.Builder.UI.DataSourceGrid()
        Me.GridPlaceholderPanel.SuspendLayout()
        Me.FooterPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.InstructionsLabel, "InstructionsLabel")
        Me.GridPlaceholderPanel.Controls.Add(Me.DataSourceGridControl)
        Me.DataSourceGridControl.CustomPropertyColumnsVisible = False
        Me.DataSourceGridControl.CustomPropertyFilter = ResourceTypeEnum.None
        Me.DataSourceGridControl.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity)
        resources.ApplyResources(Me.DataSourceGridControl, "DataSourceGridControl")
        Me.DataSourceGridControl.EnableFiltering = False
        Me.DataSourceGridControl.GridContentContextMenuDisabled = False
        Me.DataSourceGridControl.MultiSelect = True
        Me.DataSourceGridControl.Name = "DataSourceGridControl"
        Me.DataSourceGridControl.SelectedEntity = Nothing
        Me.DataSourceGridControl.UseGridAsItemPicker = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "SelectDataSourceResourceDialog"
        Me.GridPlaceholderPanel.ResumeLayout(False)
        Me.FooterPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataSourceGridControl As Questify.Builder.UI.DataSourceGrid
End Class
