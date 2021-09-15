
Imports Cito.Tester.ContentModel

<TestClass>
Public Class Integration_2InlineChoice_WithCustomDisplayValue_Grouped
    Inherits Integration_2InlineChoice_Grouped

    Protected Overrides Function InlineChoiceScoringParameters() As ChoiceScoringParameter()
        Dim choiceScoringParameters = MyBase.InlineChoiceScoringParameters()

        For Each choiceScoringParameter As ChoiceScoringParameter In choiceScoringParameters

            'For inlineChoice there is a rule that it should return the first inline choice.
            Dim subParams = choiceScoringParameter.Value
            For Each parameterCollection As ParameterCollection In subParams

                parameterCollection.InnerParameters.Add(New PlainTextParameter() With {.Value = parameterCollection.Id})

            Next

            Return choiceScoringParameters
        Next

        Return choiceScoringParameters
    End Function
End Class
