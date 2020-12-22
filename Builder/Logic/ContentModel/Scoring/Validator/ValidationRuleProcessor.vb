Imports Cito.Tester.ContentModel


Namespace ContentModel.Scoring.Validator

    Friend MustInherit Class ValidationRuleProcessor

        Protected _successor As ValidationRuleProcessor
        Public Property Successor() As ValidationRuleProcessor
            Get
                Return _successor
            End Get
            Set(ByVal value As ValidationRuleProcessor)
                _successor = value
            End Set
        End Property

        Public ReadOnly Property HasSuccessor() As Boolean
            Get
                Return _successor IsNot Nothing
            End Get
        End Property

        Public Sub ProcessValidationRule(item As AssessmentItem)
            If (Not RuleIsDisabled) Then Validate(item)
            Handover(item)
        End Sub

        Private Sub Handover(item As AssessmentItem)
            If Me.HasSuccessor() Then
                Me.Successor.ProcessValidationRule(item)
            End If
        End Sub

        Protected MustOverride Sub Validate(item As AssessmentItem)

        Public Property RuleIsDisabled As Boolean

    End Class

End Namespace