Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemSelector.Interfaces

Namespace ItemSelector

    Public Class DefaultItemSelector
        Implements IItemSelectorV2


        Private _context As TestSectionViewBase
        Private _response As Response
        Private _transaction As TransactionData



        Public Sub New()
        End Sub



        Public ReadOnly Property AllowItemPickingInAdvance() As Boolean Implements IItemSelectorV2.AllowItemPickingInAdvance
            Get
                Return True
            End Get
        End Property

        Public Property Transaction() As TransactionData Implements IItemSelectorV2.Transaction
            Get
                Return _transaction
            End Get
            Set(ByVal value As TransactionData)
                _transaction = value
            End Set
        End Property



        Private Function DetermineLastItemInTestPart(ByVal itemRef As ItemReferenceViewBase) As Boolean
            Dim parentComponent As TestPartViewBase = GetParentTestPart(itemRef)
            Return Not parentComponent.IsPickable()
        End Function

        Private Function GetParentTestPart(ByVal itemRef As ItemReferenceViewBase) As TestPartViewBase
            Dim parentComponent As TestComponentBase = itemRef.Parent
            Do Until parentComponent Is Nothing
                If TypeOf parentComponent Is TestPartViewBase Then Exit Do

                parentComponent = DirectCast(parentComponent, TestSectionViewBase).Parent
            Loop
            Return CType(parentComponent, TestPartViewBase)
        End Function

        Protected Overridable Sub OnItemSelectorNeeded(ByVal e As ItemSelectorV2NeededEventArgs)
            RaiseEvent ItemSelectorNeeded(Me, e)
        End Sub



        Public Function GetItemCount() As Integer Implements IItemSelectorV2.GetItemCount
            Dim itemCount As Integer = 0
            GetItemCountForSection(_context, itemCount)
            Return itemCount
        End Function

        Private Sub GetItemCountForSection(section As TestSectionViewBase, ByRef itemCount As Integer)
            Dim subCount As Integer = 0
            For Each c As TestComponentViewBase In section.Components
                If TypeOf c Is ItemReferenceViewBase Then
                    subCount += 1
                ElseIf TypeOf c Is TestSectionViewBase
                    GetItemCountForSection(DirectCast(c, TestSectionViewBase), subCount)
                End If
            Next
            itemCount += subCount
        End Sub

        Public Function GetPickableItemInSection() As ItemReferenceViewBase Implements IItemSelectorV2.GetPickableItemInSection
            Dim item As ItemReferenceViewBase = Nothing
            GetPickableItemInSection(_context, item)
            Return item
        End Function

        Private Sub GetPickableItemInSection(section As TestSectionViewBase, ByRef item As ItemReferenceViewBase)
            Dim firstTimePickSection As Boolean = Not section.ContainsPickedComponents()

            If section.State = ComponentState.Pickable Then
                For Each comp As TestComponentViewBase In section.Components
                    If TypeOf comp Is ItemReferenceViewBase Then
                        item = CType(comp, ItemReferenceViewBase)
                        If item.State = ComponentState.Pickable Then
                            item.State = ComponentState.Picked
                            _transaction.AddAttributeTransaction(item, "State")

                            If firstTimePickSection Then
                                item.FirstItemInSection = True
                                _transaction.AddAttributeTransaction(item, "FirstItemInSection")
                            End If

                            If DetermineLastItemInTestPart(item) Then
                                item.LastItemInTestPart = True
                                _transaction.AddAttributeTransaction(item, "LastItemInTestPart")
                                Dim parentTestPart As TestPartViewBase = GetParentTestPart(item)
                                parentTestPart.State = ComponentState.Picked
                                _transaction.AddAttributeTransaction(parentTestPart, "State")
                            End If

                            Exit For
                        Else
                            item = Nothing
                        End If
                    ElseIf TypeOf comp Is TestSectionViewBase Then
                        Dim subSection As TestSectionViewBase = CType(comp, TestSectionViewBase)
                        GetPickableItemInSection(subSection, item)
                        If item IsNot Nothing Then
                            Exit For
                        End If
                    End If
                Next
            End If

            If item IsNot Nothing Then
                section.PickedComponents += 1
                _transaction.AddAttributeTransaction(section, "PickedComponents")
            End If
        End Sub

        Public Sub InitializeItemSelector(ByVal section As TestSectionViewBase, ByVal excludeItems As ItemExclusionList) Implements IItemSelectorV2.InitializeItemSelector
            ReflectionHelper.CheckIsNotNothing(section, "Testsection")
            _context = section
            ProcessExclusionList(section, excludeItems)
        End Sub

        Private Sub ProcessExclusionList(ByVal section As TestSectionViewBase, ByVal excludeItems As ItemExclusionList)
            If excludeItems IsNot Nothing AndAlso excludeItems.ItemInfo.Count > 0 Then
                For Each itemInfo As ItemInfo In excludeItems.ItemInfo
                    ExcludeItem(section, itemInfo.ItemIdentifier)
                Next
            End If
        End Sub

        Private Function ExcludeItem(ByVal section As TestSectionViewBase, ByVal itemCode As String) As Boolean
            Dim itemsToRemove As New List(Of ItemReferenceViewBase)

            For Each component As TestComponentViewBase In section.Components
                If TypeOf component Is ItemReferenceViewBase Then
                    Dim item As ItemReferenceViewBase = CType(component, ItemReferenceViewBase)

                    If item.SourceName.Equals(itemCode, StringComparison.InvariantCultureIgnoreCase) AndAlso item.State = ComponentState.Pickable Then
                        itemsToRemove.Add(item)
                    End If
                ElseIf TypeOf component Is TestSectionViewBase Then
                    ExcludeItem(CType(component, TestSectionViewBase), itemCode)
                End If
            Next

            For Each item As ItemReferenceViewBase In itemsToRemove
                section.Components.Remove(item)
            Next
        End Function

        Public Function PickableItemInSectionPossible() As Boolean Implements IItemSelectorV2.PickableItemInSectionPossible
            Dim isPickable As Boolean = False
            PickableItemInSectionPossible(_context, isPickable)
            Return isPickable
        End Function

        Private Sub PickableItemInSectionPossible(section As TestSectionViewBase, ByRef isPickable As Boolean)
            Dim item As ItemReferenceViewBase = Nothing
            If section.State = ComponentState.Pickable Then
                For Each comp As TestComponentViewBase In section.Components
                    If TypeOf comp Is ItemReferenceViewBase Then
                        item = CType(comp, ItemReferenceViewBase)
                        If item.State = ComponentState.Pickable Then
                            isPickable = True
                            Exit Sub
                        End If
                    ElseIf TypeOf comp Is TestSectionViewBase Then
                        Dim subSection As TestSectionViewBase = CType(comp, TestSectionViewBase)
                        PickableItemInSectionPossible(subSection, isPickable)
                        If isPickable Then Exit Sub
                    End If
                Next
            End If
        End Sub

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")>
        Public Sub SetResponseData(ByVal response As Response) Implements IItemSelectorV2.SetResponseData
            _response = response
        End Sub



        Public Event ItemSelectorNeeded(ByVal sender As Object, ByVal e As ItemSelectorV2NeededEventArgs) Implements IItemSelectorV2.ItemSelectorNeeded
        Public Event ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs) Implements IItemSelectorV2.ResourceNeeded


    End Class
End NameSpace