Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Public Interface IFindingManipulator

        ReadOnly Property FactsetTarget As Integer?


        ReadOnly Property IsConceptManipulator As Boolean

        Function FindOrCreateFact(id As String) As BaseFact

        Sub Clear()

        Sub RemoveFact(id As String)

        Sub SetKey(Of T)(id As String, value As T)

        Sub SetKeyWithOptionals(Of T)(id As String, ParamArray value As T())

        Sub SetKeyWithOptionals(Of T)(score As Integer, id As String, ParamArray value As T())

        Function GetKeys(id As String) As IEnumerable(Of IEnumerable(Of BaseValue))

        Function GetFacts(factId As String) As IEnumerable(Of BaseFact)

        Function GetIds() As IEnumerable(Of String)

        Function UnSetKey(factId As String) As Integer

        Sub SetScoringMethod(method As EnumScoringMethod)

        Function GetScoringMethod() As EnumScoringMethod

        Sub ReplaceKeyWithSpecificOptionals(Of T)(id As String, value As T, index As Integer, Optional score As Integer = 1)

        Sub SetDomainOverride(override As Func(Of String, String))

        Function GetPreProcessingMethods(key As String) As IEnumerable(Of String)

        Sub SetPreProcessingMethods(key As String, preProcessing As IEnumerable(Of String))

        Sub CreateFindingTarget(key As String)


        ReadOnly Property CanManipulateSets As Boolean _

        ReadOnly Property HasSets As Boolean

        ReadOnly Property SetNumbers As IEnumerable(Of Integer)

        Property FactIdPostFix As String

        Sub SetFactSetTarget(factSetNumber As Integer?)

        Function CreateNewFactSet() As Integer


        Sub RemoveFactSetTarget(factSetNumber As Integer)


        Function CanFactBeRemovedFromTarget() As Boolean

        Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer)

    End Interface
End Namespace

