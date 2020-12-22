Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(GraphGapMatchScoringParameter))>
    Friend Class GraphGapMatchScoringManipulator : Inherits BaseScoringManipulator(Of GraphGapMatchScoringParameter) : Implements IValidatingChoiceArrayScoringManipulator(Of String)

        Private ReadOnly _gapMatchScoringParameter As GraphGapMatchScoringParameter

        Public Sub New(manipulator As IFindingManipulator, param As GraphGapMatchScoringParameter)
            MyBase.New(manipulator, param)
            _gapMatchScoringParameter = param
        End Sub

        Public Sub SetKey(ByVal key As String, ByVal value As NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.SetKey
            Dim toUseKey = GetFactIdentfier(key)
            RemoveKey(key)
            If value.NoValueIsCorrect Then
                Manipulator.SetKey(toUseKey, New NoValue)
            Else
                Manipulator.SetKey(toUseKey, value)
            End If
        End Sub

        Public Sub Clear() Implements IChoiceArrayScoringManipulator.Clear
            RemoveAllManipulatableFacts()
        End Sub

        Public Function GetKeyStatus() As IDictionary(Of String, NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.GetKeyStatus

            Dim keys As New Dictionary(Of String, NoValueType(Of String))

            Dim ids = GetKeysAlreadyManipulated()
            For Each id In ids

                Dim result = Manipulator.GetKeys(GetFactIdentfier(id))
                For Each e In result
                    For Each v In e
                        If TypeOf v Is NoValue Then
                            keys.Add(id, New NoValueType(Of String)(True))
                        End If
                        Dim val = TryCast(v, StringValue)
                        If (val Is Nothing OrElse String.IsNullOrWhiteSpace(val.Value.Trim())) Then
                            Continue For
                        End If
                        keys.Add(id, val.Value.Trim())
                    Next
                Next
            Next

            Dim ret As New Dictionary(Of String, NoValueType(Of String))

            If (ScoreParameter.Value IsNot Nothing) Then
                For Each p In ScoreParameter.Value
                    If keys.ContainsKey(p.Id) Then
                        ret.Add(p.Id, keys(p.Id))
                    Else
                        ret.Add(p.Id, String.Empty)
                    End If
                Next
            End If

            Return ret
        End Function

        Public Sub RemoveKey(ByVal key As String) Implements IChoiceArrayScoringManipulator.RemoveKey
            Manipulator.RemoveFact(GetFactIdentfier(key))
        End Sub


        Public Function IsValid(ByVal value As String) As Boolean Implements IValidatingChoiceArrayScoringManipulator(Of String).IsValid

            If String.IsNullOrEmpty(value) Then
                Return True
            End If

            If (ScoreParameter.IsTransformed) Then
                Dim countOfThisValue As Integer = GetKeyStatus().Where(Function(k) Not k.Value.NoValueIsCorrect AndAlso k.Value.Value.Contains(value)).Count()
                If countOfThisValue > 0 Then
                    Dim matchMax = GetMatchMax(value)
                    Return IsValidBusinessRule(countOfThisValue, matchMax)
                End If
            End If

            Return True
        End Function


        Private Shared Function IsValidBusinessRule(ByVal countOfThisKey As Integer, ByVal matchMax As Integer) As Boolean

            If matchMax = 0 Then
                Return True
            End If

            If (countOfThisKey > matchMax) Then
                Return False
            End If

            Return True
        End Function


        Function GetMatchMax(key As String) As Integer
            If String.IsNullOrEmpty(key) Then Return Integer.MaxValue

            If (_gapMatchScoringParameter.IsTransformed) Then
                Dim matchMax = _gapMatchScoringParameter.Gaps.FirstOrDefault(Function(t) t.Key = key).Value.FirstOrDefault(Function(v) v.Key = GapMatchScoringParameter.GapMatchMax).Value
                Dim matchResult As Integer
                If Integer.TryParse(matchMax, matchResult) Then
                    Return matchResult
                End If
            Else
                Dim par = _gapMatchScoringParameter.Value.FirstOrDefault(Function(t) t.Id = key).InnerParameters.FirstOrDefault()
                Dim matchMax = DirectCast(par, GapImageParameter).MatchMax
                Return matchMax
            End If

        End Function

        Public Overrides Function GetDomainForKey(ByVal key As String) As String
            Return FactValueDomainNameGenerators.GetDomainByVariable(key)
        End Function

    End Class
End Namespace