Namespace Controls.Canvas
    Public Interface IShapeConstructor(Of T As IShape)

        ReadOnly Property CanHandleInfinitivePoints As Boolean
        ReadOnly Property Shape As T
        ReadOnly Property Drawing As IDrawableItem

        Sub SuggestPoint(p As Point)
        Function CommitPoint(p As Point) As Boolean
        Function CommitLastPoint(p As Point) As Boolean

    End Interface
End Namespace

