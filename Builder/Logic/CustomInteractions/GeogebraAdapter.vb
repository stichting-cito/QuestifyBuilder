Imports Cito.Tester.ContentModel

Namespace CustomInteractions
    Class GeogebraAdapter
        Inherits BaseAdapter(Of GeogebraScoring)
        Protected Overrides Function MakeParameter(input As GeogebraScoring) As ScoringParameter
            Dim ret As GeogebraScoringParameter = New GeogebraScoringParameter() With {.Label = input.Label, .GeogebraKey = input.CorrectResponse}

            Dim values As ParameterSetCollection = New ParameterSetCollection()
            values.Add(New ParameterCollection() With {.Id = "A"})
            ret.Value = values

            Return ret
        End Function
    End Class
End Namespace
