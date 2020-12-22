Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class MathMlAdapter
        Inherits BaseAdapter(Of MathMlScoring)
        Protected Overrides Function MakeParameter(input As MathMlScoring) As ScoringParameter
            Dim ret As MathScoringParameter = New MathScoringParameter()

            Dim values As ParameterSetCollection = New ParameterSetCollection()
            values.Add(New ParameterCollection() With {.Id = "A"})
            ret.Value = values

            Return ret
        End Function
    End Class
End Namespace
