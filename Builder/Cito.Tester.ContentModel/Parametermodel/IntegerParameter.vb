Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class IntegerParameter
    Inherits Parameter(Of Integer)

    <XmlIgnore> _
    Public Overrides Property Value As Integer
        Get
            Dim result As Integer = 0

            If Not String.IsNullOrEmpty(Me.InnerText) Then
                Integer.TryParse(Me.InnerText, result)
            End If
            Return result
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

        Dim val As Integer
        If Integer.TryParse(value, val) Then
            Me.Value = val
            result = True
        End If

        Return result
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value.ToString
    End Function

End Class