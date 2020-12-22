Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring.Validator

    Friend Class GroupsWithSameInteractionsAreEqual
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)

            Dim solution As Solution = item.Solution

            For Each finding In solution.Findings
                If SameInteractionInOtherCombinationFound(finding) Then
                    Throw New Exception()
                End If
            Next

        End Sub

        Private Function SameInteractionInOtherCombinationFound(keyFinding As KeyFinding) As Boolean

            Dim factSetList As List(Of factset) = ToListOfSimpleFactSet(keyFinding.KeyFactsets)

            Dim uniqueFactSets As New List(Of factset)

            For Each factSet In factSetList
                If Not FactSetExistsInList(uniqueFactSets, factSet) Then
                    If DomainMatch(uniqueFactSets, factSet) Then
                        Return True
                    Else
                        uniqueFactSets.Add(factSet)
                    End If
                End If
            Next

            Return False
        End Function

        Private Function DomainMatch(factSetList As List(Of factset), factSet As factset) As Boolean

            For Each fact In factSet.facts
                For Each value In fact.values
                    If DomainIsUsedInFactSet(value.domain, factSetList) Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function

        Private Function DomainIsUsedInFactSet(domain As String, factSetList As List(Of factset)) As Boolean

            For Each factSet In factSetList
                For Each fact In factSet.facts
                    For Each value In fact.values
                        If value.domain = domain Then
                            Return True
                        End If
                    Next
                Next
            Next

            Return False
        End Function

        Private Function FactSetExistsInList(list As List(Of Factset), factSet As Factset) As Boolean

            For Each fs In list
                If fs.facts.SetEquals(factSet.Facts) Then
                    Return True
                Else
                    Dim sameFactIds As Boolean = False
                    If factSet.Facts.Count > 0 AndAlso factSet.Facts.Count = fs.Facts.Count Then
                        sameFactIds = True
                        For Each kf In factSet.Facts
                            If Not fs.Facts.Any(Function(f) f.Id.Equals(kf.Id)) Then
                                sameFactIds = False
                            End If
                        Next
                    End If
                    If sameFactIds Then Return True
                End If
            Next

            Return False
        End Function

        Private Function ToListOfSimpleFactSet(factSets As List(Of KeyFactSet)) As List(Of Factset)
            Dim rFactSets As New List(Of Factset)()
            For Each factset In factSets
                Dim simpleFactSet As New Factset()
                For Each fact In factset.Facts
                    Dim simpleFact As New Fact() With {.Id = If(fact.Id IsNot Nothing, fact.Id, String.Empty)}
                    For Each value In fact.Values
                        Dim simpleValue As New Value() With {.Domain = value.Domain}
                        simpleFact.values.Add(simpleValue)
                    Next
                    simpleFactSet.facts.Add(simpleFact)
                Next
                rFactSets.Add(simpleFactSet)
            Next
            Return rFactSets
        End Function
    End Class
End Namespace
