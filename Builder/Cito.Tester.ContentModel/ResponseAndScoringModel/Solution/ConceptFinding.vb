
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("conceptFinding")> _
Public Class ConceptFinding
    Inherits KeyFinding


    Public Sub New(id As String)
        MyBase.New(id)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub


End Class
