
Imports System.Xml.Serialization

<Serializable, XmlRoot("responseFinding")> _
Public Class ResponseFinding
    Inherits BaseFinding


    Public Sub New(id As String)
        MyBase.Id = id
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub


End Class
