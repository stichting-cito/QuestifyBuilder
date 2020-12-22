Public MustInherit Class QueryVisitor


    Private _query As Query
    Private _visitorMap As New Dictionary(Of Type, Action(Of FilterPredicate))



    Public Sub New(query As Query)
        Me._query = query
    End Sub



    Public ReadOnly Property Query() As Query
        Get
            Return _query
        End Get
    End Property



    Public Sub AddVisitorForType(filterType As Type, visitorHandler As Action(Of FilterPredicate))
        If Not Me._visitorMap.ContainsKey(filterType) Then
            Me._visitorMap.Add(filterType, visitorHandler)
        End If
    End Sub

    Public Sub Visit()
        Me.Visit(Me.Query.Filter)
    End Sub

    Public Sub Visit(filter As FilterPredicate)
        If Me._visitorMap.ContainsKey(filter.GetType) Then
            Dim visitor As Action(Of FilterPredicate) = Me._visitorMap(filter.GetType)
            visitor.Invoke(filter)
        Else
            Throw New NotSupportedException(String.Format("FilterPredicate of type [{1}] is not supported by visitor of type [{0}]", Me.GetType.ToString, filter.GetType.ToString))
        End If
    End Sub


End Class