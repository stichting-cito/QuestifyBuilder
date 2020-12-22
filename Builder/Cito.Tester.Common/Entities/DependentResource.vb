Imports System.Xml.Serialization

Public Class DependentResource

    <XmlAttribute("name")>
    Public Property Name() As String

    Public Sub New(name As String)
        Me.Name = name
    End Sub

    Public Sub New()
    End Sub
End Class