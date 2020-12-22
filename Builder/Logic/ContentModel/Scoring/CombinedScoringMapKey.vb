Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <DebuggerDisplay("{Name} isGroup:{IsGroup}")>
    Public Class CombinedScoringMapKey : Implements IEnumerable(Of ScoringMapKey)

        Public Shared Function Create(ParamArray scoringMapKeys() As ScoringMapKey) As CombinedScoringMapKey
            Return Create(DirectCast(scoringMapKeys, IEnumerable(Of ScoringMapKey)))
        End Function

        Public Shared Function Create(scoringMapKeys As IEnumerable(Of ScoringMapKey), Optional setLocations As IEnumerable(Of Integer) = Nothing) As CombinedScoringMapKey
            Dim ret As New CombinedScoringMapKey(scoringMapKeys, setLocations)
            Return ret
        End Function

        Private ReadOnly _scoringMapKeys As IEnumerable(Of ScoringMapKey)
        Private ReadOnly _setLocations As List(Of Integer)


        Private Sub New(scoringMapKeys As IEnumerable(Of ScoringMapKey), Optional setLocations As IEnumerable(Of Integer) = Nothing)
            _scoringMapKeys = scoringMapKeys

            _setLocations = If(setLocations, Enumerable.Empty(Of Integer)()).ToList()

            Dim suppress = New HashSet(Of ScoringParameter)
            Name = String.Join(" & ", scoringMapKeys.Where(Function(isDealtWith) Not suppress.Contains(isDealtWith.ScoringParameter)).
                                Select(Function(key)
                                           If key.ScoringParameter.IsSingleChoice Then
                                               suppress.Add(key.ScoringParameter)
                                               Return key.ScoringParameterName
                                           Else
                                               Return key.Name
                                           End If
                                       End Function))

            If (_setLocations.Count = 1 AndAlso _setLocations(0) = ScoringMap.InFactSetToBeAssigned) Then
                _setLocations.Clear()
            End If

        End Sub

        Public ReadOnly Property Name As String

        Public ReadOnly Property IsGroup As Boolean
            Get
                Return GetIsGroup()
            End Get
        End Property

        Private Function GetIsGroup() As Boolean

            Dim keys = New List(Of String)()

            For Each scoringMapKey In _scoringMapKeys

                If scoringMapKey.ScoringParameter.IsSingleChoice Then

                    Dim scoringMapName = scoringMapKey.ScoringParameterName

                    If (String.IsNullOrEmpty(scoringMapName)) Then
                        scoringMapName =
                            $"{scoringMapKey.ScoringParameter.GetType().Name} - { _
                                scoringMapKey.ScoringParameter.GetHashCode()}"
                    End If

                    If Not keys.Contains(scoringMapName) Then
                        keys.Add(scoringMapName)
                    End If

                Else
                    Dim scoringMapName = $"{scoringMapKey.Name} - {scoringMapKey.ScoreKey}"
                    If Not String.IsNullOrEmpty(scoringMapKey.ScoringParameter.InlineId) Then scoringMapName =
                        $"{scoringMapKey.ScoringParameter.InlineId} - {scoringMapKey.ScoreKey}"

                    If Not keys.Contains(scoringMapName) Then
                        keys.Add(scoringMapName)
                    End If
                End If
            Next

            Dim isGroup = keys.Count > 1

            Return isGroup
        End Function

        Public ReadOnly Property SetNumbers As IEnumerable(Of Integer)
            Get
                Return _setLocations
            End Get
        End Property

        Private Function GetEnumerator() As IEnumerator(Of ScoringMapKey) Implements IEnumerable(Of ScoringMapKey).GetEnumerator
            Return _scoringMapKeys.GetEnumerator()
        End Function

        Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _scoringMapKeys.GetEnumerator()
        End Function

    End Class

End Namespace
