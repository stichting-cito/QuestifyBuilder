Imports System.Xml.Serialization

<Serializable>
Public Class Shape : Implements IAssociableHotspot

    <XmlAttribute("id")>
    Public Property Identifier As String

    <XmlAttribute("label")>
    Public Property Label As String Implements IAssociableHotspot.Label

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is Shape Then
            Dim c As Shape = DirectCast(obj, Shape)
            Return c.Identifier = Me.Identifier
        Else
            Return MyBase.Equals(obj)
        End If
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Identifier.GetHashCode()
    End Function
End Class
