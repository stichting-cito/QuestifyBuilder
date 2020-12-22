

Public Class ItemPickedInTransactionEventArgs
    Inherits EventArgs


    Private _item As ItemReferenceViewBase





    Public ReadOnly Property Item As ItemReferenceViewBase
        Get
            Return _item
        End Get
    End Property



    Public Sub New(item As ItemReferenceViewBase)
        _item = item
    End Sub


End Class

