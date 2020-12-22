Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class CoordinateAdapter
        Inherits BaseAdapter(Of CoordinateScoring)
        Protected Overrides Function MakeParameter(input As CoordinateScoring) As ScoringParameter
            Dim ret As DecimalScoringParameter = New DecimalScoringParameter() With {.FractionPartMaxLength = 1, .IntegerPartMaxLength = 4, .SupportCasScoring = True}

            Dim values As ParameterSetCollection = New ParameterSetCollection()
            values.Add(New ParameterCollection() With {.Id = "X"})
            values.Add(New ParameterCollection() With {.Id = "Y"})
            ret.Value = values
            Return ret
        End Function
    End Class
End Namespace
