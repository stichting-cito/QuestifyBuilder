Imports Cito.Tester.Common.Controls.Canvas.Tools
Imports Cito.Tester.Common.Controls.Canvas.Tools.ToolState
Imports Cito.Tester.Common.Controls.Canvas.Factory.ShapeCreator
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory

Namespace Controls.Canvas.Factory

    Public Module ToolFactory

        Private shapeCnstrFac As IShapeConstructorFactory = New ByMidPointFactory()
        Private drawShapeFac As IDrawableShapeFactory = New HollowShapeFactory

        Public Function CreateNewShapeTool(Of TShape As IShape)() As ITool(Of ICanvas)
            Dim shapeConstructor As IShapeConstructor(Of TShape) = shapeCnstrFac.Create(Of TShape)(drawShapeFac)
            Dim tlState As New FirstPointState(Of TShape)(shapeConstructor)
            Return New CreateTool(Of TShape)(tlState)
        End Function


        Public Function CreateNewShapeTool(Of TShape As IShape)(shapeFactory As IDrawableShapeFactory) As ITool(Of ICanvas)
            Dim shapeConstructor As IShapeConstructor(Of TShape) = shapeCnstrFac.Create(Of TShape)(drawShapeFac)
            Dim tlState As New FirstPointState(Of TShape)(shapeConstructor)
            Return New CreateTool(Of TShape)(tlState, shapeFactory)
        End Function

        Public Function CreateNewSelectTool(Of TShape As IShape)() As ITool(Of ICanvas)
            Return New SelectTool(Nothing)
        End Function

        Public Function CreateRectangle() As ITool(Of ICanvas)
            Return CreateNewShapeTool(Of IRectangle)()
        End Function

        Public Function CreateCircle() As ITool(Of ICanvas)
            Return CreateNewShapeTool(Of ICircle)()
        End Function

        Public Function CreateEllipse() As ITool(Of ICanvas)
            Return CreateNewShapeTool(Of IEllipse)()
        End Function

        Public Function CreatePolygon() As ITool(Of ICanvas)
            Return CreateNewShapeTool(Of IPolygon)()
        End Function

    End Module
End Namespace