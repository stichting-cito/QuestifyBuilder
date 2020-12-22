Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend Class FindingToFindingManipulator

        Private ReadOnly _from As IFindingManipulator
        Private ReadOnly _dest As IFindingManipulator

        Public Sub New(fromScoring As IFindingManipulator, destination As IFindingManipulator)
            _from = fromScoring
            _dest = destination
            Logic = New DefaultFindingToFindingManipulatorLogic()
        End Sub

        Public Property Logic As IFindingToFindingManipulatorLogic

        Public Sub Execute()

            _from.SetFactSetTarget(Nothing)
            _dest.SetFactSetTarget(Nothing)

            _dest.SetDomainOverride(Function(id) GetDomainFromSource(id, _from))

            DoActionForCurrentTarget()

            For Each sourceTarget In _from.SetNumbers
                _from.SetFactSetTarget(sourceTarget)
                Dim destinationTarget As Integer = Logic.GetDestinationTarget(_from, sourceTarget, _dest)
                _dest.SetFactSetTarget(destinationTarget)

                DoActionForCurrentTarget()
            Next

        End Sub

        Private Sub DoActionForCurrentTarget()

            Dim presentKeys As New HashSet(Of String)(_dest.GetIds())

            For Each id In _from.GetIds()

                For Each x In _from.GetKeys(id)
                    If (presentKeys.Contains(id)) Then
                        _dest.UnSetKey(id)
                    End If
                    _dest.SetKeyWithOptionals(id, x.ToArray())
                Next

                If _from.GetFacts(id).Count > 0 AndAlso _from.GetKeys(id).Count = 0 Then
                    _dest.FindOrCreateFact(id)
                End If

                DoPreProcessing(id)
            Next

        End Sub

        Private Sub DoPreProcessing(id As String)
            Dim rules = _from.GetPreProcessingMethods(id)
            _dest.SetPreProcessingMethods(id, rules)
        End Sub

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
