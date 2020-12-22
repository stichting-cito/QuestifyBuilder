Imports Questify.Builder.Logic.ItemSelector
Imports Questify.Builder.Logic.ItemSelector.Interfaces

Namespace QTI.ItemSelector

    Public Class QTIItemSelector
        Inherits DefaultItemSelector
        Implements IItemSelectorV2

        Protected Overrides Sub OnItemSelectorNeeded(e As ItemSelectorV2NeededEventArgs)
            Dim itemSelector As IItemSelectorV2 = New QTIItemSelector()
            itemSelector.InitializeItemSelector(e.Section, Nothing)
            e.Selector = itemSelector
        End Sub
    End Class
End NameSpace