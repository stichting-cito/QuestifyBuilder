Public Class PersistTransactionEventArgs
    Inherits EventArgs


    Private _transaction As TransactionData



    Public Property Transaction As TransactionData
        Get
            Return _transaction
        End Get
        Set
            _transaction = value
        End Set
    End Property



    Public Sub New(transaction As TransactionData)
        _transaction = transaction
    End Sub


End Class
