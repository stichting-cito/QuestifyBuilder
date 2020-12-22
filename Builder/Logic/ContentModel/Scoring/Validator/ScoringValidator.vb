Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring.Validator

    Public Class ScoringValidator

        Private ReadOnly _startRule As ValidationRuleProcessor

        Public Sub New()
            _startRule = New NoOrBetweenSingleInteractions() With {
.Successor = New NoAndOnSameInteraction() With {
.RuleIsDisabled = True, .Successor = New EachInteractionHasScoringDefinition() With {
.Successor = New GroupConsistsOfMultipleDifferentInteractions() With {
    .Successor = New OtherGroupWithSameInteractionsExists() With {
         .RuleIsDisabled = True, .Successor = New InteractionOfGroupCannotExistOutsideAGroup() With {
            .Successor = New GroupsWithSameInteractionsAreEqual()}
    }
}
}
}
}

        End Sub

        Public Sub Validate(item As AssessmentItem)
            _startRule.ProcessValidationRule(item)
        End Sub

    End Class
End Namespace
