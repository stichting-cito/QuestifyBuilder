Public Class QueryEditorContext


    Private _contextOutOfSync As Boolean = False
    Private _currentQuery As Query
    Private _parentContainerFilter As FilterPredicate
    Private _selectedFilter As FilterPredicate



    Public Sub New(query As Query)
        SetCurrentQuery(query)
        Me._currentQuery = query
    End Sub

    Public Sub New()
    End Sub



    Public Event ContextChanged As EventHandler(Of EventArgs)

    Public Event RequestUIUpdate As EventHandler(Of EventArgs)



    Public ReadOnly Property CurrentQuery() As Query
        Get
            Return _currentQuery
        End Get
    End Property

    Public ReadOnly Property ParentContainerFilter() As FilterPredicate
        Get
            Return _parentContainerFilter
        End Get
    End Property

    Public Property SelectedFilter() As FilterPredicate
        Get
            Return _selectedFilter
        End Get
        Set(value As FilterPredicate)
            _selectedFilter = value
            _contextOutOfSync = True
        End Set
    End Property



    Public Sub SetCurrentQuery(query As Query)
        Me._currentQuery = query
        SetSelectedFilter(Nothing, Nothing)
    End Sub

    Public Sub SetSelectedFilter(filter As FilterPredicate, parentFilter As FilterPredicate)
        If parentFilter IsNot Nothing AndAlso Not parentFilter.IsContainer Then
            Throw New ArgumentException("parent filter must be a container filter", "parentFilter")
        End If

        If parentFilter IsNot Nothing AndAlso filter Is Nothing Then
            Throw New ArgumentNullException("cannot have value for parentFilter with filter null", "filter")
        End If

        Me._selectedFilter = filter
        Me._parentContainerFilter = parentFilter

        DoContextChanged()
    End Sub

    Protected Friend Sub DoContextChanged()
        RaiseEvent ContextChanged(Me, New EventArgs)
    End Sub

    Protected Friend Sub DoRequestUIUpdate(sender As Object, e As EventArgs)
        RaiseEvent RequestUIUpdate(sender, e)
        _contextOutOfSync = False
    End Sub


End Class