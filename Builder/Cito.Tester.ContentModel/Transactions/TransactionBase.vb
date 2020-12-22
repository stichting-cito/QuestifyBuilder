Imports Cito.Tester.Common

Public MustInherit Class TransactionBase

    Private _id As String
    Private _objectType As String

    Public Property Id As String
        Get
            Return _id
        End Get
        Set
            _id = value
        End Set
    End Property

    Public Property ObjectType As String
        Get
            Return _objectType
        End Get
        Set
            _objectType = value
        End Set
    End Property

    Protected Sub New(component As Object)
        If component IsNot Nothing Then
            _objectType = component.GetType().ToString()
        Else
            Throw New TestViewerException(My.Resources.Error_TransactionBase_Constructor)
        End If

    End Sub

    Protected Sub New()
    End Sub

End Class