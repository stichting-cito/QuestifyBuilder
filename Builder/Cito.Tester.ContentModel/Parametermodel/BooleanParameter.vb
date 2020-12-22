Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class BooleanParameter
    Inherits Parameter(Of Boolean)

    <XmlIgnore> _
    Public Overrides Property Value As Boolean
        Get
            If String.IsNullOrEmpty(Me.InnerText) Then
                Return False
            Else
                Return Boolean.Parse(Me.InnerText)
            End If
        End Get
        Set
            Dim doc As New XHtmlDocument

            Dim rootElement As XmlElement = doc.CreateElement("root")
            rootElement.InnerXml = Value.ToString()
            doc.AppendChild(rootElement)

            Dim nodes As New ArrayList
            For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                nodes.Add(node.CloneNode(True))
            Next
            Me.Nodes = DirectCast(nodes.ToArray(GetType(XmlNode)), XmlNode())
        End Set
    End Property

    Public Overrides Function SetValue(value As String) As Boolean
        Dim result As Boolean

        Dim val As Boolean
        If Boolean.TryParse(value, val) Then
            Me.Value = val
            result = True
        End If

        Return result
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value.ToString
    End Function

End Class