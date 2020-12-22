Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Public Interface IScoreManipulator

        Function GetHowToScoreMethod() As EnumScoringMethod

        Sub SetHowToScoreMethod(e As EnumScoringMethod)

        Function GetKeysAlreadyManipulated() As IEnumerable(Of String)

        Function GetManipulatableKeys() As IEnumerable(Of String)

        Function GetFactIdForKey(key As String) As String

        Function GetDomainForKey(key As String) As String

        Function GetDisplayValueForKey(key As String) As String

        Function GetBaseValueForKey(key as String) as BaseValue

        Function GetValueForKey(key As String) As String

        Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer)


        ReadOnly Property FactSetTarget As Integer?

        Sub SetFactSetTarget(factSetNumber As Integer?)

        Function CreateFactSetTarget() As Integer

        Sub CreateFindingTarget(parameterCollectionId As String)

        Sub RemoveFactSetTarget(factSetNumber As Integer)


        Function CanBeRemovedFromFactSet(parameterCollectionId As String) As Boolean
    End Interface
End Namespace