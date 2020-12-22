Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class GraphGapMatchScoringTransformedManipulator
        Inherits ValidatingChoiceArrayScoringManipulator(Of GraphGapMatchScoringParameter)

        Public Sub New(param As GraphGapMatchScoringParameter, decoree As IValidatingChoiceArrayScoringManipulator(Of String))
            MyBase.New(param, decoree)
            If (Not param.IsTransformed) Then Throw New ArgumentException()
        End Sub

        Protected Overrides Function GetDisplayValueForKey(ByVal key As String) As String
            Dim fallBackValue = MyBase.GetDisplayValueForKey(key)

            Dim gapsDictionary As Dictionary(Of String, String) = Nothing

            Dim status = GetKeyStatus()

            Debug.Assert(Param.IsTransformed, "Expected parameter to be transformed")

            Dim keyToUse As NoValueType(Of String) = Nothing
            If (status.TryGetValue(key, keyToUse)) Then
                If keyToUse.NoValueIsCorrect Then
                    Return keyToUse.ToString
                ElseIf (Param.Gaps.TryGetValue(keyToUse, gapsDictionary)) Then
                    Debug.Assert(gapsDictionary IsNot Nothing)

                    Dim name = gapsDictionary(GapMatchScoringParameter.GapMatchLabel)
                    If String.IsNullOrEmpty(name) Then
                        name = gapsDictionary(GapMatchScoringParameter.GapMatchName)
                        name = $"{name} {keyToUse}"
                    End If
                    Return name
                End If
            End If


            Return fallBackValue
        End Function
    End Class
End Namespace
