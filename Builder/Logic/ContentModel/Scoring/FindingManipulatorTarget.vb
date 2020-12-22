Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel.Scoring
    Friend Class FindingManipulatorTarget
        Implements IFindingManipulatorTarget


        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of BaseFinding)
        Private ReadOnly _findingManipulator As FindingManipulatorBase
        Private ReadOnly _postfix As String = String.Empty



        Sub New(justInTimeFinding As CreateObjectJIT(Of BaseFinding),
        findingManipulator As FindingManipulatorBase, postFix As String)
            _justInTimeFinding = justInTimeFinding
            _findingManipulator = findingManipulator
            _postfix = postFix
        End Sub



        Public Property FactSetNumber As Integer? Implements IFindingManipulatorTarget.FactSetNumber
            Get
                Return Nothing
            End Get
            Set(value As Integer?)
            End Set
        End Property


        Public ReadOnly Property IsInFactSet As Boolean Implements IFindingManipulatorTarget.IsInFactSet
            Get
                Return False
            End Get
        End Property

        Public Function GetFactSetNumbers(parameterCollectionId As String) As IEnumerable(Of Integer) _
            Implements IFindingManipulatorTarget.GetFactSetNumbers
            Return Enumerable.Empty(Of Integer)()
        End Function

        Public Function CreateFactSet() As Integer Implements IFindingManipulatorTarget.CreateFactSet
        End Function

        Public Sub RemoveFactSet() Implements IFindingManipulatorTarget.RemoveFactSet
        End Sub

        Public ReadOnly Property Target As ManipulatorTarget Implements IFindingManipulatorTarget.Target
            Get
                Return ManipulatorTarget.InFinding
            End Get
        End Property

        Public Function GetFactSets(id As String) As IEnumerable(Of KeyFactSet) _
            Implements IFindingManipulatorTarget.GetFactSets
            Return New List(Of KeyFactSet)()
        End Function

        Public Function GetFacts() As IEnumerable(Of BaseFact) Implements IFindingManipulatorTarget.GetFacts
            If _justInTimeFinding.CurrentValue Is Nothing Then
                Return New List(Of BaseFact)()
            Else
                Dim baseFacts = _justInTimeFinding.CurrentValue.Facts.Where(Function(fact) FactCanBeManipulated(fact))
                Return baseFacts
            End If
        End Function

        Public Function GetFact(id As String) As BaseFact Implements IFindingManipulatorTarget.GetFact
            Return GetFacts().FirstOrDefault(Function(fact) fact.Id.StartsWith(id))
        End Function

        Private Function FactCanBeManipulated(fact As BaseFact) As Boolean
            Return fact.Id.EndsWith(_postfix)
        End Function

        Public Function CreateFact(id As String) As BaseFact Implements IFindingManipulatorTarget.CreateFact
            Dim finding = _justInTimeFinding.GetEnsuredValue()
            Dim newFact = _findingManipulator.CreateFact(id)

            finding.Facts.Add(newFact)

            Return newFact
        End Function

        Public Sub Clear() Implements IFindingManipulatorTarget.Clear
        End Sub

        Public Sub RemoveFact(id As String) Implements IFindingManipulatorTarget.RemoveFact
            Dim finding = _justInTimeFinding.GetEnsuredValue()

            Dim factoRemove = GetFact(id)

            If factoRemove IsNot Nothing Then
                finding.Facts.Remove(factoRemove)
            End If
        End Sub

        Public Function CanFactBeRemoved() As Boolean Implements IFindingManipulatorTarget.CanFactBeRemoved
            Return GetFacts().Any()
        End Function
    End Class
End Namespace