
Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class DecimalAdapter
        Inherits BaseAdapter(Of DecimalScoring)
        Protected Overrides Function MakeParameter(input As DecimalScoring) As ScoringParameter
            Dim ret As DecimalScoringParameter = New DecimalScoringParameter() With {.IntegerPartMaxLength = input.MaxIntegerPart, .FractionPartMaxLength = input.MaxFactionPart, .SupportCasScoring = True}

            Dim values As ParameterSetCollection = New ParameterSetCollection()
            values.Add(New ParameterCollection() With {.Id = "A"})
            ret.Value = values

            Return ret
        End Function
    End Class
End Namespace
