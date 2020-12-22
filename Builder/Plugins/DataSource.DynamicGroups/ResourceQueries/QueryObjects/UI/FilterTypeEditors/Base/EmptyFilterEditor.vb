Public Class EmptyFilterEditor


    Public Overrides Property Filter() As FilterPredicate
        Get
            Return MyBase.Filter
        End Get
        Set(value As FilterPredicate)
            MyBase.Filter = value
            FilterPredicateBindingSource.DataSource = Me.Filter
        End Set
    End Property


End Class