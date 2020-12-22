Namespace CustomInteractions

    Class CoordinateScoringValidation : Implements IScoringValidation

        Private ReadOnly _coordinateScoring As CoordinateScoring

        Sub New(coordinateScoring As CoordinateScoring)
            _coordinateScoring = coordinateScoring
        End Sub


        Function IsValid(ByRef errorMessage As List(Of String)) As Boolean Implements IScoringValidation.IsValid
            Dim valid = True
            If HasCorrectResponse() AndAlso Not (HasXaxis() AndAlso HasYaxis()) Then
                errorMessage.Add(String.Format(My.Resources.CoordinateCorrectResponseNotValid, "(x:0.4)(y:1.5)"))
                valid = False
            End If
            Return valid
        End Function



        Function Xaxis() As Decimal?
            If Not String.IsNullOrEmpty(_coordinateScoring.CorrectResponse) Then
                Dim decimalX As Decimal
                If _coordinateScoring.CorrectResponse.GetCoordinateX.IsCultureInvariantDecimal(decimalX) Then
                    Return decimalX
                End If
            End If
            Return Nothing
        End Function

        Function HasXaxis() As Boolean
            Return Xaxis() IsNot Nothing
        End Function


        Function Yaxis() As Decimal?
            If Not String.IsNullOrEmpty(_coordinateScoring.CorrectResponse) Then
                Dim decimalY As Decimal
                If _coordinateScoring.CorrectResponse.GetCoordinateY.IsCultureInvariantDecimal(decimalY) Then
                    Return decimalY
                End If
            End If
            Return Nothing
        End Function

        Function HasYaxis() As Boolean
            Return Yaxis() IsNot Nothing
        End Function

        Private Function HasCorrectResponse() As Boolean
            Return Not String.IsNullOrEmpty(_coordinateScoring.CorrectResponse)
        End Function
    End Class
End Namespace