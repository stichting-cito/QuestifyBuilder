Imports Cito.Tester.Common
Imports System.Xml.Linq
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Public Class XHtmlParameter
    Inherits Parameter(Of String)

    <XmlIgnore> _
    Public Overrides Property Value As String
        Get
            Return Me.InnerText
        End Get
        Set
            If Me.InnerText <> Value Then
                Dim doc As New XHtmlDocument

                Dim rootElement As XmlElement = doc.CreateElement("root")
                rootElement.InnerXml = Value
                doc.AppendChild(rootElement)

                Dim xmlNodes As New ArrayList
                For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                    xmlNodes.Add(node.CloneNode(True))
                Next
                Me.Nodes = DirectCast(xmlNodes.ToArray(GetType(XmlNode)), XmlNode())
            End If
        End Set
    End Property


    Public Function GetInlineElements() As Dictionary(Of String, InlineElement)
        Dim cito As XNamespace = "http://www.cito.nl/citotester"

        Dim ret As New Dictionary(Of String, InlineElement)()

        Dim doc = XDocument.Load(New StringReader("<root>" & Value & "</root>"))

        For Each e In doc.Descendants(cito + "InlineElement")
            Dim inline = SerializeHelper.XmlDeserializeFromString(Of InlineElement)(OuterXmltoStr(e).Trim())
            If Not ret.ContainsKey(inline.Identifier) Then
                ret.Add(inline.Identifier, inline)
            End If
        Next

        Return ret
    End Function

    Public Overrides Function SetValue(value As String) As Boolean
        Me.Value = value
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Me.InnerText
    End Function

    Private Function OuterXmltoStr(x As XElement) As String
        Dim reader = x.CreateReader()
        reader.MoveToContent()
        Return reader.ReadOuterXml()
    End Function

End Class
