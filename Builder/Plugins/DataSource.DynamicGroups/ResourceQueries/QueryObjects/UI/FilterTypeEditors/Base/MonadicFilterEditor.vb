Public Class MonadicFilterEditor



    Public Overrides Property Filter() As FilterPredicate
        Get
            Return MyBase.Filter
        End Get
        Set(value As FilterPredicate)
            MyBase.Filter = value
            MonadicFilterPredicateBindingSource.DataSource = Me.Filter
        End Set
    End Property



End Class
