Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class GapMatchRichTextScoringNonTransformedManipulator
        Inherits GapMatchScoringNonTransformedManipulator

        Public Sub New(param As GapMatchScoringParameter, decoree As IValidatingChoiceArrayScoringManipulator(Of String))
            MyBase.New(param, decoree)
            If (param.IsTransformed) Then Throw New ArgumentException()
        End Sub

    End Class
End Namespace