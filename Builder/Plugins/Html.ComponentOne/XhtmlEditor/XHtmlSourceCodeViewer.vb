Imports System.Windows.Forms

Public Class XHtmlSourceCodeViewer

    Private _xhtml As String

    Public Sub New(ByVal xHtml As String)
        InitializeComponent()

        _xhtml = xHtml

        PopulateTextEditor()
        PopulateWebBrowser()
    End Sub

    Public ReadOnly Property Xhtml As String
        Get
            Return _xhtml
        End Get
    End Property

    Private Sub PopulateWebBrowser()
        WebBrowser1.Navigate("about:blank")
        WebBrowser1.Document.Write(_xhtml)
    End Sub

    Private Sub PopulateTextEditor()
        ResourceEditor1.Text = _xhtml
    End Sub

    Protected Overrides Function OnOk() As Boolean
        _xhtml = ResourceEditor1.Text

        Me.DialogResult = DialogResult.OK

        Return True
    End Function

    Protected Overrides Function OnCancel() As Boolean
        Me.DialogResult = DialogResult.Cancel

        Return True
    End Function

    Private Sub TabControl1_TabIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControl1.Selected
        If DirectCast(sender, TabControl).SelectedTab.Name = "LayoutTabPage" Then
            WebBrowser1.Document.OpenNew(False)
            WebBrowser1.Document.Write(ResourceEditor1.Text)
        End If
    End Sub

End Class