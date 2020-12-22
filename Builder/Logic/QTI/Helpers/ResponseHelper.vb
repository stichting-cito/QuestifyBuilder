Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Helpers

    Public Class ResponseHelper
        Public Shared Function GetGroup(groups As List(Of List(Of KeyFactSet)), factSet As KeyFactSet) As List(Of KeyFactSet)
            For Each group In groups
                If group.Any() AndAlso (SameValueDomain(group.First().Facts, factSet.Facts) OrElse ValueDomainAlreadyInOtherGroup(group.First().Facts, factSet.Facts)) Then
                    Return group
                End If
            Next
            Return New List(Of KeyFactSet)
        End Function

        Private Shared Function SameValueDomain(facts1 As List(Of BaseFact), facts2 As List(Of BaseFact)) As Boolean
            If facts1.Count <> facts2.Count Then Return False

            Dim orderedFacts1 = facts1.OrderBy(Function(f) f.Values.First().Domain)
            Dim orderedFacts2 = facts2.OrderBy(Function(f) f.Values.First().Domain)

            For i As Integer = 0 To orderedFacts1.Count - 1
                If orderedFacts1(i).Values.First().Domain <> orderedFacts2(i).Values.First().Domain Then
                    Return False
                End If
            Next
            Return True
        End Function

        Private Shared Function ValueDomainAlreadyInOtherGroup(facts1 As List(Of BaseFact), facts2 As List(Of BaseFact)) As Boolean
            If facts2.GroupBy(Function(f) f.Values.First().Domain).Count > 1 Then
                For Each fact As BaseFact In facts2
                    If facts1.Any(Function(f) f.Values.First().Domain = fact.Values.First().Domain) Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function

    End Class
End NameSpace