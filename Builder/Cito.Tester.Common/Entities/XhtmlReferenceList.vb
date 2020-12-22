Public Class XhtmlReferenceList
    Inherits List(Of XhtmlReference)

    Public Function GetCountByType(type As XhtmlReferenceType) As Integer
        Dim count As Integer = 0
        For Each reference As XhtmlReference In Me
            If reference.Type = type Then
                count += 1
            End If
        Next

        Return count
    End Function

    Public Function GetReferenceById(id As String) As XhtmlReference
        For Each reference As XhtmlReference In Me
            If reference.ID = id Then
                Return reference
            End If
        Next
        Return Nothing
    End Function

    Public Function GetIndexById(id As String) As Integer
        For index As Integer = 0 To Me.Count - 1
            If Me(index).ID = id Then
                Return index
            End If
        Next
        Return -1
    End Function

    Public Function GetReferenceListFilterdByType(type As XhtmlReferenceType) As XhtmlReferenceList
        Dim list As New XhtmlReferenceList()
        For Each ref As XhtmlReference In Me
            If ref.Type = type Then
                list.Add(ref)
            End If
        Next
        Return list
    End Function
End Class