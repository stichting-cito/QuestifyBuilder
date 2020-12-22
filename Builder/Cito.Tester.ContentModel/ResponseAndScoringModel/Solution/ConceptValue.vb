

Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("conceptValue")> _
Public Class ConceptValue
    Inherits KeyValue


    Public Sub New()
    End Sub

    Public Sub New(domain As String, occur As Integer)
        MyBase.New(domain, occur)
    End Sub

    Public Sub New(domain As String, occur As Integer, value As BaseValue)
        MyBase.New(domain, occur, value)
    End Sub


End Class
