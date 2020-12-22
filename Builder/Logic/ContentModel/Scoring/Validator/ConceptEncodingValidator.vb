Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring.Validator

    Public Class ConceptEncodingValidator

        Private ReadOnly _startRule As ValidationRuleProcessor

        Public Sub New()
            _startRule = New ConceptEncodingOutOfSync()
        End Sub

        Public Sub Validate(item As AssessmentItem)
            _startRule.ProcessValidationRule(item)
        End Sub

    End Class

End Namespace