Imports Questify.Builder.UI
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XHtmlSourceCodeViewer
    Inherits DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XHtmlSourceCodeViewer))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.SourceTabPage = New System.Windows.Forms.TabPage()
        Me.ResourceEditor1 = New System.Windows.Forms.TextBox()
        Me.LayoutTabPage = New System.Windows.Forms.TabPage()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.ContentPanel.SuspendLayout
        Me.TabControl1.SuspendLayout
        Me.SourceTabPage.SuspendLayout
        Me.LayoutTabPage.SuspendLayout
        Me.SuspendLayout
        Me.ContentPanel.Controls.Add(Me.TabControl1)
        Me.ContentPanel.Size = New System.Drawing.Size(729, 394)
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(276, 163)
        Me.WebBrowser1.TabIndex = 2
        Me.TabControl1.Controls.Add(Me.SourceTabPage)
        Me.TabControl1.Controls.Add(Me.LayoutTabPage)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(729, 394)
        Me.TabControl1.TabIndex = 3
        Me.SourceTabPage.Controls.Add(Me.ResourceEditor1)
        Me.SourceTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SourceTabPage.Name = "SourceTabPage"
        Me.SourceTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SourceTabPage.Size = New System.Drawing.Size(721, 368)
        Me.SourceTabPage.TabIndex = 0
        Me.SourceTabPage.Text = "Source"
        Me.SourceTabPage.UseVisualStyleBackColor = true
        Me.ResourceEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResourceEditor1.Location = New System.Drawing.Point(3, 3)
        Me.ResourceEditor1.MaxLength = 0
        Me.ResourceEditor1.Multiline = true
        Me.ResourceEditor1.Name = "ResourceEditor1"
        Me.ResourceEditor1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ResourceEditor1.Size = New System.Drawing.Size(715, 362)
        Me.ResourceEditor1.TabIndex = 0
        Me.ResourceEditor1.WordWrap = false
        Me.LayoutTabPage.Controls.Add(Me.WebBrowser1)
        Me.LayoutTabPage.Location = New System.Drawing.Point(4, 22)
        Me.LayoutTabPage.Name = "LayoutTabPage"
        Me.LayoutTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.LayoutTabPage.Size = New System.Drawing.Size(282, 169)
        Me.LayoutTabPage.TabIndex = 1
        Me.LayoutTabPage.Text = "Layout"
        Me.LayoutTabPage.UseVisualStyleBackColor = true
        Me.OKButton.Location = New System.Drawing.Point(0, 0)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 23)
        Me.OKButton.TabIndex = 0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 432)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "XHtmlSourceCodeViewer"
        Me.Text = "XHtml Sourcecode Viewer"
        Me.ContentPanel.ResumeLayout(false)
        Me.TabControl1.ResumeLayout(false)
        Me.SourceTabPage.ResumeLayout(false)
        Me.SourceTabPage.PerformLayout
        Me.LayoutTabPage.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents SourceTabPage As System.Windows.Forms.TabPage
    Friend WithEvents LayoutTabPage As System.Windows.Forms.TabPage
    Friend Shadows WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents ResourceEditor1 As System.Windows.Forms.TextBox
End Class
