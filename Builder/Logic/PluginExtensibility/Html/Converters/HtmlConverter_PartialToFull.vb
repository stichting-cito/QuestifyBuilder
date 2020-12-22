Option Infer On
Imports System.Xml

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_PartialToFull
        Inherits HtmlConverterBase

        Private _styles As Dictionary(Of String, String)
        Private _headerStyle As String
        Private _contextIdentifier As Integer?
        Private _namespaceManager As XmlNamespaceManager
        Private _behavior As IHtmlEditorBehaviour

        Sub New(styles As Dictionary(Of String, String), headerStyle As String, contextIdentifier As Integer?, namespacemanager As XmlNamespaceManager, Optional behavior As IHtmlEditorBehaviour = Nothing)
            _styles = styles
            _headerStyle = headerStyle
            _contextIdentifier = contextIdentifier
            _namespaceManager = namespacemanager
            If _namespaceManager Is Nothing Then _namespaceManager = GetNamespaceMng()
            _behavior = behavior
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim ch As New HtmlContentHelper()

            Dim tmp As String = ch.CreateHtmlDoc(_styles, _headerStyle, _contextIdentifier)
            Dim doc As New XmlDocument
            doc.PreserveWhitespace = True
            doc.LoadXml(String.Format(tmp, html))
            If _behavior IsNot Nothing AndAlso _behavior.StoreSizeOfHtml Then
                If doc.SelectNodes("//def:div[@id='calculateSizeDiv']", _namespaceManager).Count > 0 Then
                    html = doc.SelectNodes("//def:div[@id='calculateSizeDiv']", _namespaceManager)(0).InnerXml
                End If
                doc.LoadXml(String.Format(tmp, html))
            End If

            If doc.SelectNodes("//def:html", _namespaceManager).Count = 2 Then
                Return doc.SelectNodes("(//def:html)[last()]", _namespaceManager)(0).OuterXml
            Else
                Return doc.OuterXml
            End If
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)

            If disposing Then
                _headerStyle = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

    End Class
End Namespace