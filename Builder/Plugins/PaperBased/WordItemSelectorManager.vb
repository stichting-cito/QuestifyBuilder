Imports Cito.Tester.Common
Imports Cito.Tester.Common.Logging
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemSelector
Imports Questify.Builder.Logic.ItemSelector.Interfaces

Public Class WordItemSelectorManager


    Private _currentTestPart As TestPartViewBase
    Private _sectionItemSelectors As Dictionary(Of String, IItemSelectorV2)
    Private _pickedItems As PickedItemCollectionV2
    Private _test As AssessmentTestViewBase
    Private _isLastItemInTest As Boolean

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


    Public ReadOnly Property IsLastItemInTest() As Boolean
        Get
            Return _isLastItemInTest
        End Get
    End Property

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



    Private Function DetermineLastItemInTest() As Boolean
        Dim returnValue As Boolean = True

        For Each t As TestPartViewBase In Me.Test.TestParts
            If t.IsPickable() Then
                returnValue = False
                Exit For
            End If
        Next

        Return returnValue
    End Function

    Private Sub InitialiseItemSelector(section As TestSectionViewBase, itemSelector As IItemSelectorV2)
        AddHandler itemSelector.ResourceNeeded, AddressOf ItemSelector_OnResourceNeeded

        itemSelector.InitializeItemSelector(section, Nothing)

        If Not _sectionItemSelectors.ContainsKey(section.Identifier) Then
            _sectionItemSelectors.Add(section.Identifier, itemSelector)
        End If
    End Sub

    Protected Shared Function GetTestPartOf(item As ItemReferenceViewBase) As TestPartViewBase
        Dim parent As TestComponentBase = item.Parent

        Do
            If TypeOf parent Is TestPartViewBase Then Return DirectCast(parent, TestPartViewBase)
            parent = DirectCast(parent, TestComponentViewBase).Parent

        Loop Until parent Is Nothing

        Throw New TestViewerException(String.Format("Could not found test part of item with itemreference '{0}'", item.Identifier))
    End Function

    Private Function PickNewItem(lastResponse As Response, index As Integer, transactionToUse As TransactionData) As ItemReferenceViewBase
        Dim pickedItem As ItemReferenceViewBase = Nothing
        For Each part As TestPartViewBase In _test.TestParts

            If part.State = ComponentState.Pickable Then
                For Each section As TestSectionViewBase In part.Sections
                    For Each subSection As TestComponentViewBase In section.Components.OfType(Of TestSectionViewBase)()
                        pickedItem = PickItemForSection(part, CType(subSection, TestSectionViewBase), transactionToUse, lastResponse, index)
                        If pickedItem IsNot Nothing Then
                            CurrentTestPart = part
                            Return pickedItem
                        End If
                    Next
                    pickedItem = PickItemForSection(part, section, transactionToUse, lastResponse, index)
                    If pickedItem IsNot Nothing Then
                        CurrentTestPart = part
                        Return pickedItem
                    End If
                Next
            End If
        Next

        Throw New TestViewerException("A new item was not picked by the item selector, while it was expected that an item would be available!")
    End Function


    Private Function PickItemForSection(part As TestPartViewBase, section As TestSectionViewBase, transactionToUse As TransactionData, lastResponse As Response, index As Integer) As ItemReferenceViewBase
        Dim selector As IItemSelectorV2 = New DefaultItemSelector()
        Dim pickedItem As ItemReferenceViewBase

        If section.State = ComponentState.Pickable Then
            InitialiseItemSelector(section, selector)

            selector.Transaction = transactionToUse
            selector.SetResponseData(lastResponse)

            pickedItem = selector.GetPickableItemInSection()
            If pickedItem IsNot Nothing Then
                _pickedItems.Add(index, pickedItem)
                Log.TraceInformation(TraceCategory.ItemPicking, "New item with resourcename '{0}' picked.", pickedItem.SourceName)

                part.Parent = _test.TestModel

                RaiseTestPartSectionEvents(part, DirectCast(pickedItem.Parent, TestSectionViewBase))
                Return pickedItem
            Else
                section.State = ComponentState.Picked
                If transactionToUse IsNot Nothing Then
                    transactionToUse.AddAttributeTransaction(section, "State")
                End If
            End If
        ElseIf section.State = ComponentState.TimeCriteriaMet Then
            InitialiseItemSelector(section, selector)
        End If
        Return Nothing
    End Function

    Private Function GetItemCountOfSection(section As TestSectionViewBase) As Integer
        Dim itemCount As Integer = 0
        GetItemCountForSection(section, itemCount)
        Return itemCount
    End Function

    Private Sub GetItemCountForSection(section As TestSectionViewBase, ByRef itemCount As Integer)
        Dim subCount As Integer = 0
        For Each c As TestComponentViewBase In section.Components
            If TypeOf c Is ItemReferenceViewBase Then
                subCount += 1
            ElseIf TypeOf c Is TestSectionViewBase Then
                GetItemCountForSection(DirectCast(c, TestSectionViewBase), subCount)
            End If
        Next
        itemCount += subCount
    End Sub

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

    Private Sub OnResourceNeeded(e As ResourceNeededEventArgs)
        If e IsNot Nothing Then
            RaiseEvent ResourceNeeded(Me, e)

            If e.BinaryResource.ResourceObject Is Nothing Then
                Throw New TestViewerException("Internal error: resource could not be resolved")
            End If
        Else
            Throw New TestViewerException("Internal error: ResourceNeededEventArgs is nothing")
        End If
    End Sub

    Private Sub ItemSelector_OnResourceNeeded(sender As Object, ResourceEventArgs As ResourceNeededEventArgs)
        OnResourceNeeded(ResourceEventArgs)
    End Sub



    Public Function PickNewItem(index As Integer) As WordItemReference
        Dim newItem As WordItemReference = DirectCast(Me.PickNewItem(Nothing, index, New TransactionData), WordItemReference)

        Me.CurrentTestPart = GetTestPartOf(newItem)

        _isLastItemInTest = DetermineLastItemInTest()

        Return newItem
    End Function

    Public Function GetItemCountOfTestPart(testPart As TestPartViewBase) As Integer
        Dim cnt As Integer = 0

        For Each section As TestSectionViewBase In testPart.Sections
            cnt += GetItemCountOfSection(section)
        Next

        Return cnt
    End Function



    Public Event ItemSelectorNeeded(ByVal sender As Object, ByVal e As ItemSelectorV2NeededEventArgs)
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Public Event TestPartChangeEvent As EventHandler(Of TestPartChangeEventArgs)
    Public Event SectionChangeEvent As EventHandler(Of SectionChangeEventArgs)


End Class