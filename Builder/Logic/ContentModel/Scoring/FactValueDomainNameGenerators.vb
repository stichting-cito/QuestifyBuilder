Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class FactValueDomainNameGenerators

        Public Shared Function GetDomainByScoringParam(ByVal scoreParameter As ScoringParameter) As String
            If (Not String.IsNullOrEmpty(ScoreParameter.InlineId)) Then
                Return ScoreParameter.InlineId
            Else
                If (Not String.IsNullOrEmpty(ScoreParameter.CollectionIdx)) Then
                    Return ScoreParameter.CollectionIdx
                Else
                    Return ScoreParameter.ControllerId
                End If
            End If
        End Function

        Public Shared Function GetDomainByScoringParamPlusPrefix(scoreParameter As ScoringParameter, currentKeyId As String, key As String) As String

            If String.IsNullOrEmpty(currentKeyId) Then
                currentKeyId = DefaultStringOperations.GetSubParameterId(key)
            End If

            If (Not String.IsNullOrEmpty(scoreParameter.InlineId)) Then
                Return $"{currentKeyId}-{scoreParameter.InlineId}"
            Else
                If (Not String.IsNullOrEmpty(scoreParameter.CollectionIdx)) Then
                    Return scoreParameter.CollectionIdx
                Else
                    Return $"{currentKeyId}-{scoreParameter.ControllerId}"
                End If
            End If
        End Function

        Public Shared Function GetDomainByScoringParamPlusPostFix(scoreParameter As ScoringParameter, postfix As String, key As String) As String

            If String.IsNullOrEmpty(postfix) Then
                postfix = DefaultStringOperations.GetSubParameterId(key)
            End If

            If (Not String.IsNullOrEmpty(scoreParameter.InlineId)) Then
                Return scoreParameter.InlineId + postfix
            Else
                If (Not String.IsNullOrEmpty(scoreParameter.CollectionIdx)) Then
                    Debug.Assert(False)
                    Return scoreParameter.CollectionIdx
                Else
                    Return scoreParameter.ControllerId + postfix
                End If
            End If
        End Function

        Shared Function GetDomainByVariable(currentKeyId As String) As String
            Return currentKeyId
        End Function

    End Class
End Namespace