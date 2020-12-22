

<Serializable>
Public Class TransactionDataCollection
    Inherits List(Of TransactionData)

    Public Sub SortOnTransactionNumber()
        Me.Sort(New TransactionNumberComparer)
    End Sub



    Public Function GetLastTransactionNumberInCollection() As Long
        Dim highestNumber As Long = 0

        For Each transaction As TransactionData In Me
            If transaction.TransactionNumber > highestNumber Then
                highestNumber = transaction.TransactionNumber
            End If
        Next

        Return highestNumber
    End Function


    Public Function GetTransactionByTransactionNr(nr As Long) As TransactionData

        For Each transaction As TransactionData In Me
            If transaction.TransactionNumber = nr Then
                Return transaction
            End If
        Next

        Return Nothing
    End Function


End Class


Public Class TransactionNumberComparer
    Implements IComparer(Of TransactionData)


    Public Function Compare(x As TransactionData, y As TransactionData) As Integer Implements IComparer(Of TransactionData).Compare
        Return CInt(x.TransactionNumber - y.TransactionNumber)
    End Function
End Class

