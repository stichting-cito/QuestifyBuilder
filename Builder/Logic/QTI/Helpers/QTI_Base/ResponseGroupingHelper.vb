Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Helpers.QTI_Base

    Public Class ResponseGroupingHelper


        Private ReadOnly _solutionIdentifierHelper As SolutionIdentifierHelper



        Public Sub New()
            _solutionIdentifierHelper = New SolutionIdentifierHelper
        End Sub

        Public Sub New(solutionIdentifierHelper As SolutionIdentifierHelper)
            _solutionIdentifierHelper = solutionIdentifierHelper
        End Sub


        Public ReadOnly Property IdentifierHelper As SolutionIdentifierHelper
            Get
                Return _solutionIdentifierHelper
            End Get
        End Property

        Public Function GetGroups(finding As KeyFinding) As IEnumerable(Of IEnumerable(Of KeyFactSet))

            Dim groups = New List(Of List(Of KeyFactSet))()

            For Each factSet In finding.KeyFactsets
                Dim group As List(Of KeyFactSet) = GetGroup(groups, factSet)
                If Not group.Any() Then groups.Add(group)
                group.Add(factSet)
            Next

            Return groups
        End Function

        Private Function GetGroup(groups As List(Of List(Of KeyFactSet)), factSet As KeyFactSet) As List(Of KeyFactSet)

            For Each group In groups
                If group.Any() AndAlso (SameFactIds(group.First().Facts, factSet.Facts) OrElse ValueDomainAlreadyInOtherGroup(group.First().Facts, factSet.Facts)) Then
                    Return group
                End If
            Next

            Return New List(Of KeyFactSet)
        End Function

        Private Function SameFactIds(facts1 As List(Of BaseFact), facts2 As List(Of BaseFact)) As Boolean

            If TypeOf facts2.First() Is ConceptFact Then
                Dim catchAllValue = facts2.All(Function(x) DirectCast(x.Values.First(), ConceptValue).Values.All(Function(bv) TypeOf bv Is CatchAllValue))
                If catchAllValue Then
                    Dim uniqueDomains1 = GetUniqueDomainsFromFactSet(facts1)
                    Dim uniqueDomains2 = GetUniqueDomainsFromFactSet(facts2)
                    If uniqueDomains1.Count <> uniqueDomains2.Count Then Return False
                    For Each domain As String In uniqueDomains1
                        If Not uniqueDomains2.Contains(domain) Then Return False
                    Next
                    Return True
                End If
            End If

            If facts1.Count <> facts2.Count Then Return False

            Dim orderedFacts1 = facts1.OrderBy(Function(f) f.Values.First().Domain)
            Dim orderedFacts2 = facts2.OrderBy(Function(f) f.Values.First().Domain)

            For i As Integer = 0 To orderedFacts1.Count - 1

                Dim domain1 As String = orderedFacts1(i).Values.First().Domain
                Dim domain2 As String = orderedFacts2(i).Values.First().Domain

                If domain1.Contains("-I") AndAlso domain2.Contains("-I") Then
                    If domain1.Substring(domain1.IndexOf("-I")) <> domain2.Substring(domain2.IndexOf("-I")) Then
                        Return False
                    End If
                ElseIf TypeOf orderedFacts1(i) Is ConceptFact Then
                    If _solutionIdentifierHelper.GetStrippedId(domain1) <> _solutionIdentifierHelper.GetStrippedId(domain2) Then
                        Return False
                    End If
                Else
                    If orderedFacts1(i).Id <> orderedFacts2(i).Id Then
                        Return False
                    End If
                End If
            Next

            Return True
        End Function

        Private Function ValueDomainAlreadyInOtherGroup(facts1 As List(Of BaseFact), facts2 As List(Of BaseFact)) As Boolean
            If facts2.GroupBy(Function(f) f.Values.First().Domain).Count > 1 Then
                For Each fact As BaseFact In facts2
                    If facts1.Any(Function(f) f.Values.First().Domain = fact.Values.First().Domain) Then
                        Return True
                    End If
                Next
            End If

            Return False
        End Function

        Private Function GetUniqueDomainsFromFactSet(facts As List(Of BaseFact)) As List(Of String)
            Dim uniqueDomains As New List(Of String)
            For Each fact As KeyFact In facts
                Dim domain As String = _solutionIdentifierHelper.GetStrippedId(fact.Values(0).Domain, True)
                If Not uniqueDomains.Contains(domain) Then uniqueDomains.Add(domain)
            Next
            Return uniqueDomains
        End Function

    End Class
End NameSpace