<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardOverviewTabControl
    Inherits WizardTabContentControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardOverviewTabControl))
        Me.OverviewRichTextBox = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        resources.ApplyResources(Me.OverviewRichTextBox, "OverviewRichTextBox")
        Me.OverviewRichTextBox.Name = "OverviewRichTextBox"
        Me.OverviewRichTextBox.DetectUrls = False
        Me.OverviewRichTextBox.ReadOnly = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.OverviewRichTextBox)
        Me.Name = "WizardOverviewTabControl"
        Me.Controls.SetChildIndex(Me.OverviewRichTextBox, 0)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents OverviewRichTextBox As System.Windows.Forms.RichTextBox

End Class
