<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WordGenericResourceViewer
    Inherits GenericResourceViewerBase


    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                CleanUpPreviewFragment()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.WordFragmentPreviewPanel = New Questify.Builder.UI.PreviewPanel
        Me.SuspendLayout()
        Me.WordFragmentPreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WordFragmentPreviewPanel.Location = New System.Drawing.Point(0, 0)
        Me.WordFragmentPreviewPanel.Name = "WordFragmentPreviewPanel"
        Me.WordFragmentPreviewPanel.PreviewSource = Nothing
        Me.WordFragmentPreviewPanel.Size = New System.Drawing.Size(379, 227)
        Me.WordFragmentPreviewPanel.TabIndex = 0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.WordFragmentPreviewPanel)
        Me.Name = "WordGenericResourceViewer"
        Me.Size = New System.Drawing.Size(379, 227)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WordFragmentPreviewPanel As Questify.Builder.UI.PreviewPanel

End Class
