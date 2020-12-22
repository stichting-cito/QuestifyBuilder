Imports System.Threading

Public NotInheritable Class ListManipulation


    Public Shared Function ShuffleList(org As IList) As IList
        Dim rnd As Random = New Random
        Dim arr As IList = CType(org, IList)
        Dim newPos As Integer
        Dim tempObj As Object
        Dim index As Integer = arr.Count

        While Interlocked.Decrement(index) >= 0
            newPos = rnd.Next(arr.Count)
            tempObj = arr(index)
            arr(index) = arr(newPos)
            arr(newPos) = tempObj
        End While
        Return arr
    End Function

    Private Sub New()
    End Sub

End Class
