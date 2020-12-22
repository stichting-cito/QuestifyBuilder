<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SvgImageViewer
    Inherits GenericResourceViewerBase

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
        Me.WebBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        Me.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.WebBrowser.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser.Name = "WebBrowser"
        Me.WebBrowser.ScrollBarsEnabled = False
        Me.WebBrowser.Size = New System.Drawing.Size(150, 150)
        Me.WebBrowser.TabIndex = 0
        Me.WebBrowser.WebBrowserShortcutsEnabled = False
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.WebBrowser)
        Me.Name = "SvgImageViewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebBrowser As System.Windows.Forms.WebBrowser

End Class
