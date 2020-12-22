
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas
Imports Cito.Tester.Common.Controls.Canvas.LabelGenerator
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports System.Linq
Imports Cito.Tester.Common.Controls.Canvas.Tools
Imports Questify.Builder.UI.Commanding

Public Class AreaEditor

    Public Event ShapeRepositioned As EventHandler(Of EventArgs)
    Public Event ShapeRemoved As EventHandler(Of EventArgs)
    Public Event ShapeAdded As EventHandler(Of EventArgs)


    Private ReadOnly _canvas As ICanvas
    Private _shapeFactory As IDrawableShapeFactory
    Private _defaultShapeSize As New Size(50, 50)
    Private _shapeList As New ShapeList()
    Private _interestedInNotifications As Boolean = True
    Private _shapeConverter As ShapeConverter
    Private _idGenerator As IIdentifierGenerator(Of String)
    Private _maxNrOfShapesToCreate As Integer = 150


    Public Sub New()

        InitializeComponent()

        _idGenerator = New AlphabeticIdentifierGenerator()
        _shapeFactory = New LabeledShapeFactory(_idGenerator)
        _shapeConverter = New ShapeConverter(_shapeFactory)
        _canvas = DirectCast(Canvas1, ICanvas)

        BindCommands()

        AddHandler _canvas.CollectionChanged, AddressOf HandleCollectionChanged
        AddHandler _canvas.SelectionChanged, AddressOf HandleSelectionChanged
    End Sub



    Public Property DefaultShapeSize As Size
        Get
            Return _defaultShapeSize
        End Get
        Set(value As Size)
            _defaultShapeSize = value
            ToolStripWidthTextBox.Text = value.Width
            ToolStripHeightTextBox.Text = value.Height
        End Set
    End Property
    Public Property MaxNrOfShapesToCreate As Integer
        Get
            Return _maxNrOfShapesToCreate
        End Get
        Set(value As Integer)
            _maxNrOfShapesToCreate = value
        End Set
    End Property

    Public Property NewCircleButtonVisible As Boolean
        Get
            Return bNewCircle.Visible
        End Get
        Set(value As Boolean)
            bNewCircle.Visible = value
        End Set
    End Property

    Public Property NewRectangleButtonVisible As Boolean
        Get
            Return bNewRect.Visible
        End Get
        Set(value As Boolean)
            bNewRect.Visible = value
        End Set
    End Property

    Public ReadOnly Property AreaToolstrip As ToolStrip
        Get
            Return ToolStrip
        End Get
    End Property

    Public ReadOnly Property ShapeFactory() As IDrawableShapeFactory
        Get
            Return _shapeFactory
        End Get
    End Property

    Public Property ShapeList As ShapeList
        Get
            Return _shapeList
        End Get
        Set(value As ShapeList)
            If value IsNot Nothing Then
                Init(value, False, False)
                _shapeList = value
            End If
        End Set
    End Property




    Public Sub SetEditImage(img As Image)
        Canvas1.BackgroundImage = img
        Canvas1.Size = img.Size
        Panel1.Size = img.Size
        Panel1.AutoScrollMinSize = img.Size
    End Sub

    Public Sub SetExternalShapeList(shapeList As ShapeList)
        Init(shapeList, True, True)
    End Sub

    Public Sub AddShapeToCanvas(ByVal shape As IDrawableItem)
        _canvas.AddItem(shape)
    End Sub

    Public Sub RemoveShapeFromCanvas(ByVal shape As IDrawableItem)
        _canvas.RemoveItem(shape)
    End Sub

    Public ReadOnly Property CanvasItems As IEnumerable(Of IDrawableItem)
        Get
            Return _canvas.Items
        End Get
    End Property

    Public Sub ClearCanvas()
        _shapeList.Clear()
        _canvas.Clear()
    End Sub

    Public Sub DeleteAll(afterConfirmation As Boolean)
        Dim doRemove As Boolean
        If afterConfirmation Then
            doRemove = _canvas.IsConfirmedToRemoveAllShapes
        Else
            doRemove = True
        End If
        If doRemove Then
            RaiseEvent ShapeRemoved(Me, New EventArgs())
            _canvas.Clear()
        End If
    End Sub


    Private Sub BindCommands()
        Dim remove As New DelegateCommand(Of Boolean)(bDelete.Text, Sub() Delete(), Function() TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _canvas.EditItem IsNot Nothing)
        Dim removeAll As New DelegateCommand(Of Boolean)(bDeleteAll.Text, Sub() DeleteAll(True), Function() TryCast(_canvas.Tool, SimpleTool) IsNot Nothing)
        Dim circle As New DelegateCommand(Of Boolean)(bNewCircle.Text, Sub() _canvas.Tool = ToolFactory.CreateNewShapeTool(Of ICircle)(_shapeFactory), Sub() _canvas.AddItem(CreateCircle), Function() (TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _shapeList.Count < _maxNrOfShapesToCreate))
        Dim ellipse As New DelegateCommand(Of Boolean)(bNewEllipse.Text, Sub() _canvas.Tool = ToolFactory.CreateNewShapeTool(Of IEllipse)(_shapeFactory), Sub() _canvas.AddItem(CreateEllipse), Function() (TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _shapeList.Count < _maxNrOfShapesToCreate))
        Dim rectangle As New DelegateCommand(Of Boolean)(bNewRect.Text, Sub() _canvas.Tool = ToolFactory.CreateNewShapeTool(Of IRectangle)(_shapeFactory), Sub() _canvas.AddItem(_shapeFactory.CreateShape(DirectCast(_shapeConverter.ToDrawing(New RectangleShape() With {.TopLeft = New Point(0, 0), .BottomRight = New Point(DefaultShapeSize.Width, DefaultShapeSize.Height)}), IRectangle))), Function() (TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _shapeList.Count < _maxNrOfShapesToCreate))
        Dim pointUpTriangle As New DelegateCommand(Of Boolean)(bNewPointUpTriangle.Text, Sub() _canvas.Tool = ToolFactory.CreateNewShapeTool(Of IPointUpTriangle)(_shapeFactory), Sub() _canvas.AddItem(_shapeFactory.CreateShape(DirectCast(_shapeConverter.ToDrawing(New PointUpTriangleShape() With {.BaseLeft = New Point(0, 0), .BaseRight = New Point(DefaultShapeSize.Width, DefaultShapeSize.Height), .Top = New Point(DefaultShapeSize.Width / 2, DefaultShapeSize.Height)}), IPointUpTriangle))), Function() (TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _shapeList.Count < _maxNrOfShapesToCreate))
        Dim pointDownTriangle As New DelegateCommand(Of Boolean)(bNewPointDownTriangle.Text, Sub() _canvas.Tool = ToolFactory.CreateNewShapeTool(Of IPointDownTriangle)(_shapeFactory), Sub() _canvas.AddItem(_shapeFactory.CreateShape(DirectCast(_shapeConverter.ToDrawing(New PointDownTriangleShape() With {.BaseLeft = New Point(0, 0), .BaseRight = New Point(DefaultShapeSize.Width, DefaultShapeSize.Height), .Top = New Point(DefaultShapeSize.Width / 2, DefaultShapeSize.Height)}), IPointDownTriangle))), Function() (TryCast(_canvas.Tool, SimpleTool) IsNot Nothing AndAlso _shapeList.Count < _maxNrOfShapesToCreate))

        CmdManager.Bind(remove, bDelete)
        CmdManager.Bind(removeAll, bDeleteAll)
        CmdManager.Bind(circle, bNewCircle)
        CmdManager.Bind(ellipse, bNewEllipse)
        CmdManager.Bind(rectangle, bNewRect)
        CmdManager.Bind(pointUpTriangle, bNewPointUpTriangle)
        CmdManager.Bind(pointDownTriangle, bNewPointDownTriangle)

    End Sub




    Private Function CreateCircle() As ICircle
        Dim radius = (Math.Max(DefaultShapeSize.Height, DefaultShapeSize.Width) / 2)
        Dim circle As ICircle = _shapeFactory.CreateShape(DirectCast(_shapeConverter.ToDrawing(New CircleShape() With {.Radius = radius}), ICircle))
        circle.AnchorPoint = New Point(radius, radius)
        Return circle
    End Function

    Private Function CreateEllipse() As IEllipse
        Dim hRadius = ((DefaultShapeSize.Height) / 2)
        Dim vRadius = ((DefaultShapeSize.Width) / 2)
        Dim ellipse As IEllipse = _shapeFactory.CreateShape(DirectCast(_shapeConverter.ToDrawing(New EllipseShape() With {.HRadius = hRadius, .VRadius = vRadius}), IEllipse))
        ellipse.AnchorPoint = New Point(vRadius, hRadius)
        Return ellipse
    End Function

    Private Sub Delete()
        Dim selected As IDrawableItem = _canvas.EditItem

        If _canvas.IsConfirmedToRemoveShape Then
            RaiseEvent ShapeRemoved(Me, New EventArgs())
            _canvas.RemoveItem(selected)
        End If
    End Sub

    Private Sub HandleCollectionChanged(s As Object, e As Cito.Tester.Common.Controls.Canvas.NotifyCollectionChangedEventArgs)
        If (_interestedInNotifications) Then
            Debug.Assert(_shapeList IsNot Nothing)
            Select Case e.Action
                Case Cito.Tester.Common.Controls.Canvas.NotifyCollectionChangedAction.Add
                    Dim o As IDrawableItem = DirectCast(e.NewItems(0), IDrawableItem)
                    _shapeList.Add(_shapeConverter.ToShape(o))
                    DefaultShapeSize = New Size(o.BoundingBox.Width, o.BoundingBox.Height)
                    RaiseEvent ShapeAdded(Me, New EventArgs())
                Case Cito.Tester.Common.Controls.Canvas.NotifyCollectionChangedAction.Replace
                    Dim o As IDrawableItem = DirectCast(e.NewItems(0), IDrawableItem)
                    Dim old = DirectCast(e.OldItems(0), IDrawableItem)
                    Dim oldShape = _shapeConverter.ToShape(old)
                    Dim oldIndex = _shapeList.IndexOf(oldShape)
                    _shapeList.Remove(oldShape)

                    If oldIndex <> -1 Then
                        DefaultShapeSize = New Size(o.BoundingBox.Width, o.BoundingBox.Height)
                        _shapeList.Insert(oldIndex, _shapeConverter.ToShape(o))
                    End If

                    RaiseEvent ShapeRepositioned(Me, New EventArgs())
                Case Cito.Tester.Common.Controls.Canvas.NotifyCollectionChangedAction.Remove
                    Dim o = DirectCast(e.OldItems(0), IDrawableItem)
                    _shapeList.Remove(_shapeList.FirstOrDefault(Function(shape) shape.Identifier = o.ID))

                    RelabelShapes()

                Case Else
                    Debug.Assert(False, "Not expected")
            End Select
        End If
    End Sub

    Private Sub HandleSelectionChanged(s As Object, e As EventArgs)
        Dim item = TryCast(s, IDrawableItem)
        If item IsNot Nothing Then
            ToolStripWidthTextBox.Text = item.BoundingBox.Width.ToString
            ToolStripHeightTextBox.Text = item.BoundingBox.Height.ToString
        End If
    End Sub

    Private Sub RelabelShapes()
        _idGenerator.Reset()

        Init(_shapeList, True, False)

        _shapeList.Clear()

        For Each drawableItem As IDrawableItem In _canvas.Items
            _shapeList.Add(_shapeConverter.ToShape(drawableItem))
        Next

    End Sub

    Private Sub Init(value As ShapeList, relabel As Boolean, interestedInNotifications As Boolean)
        _interestedInNotifications = interestedInNotifications
        _canvas.Clear()

        If Not relabel Then
            _idGenerator = New AlphabeticIdentifierGenerator(value.Count + 1)
            _shapeFactory = New LabeledShapeFactory(_idGenerator)
            _shapeConverter = New ShapeConverter(_shapeFactory)
        End If

        For Each e As Shape In value.OrderBy(Function(i) i.Identifier)
            _canvas.AddItem(_shapeConverter.ToDrawing(e, relabel))
        Next

        _interestedInNotifications = True
    End Sub

    Private Sub ToolStripWidthTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripWidthTextBox.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub ToolStripHeightTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripHeightTextBox.KeyPress
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub ToolStripWidthTextBox_TextChanged(sender As Object, e As EventArgs) Handles ToolStripWidthTextBox.TextChanged
        Dim newWidth As Integer
        Integer.TryParse(ToolStripWidthTextBox.Text, newWidth)
        _defaultShapeSize.Width = newWidth
    End Sub

    Private Sub ToolStripHeightTextBox_TextChanged(sender As Object, e As EventArgs) Handles ToolStripHeightTextBox.TextChanged
        Dim newHeight As Integer
        Integer.TryParse(ToolStripHeightTextBox.Text, newHeight)
        _defaultShapeSize.Height = newHeight
    End Sub

    Private Sub AreaEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToolStripWidthTextBox.Text = _defaultShapeSize.Width
        ToolStripHeightTextBox.Text = _defaultShapeSize.Height
    End Sub

End Class
