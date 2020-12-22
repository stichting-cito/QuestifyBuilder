Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel.Scoring
    Friend Class FactSetManipulatorTarget(Of TFinding As KeyFinding, TFactSet As {KeyFactSet, New})
        Implements IFindingManipulatorTarget


        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of TFinding)
        Private ReadOnly _findingManipulator As FindingManipulatorBase
        Private ReadOnly _postFix As String
        Private _currentFactSet As KeyFactSet
        Private _factSetNumber As Integer?



        Sub New(justInTimeFinding As CreateObjectJIT(Of TFinding),
        findingManipulator As FindingManipulatorBase, postFix As String)
            _justInTimeFinding = justInTimeFinding
            _findingManipulator = findingManipulator
            _postFix = postFix
            TryEnsureTargetFactSet(_postFix)
        End Sub



        Public Property FactSetNumber As Integer? Implements IFindingManipulatorTarget.FactSetNumber
            Get
                Return _factSetNumber
            End Get
            Set(value As Integer?)
                _factSetNumber = value
                _currentFactSet = Nothing
            End Set
        End Property


        Public ReadOnly Property IsInFactSet As Boolean Implements IFindingManipulatorTarget.IsInFactSet
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property Target As ManipulatorTarget Implements IFindingManipulatorTarget.Target
            Get
                Return ManipulatorTarget.InFactSet
            End Get
        End Property


        Function ContainsFactWithPostfix(postFix As String) As Boolean

            If (_justInTimeFinding.CurrentValue Is Nothing) Then Return False

            Dim ret = _justInTimeFinding.CurrentValue.KeyFactsets.Any(Function(factSet)
                                                                          Dim anyFactIdMatch = factSet.Facts.Any(Function(fact) FactIdEndsWithPostFix(fact, postFix))
                                                                          Return anyFactIdMatch
                                                                      End Function)
            Return ret
        End Function

        Public Function GetFactSetNumbers(parameterCollectionId As String) As IEnumerable(Of Integer) _
            Implements IFindingManipulatorTarget.GetFactSetNumbers
            Dim numbers As New List(Of Integer)()
            Dim finding As TFinding = _justInTimeFinding.CurrentValue

            If (finding IsNot Nothing) Then


                Dim factSetsWithPosition = finding.KeyFactsets.Select(Function(item, index) New KeyValuePair(Of KeyFactSet, Integer)(item, index))
                Dim factSetWithManipulatableFacts = factSetsWithPosition.Where(Function(kvp) kvp.Key.Facts.Any(Function(fact) FactCanBeManipulated(fact, _postFix) AndAlso fact.Id.StartsWith(parameterCollectionId)))


                For Each factSetWithPosition In factSetWithManipulatableFacts
                    numbers.Add(factSetWithPosition.Value)
                Next

            End If
            Return numbers
        End Function

        Public Function GetFacts() As IEnumerable(Of BaseFact) Implements IFindingManipulatorTarget.GetFacts
            TryEnsureTargetFactSet(_postFix)

            If (_currentFactSet Is Nothing) Then Return New List(Of BaseFact)()

            Return _currentFactSet.Facts.Where(Function(fact) FactCanBeManipulated(fact, _postFix))
        End Function

        Public Function GetFact(id As String) As BaseFact Implements IFindingManipulatorTarget.GetFact
            Return GetFacts().FirstOrDefault(Function(fact) fact.Id.StartsWith(id))
        End Function

        Public Sub RemoveFact(id As String) Implements IFindingManipulatorTarget.RemoveFact
            _justInTimeFinding.GetEnsuredValue()

            Dim factoRemove = GetFact(id)

            If factoRemove IsNot Nothing Then
                _currentFactSet.Facts.Remove(factoRemove)
            End If
        End Sub

        Public Function CreateFactSet() As Integer Implements IFindingManipulatorTarget.CreateFactSet

            Dim finding = _justInTimeFinding.GetEnsuredValue()

            _currentFactSet = New TFactSet()
            finding.KeyFactsets.Add(_currentFactSet)

            Return finding.KeyFactsets.Count - 1
        End Function

        Public Sub RemoveFactSet() Implements IFindingManipulatorTarget.RemoveFactSet

            Dim finding = _justInTimeFinding.GetEnsuredValue()
            finding.KeyFactsets.Remove(finding.KeyFactsets(_factSetNumber.Value))
        End Sub

        Public Function CreateFact(id As String) As BaseFact Implements IFindingManipulatorTarget.CreateFact
            EnsureTargetFactSet()
            Dim newFact = _findingManipulator.CreateFact(id)
            _currentFactSet.Facts.Add(newFact)
            Return newFact
        End Function

        Public Function GetFactSets(id As String) As IEnumerable(Of KeyFactSet) _
            Implements IFindingManipulatorTarget.GetFactSets
            If (_justInTimeFinding.CurrentValue IsNot Nothing) Then
                Return _
                    _justInTimeFinding.CurrentValue.KeyFactsets.Where(
                        Function(factset) factset.Facts.Any(Function(fact) FactCanBeManipulated(fact, id)))
            End If
            Return New List(Of KeyFactSet)()
        End Function


        Public Sub Clear() Implements IFindingManipulatorTarget.Clear

            For Each e In GetFacts().ToArray()
                RemoveFact(e.Id)
            Next
        End Sub

        Public Function CanFactBeRemoved() As Boolean Implements IFindingManipulatorTarget.CanFactBeRemoved
            Return GetFacts().Count > 0
        End Function

        Private Sub TryEnsureTargetFactSet(postfix As String)
            If (_currentFactSet Is Nothing) Then
                Dim finding = _justInTimeFinding.CurrentValue
                If (_justInTimeFinding.CurrentValue IsNot Nothing AndAlso _justInTimeFinding.CurrentValue.KeyFactsets.Any) Then

                    If _factSetNumber > finding.KeyFactsets.Count - 1 Then _
                        Throw New IndexOutOfRangeException($"FactSetNumber {_factSetNumber} not found")

                    If (_factSetNumber.HasValue) Then
                        _currentFactSet = finding.KeyFactsets(_factSetNumber.Value)
                    Else
                        If _
                            _justInTimeFinding.CurrentValue.KeyFactsets.Any(
                                Function(factSet) factSet.Facts.Any(Function(fact) FactIdEndsWithPostFix(fact, postfix))) _
                            Then
                            _currentFactSet =
                                _justInTimeFinding.CurrentValue.KeyFactsets.First(
                                    Function(factSet) factSet.Facts.Any(Function(fact) FactIdEndsWithPostFix(fact, postfix))
                                        )
                            If (_currentFactSet IsNot Nothing) Then
                                _factSetNumber = _justInTimeFinding.CurrentValue.KeyFactsets.IndexOf(_currentFactSet)
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub EnsureTargetFactSet()


            If (_currentFactSet Is Nothing) Then
                Dim factSets = _justInTimeFinding.GetEnsuredValue()
                _currentFactSet = factSets.KeyFactsets.FirstOrDefault()

                If (_currentFactSet Is Nothing) Then
                    _currentFactSet = New KeyFactSet()
                    factSets.KeyFactsets.Add(_currentFactSet)
                End If

            End If

            Debug.Assert(_currentFactSet IsNot Nothing, "Can not occur")
        End Sub


        Private Function FactCanBeManipulated(fact As BaseFact, id As String) As Boolean
            If (String.IsNullOrEmpty(fact.Id)) Then Return False
            Return fact.Id.Contains(id)
        End Function

        Private Function FactIdEndsWithPostFix(fact As BaseFact, postFix As String) As Boolean
            If (String.IsNullOrEmpty(fact.Id)) Then Return False
            Return fact.Id.EndsWith(postFix)
        End Function
    End Class
End Namespace
