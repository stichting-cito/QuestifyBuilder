<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InlineElementPropertiesDialog
    Inherits DialogBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            Me.ParameterSetsEditorInstance.FormClosing = True
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InlineElementPropertiesDialog))
        Me.ParameterSetsEditorInstance = New Questify.Builder.UI.ParameterSetsEditor()
        Me.ContentPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.ContentPanel.Controls.Add(Me.ParameterSetsEditorInstance)
        resources.ApplyResources(Me.ParameterSetsEditorInstance, "ParameterSetsEditorInstance")
        Me.ParameterSetsEditorInstance.ContextIdentifierForEditors = Nothing
        Me.ParameterSetsEditorInstance.FilterParameter = Nothing
        Me.ParameterSetsEditorInstance.FormClosing = False
        Me.ParameterSetsEditorInstance.HasLoadedOldItemLayoutTemplate = False
        Me.ParameterSetsEditorInstance.ItemLayoutAdapterForItem = Nothing
        Me.ParameterSetsEditorInstance.ItemSaving = False
        Me.ParameterSetsEditorInstance.Name = "ParameterSetsEditorInstance"
        Me.ParameterSetsEditorInstance.ParameterSets = Nothing
        Me.ParameterSetsEditorInstance.ParentIsInlineElement = False
        Me.ParameterSetsEditorInstance.ResourceEntity = Nothing
        Me.ParameterSetsEditorInstance.ResourceManager = Nothing
        Me.ParameterSetsEditorInstance.ShouldSort = False
        Me.ParameterSetsEditorInstance.Solution = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.Name = "InlineElementPropertiesDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.ContentPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ParameterSetsEditorInstance As Questify.Builder.UI.ParameterSetsEditor
End Class
