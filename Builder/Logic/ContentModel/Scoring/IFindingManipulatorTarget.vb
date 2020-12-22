Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Interface IFindingManipulatorTarget
        ReadOnly Property Target As ManipulatorTarget

        Property FactSetNumber As Integer?

        Function GetFacts() As IEnumerable(Of BaseFact)

        Function GetFact(id As String) As BaseFact

        Function CreateFact(id As String) As BaseFact

        Sub Clear()

        Sub RemoveFact(id As String)

        ReadOnly Property IsInFactSet As Boolean


        Function GetFactSets(id As String) As IEnumerable(Of KeyFactSet)


        Function GetFactSetNumbers(parameterCollectionId As String) As IEnumerable(Of Integer)

        Function CreateFactSet() As Integer

        Sub RemoveFactSet()


        Function CanFactBeRemoved() As Boolean
    End Interface

    <Flags>
    Friend Enum ManipulatorTarget As Integer
        NotSet = 0
        InFinding = 1
        InFactSet = 2
    End Enum
End Namespace