Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Net

Namespace Scoring

    Public Class ConceptScoringBrowser

        Private ReadOnly _conceptScoringBrowserDataProvider As IConceptScoringBrowserDataProvider
        Private ReadOnly _conceptScoringBrowserObjcectFactory As IConceptScoringBrowserObjectFactory
        Private ReadOnly _scoringParameters As IEnumerable(Of ScoringParameter)
        Private ReadOnly _solution As Solution
        Private ReadOnly _mainHierarchyPart As IConceptScoringBrowserHierarchyPart
        Private ReadOnly _structure As Collection(Of IConceptScoringBrowserHierarchyPart)
        Private ReadOnly _scorableKeyCombinations As Dictionary(Of CombinedScoringMapKey, String)
        Private _currentScoringMap As CombinedScoringMapKey
        Private _currentConceptManipulator As IConceptScoreManipulator

        Public Sub New(
                      conceptScoringBrowserDataProvider As IConceptScoringBrowserDataProvider,
                      conceptStructureHierarchyPartFactory As IConceptScoringBrowserObjectFactory,
                      itmResource As ItemResourceEntity,
                      scoringParameters As IEnumerable(Of ScoringParameter),
                      solution As Solution)

            _conceptScoringBrowserDataProvider = conceptScoringBrowserDataProvider

            _conceptScoringBrowserObjcectFactory = conceptStructureHierarchyPartFactory
            _scoringParameters = scoringParameters
            _solution = solution

            Dim combinedScoringMapKeys As List(Of CombinedScoringMapKey) = New ScoringMap(scoringParameters, solution).GetMap().ToList()

            If Not combinedScoringMapKeys.Any() Then
                Return
            End If

            CurrentScoringMapKey = combinedScoringMapKeys.FirstOrDefault()

            _scorableKeyCombinations = New Dictionary(Of CombinedScoringMapKey, [String])()

            For Each combinedScoringMapKey As CombinedScoringMapKey In combinedScoringMapKeys
                _scorableKeyCombinations.Add(combinedScoringMapKey, WebUtility.HtmlDecode(combinedScoringMapKey.Name))
            Next

            Dim selectedConceptStructurePart As ConceptStructurePartCustomBankPropertyEntity = GetSelectedConceptStructurePart(itmResource)
            If selectedConceptStructurePart Is Nothing Then
                Return
            End If

            _mainHierarchyPart = _conceptScoringBrowserObjcectFactory.CreateHierarchyPart(selectedConceptStructurePart, Nothing)
            _mainHierarchyPart.IsSelected = True

            _structure = New Collection(Of IConceptScoringBrowserHierarchyPart)()

            Dim idMainConceptStructurePart As Guid = selectedConceptStructurePart.ConceptStructurePartCustomBankPropertyId
            Dim rootParts As HashSet(Of Guid) = New HashSet(Of Guid)(From a In _mainHierarchyPart.Part.ChildConceptStructurePartCustomBankPropertyCollection Where a.ChildConceptStructurePartCustomBankPropertyId <> idMainConceptStructurePart Select a.ChildConceptStructurePartCustomBankPropertyId)

            Dim fetched = New Dictionary(Of Guid, ConceptStructurePartCustomBankPropertyEntity)()
            Dim counter As Integer = 0
            PopulateChildren(idMainConceptStructurePart, _mainHierarchyPart, rootParts, _structure, fetched, counter)

            LoadConceptScoresForCurrentScoreMapKey()
        End Sub

        Public ReadOnly Property MainHierarchyPart() As IConceptScoringBrowserHierarchyPart
            Get
                Return _mainHierarchyPart
            End Get
        End Property

        Public ReadOnly Property [Structure]() As Collection(Of IConceptScoringBrowserHierarchyPart)
            Get
                Return _structure
            End Get
        End Property

        Public ReadOnly Property ScorableKeyCombinations() As Dictionary(Of CombinedScoringMapKey, String)
            Get
                Return _scorableKeyCombinations
            End Get
        End Property

        Public Property CurrentScoringMapKey() As CombinedScoringMapKey
            Get
                Return _currentScoringMap
            End Get
            Set
                If _currentScoringMap IsNot Value Then
                    _currentScoringMap = Value
                    If _currentScoringMap IsNot Nothing Then
                        _currentConceptManipulator = _currentScoringMap.GetConceptManipulator(_solution)
                    End If

                    LoadConceptScoresForCurrentScoreMapKey()
                End If
            End Set
        End Property

        Public ReadOnly Property CurrentConceptManipulator() As IConceptScoreManipulator
            Get
                Return _currentConceptManipulator
            End Get
        End Property

        Public ReadOnly Property ScorableItemColumns() As List(Of ScorableItemColumn)
            Get
                Dim scorableItemColumns1 As List(Of ScorableItemColumn) = New List(Of ScorableItemColumn)()

                If CurrentScoringMapKey IsNot Nothing Then
                    Dim t As Integer = 0
                    For Each conceptId As String In CurrentConceptIds
                        Dim displayValue As String = _currentConceptManipulator.GetDisplayValueForConceptId(conceptId)
                        Dim originalValue As String = _currentConceptManipulator.GetValueForConceptId(conceptId)
                        scorableItemColumns1.Add(New ScorableItemColumn(conceptId, t, displayValue, Not _currentConceptManipulator.IsConceptIdDeletable(conceptId), _currentConceptManipulator.HasPreProcessingRules(conceptId), originalValue))
                        t += 1
                    Next
                End If

                Return scorableItemColumns1
            End Get
        End Property

        Private ReadOnly Property CurrentConceptIds() As List(Of String)
            Get
                If _currentConceptManipulator IsNot Nothing Then
                    Return _currentConceptManipulator.GetConceptIds().ToList()
                End If

                Return Nothing
            End Get
        End Property



        Public Sub SyncWithSolution()
            Dim combinedScoringMapKeys As List(Of CombinedScoringMapKey) = New ScoringMap(_scoringParameters, _solution).GetMap().ToList()

            If CurrentScoringMapKey IsNot Nothing Then
                CurrentScoringMapKey = combinedScoringMapKeys.First(Function(csmk) csmk.Name = CurrentScoringMapKey.Name)
            End If
        End Sub


        Private Function GetSelectedConceptStructurePart(itmResource As ItemResourceEntity) As ConceptStructurePartCustomBankPropertyEntity
            Dim selectedConceptValue As ConceptStructureCustomBankPropertyValueEntity = itmResource.CustomBankPropertyValueCollection.OfType(Of ConceptStructureCustomBankPropertyValueEntity)().FirstOrDefault()

            If selectedConceptValue Is Nothing Then
                Return Nothing
            End If

            Dim selectedConceptCbp As ConceptStructureCustomBankPropertyEntity = _conceptScoringBrowserDataProvider.ReadConceptStructureCustomBankProperty(selectedConceptValue.CustomBankPropertyId)
            If selectedConceptCbp Is Nothing Then
                Return Nothing
            End If

            Dim availableParts As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity) = GetConceptStructureParts(selectedConceptCbp)
            Dim selectedPartEntity As ConceptStructureCustomBankPropertySelectedPartEntity = selectedConceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.FirstOrDefault()
            If selectedPartEntity Is Nothing Then
                Return Nothing
            End If

            Dim ret As ConceptStructurePartCustomBankPropertyEntity = availableParts.FirstOrDefault(Function(cs) cs.ConceptStructurePartCustomBankPropertyId = selectedPartEntity.ConceptStructurePartId)
            Return ret
        End Function

        Private Sub PopulateChildren(
                                    idMainConceptStructurePart As Guid,
                                    parent As IConceptScoringBrowserHierarchyPart,
                                    rootParts As ICollection(Of Guid),
                                    ByRef all As Collection(Of IConceptScoringBrowserHierarchyPart),
                                    ByRef fetched As Dictionary(Of Guid, ConceptStructurePartCustomBankPropertyEntity),
                                    ByRef counter As Integer)
            counter += 1

            Dim partsToCheck As IEnumerable(Of ChildConceptStructurePartCustomBankPropertyEntity) =
From a in parent.Part.ChildConceptStructurePartCustomBankPropertyCollection
Where (parent.IsParentPart(a.ChildConceptStructurePartCustomBankPropertyId) = False)
Where (a.ChildConceptStructurePartCustomBankPropertyId <> idMainConceptStructurePart)
Where (parent.Depth < 1 OrElse Not rootParts.Contains(a.ChildConceptStructurePartCustomBankPropertyId))
Select (a)

            partsToCheck = partsToCheck.OrderBy(Function(e) e.VisualOrder).ThenBy(Function(i) i.ChildConceptStructurePartCustomBankProperty.Name).ToList()

            For Each item As ChildConceptStructurePartCustomBankPropertyEntity In partsToCheck
                Dim conceptPart As ConceptStructurePartCustomBankPropertyEntity = item.ChildConceptStructurePartCustomBankProperty

                If _conceptScoringBrowserDataProvider Is Nothing Then
                    Return
                End If
                If Not fetched.ContainsKey(conceptPart.ConceptStructurePartCustomBankPropertyId) Then
                    Dim part As ConceptStructurePartCustomBankPropertyEntity = _conceptScoringBrowserDataProvider.PopulateConceptCustomBankPropertyHierarchy(conceptPart.ConceptStructurePartCustomBankPropertyId)

                    fetched.Add(conceptPart.ConceptStructurePartCustomBankPropertyId, part)
                    conceptPart = part
                End If

                Dim newPart as IConceptScoringBrowserHierarchyPart = _conceptScoringBrowserObjcectFactory.CreateHierarchyPart(conceptPart, parent)
                Dim linkup As IConceptScoringBrowserHierarchyPart = all.FirstOrDefault(Function(e) e.Id = newPart.Id)
                AttachScoreToConceptPart(linkup, newPart)
                all.Add(newPart)

                PopulateChildren(idMainConceptStructurePart, newPart, rootParts, all, fetched, counter)
            Next
        End Sub

        Private Function GetConceptStructureParts(conceptStructureCustomBankProperty As ConceptStructureCustomBankPropertyEntity) As IEnumerable(Of ConceptStructurePartCustomBankPropertyEntity)
            Dim conceptParts As List(Of ConceptStructurePartCustomBankPropertyEntity) = conceptStructureCustomBankProperty.ConceptStructurePartCustomBankPropertyCollection.ToList()
            Return conceptParts.OrderBy(Function(cp) cp.Name).ToList()
        End Function


        Private Sub LoadConceptScoresForCurrentScoreMapKey()
            If [Structure] Is Nothing Then
                Return
            End If

            Dim sameParts = New Dictionary(Of Guid, IConceptScoringBrowserHierarchyPart)()

            For i As Integer = [Structure].Count - 1 To 0 Step -1
                Dim part As IConceptScoringBrowserHierarchyPart = [Structure](i)
                part.SetIsSelectedToFalseNoNotification()

                Dim linkup As IConceptScoringBrowserHierarchyPart

                Dim b As Boolean = sameParts.TryGetValue(part.Id, linkup)
                If Not b Then
                    sameParts.Add(part.Id, part)
                End If


                AttachScoreToConceptPart(linkup, part)
            Next
        End Sub


        Private Sub AttachScoreToConceptPart(linkup As IConceptScoringBrowserHierarchyPart, newPart As IConceptScoringBrowserHierarchyPart)
            If newPart Is Nothing Then
                Throw New ArgumentNullException(nameof(newPart))
            End If

            If linkup IsNot Nothing Then
                newPart.ConceptScorePart = linkup.ConceptScorePart
                For Each singleConceptScoreContainer As IConceptScoringBrowserScoreContainer In linkup.ConceptScorePart
                    singleConceptScoreContainer.AddParent(newPart)
                Next
                If linkup.IsSelected Then
                    newPart.IsSelected = linkup.IsSelected
                End If
            Else
                If _currentConceptManipulator IsNot Nothing Then
                    Dim scores As Nullable(Of Integer)() = _currentConceptManipulator.GetScoreForPart(newPart.PartName, CurrentConceptIds).ToArray()
                    Dim x as ObservableCollection(Of IConceptScoringBrowserScoreContainer) = New ObservableCollection(Of IConceptScoringBrowserScoreContainer)()
                    For i As Integer = 0 To CurrentConceptIds.Count - 1
                        x.Add(_conceptScoringBrowserObjcectFactory.CreatePartScoreContainer(newPart, CurrentConceptIds(i), scores(i), _currentConceptManipulator))
                    Next
                    newPart.ConceptScorePart = x
                    Dim [select] As Boolean = scores.Any(Function(e) e IsNot Nothing)
                    If [select] Then
                        newPart.IsSelected = True
                    End If
                End If
            End If

            If _currentConceptManipulator IsNot Nothing Then
                Debug.Assert(newPart.ConceptScorePart IsNot Nothing)
            End If
        End Sub


    End Class
End Namespace
