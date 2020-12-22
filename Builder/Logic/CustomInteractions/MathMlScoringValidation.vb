Imports System.Text.RegularExpressions

Namespace CustomInteractions

    Class MathMlScoringValidation : Implements IScoringValidation

        Private ReadOnly _mathmlScoring As MathMlScoring

        Sub New(mathmlScoring As MathMlScoring)
            _mathmlScoring = mathmlScoring
        End Sub

        Public Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid
            Dim valid = True
            If HasCorrectResponse() AndAlso Not LooksLikeMathML() Then
                errorMessage.Add(My.Resources.MathMlCorrectResponseNotValid)
                valid = False
            End If
            Return valid
        End Function

        Private Function LooksLikeMathML() As Boolean
            Return Regex.IsMatch(_mathmlScoring.CorrectResponse, "<math[>\s](?s).*?</math>")
        End Function

        Private Function HasCorrectResponse() As Boolean
            Return Not String.IsNullOrEmpty(_mathmlScoring.CorrectResponse)
        End Function
    End Class

End Namespace