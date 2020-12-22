Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class ChoiceAdapter
        Inherits BaseAdapter(Of ChoiceScoring)
        Protected Overrides Function MakeParameter(input As ChoiceScoring) As ScoringParameter
            Dim ret As ChoiceScoringParameter = New ChoiceScoringParameter() With {.MaxChoices = input.MaxChoices, .MinChoices = input.MinChoices}
            ret.Value = MakeChoices(input.Choices)
            Return ret
        End Function

        Private Function MakeChoices(choices As SubChoice()) As ParameterSetCollection
            Dim ret As ParameterSetCollection = New ParameterSetCollection()

            For Each sbChoice As SubChoice In choices
                Dim alternative as ParameterCollection = New ParameterCollection() With {.Id = sbChoice.Id}

                If Not String.IsNullOrEmpty(sbChoice.Label) Then
                    alternative.InnerParameters.Add(New PlainTextParameter() With {.Value = sbChoice.Label})
                End If
                ret.Add(alternative)
            Next

            Return ret
        End Function
    End Class
End Namespace
