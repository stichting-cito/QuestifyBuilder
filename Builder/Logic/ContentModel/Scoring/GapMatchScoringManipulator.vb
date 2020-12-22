Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(GapMatchScoringParameter))>
    Friend Class GapMatchScoringManipulator : Inherits BaseScoringManipulator(Of GapMatchScoringParameter) : Implements IValidatingChoiceArrayScoringManipulator(Of String)

        Private ReadOnly _gapMatchScoringParameter As GapMatchScoringParameter
        Private _currentKeyId As String = String.Empty

        Public Sub New(manipulator As IFindingManipulator, param As GapMatchScoringParameter)
            MyBase.New(manipulator, param)
            _gapMatchScoringParameter = param
            manipulator.SetDomainOverride(Function(id) GetDomainForKey(id))
        End Sub

        Public Sub SetKey(ByVal key As String, ByVal gapId As NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.SetKey
            Debug.Assert(ValidateParam(key, gapId.ToString), "Arguments are not valid")

            _currentKeyId = key
            Dim toUseKey = GetFactIdentfier(key)
            RemoveKey(key)
            If gapId.NoValueIsCorrect Then
                Manipulator.SetKey(toUseKey, New NoValue)
            Else
                Manipulator.SetKey(toUseKey, gapId)
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
                        If (val Is Nothing OrElse String.IsNullOrWhiteSpace(val.Value)) Then
                            Continue For
                        End If
                        keys.Add(id, val.Value.Trim())
                    Next
                Next
            Next

            Dim ret As New Dictionary(Of String, NoValueType(Of String))

            If (ScoreParameter.Value IsNot Nothing) Then
                For Each subParam In ScoreParameter.Value
                    Dim id = subParam.Id
                    If (keys.ContainsKey(id)) Then
                        ret.Add(id, keys(id))
                    Else
                        ret.Add(id, String.Empty)
                    End If
                Next
            End If

            Return ret
        End Function

        Public Sub RemoveKey(ByVal key As String) Implements IChoiceArrayScoringManipulator.RemoveKey
            _currentKeyId = key
            Manipulator.RemoveFact(GetFactIdentfier(key))
        End Sub


        Public Function IsValid(ByVal key As String) As Boolean Implements IValidatingChoiceArrayScoringManipulator(Of String).IsValid

            If String.IsNullOrEmpty(key) Then
                Return True
            End If

            Dim countOfThisKey As Integer = GetKeyStatus().Where(Function(k) k.Value = key).Count()
            If countOfThisKey > 0 Then
                Dim matchMax = GetMatchMax(key)
                If (countOfThisKey > matchMax) Then
                    Return False
                End If
            End If

            Return True
        End Function


        Function GetMatchMax(key As String) As Integer
            If String.IsNullOrEmpty(key) Then Return Integer.MaxValue
            Dim matchMax = _gapMatchScoringParameter.Gaps.FirstOrDefault(Function(t) t.Key = key).Value.FirstOrDefault(Function(v) v.Key = GapMatchScoringParameter.GapMatchMax).Value
            Dim matchResult As Integer
            If Integer.TryParse(matchMax, matchResult) Then
                Return matchResult
            End If

            Return 0
        End Function

        Public Overrides Function GetDomainForKey(ByVal key As String) As String
            If (String.IsNullOrEmpty(_currentKeyId) AndAlso DefaultStringOperations.IsCatchAllOrAnswerCategoryFactId(key)) Then
                Dim toSet = DefaultStringOperations.GetSubParameterId(key)
                Return FactValueDomainNameGenerators.GetDomainByVariable(toSet)
            End If
            Return FactValueDomainNameGenerators.GetDomainByVariable(_currentKeyId)
        End Function

        Private Function ValidateParam(key As String, gapId As String) As Boolean

            Dim valid As Boolean = True
            valid = valid AndAlso KeyMustBeContainedInValue(key)
            valid = valid AndAlso GapIdMustBeContainedInGaps(gapId)

            Return valid
        End Function

        Private Function KeyMustBeContainedInValue(key As String) As Boolean
            Return ScoreParameter.Value.Any(Function(collection) collection.Id = key)
        End Function

        Private Function GapIdMustBeContainedInGaps(gapId As String) As Boolean
            Return (ScoreParameter.Gaps.ContainsKey(gapId) OrElse gapId = "Ø" OrElse String.IsNullOrEmpty(gapId))
        End Function

    End Class
End Namespace