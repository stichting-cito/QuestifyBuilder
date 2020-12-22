Imports System.Globalization

Namespace CustomInteractions

    Class IntegerScoringValidation : Implements IScoringValidation

        Private ReadOnly _integerScoring As IntegerScoring

        Sub New(integerScoring As IntegerScoring)
            _integerScoring = integerScoring
        End Sub


        Public Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid
            Dim valid = True
            If HasCorrectResponse() AndAlso Not CorrectResponseIsAnInteger() Then
                errorMessage.Add(String.Format(My.Resources.IntegerScoringNotValid, _integerScoring.CorrectResponse))
                valid = False
            End If
            Return valid
        End Function

        Private Function CorrectResponseIsAnInteger() As Boolean
            Return Integer.TryParse(_integerScoring.CorrectResponse, NumberStyles.Any, CultureInfo.InvariantCulture, New Integer)
        End Function

        Private Function HasCorrectResponse() As Boolean
            Return Not String.IsNullOrEmpty(_integerScoring.CorrectResponse)
        End Function

    End Class
End Namespace