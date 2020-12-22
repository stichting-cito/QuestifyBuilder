Namespace Controls.Canvas
    Public Interface IShape
        Inherits IDrawableItem

        Property Label As String

        Function SetDimensions(shape As IShape) As IShape
    End Interface

    Public Interface ICircle
        Inherits IShape

        Property Radius As Integer

    End Interface


    Public Interface IRectangle
        Inherits IShape

        Property Left As Integer

        Property Top As Integer

        Property Right As Integer

        Property Bottom As Integer

        ReadOnly Property Width As Integer

        ReadOnly Property Height As Integer

    End Interface


    Public Interface IEllipse
        Inherits IShape

        Property HRadius As Integer

        Property VRadius As Integer

    End Interface


    Public Interface IPolygon
        Inherits IShape

        Property Coordinates As List(Of Point)

    End Interface

    Public Interface ITriangle
        Inherits IShape, IPolygon


        Property Left As Integer

        Property Top As Integer

        Property Right As Integer

        Property Bottom As Integer


        ReadOnly Property Width As Integer

        ReadOnly Property Height As Integer

    End Interface

    Public Interface IPointUpTriangle
        Inherits ITriangle
    End Interface

    Public Interface IPointDownTriangle
        Inherits ITriangle
    End Interface


End Namespace
