Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend NotInheritable Class FactSetEqualityComparer
        Implements IEqualityComparer(Of KeyFactSet)

        Public Function CompareEquals(ByVal x As KeyFactSet, ByVal y As KeyFactSet) As Boolean Implements IEqualityComparer(Of KeyFactSet).Equals
            Return FactsCountAreEqual(x, y) AndAlso FactIdsAreEqual(x, y) AndAlso FactsAreEqual(x, y)
        End Function

        Private Function FactsCountAreEqual(ByVal set1 As KeyFactSet, ByVal set2 As KeyFactSet) As Boolean
            Return set1.Facts.Count = set2.Facts.Count
        End Function

        Private Function FactIdsAreEqual(ByVal set1 As KeyFactSet, ByVal set2 As KeyFactSet) As Boolean
            Dim s1 = New HashSet(Of String)(set1.Facts.Select(Function(fact) fact.Id))
            Dim ret = s1.SetEquals(set2.Facts.Select(Function(fact) fact.Id))
            Return ret
        End Function

        Private Function FactsAreEqual(ByVal set1 As KeyFactSet, ByVal set2 As KeyFactSet) As Boolean

            Dim s1 = New HashSet(Of KeyFact)(set1.Facts.Cast(Of KeyFact), New FactEqualityComparer())
            Dim ret = s1.SetEquals(set2.Facts.Cast(Of KeyFact))
            Return ret
        End Function

        Public Function CompareGetHashCode(ByVal obj As KeyFactSet) As Integer Implements IEqualityComparer(Of KeyFactSet).GetHashCode
            obj.Facts.Select(Function(fact) fact.Id.GetHashCode())
        End Function

    End Class

End Namespace