Option Infer On

Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_C1RefToCitoRef
        Inherits HtmlConverterBase

        Private CCito As String = "cito_"
        Private nsm As New XmlNamespaceManager(New NameTable())
        Private Cito As XNamespace = "http://www.cito.nl/citotester"
        Private W3 As XNamespace = "http://www.w3.org/1999/xhtml"


        Public Sub New()
            nsm.AddNamespace("cito", Cito.NamespaceName)
            nsm.AddNamespace("w3", W3.NamespaceName)
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim doc = XDocument.Load(New StringReader("<root>" + html + "</root>"), LoadOptions.PreserveWhitespace)

            For Each e In doc.Descendants(W3 + "span")
                transform(e, e.Attribute(CCito + "type"))
                transform(e, e.Attribute(CCito + "reftype"))
                transform(e, e.Attribute(CCito + "description"))
                transform(e, e.Attribute(CCito + "value"))
            Next

            Dim tmp = doc.Descendants("root").First().InnerXml()
            Return tmp
        End Function

        Private Sub transform(e As XElement, a As XAttribute)
            If a IsNot Nothing Then

                e.SetAttributeValue(XNamespace.Xmlns + "cito", Cito)

                Select Case a.Name
                    Case CCito + "type", CCito + "reftype", CCito + "description", CCito + "value"
                        Dim s As String = a.Name.LocalName.Remove(0, CCito.Length)
                        e.SetAttributeValue(Cito + s, a.Value)
                        a.Remove()
                    Case Else
                        Debug.Assert(False)
                End Select
            End If
        End Sub

    End Class
End Namespace