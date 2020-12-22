Namespace ContentModel.Scoring

    Public Interface IValidatingChoiceArrayScoringManipulator(Of TValue) : Inherits IChoiceArrayScoringManipulator

        Function IsValid(ByVal value As TValue) As Boolean

    End Interface

End NameSpace