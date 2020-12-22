Public Class QueryDetails


    Public Property ItemQuery() As ItemQuery
        Get
            Return ItemQueryBindingSource.DataSource
        End Get
        Set(value As ItemQuery)
            ItemQueryBindingSource.DataSource = value
        End Set
    End Property



    Private Sub QueryDetails_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub


End Class