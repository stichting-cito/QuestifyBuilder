Namespace Controls.Canvas
    Public Interface IDrawableItemPainter(Of T As IDrawableItem)

        Sub Draw(g As Graphics, color as Color, drawable As T)

    End Interface

End Namespace