<Serializable> _
Public Class ResponseCollection
    Inherits List(Of Response)

    Public Sub SortOnResponseNumber()
        Me.Sort(New ResponseNumberComparer)
    End Sub

    Public Function GetLastResponseNumberInCollection() As Long
        Dim highestNumber As Long = 0

        For Each r As Response In Me
            If r.ResponseNumber > highestNumber Then
                highestNumber = r.ResponseNumber
            End If
        Next

        Return highestNumber
    End Function

    Public Function GetResponseByResponseNr(nr As Long) As Response

        For Each r As Response In Me
            If r.ResponseNumber = nr Then
                return r
            End If
        Next

        Return Nothing
    End Function

    Public Function GetResponseByItemIdentifier(itemId As String) As Response

        For Each r As Response In Me
            If r.Id = itemId Then
                return r
            End If
        Next

        Return nothing
    End Function

End Class


Public Class ResponseNumberComparer
    Implements IComparer(Of Response)

    Public Function Compare(x As Response, y As Response) As Integer Implements IComparer(Of Response).Compare
        Return CInt(x.ResponseNumber - y.ResponseNumber)
    End Function
End Class

