Imports System.Text.RegularExpressions

Namespace ContentModel

    Public Class DefaultStringOperations

        Friend Shared ReadOnly FactIdMatch As New Regex("(\w+-)*(\w+)(?:\[[A-Za-z0-9_*]+\])?(-\w+)*")

        Friend Shared ReadOnly ScoringMapKeyMatch As New Regex("(\w+-)*(\w+)(?:\[[A-Za-z0-9_*]+\])?")

        Friend Shared ReadOnly AnswerCategoryPart As New Regex("(?:\w+-)*(?:\w+)(\[[A-Za-z0-9_*]+\])?")

        Friend Const CCatchAll As String = "{∗}"

        Public Shared Function FactIdEquals(id1 As String, id2 As String) As Boolean
            Return id1.EqualStringByRegex(id2, FactIdMatch)
        End Function

        Public Shared Function GetSubParameterId(scoringMapKey As String) As String
            Return StringRegexOp.GetCapturedString(scoringMapKey, ScoringMapKeyMatch)
        End Function

        Public Shared Function GetAnswerCategoryId(scoringMapKey As String) As String
            Return StringRegexOp.GetCapturedString(scoringMapKey, AnswerCategoryPart)
        End Function

        Public Shared Function IsCatchAllFactId(factId As String) As Boolean
            Return GetAnswerCategoryId(factId) = "[*]"
        End Function


        Public Shared Function IsCatchAllOrAnswerCategoryFactId(factId As String) As Boolean
            If (String.IsNullOrEmpty(factId)) Then Return False
            Dim result = GetAnswerCategoryId(factId)
            Return result.StartsWith("[") AndAlso result.EndsWith("]")
        End Function

        Public Shared Function GetNumberFromFactId(factId As String) As Integer?
            Dim tmpResult = GetAnswerCategoryId(factId)
            If (tmpResult.Length > 2) Then
                Dim result As Integer
                If (Integer.TryParse(tmpResult.Substring(1, tmpResult.Length - 2), result)) Then
                    Return result
                End If
            End If
            Return Nothing
        End Function

        Friend Shared Function IsKeyValueCatchAll(value As String) As Boolean
            Return value = CCatchAll
        End Function

    End Class

End Namespace