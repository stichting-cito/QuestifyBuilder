Imports System.Runtime.Serialization

<Serializable()> _
Public Class DisplayException
    Inherits TesterException

    Public Sub New()
        MyBase.New()
    End Sub


    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class
