Public Module TypeSwitch

    Public Delegate Sub Action()

    Public Class CaseInfo
        Public Property IsDefault() As Boolean
        Public Property Target() As Type
        Public Property Action() As Action(Of Object)
    End Class

    Public Sub WhenObject(source As Object, ParamArray cases As CaseInfo())
        Dim type As Type = source.[GetType]()
        For i As Integer = 0 To cases.Length - 1
            Dim entry As CaseInfo = cases(i)
            If entry.IsDefault OrElse type Is entry.Target OrElse entry.Target.IsAssignableFrom(type) Then
                Dim act As Action(Of Object) = entry.Action
                act(source)
                Exit For
            End If
        Next
    End Sub


    Public Function IsType(Of T)(action As Action(Of T)) As CaseInfo
        Return New CaseInfo() With {.Action = Sub(x As Object)
                                                  action(CType(x, T))
                                              End Sub, .Target = GetType(T)}
    End Function


    Public Function Otherwise(action As Action) As CaseInfo
        Return New CaseInfo() With {.Action = Sub(x As Object)
                                                  action()
                                              End Sub, .IsDefault = True}
    End Function

End Module