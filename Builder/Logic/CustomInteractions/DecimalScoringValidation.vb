Namespace CustomInteractions

    Class DecimalScoringValidation : Implements IScoringValidation
        Private ReadOnly _decimalScoring As DecimalScoring

        Public Sub New(decimalScoring As DecimalScoring)
            _decimalScoring = decimalScoring
        End Sub


        Public Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid
            Dim valid = True
            If HasCorrectResponse() AndAlso Not _decimalScoring.CorrectResponse.IsCultureInvariantDecimal Then
                errorMessage.Add(String.Format(My.Resources.DecimalScoringNotValid, _decimalScoring.CorrectResponse))
                valid = False
            End If
            Return valid
        End Function

        Private Function HasCorrectResponse() As Boolean
            Return Not String.IsNullOrEmpty(_decimalScoring.CorrectResponse)
        End Function
    End Class
End Namespace