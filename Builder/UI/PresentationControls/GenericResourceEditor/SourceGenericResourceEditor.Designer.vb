<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SourceGenericResourceEditor
    Inherits Questify.Builder.UI.GenericResourceViewerBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SourceGenericResourceEditor))
        Me.SourceTextEditorControl = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        resources.ApplyResources(Me.SourceTextEditorControl, "SourceTextEditorControl")
        Me.SourceTextEditorControl.Name = "SourceTextEditorControl"
        Me.SourceTextEditorControl.Multiline = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SourceTextEditorControl)
        Me.Name = "SourceGenericResourceEditor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SourceTextEditorControl As System.Windows.Forms.TextBox

End Class
