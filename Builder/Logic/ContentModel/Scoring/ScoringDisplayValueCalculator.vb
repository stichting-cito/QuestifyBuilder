Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring


    Public Class ScoringDisplayValueCalculator

        Private ReadOnly _scoringParameters As IEnumerable(Of ScoringParameter)
        Private ReadOnly _solution As Solution

        Public Sub New(scoringParameters As IEnumerable(Of ScoringParameter), solution As Solution)
            _scoringParameters = scoringParameters
            _solution = solution
        End Sub


        Public Function GetScoreDisplayValue() As String
            Dim scoringMap = New ScoringMap(_scoringParameters, _solution).GetMap()
            Dim findings As New List(Of String)
            For Each finding In _solution.Findings
                Dim groups As New Dictionary(Of CombinedScoringMapKey, IList(Of String))
                For Each combinedScoringKey As CombinedScoringMapKey In scoringMap

                    If Not groups.ContainsKey(combinedScoringKey) Then
                        groups.Add(combinedScoringKey, New List(Of String))
                    End If

                    Dim result As String
                    If (combinedScoringKey.IsGroup) Then
                        For Each setNumber In combinedScoringKey.SetNumbers
                            result = GetScoreDisplayValueFor(combinedScoringKey, finding.Id, setNumber)
                            If Not String.IsNullOrEmpty(result) Then
                                result = $"({result})"
                            End If
                            groups(combinedScoringKey).Add(result)
                        Next
                    Else
                        result = GetScoreDisplayValueFor(combinedScoringKey, finding.Id, Nothing)
                        groups(combinedScoringKey).Add(result)
                    End If
                Next

                Dim groupDisplayStrings As New List(Of String)
                For Each group As IList(Of String) In groups.Values
                    groupDisplayStrings.Add(String.Join("|", group.Where(Function(s) Not String.IsNullOrEmpty(s))))
                Next
                findings.Add(String.Join("&", groupDisplayStrings.Where(Function(s) Not String.IsNullOrEmpty(s))))
            Next

            If _solution.AspectReferenceSetCollection IsNot Nothing AndAlso _solution.AspectReferenceSetCollection.Count = 1 Then
                findings.Add(String.Join(",", _solution.AspectReferenceSetCollection.Item(0).Items.OrderBy(Function(x) x.SourceName).Select(Function(x) x.SourceName).ToArray()))
            End If

            Return String.Join("|", findings.Where(Function(s) Not String.IsNullOrEmpty(s)))

        End Function

        Private Function GetScoreDisplayValueFor(combinedScoringKey As CombinedScoringMapKey, findingId As String, setNumber As Integer?) As String

            Dim result As New List(Of String)
            For Each sk As ScoringMapKey In combinedScoringKey.Where(Function(k As ScoringMapKey) k.ScoringParameter.FindingId = findingId)
                Dim manipulator = GetScoreManipulator(sk.ScoringParameter, _solution)
                if manipulator IsNot nothing Then
                    manipulator.SetFactSetTarget(setNumber)
                    Dim value = manipulator.GetDisplayValueForKey(sk.ScoreKey)
                    If Not String.IsNullOrEmpty(value) Then
                        result.Add(value)
                    End If
                End If
            Next
            Return String.Join("&", result)
        End Function

    End Class
End Namespace