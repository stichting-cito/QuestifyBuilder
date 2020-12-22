
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend Class FindingToFindingComparer

        Private ReadOnly _fromFinding As IFindingManipulator
        Private ReadOnly _destFinding As IFindingManipulator

        Public Sub New(fromScoring As IFindingManipulator, destination As IFindingManipulator)
            _fromFinding = fromScoring
            _destFinding = destination
            Logic = New DefaultFindingToFindingManipulatorLogic()
        End Sub

        Public Property Logic As IFindingToFindingManipulatorLogic

        Public Function AreEqual() As Boolean

            _fromFinding.SetFactSetTarget(Nothing)
            _destFinding.SetFactSetTarget(Nothing)
            _destFinding.SetDomainOverride(Function(id) GetDomainFromSource(id, _fromFinding))

            If Not CompareForCurrentTarget() Then Return False

            For Each sourceTarget In _fromFinding.SetNumbers
                _fromFinding.SetFactSetTarget(sourceTarget)

                If _fromFinding.GetIds().ToList().All(Function(id) _fromFinding.GetKeys(id).Count > 0 AndAlso _fromFinding.GetKeys(id).All(Function(k) k.ToList().All(Function(b) b.HasValue()))) Then
                    Dim destinationTarget As Integer = Logic.GetDestinationTarget(_fromFinding, sourceTarget, _destFinding)
                    _destFinding.SetFactSetTarget(destinationTarget)

                    If Not CompareForCurrentTarget() Then Return False
                End If
            Next

            Return True
        End Function

        Private Function CompareForCurrentTarget() As Boolean
            Dim result As Boolean = True
            Dim presentKeys As New HashSet(Of String)(_destFinding.GetIds())
            If _fromFinding.GetIds().Any(Function(id) Not presentKeys.Contains(id)) Then Return False

            _fromFinding.GetIds().ToList().ForEach(Sub(id)
                                                       If result Then
                                                           Dim fromFacts = New HashSet(Of KeyFact)(_fromFinding.GetFacts(id).Cast(Of KeyFact), New FactEqualityComparer())
                                                           If Not fromFacts.SetEquals(_destFinding.GetFacts(id).Cast(Of KeyFact)) Then result = False
                                                       End If
                                                   End Sub)

            Return result
        End Function

        Private Function GetDomainFromSource(id As String, from As IFindingManipulator) As String
            Dim facts = from.GetFacts(id).Cast(Of KeyFact).FirstOrDefault()

            If (facts IsNot Nothing) Then
                Dim keyValue = facts.Values.FirstOrDefault()

                If (keyValue IsNot Nothing) Then Return keyValue.Domain
            End If

            Debug.Assert(False)
            Return id
        End Function

    End Class

End Namespace