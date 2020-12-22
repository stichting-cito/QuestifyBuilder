Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.Common.Logging
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemSelector.Interfaces

Namespace ItemSelector

    Public MustInherit Class ItemSelectorManagerBase



        Private _currentTestPart As TestPartViewBase
        Private _pickedItems As PickedItemCollectionV2
        Private _sectionItemSelectors As Dictionary(Of String, IItemSelectorV2)
        Private _test As AssessmentTestViewBase

        Private _previousTestPartName As String = String.Empty
        Private ReadOnly _listOfSections As New List(Of String)



        Protected Sub New()
            _sectionItemSelectors = New Dictionary(Of String, IItemSelectorV2)
            _pickedItems = New PickedItemCollectionV2
            TestSessionContext.PickedItemsV2 = _pickedItems
        End Sub

        Public Sub New(test As AssessmentTestViewBase)
            Me.New()
            _test = test
        End Sub



        Public Property CurrentTestPart() As TestPartViewBase
            Get
                Return _currentTestPart
            End Get
            Protected Set(value As TestPartViewBase)
                _currentTestPart = value
            End Set
        End Property

        Public ReadOnly Property Test() As AssessmentTestViewBase
            Get
                Return _test
            End Get
        End Property

        Protected ReadOnly Property PickedItems() As PickedItemCollectionV2
            Get
                Return _pickedItems
            End Get
        End Property

        Public ReadOnly Property RegularItemsPickedCount() As Integer
            Get
                Return Me.PickedItems.Values.Where(Function(i) i.ItemFunctionalType = ItemFunctionalType.Regular).Count()
            End Get
        End Property



        Protected Function GetItemSelectorForSection(section As TestSectionViewBase) As IItemSelectorV2
            Dim selector As IItemSelectorV2 = Nothing
            Dim sectionWithItemSelector As TestSectionViewBase = GetItemSelectorSourceSectionRecursive(section)

            If _sectionItemSelectors.ContainsKey(sectionWithItemSelector.Identifier) Then
                selector = _sectionItemSelectors(sectionWithItemSelector.Identifier)
            End If

            Return selector
        End Function


        Private Sub InitialiseItemSelector(section As TestSectionViewBase, itemSelector As IItemSelectorV2)
            AddHandler itemSelector.ResourceNeeded, AddressOf ItemSelector_OnResourceNeeded
            AddHandler itemSelector.ItemSelectorNeeded, AddressOf ItemSelector_SelectorNeeded

            itemSelector.InitializeItemSelector(section, Nothing)

            If Not _sectionItemSelectors.ContainsKey(section.Identifier) Then
                _sectionItemSelectors.Add(section.Identifier, itemSelector)
            End If
        End Sub


        Private Function GetItemSelectorSourceSectionRecursive(section As TestSectionViewBase) As TestSectionViewBase
            If TypeOf section.Parent Is TestSectionViewBase Then
                Return GetItemSelectorSourceSectionRecursive(CType(section.Parent, TestSectionViewBase))
            Else
                Return Nothing
            End If
        End Function

        Protected Shared Function GetTestPartOf(item As ItemReferenceViewBase) As TestPartViewBase
            Dim parent As TestComponentBase = item.Parent

            Do
                If TypeOf parent Is TestPartViewBase Then Return DirectCast(parent, TestPartViewBase)
                parent = DirectCast(parent, TestComponentViewBase).Parent

            Loop Until parent Is Nothing

            Throw New TestViewerException(String.Format("Could not found test part of item with itemreference '{0}'", item.Identifier))
        End Function

        Private Sub ItemSelector_OnResourceNeeded(sender As Object, ResourceEventArgs As ResourceNeededEventArgs)
            OnResourceNeeded(ResourceEventArgs)
        End Sub

        Private Sub ItemSelector_SelectorNeeded(sender As Object, e As ItemSelectorV2NeededEventArgs)
            Dim selector As IItemSelectorV2 = GetItemSelectorForSection(e.Section)
            e.Selector = selector
        End Sub



        Protected Sub OnResourceNeeded(e As ResourceNeededEventArgs)
            If e IsNot Nothing Then
                RaiseEvent ResourceNeeded(Me, e)

                If e.BinaryResource.ResourceObject Is Nothing Then
                    Throw New TestViewerException("Internal error: resource could not be resolved")
                End If
            Else
                Throw New TestViewerException("Internal error: ResourceNeededEventArgs is nothing")
            End If
        End Sub



        Protected Function PickNewItem(lastResponse As Response, index As Integer, transactionToUse As TransactionData, overriddenSelector As IItemSelectorV2) As ItemReferenceViewBase
            Dim pickedItem As ItemReferenceViewBase = Nothing
            For Each part As TestPartViewBase In _test.TestParts

                If part.State = ComponentState.Pickable Then
                    For Each section As TestSectionViewBase In part.Sections
                        For Each subSection As TestComponentViewBase In section.Components.OfType(Of TestSectionViewBase)()
                            pickedItem = PickItemForSection(part, CType(subSection, TestSectionViewBase), overriddenSelector, transactionToUse, lastResponse, index)
                            If pickedItem IsNot Nothing Then
                                CurrentTestPart = part
                                Return pickedItem
                            End If
                        Next
                        pickedItem = PickItemForSection(part, section, overriddenSelector, transactionToUse, lastResponse, index)
                        If pickedItem IsNot Nothing Then
                            CurrentTestPart = part
                            Return pickedItem
                        End If
                    Next
                End If
            Next

            Throw New TestViewerException("A new item was not picked by the item selector, while it was expected that an item would be available!")
        End Function

        Private Function PickItemForSection(part As TestPartViewBase, section As TestSectionViewBase, overriddenSelector As IItemSelectorV2, transactionToUse As TransactionData, lastResponse As Response, index As Integer) As ItemReferenceViewBase
            Dim selector As IItemSelectorV2
            Dim pickedItem As ItemReferenceViewBase

            selector = Nothing

            If section.State = ComponentState.Pickable Then

                If overriddenSelector Is Nothing Then
                    selector = GetItemSelectorForSection(section)
                Else
                    selector = overriddenSelector
                    InitialiseItemSelector(section, selector)
                End If

                selector.Transaction = transactionToUse
                selector.SetResponseData(lastResponse)

                pickedItem = selector.GetPickableItemInSection()
                If pickedItem IsNot Nothing Then
                    _pickedItems.Add(index, pickedItem)
                    Log.TraceInformation(TraceCategory.ItemPicking, My.Resources.Trace_ItemSelectorManager_GetItemReferenceAtIndex_NewItemPicked, pickedItem.SourceName)

                    part.Parent = _test.TestModel

                    RaiseTestPartSectionEvents(part, DirectCast(pickedItem.Parent, TestSectionViewBase))
                    Return pickedItem
                Else
                    section.State = ComponentState.Picked
                    If transactionToUse IsNot Nothing Then
                        transactionToUse.AddAttributeTransaction(section, "State")
                    End If

                    If selector.AllowItemPickingInAdvance = False Then
                        Return Nothing
                    End If
                End If
            ElseIf section.State = ComponentState.TimeCriteriaMet Then
                If overriddenSelector Is Nothing Then
                    selector = GetItemSelectorForSection(section)
                Else
                    selector = overriddenSelector
                    InitialiseItemSelector(section, selector)
                End If
            End If
            Return Nothing
        End Function


        Private Sub RaiseTestPartSectionEvents(testpart As TestPartViewBase, section As TestSectionViewBase)
            If Not _previousTestPartName = testpart.Identifier Then
                RaiseEvent TestPartChangeEvent(Nothing, New TestPartChangeEventArgs(testpart))
                _previousTestPartName = testpart.Identifier
            End If
            If section.Parent IsNot Nothing AndAlso TypeOf section.Parent Is TestSectionViewBase Then
                RaiseTestPartSectionEvents(testpart, DirectCast(section.Parent, TestSectionViewBase))
            End If
            If Not _listOfSections.Contains(section.Identifier) Then
                RaiseEvent SectionChangeEvent(Nothing, New SectionChangeEventArgs(section))
                _listOfSections.Add(section.Identifier)
            End If
        End Sub




        Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
        Public Event TestPartChangeEvent As EventHandler(Of TestPartChangeEventArgs)
        Public Event SectionChangeEvent As EventHandler(Of SectionChangeEventArgs)

    End Class

End Namespace
