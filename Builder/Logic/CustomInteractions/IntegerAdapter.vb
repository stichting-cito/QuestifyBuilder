Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class IntegerAdapter
        Inherits BaseAdapter(Of IntegerScoring)
        Protected Overrides Function MakeParameter(input As IntegerScoring) As ScoringParameter
            Dim ret As IntegerScoringParameter = New IntegerScoringParameter() With {.MaxLength = input.MaxLength}

            Dim values As ParameterSetCollection = New ParameterSetCollection()
            values.Add(New ParameterCollection() With {.Id = "A"})
            ret.Value = values

            Return ret
        End Function
    End Class
End Namespace
