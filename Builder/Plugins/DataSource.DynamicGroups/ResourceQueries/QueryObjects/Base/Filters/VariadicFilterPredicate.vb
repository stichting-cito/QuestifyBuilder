Public Class VariadicFilterPredicate
    Inherits FilterPredicate


    Private _wrapped As List(Of FilterPredicate)



    Public Sub New()
        MyBase.New(True)
    End Sub



    Public ReadOnly Property Wrapped() As List(Of FilterPredicate)
        Get
            Return _wrapped
        End Get
    End Property


End Class