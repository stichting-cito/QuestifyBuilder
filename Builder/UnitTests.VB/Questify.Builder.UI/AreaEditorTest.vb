
Imports System.Drawing
Imports Questify.Builder.UI
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas.LabelGenerator
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas

<TestClass()> _
Public Class AreaEditorTest

    Private _areaEditor As AreaEditor
    Private _rectangleShape As RectangleShape
    Private _circleShape As CircleShape
    Private _shapeFactory As IDrawableShapeFactory
    Private _shapeConverter As ShapeConverter
    Private _idGenerator As IIdentifierGenerator(Of String)

    <TestInitialize()> _
    Public Sub TestInitialize()
        _areaEditor = New AreaEditor()

        _rectangleShape = New RectangleShape()
        _rectangleShape.TopLeft = New Point(10, 10)
        _rectangleShape.BottomRight = New Point(20, 20)

        _circleShape = New CircleShape()
        _circleShape.AnchorPoint = New Point(0, 0)
        _circleShape.Radius = 20

        _idGenerator = New AlphabeticIdentifierGenerator()
        _shapeFactory = New LabeledShapeFactory(_idGenerator)
        _shapeConverter = New ShapeConverter(_shapeFactory)
    End Sub

    <TestCleanup()> _
    Public Sub TestCleanup()

    End Sub

    <TestMethod()> _
    Public Sub AddOneRectangleToShapeList()
        Dim shapeList As New ShapeList()
        shapeList.Add(_rectangleShape)

        _areaEditor.ShapeList = shapeList

        Assert.IsTrue(_areaEditor.ShapeList.Count = 1)
    End Sub

    <TestMethod()> _
    Public Sub AddOneRectangleToCanvas()

        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_rectangleShape, True))

        Assert.IsTrue(_areaEditor.ShapeList.Count = 1)
        Assert.IsTrue(_areaEditor.ShapeList(0).Identifier = "A")
    End Sub

    <TestMethod()> _
    Public Sub AddOneCircleToCanvas()

        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_circleShape, True))

        Assert.IsTrue(_areaEditor.ShapeList.Count = 1)
        Assert.IsTrue(_areaEditor.ShapeList(0).Identifier = "A")
    End Sub

    <TestMethod()> _
    Public Sub AddOneCircleAndOneRectangleToCanvas()

        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_circleShape, True))
        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_rectangleShape, True))

        Assert.IsTrue(_areaEditor.ShapeList.Count = 2)
        Assert.IsTrue(_areaEditor.ShapeList(0).Identifier = "A")
        Assert.IsTrue(_areaEditor.ShapeList(1).Identifier = "B")
    End Sub

    <TestMethod()> _
    Public Sub AddOneCircleAndOneRectangleToCanvasAndRemoveTheCircle()
        Dim circleDrawing As IDrawableItem = _shapeConverter.ToDrawing(_circleShape, True)

        _areaEditor.AddShapeToCanvas(circleDrawing)
        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_rectangleShape, True))
        _areaEditor.RemoveShapeFromCanvas(circleDrawing)

        Assert.IsTrue(_areaEditor.ShapeList.Count = 1)
        Assert.IsTrue(_areaEditor.ShapeList(0).Identifier = "A")
        Assert.IsInstanceOfType(_areaEditor.ShapeList(0), GetType(RectangleShape))
    End Sub

    <TestMethod()> _
    Public Sub AddOneCircleAndOneRectangleToCanvasAndRemoveTheRectangle()
        Dim rectangleDrawing As IDrawableItem = _shapeConverter.ToDrawing(_rectangleShape, True)

        _areaEditor.AddShapeToCanvas(_shapeConverter.ToDrawing(_circleShape, True))
        _areaEditor.AddShapeToCanvas(rectangleDrawing)
        _areaEditor.RemoveShapeFromCanvas(rectangleDrawing)

        Assert.IsTrue(_areaEditor.ShapeList.Count = 1)
        Assert.IsTrue(_areaEditor.ShapeList(0).Identifier = "A")
        Assert.IsInstanceOfType(_areaEditor.ShapeList(0), GetType(CircleShape))
    End Sub

End Class
