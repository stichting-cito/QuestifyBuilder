Imports System.Linq
Imports System.Xml.XPath
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Helpers.QTI30

    Public Class ResponseProcessingHelper

        Public Shared Function GetOrderedConceptFactSetGroups(groups As IEnumerable(Of IEnumerable(Of KeyFactSet)), responseTypeFactory As ResponseTypeFactory) As List(Of IEnumerable(Of KeyFactSet))
            Dim ret As New List(Of IEnumerable(Of KeyFactSet))
            Dim orderedGroups As New List(Of Tuple(Of Integer, IEnumerable(Of KeyFactSet)))
            For x As Integer = 0 To groups.Count - 1
                Dim firstIndex As Integer = Integer.MaxValue
                For Each kfs As KeyFactSet In groups(x)
                    Dim indexOfFirstFact As Integer = responseTypeFactory.GetConceptIndexByIdentifier(responseTypeFactory.GetFactIdentifier(CType(kfs.Facts.OrderBy(Function(kf) responseTypeFactory.GetConceptIndexByIdentifier(responseTypeFactory.GetFactIdentifier(kf))).First(), KeyFact)))
                    If indexOfFirstFact < firstIndex Then firstIndex = indexOfFirstFact
                Next
                orderedGroups.Add(New Tuple(Of Integer, IEnumerable(Of KeyFactSet))(firstIndex, groups(x)))
            Next
            For Each lkfs As Tuple(Of Integer, IEnumerable(Of KeyFactSet)) In orderedGroups.OrderBy(Function(z) z.Item1)
                ret.Add(lkfs.Item2)
            Next
            Return ret
        End Function

        Public Shared Function GetListOfUniqueDomainsFromConceptFindingFacts(finding As ConceptFinding, responseTypeFactory As ResponseTypeFactory) As Dictionary(Of String, Integer)
            Dim uniqueDomains As New Dictionary(Of String, Integer)
            Dim sortedFacts = finding.Facts.OrderBy(Function(f) responseTypeFactory.GetConceptIndexByIdentifier(responseTypeFactory.GetFactIdentifier(f)))
            For Each fact As KeyFact In sortedFacts
                Dim domain As String = responseTypeFactory.GetFactIdentifier(fact)
                If Not uniqueDomains.ContainsKey(domain) Then
                    uniqueDomains.Add(domain, 1)
                Else
                    uniqueDomains(domain) += 1
                End If
            Next
            Return uniqueDomains
        End Function

        Public Shared Sub AddResponseProcessingIfNeeded(navigator As XPathNavigator)
            navigator.MoveToRoot()
            If Not navigator.MoveToChild("qti-response-processing", String.Empty) Then
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-response-processing", Nothing, True)
            End If
        End Sub

        Friend Shared Function MergeResponseProcessing(valueFirst As ResponseProcessingType, valueSecond As ResponseProcessingType) As ResponseProcessingType
            If valueFirst IsNot Nothing AndAlso valueSecond IsNot Nothing Then
                valueFirst.Items.AddRange(valueSecond.Items)
                Return valueFirst
            End If

            If valueFirst Is Nothing AndAlso valueSecond IsNot Nothing Then
                Return valueSecond
            End If

            If valueFirst IsNot Nothing AndAlso valueSecond Is Nothing Then
                Return valueFirst
            End If

            Return Nothing
        End Function
    End Class
End Namespace