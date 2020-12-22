Public MustInherit Class DyadicFilterPredicate
    Inherits FilterPredicate


    Private _one As FilterPredicate
    Private _other As FilterPredicate



    Public Sub New(one As FilterPredicate, other As FilterPredicate)
        Me.New()
        Me.One = one
        Me.Other = other
    End Sub

    Public Sub New()
        MyBase.New(True)
    End Sub



    Public Property One() As FilterPredicate
        Get
            Return _one
        End Get
        Set(ByVal value As FilterPredicate)
            _one = value
        End Set
    End Property

    Public Property Other() As FilterPredicate
        Get
            Return _other
        End Get
        Set(ByVal value As FilterPredicate)
            _other = value
        End Set
    End Property



    Public Overrides Function AddFilter(filter As FilterPredicate) As Boolean
        Dim result As Boolean = False

        If Me.One Is Nothing OrElse TypeOf Me.One Is NOOPFilterPredicate Then
            Me.One = filter
            result = True
        ElseIf Me.Other Is Nothing OrElse TypeOf Me.Other Is NOOPFilterPredicate Then
            Me.Other = filter
            result = True
        End If

        Return result
    End Function

    Public Overrides Function FindContainerForFilter(filter As FilterPredicate) As FilterPredicate
        Dim result As FilterPredicate = Nothing

        If filter.Equals(Me.One) OrElse filter.Equals(Me.Other) Then
            result = Me
        Else
            If Me.One IsNot Nothing Then
                result = One.FindContainerForFilter(filter)
            End If

            If Me.Other IsNot Nothing AndAlso result Is Nothing Then
                result = Other.FindContainerForFilter(filter)
            End If
        End If

        Return result
    End Function

    Public Overrides Function RemoveFilter(filter As FilterPredicate) As Boolean
        Dim result As Boolean = False

        If filter.Equals(Me.One) Then
            Me.One = New NOOPFilterPredicate
            result = True
        ElseIf filter.Equals(Other) Then
            Me.Other = New NOOPFilterPredicate
            result = True
        End If

        Return result
    End Function



End Class