Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class PlainTextParameter
    Inherits Parameter(Of String)

    <XmlIgnore> _
    Public Overrides Property Value As String
        Get
            Return InnerText
        End Get
        Set
            Dim doc As New XHtmlDocument

            Dim rootElement As XmlElement = doc.CreateElement("root")
            rootElement.InnerXml = Value
            doc.AppendChild(rootElement)

            Dim nodeList As New ArrayList
            For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                nodeList.Add(node.CloneNode(True))
            Next
            Nodes = DirectCast(nodeList.ToArray(GetType(XmlNode)), XmlNode())
        End Set
    End Property

    Public Overrides Function SetValue(val As String) As Boolean
        Me.Value = val
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value
    End Function

End Class
