<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NoPreviewResourceViewer
    Inherits GenericResourceViewerBase


    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NoPreviewResourceViewer))
        Me.OpenButton = New System.Windows.Forms.Button()
        Me.SuspendLayout
        resources.ApplyResources(Me.OpenButton, "OpenButton")
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.OpenButton)
        Me.Name = "NoPreviewResourceViewer"
        Me.ResumeLayout(false)

    End Sub

    Friend WithEvents OpenButton As Button
End Class
