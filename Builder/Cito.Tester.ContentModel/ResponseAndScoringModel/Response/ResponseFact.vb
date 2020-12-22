<Serializable> _
Public Class ResponseFact
    Inherits BaseFact


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(id As String)
        MyBase.New()
        Me.Id = id
    End Sub


End Class
