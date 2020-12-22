Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class FactEqualityComparer
        Implements IEqualityComparer(Of KeyFact)

        Public Function CompareEquals(ByVal x As KeyFact, ByVal y As KeyFact) As Boolean Implements IEqualityComparer(Of KeyFact).Equals
            Return (FactIdAndValuesCountSame(x, y) AndAlso KeyValuesEqual(x, y))
        End Function

        Public Function CompareGetHashCode(ByVal obj As KeyFact) As Integer Implements IEqualityComparer(Of KeyFact).GetHashCode
            Return obj.Id.GetHashCode() Xor obj.ToString().GetHashCode()
        End Function

        Private Function FactIdAndValuesCountSame(ByVal x As KeyFact, ByVal y As KeyFact) As Boolean

            Dim factIdsAreEqual = x.Id = y.Id
            Dim keyValueCountAreEqual = x.Values.Count = y.Values.Count

            Return (factIdsAreEqual AndAlso keyValueCountAreEqual)
        End Function

        Private Function KeyValuesEqual(ByVal x As KeyFact, ByVal y As KeyFact) As Boolean
            Debug.Assert(x.Values.Count <= 1, "Unable to deal with multiple values quite yet.")

            If (x.Values.Count = 1) Then
                Return x.Values(0).ToString() = y.Values(0).ToString()
            End If

            Return (x.Values.Count = 0)

        End Function

    End Class

End Namespace
