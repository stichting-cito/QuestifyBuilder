Option Infer On

Imports System.IO
Imports System.Xml
Imports System.Xml.Linq

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_AssignDivId
        Inherits HtmlConverterBase

        Private CCito As String = "cito_"
        Private nsm As New XmlNamespaceManager(New NameTable())
        Private Cito As XNamespace = "http://www.cito.nl/citotester"
        Private W3 As XNamespace = "http://www.w3.org/1999/xhtml"
        Private _idNr As Integer = 0


        Public Sub New()
            nsm.AddNamespace("w3", W3.NamespaceName)
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim doc = XDocument.Load(New StringReader("<root>" + html + "</root>"), LoadOptions.PreserveWhitespace)

            For Each e In doc.Descendants(W3 + "div")
                If (e.Attribute("id") Is Nothing) Then
                    e.Add(New XAttribute("id", String.Format("div{0}", _idNr)))
                    _idNr = _idNr + 1
                End If
            Next

            Dim tmp = doc.ToString()
            Return tmp.Substring(6, tmp.Length - 13)
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

        Friend ReadOnly Property IdCounter As Integer
            Get
                Return _idNr
            End Get
        End Property

    End Class
End Namespace