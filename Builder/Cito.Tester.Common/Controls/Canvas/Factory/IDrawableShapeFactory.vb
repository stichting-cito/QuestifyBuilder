Namespace Controls.Canvas.Factory
    Public Interface IDrawableShapeFactory

        Function DuplicateShape(drawable As IShape) As IShape
        Function CreateShape(Of T As IShape)() As T
        Function CreateShape(Of T As IShape)(id As String) As T
        Function CreateShape(Of T As IShape)(takeDimensions As T) As T

        Function CreateShape(Of T As IShape)(id As String, takeDimensions As T) As T
        Function CreateShape(Of T As IShape)(id As String, label As String) As T
        Function CreateShape(Of T As IShape)(id As String, label As String, takeDimensions As T) As T

    End Interface
End Namespace

