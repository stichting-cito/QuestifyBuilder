Namespace ContentModel.Scoring
    Public Interface IConceptScoreManipulator
        Function GetScoreForPart(partName As String, ids As IEnumerable(Of String)) As IEnumerable(Of Integer?)


        Sub SetScore(partName As String, id As String, score As Integer?)


        Function GetConceptIds() As IEnumerable(Of String)

        Function GetValueForConceptId(id As String) As String


        Function GetDisplayValueForConceptId(id As String) As String


        Function IsConceptIdDeletable(id As String) As Boolean


        Sub RemoveConcept(id As String)


        Function ContainsCatchAllAnswerCategory() As Boolean


        Sub AddCatchAllAnswerCategory()

        Function HasPreProcessingRules(id As String) As Boolean
    End Interface
End Namespace