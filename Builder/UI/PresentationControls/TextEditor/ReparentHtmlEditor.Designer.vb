<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReparentHtmlEditor
    Inherits System.Windows.Forms.UserControl


    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            DoDispose(disposing)
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
        Me.XHtmlViewer1 = New Questify.Builder.UI.XHtmlViewer()
        Me.SuspendLayout
        Me.XHtmlViewer1.AccessibleName = "XHtmlViewer"
        Me.XHtmlViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XHtmlViewer1.EditorHasStarted = false
        Me.XHtmlViewer1.Location = New System.Drawing.Point(0, 0)
        Me.XHtmlViewer1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.XHtmlViewer1.MouseFocused = false
        Me.XHtmlViewer1.Name = "XHtmlViewer1"
        Me.XHtmlViewer1.Size = New System.Drawing.Size(498, 248)
        Me.XHtmlViewer1.TabIndex = 0
        Me.AccessibleName = "DelayedHtmlEditor"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.XHtmlViewer1)
        Me.Name = "ReparentHtmlEditor"
        Me.Size = New System.Drawing.Size(498, 248)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents XHtmlViewer1 As Questify.Builder.UI.XHtmlViewer

End Class
