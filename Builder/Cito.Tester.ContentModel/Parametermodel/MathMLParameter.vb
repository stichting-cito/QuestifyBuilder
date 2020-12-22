Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class MathMLParameter
    Inherits Parameter(Of String)

    <XmlIgnore> _
    Public Overrides Property Value As String
        Get
            Return Me.InnerText
        End Get
        Set
            If Me.InnerText <> Value AndAlso MathMLValidator.IsValidMathML(Value) Then
                Dim doc As New XmlDocument

                Dim rootElement As XmlElement = doc.CreateElement("root")
                rootElement.InnerXml = Value
                doc.AppendChild(rootElement)

                Dim nodes As New ArrayList
                For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
                    nodes.Add(node.CloneNode(True))
                Next
                Me.Nodes = DirectCast(nodes.ToArray(GetType(XmlNode)), XmlNode())
            End If
        End Set
    End Property

    Public Overrides Function SetValue(value As String) As Boolean
        Me.Value = value
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Me.InnerText
    End Function

End Class
