Public MustInherit Class MonadicFilterPredicate
    Inherits FilterPredicate


    Private _wrapped As FilterPredicate



    Public Sub New(wrapped As FilterPredicate)
        Me.New()
        Me.Wrapped = wrapped
    End Sub

    Public Sub New()
        MyBase.New(True)
    End Sub



    Public Property Wrapped() As FilterPredicate
        Get
            Return _wrapped
        End Get
        Set(ByVal value As FilterPredicate)
            _wrapped = value
        End Set
    End Property



    Public Overrides Function AddFilter(filter As FilterPredicate) As Boolean
        Dim result As Boolean = False

        If Me.Wrapped Is Nothing OrElse TypeOf Me.Wrapped Is NOOPFilterPredicate Then
            Me.Wrapped = filter
            result = True
        End If

        Return result
    End Function

    Public Overrides Function FindContainerForFilter(filter As FilterPredicate) As FilterPredicate
        Dim result As FilterPredicate = Nothing

        If filter.Equals(Me.Wrapped) Then
            result = Me
        ElseIf Me.Wrapped IsNot Nothing Then
            result = Wrapped.FindContainerForFilter(filter)
        End If

        Return result
    End Function

    Public Overrides Function RemoveFilter(filter As FilterPredicate) As Boolean
        Dim result As Boolean = False

        If filter.Equals(Me.Wrapped) Then
            Me.Wrapped = New NOOPFilterPredicate
            result = True
        End If

        Return result
    End Function


End Class