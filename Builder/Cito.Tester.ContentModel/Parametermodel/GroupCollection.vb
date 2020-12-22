Public Class GroupCollection
    Inherits List(Of Group)

    Public Function GetGroupByIdentifier(identifier As String) As Group
        For Each setting As Group In Me
            If setting.Identifier.Equals(identifier) Then
                Return setting
            End If
        Next

        Return Nothing
    End Function

    Public Function GetGroupByTitle(title As String) As Group
        For Each setting As Group In Me
            If setting.Title.Equals(title) Then
                Return setting
            End If
        Next

        Return Nothing
    End Function
End Class
