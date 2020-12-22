Option Infer On

Imports System.IO
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_CitoRefToC1Ref
        Inherits HtmlConverterBase

        Private CCito As String = "cito_"
        Private NsCito As XNamespace = "http://www.cito.nl/citotester"
        Private NsW3 As XNamespace = "http://www.w3.org/1999/xhtml"


        Public Sub New()

        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim doc = XDocument.Load(New StringReader("<root>" + html + "</root>"), LoadOptions.PreserveWhitespace)

            For Each e In doc.Descendants(NsW3 + "span")
                transform(e, e.Attribute(NsCito + "type"))
                transform(e, e.Attribute(NsCito + "reftype"))
                transform(e, e.Attribute(NsCito + "description"))
                transform(e, e.Attribute(NsCito + "value"))
            Next

            Dim tmp = doc.Descendants("root").First().InnerXml()
            Return tmp
        End Function

        Private Sub transform(e As XElement, a As XAttribute)
            If a IsNot Nothing Then

                Select Case a.Name
                    Case NsCito + "type", NsCito + "reftype", NsCito + "description", NsCito + "value"
                        e.SetAttributeValue(String.Format("{0}{1}", CCito, a.Name.LocalName), a.Value)
                        a.Remove()
                    Case Else
                        Debug.Assert(False)
                End Select
            End If
        End Sub

    End Class
End Namespace