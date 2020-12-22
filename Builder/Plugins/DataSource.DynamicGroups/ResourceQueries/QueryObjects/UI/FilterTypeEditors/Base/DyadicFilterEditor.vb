Public Class DyadicFilterEditor


    Public Overrides Property Filter() As FilterPredicate
        Get
            Return MyBase.Filter
        End Get
        Set(value As FilterPredicate)
            MyBase.Filter = value
            DyadicFilterPredicateBindingSource.DataSource = Me.Filter
        End Set
    End Property


End Class