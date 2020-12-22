Imports System.Diagnostics.CodeAnalysis
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Namespace ItemSelector.Interfaces

    Public Interface IItemSelectorV2

        Event ItemSelectorNeeded As EventHandler(Of ItemSelectorV2NeededEventArgs)

        Property Transaction() As TransactionData

        ReadOnly Property AllowItemPickingInAdvance() As Boolean


        Sub InitializeItemSelector(section As TestSectionViewBase, excludeItems As ItemExclusionList)

        <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
        Sub SetResponseData(responses As Response)


        Function GetPickableItemInSection() As ItemReferenceViewBase


        Function GetItemCount() As Integer

        Function PickableItemInSectionPossible() As Boolean

        Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    End Interface

End Namespace