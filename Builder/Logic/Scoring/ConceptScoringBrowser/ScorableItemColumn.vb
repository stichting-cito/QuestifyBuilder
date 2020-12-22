Namespace Scoring
    Public Class ScorableItemColumn
        Public Property ConceptId As String

        Public Property ColumnIndex As Integer

        Public Property Caption As String

        Public Property IsRelatedToCorrectAnswer As Boolean

        Public Property HasPreProcessingRules As Boolean

        Public Property OriginalValue As String

        Public Sub New(conceptIdInput As String, columnIndexInput As Integer, captionInput As String, isRelatedToCorrectAnswerInput As Boolean, hasPreProcessingRulesInput As Boolean, originalValueInput As String)
            ConceptId = conceptIdInput
            ColumnIndex = columnIndexInput
            Caption = captionInput
            IsRelatedToCorrectAnswer = isRelatedToCorrectAnswerInput
            HasPreProcessingRules = hasPreProcessingRulesInput
            OriginalValue = originalValueInput
        End Sub
    End Class
End Namespace
