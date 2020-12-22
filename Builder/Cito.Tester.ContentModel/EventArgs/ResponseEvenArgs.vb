Public Class ResponseEvenArgs
    Inherits EventArgs

    Public Sub New(response As Response)
        Me._response = response
    End Sub

    Private _response As Response
    Public ReadOnly Property Response As Response
        Get
            Return _response
        End Get
    End Property


End Class
