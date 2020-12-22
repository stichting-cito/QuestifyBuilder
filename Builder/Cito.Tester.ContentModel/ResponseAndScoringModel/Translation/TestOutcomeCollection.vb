
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("testOutcomeCollection")> _
Public Class TestOutcomeCollection
    Inherits ValidatingEntityCollectionBase(Of TestOutcome)


    Private _scores As New SortedList(Of Double, TestOutcome)



    Public Overloads Sub Add(score As Double, inclusive As Boolean, outcomeValue As String)
        Dim testOutcome As New TestOutcome

        testOutcome.Score = score
        testOutcome.Inclusive = inclusive
        testOutcome.Outcome = outcomeValue
        MyBase.Add(testOutcome)
    End Sub

    Public Overloads Sub Remove(score As Double)
        For Each testOutcome As TestOutcome In Me
            If testOutcome.Score = score Then
                MyBase.Remove(testOutcome)
                Return
            End If
        Next
    End Sub

    Public Overloads Sub Sort()
        MyBase.Sort(New TestOutcomeCollectionComparer)
    End Sub



    Private Class TestOutcomeCollectionComparer
        Implements IComparer(Of TestOutcome)

        Public Function Compare(x As TestOutcome, y As TestOutcome) As Integer Implements IComparer(Of TestOutcome).Compare
            Return x.Score.CompareTo(y.Score)
        End Function

    End Class


End Class