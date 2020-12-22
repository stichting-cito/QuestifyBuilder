Namespace Controls.Canvas.Factory
    Public Interface IShapeConstructorFactory

        Function Create(Of T As IShape)(shapeFactory As IDrawableShapeFactory) As IShapeConstructor(Of T)

    End Interface

End Namespace
