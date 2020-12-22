Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemDataSourcePreview
    Inherits DataSourcePreview

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
        Me.ResultItemGrid = New Questify.Builder.UI.ItemGrid()
        Me.DataSourceResultUIPanel.SuspendLayout()
        Me.SuspendLayout()
        Me.DataSourceResultUIPanel.Controls.Add(Me.ResultItemGrid)
        Me.ResultItemGrid.CustomPropertyColumnsVisible = False
        Me.ResultItemGrid.CustomPropertyFilter = ResourceTypeEnum.None
        Me.ResultItemGrid.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity)
        Me.ResultItemGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultItemGrid.EnableFiltering = False
        Me.ResultItemGrid.Location = New System.Drawing.Point(0, 0)
        Me.ResultItemGrid.Name = "ResultItemGrid"
        Me.ResultItemGrid.SearchToolbarVisibility = False
        Me.ResultItemGrid.SelectedEntity = Nothing
        Me.ResultItemGrid.Size = New System.Drawing.Size(714, 324)
        Me.ResultItemGrid.TabIndex = 0
        Me.ResultItemGrid.UseGridAsItemPicker = False
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "ItemDataSourcePreview"
        Me.DataSourceResultUIPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ResultItemGrid As Questify.Builder.UI.ItemGrid

End Class
