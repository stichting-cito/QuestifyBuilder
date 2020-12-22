Public Class ItemRefTransaction2
    Inherits TransactionBase

    Private _itemRef As ItemReference2



    Sub New()
        MyBase.New()
    End Sub

    Public Sub New(section As TestSectionViewBase, itemRef As ItemReference2)
        Me.Id = section.Identifier
        Me.ItemRef = itemRef
    End Sub



    Public Property ItemRef As ItemReference2
        Get
            Return _itemRef
        End Get
        Set
            _itemRef = value
        End Set
    End Property


End Class