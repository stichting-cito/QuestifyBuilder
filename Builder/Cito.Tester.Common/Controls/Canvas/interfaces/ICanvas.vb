
Namespace Controls.Canvas
    Public Interface ICanvas

        Property Color As Color
        ReadOnly Property EditItem As IDrawableItem

        Property Adorner As IAdorner

        Property Tool As ITool(Of ICanvas)

        ReadOnly Property Items As IEnumerable(Of IDrawableItem)


        ReadOnly Property BackgroundImageBounds As Rectangle

        Event CollectionChanged As EventHandler(Of NotifyCollectionChangedEventArgs)
        Event SelectionChanged As EventHandler(Of EventArgs)


        Sub SetAsTemporaryEditedDrawing(drawableItem As IDrawableItem)

        Sub AddItem(itm As IDrawableItem)

        Sub RemoveItem(itm As IDrawableItem)

        Sub RemoveSelected()

        Sub [Select](itm As IDrawableItem)

        Sub DeSelect()

        Function HitTest(point As Point) As IList(Of IDrawableItem)

        Function IsConfirmedToRemoveAllShapes() As Boolean

        Function IsConfirmedToRemoveShape() As Boolean

        Sub Invalidate(ParamArray itm As IDrawableItem())

        Sub Clear()


    End Interface

End Namespace
