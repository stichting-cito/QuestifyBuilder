Option Infer On

Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_RemoveHyperlinks
        Inherits HtmlConverterBase
        Private nsm As New XmlNamespaceManager(New NameTable)
        Private Cito As XNamespace = "http://www.cito.nl/citotester"
        Private W3 As XNamespace = "http://www.w3.org/1999/xhtml"

        Public Sub New()
            nsm.AddNamespace("cito", Cito.NamespaceName)
            nsm.AddNamespace("w3", W3.NamespaceName)
        End Sub

        Protected Overrides Function DoConvert(ByVal html As String) As String
            Dim doc = XDocument.Load(New StringReader("<root>" + html + "</root>"), LoadOptions.PreserveWhitespace)

            Dim aElement As XElement = doc.Descendants(W3 + "a").FirstOrDefault()
            While aElement IsNot Nothing
                RemoveElementKeepValue(aElement)
                aElement = doc.Descendants(W3 + "a").FirstOrDefault()
            End While

            Dim tmp = doc.Descendants("root").First().InnerXml()
            Return tmp
        End Function

        Private Sub RemoveElementKeepValue(ByVal elToReplace As XElement)
            If elToReplace IsNot Nothing Then
                elToReplace.ReplaceWith(elToReplace.Value)
            End If
        End Sub
    End Class
End Namespace