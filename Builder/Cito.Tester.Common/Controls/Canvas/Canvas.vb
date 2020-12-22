Option Infer On

Imports System.ComponentModel
Imports System.Windows.Forms
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports Cito.Tester.Common.Controls.Canvas.Tools
Imports Cito.Tester.Common.Controls.Canvas.Drawable.Ardorner

Namespace Controls.Canvas
    Public NotInheritable Class Canvas
        Inherits Control
        Implements ICanvas

        Private _buffer As Bitmap
        Private ReadOnly _drawableItems As BindingList(Of IDrawableItem)
        Private _color As Color = Color.Black
        Private ReadOnly _toolAdapter As ToolAdapter(Of Object, ICanvas)

        Private _editItem As IDrawableItem
        Private _drawMethod As CanvasBackGroundImageDrawMethod
        Private _adorner As IAdorner
        Friend CopyOfEditItem As IDrawableItem


        Public Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.Selectable Or
                     ControlStyles.UserPaint, True)
            BackColor = Color.White
            _drawableItems = New BindingList(Of IDrawableItem)

            Dim tool = New SimpleTool()
            _toolAdapter = New ToolAdapter(Of Object, ICanvas)(tool)
            _adorner = New SelectArdorner()
            InitialiseDrawingBuffer()
            InitUI()
        End Sub


        Sub InitUI()
            AddHandler MouseMove, AddressOf _toolAdapter.MouseMove
            AddHandler MouseWheel, AddressOf _toolAdapter.MouseWheel
            AddHandler MouseDown, AddressOf _toolAdapter.MouseDown
            AddHandler MouseEnter, AddressOf _toolAdapter.MouseEnter
            AddHandler MouseLeave, AddressOf _toolAdapter.MouseLeave
            AddHandler MouseHover, AddressOf _toolAdapter.MouseHover

            AddHandler MouseUp, AddressOf _toolAdapter.MouseUp
            AddHandler KeyDown, AddressOf _toolAdapter.KeyDown
            AddHandler KeyPress, AddressOf _toolAdapter.KeyPress
            AddHandler KeyUp, AddressOf _toolAdapter.KeyUp
        End Sub



        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            For Each d As IDrawableItem In _drawableItems
                d.Draw(g, Color)
            Next
            If _editItem IsNot Nothing Then
                _editItem.Draw(g, Color)
            End If
            If _adorner IsNot Nothing Then
                _adorner.Draw(g)
            End If
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.DrawImageUnscaled(_buffer, Point.Empty)
        End Sub

        Private Sub DoInvalidate(prev As IDrawableItem, editItem As IDrawableItem)
            If prev.BoundingBox.IntersectsWith(editItem.BoundingBox) Then
                Invalidate(Rectangle.Union(prev.BoundingBox, editItem.BoundingBox))
            Else
                Invalidate(prev.BoundingBox) :
                Invalidate(editItem.BoundingBox)
            End If
        End Sub



        Private Sub InitialiseDrawingBuffer()
            If (_buffer IsNot Nothing) Then
                _buffer.Dispose() :
                _buffer = Nothing
            End If
            _buffer = New Bitmap(Math.Max(Width, 1), Math.Max(Height, 1))
            Using g As Graphics = Graphics.FromImage(_buffer)
                g.Clear(BackColor)
                Select Case _drawMethod
                    Case CanvasBackGroundImageDrawMethod.Unscaled
                        If (MyBase.BackgroundImage IsNot Nothing) Then
                            g.DrawImageUnscaled(MyBase.BackgroundImage, Point.Empty)
                        End If
                    Case Else
                        CustomBackGroundDrawing.Invoke(g)
                End Select
            End Using
        End Sub



        Public Property BackImageDrawOption As CanvasBackGroundImageDrawMethod
            Get
                Return _drawMethod
            End Get
            Set(value As CanvasBackGroundImageDrawMethod)
                _drawMethod = value
                InitialiseDrawingBuffer()
            End Set
        End Property

        Public Overrides Property BackgroundImage As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
                InitialiseDrawingBuffer()
            End Set
        End Property


        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property CustomBackGroundDrawing As Action(Of Graphics)



        Private Event CollectionChanged As EventHandler(Of NotifyCollectionChangedEventArgs) Implements ICanvas.CollectionChanged
        Public Event SelectionChanged As EventHandler(Of EventArgs) Implements ICanvas.SelectionChanged

        Public Property Color As Color Implements ICanvas.Color
            Get
                Return _color
            End Get
            Set(value As Color)
                _color = value
            End Set
        End Property

        Private ReadOnly Property EditItem As IDrawableItem Implements ICanvas.EditItem
            Get
                Return _editItem
            End Get
        End Property

        Private Sub SetEditItem(value As IDrawableItem, doAttachHandlers As Boolean)
            CopyOfEditItem = Nothing
            If (Adorner IsNot Nothing) Then
                Adorner.AdornedElement = Nothing
            End If
            If _editItem IsNot Nothing Then
                DoInvalidate(_editItem)
                DetachPropertyChangedHandler()
            End If

            _editItem = value
            If _editItem IsNot Nothing Then
                DoInvalidate(_editItem)
                If (doAttachHandlers) Then
                    AttachPropertyChangedHandler()
                End If
                If (Adorner IsNot Nothing) Then
                    Adorner.AdornedElement = _editItem
                End If
            End If

            CopyOfEditItem = If(value IsNot Nothing, New DefaultShapeFactory().DuplicateShape(TryCast(value, IShape)), Nothing)
            RaiseEvent SelectionChanged(_editItem, new EventArgs)
        End Sub

        Private Property Adorner As IAdorner Implements ICanvas.Adorner
            Get
                Return _adorner
            End Get
            Set(value As IAdorner)
                If value IsNot Nothing AndAlso _adorner IsNot Nothing Then
                    _adorner.ToString()
                End If
                _adorner = value
                If _adorner IsNot Nothing Then
                    AddHandler _adorner.PropertyChanged, AddressOf handleAdornerPropertyChanged
                End If
            End Set

        End Property

        Private Property Tool As ITool(Of ICanvas) Implements ICanvas.Tool
            Get
                Return _toolAdapter.Adaptee
            End Get
            Set(value As ITool(Of ICanvas))
                _toolAdapter.EndTool(Me)
                _toolAdapter.Adaptee = value
            End Set
        End Property

        Private ReadOnly Property BackgroundImageBounds As Rectangle Implements ICanvas.BackgroundImageBounds
            Get
                Return If(BackgroundImage IsNot Nothing, New Rectangle(New Point(0, 0), BackgroundImage.Size), ClientRectangle)
            End Get
        End Property

        Private ReadOnly Property Items As IEnumerable(Of IDrawableItem) Implements ICanvas.Items
            Get
                Return _drawableItems
            End Get
        End Property

        Private Sub AddItem(itm As IDrawableItem) Implements ICanvas.AddItem
            _drawableItems.Add(itm)
            Invalidate(itm.BoundingBox)
            ElementAdded(itm)
        End Sub

        Private Sub SetAsTemporaryEditedDrawing(itm As IDrawableItem) Implements ICanvas.SetAsTemporaryEditedDrawing
            SetEditItem(itm, False)
            If (itm IsNot Nothing) Then
                Invalidate(itm.BoundingBox)
            End If
        End Sub

        Private Sub RemoveItem(itm As IDrawableItem) Implements ICanvas.RemoveItem
            If (_drawableItems.Remove(itm)) Then
                Invalidate(itm.BoundingBox)
                ElementRemoved(itm)
            End If
        End Sub

        Private Sub DoInvalidate(ParamArray itms As IDrawableItem()) Implements ICanvas.Invalidate
            For Each e In itms
                Dim r = e.BoundingBox
                r.Inflate(1, 1)
                Invalidate(r)
            Next

            RemoveFreakedUpDrawableItems()
        End Sub

        Private Function IsConfirmedToRemoveAllShapes() As Boolean Implements ICanvas.IsConfirmedToRemoveAllShapes
            If MessageBox.Show(My.Resources.RemoveAllShapesConfirm, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Return True
            End If

            Return False
        End Function

        Private Function IsConfirmedToRemoveShape() As Boolean Implements ICanvas.IsConfirmedToRemoveShape
            If MessageBox.Show(My.Resources.RemoveShapeConfirm, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Return True
            End If

            Return False
        End Function

        Private Sub RemoveFreakedUpDrawableItems()
            For i As Integer = _drawableItems.Count - 1 To 0 Step -1
                If Char.IsDigit(CChar(_drawableItems(i).ID)) Then
                    _drawableItems.RemoveAt(i)
                End If
            Next i
        End Sub

        Private Function HitTest(p As Point) As IList(Of IDrawableItem) Implements ICanvas.HitTest
            Dim ret As New List(Of IDrawableItem)
            For Each e In _drawableItems
                If (e.BoundingBox.Contains(p)) Then
                    ret.Add(e)
                End If
            Next
            Return ret
        End Function

        Private Sub RemoveSelected() Implements ICanvas.RemoveSelected
            If (EditItem IsNot Nothing) Then
                DoInvalidate(EditItem)
                If (_adorner IsNot Nothing) Then
                    _adorner.AdornedElement = Nothing
                End If
                ElementRemoved(EditItem)
                SetEditItem(Nothing, False)
            End If
        End Sub

        Private Sub DoSelect(item As IDrawableItem) Implements ICanvas.Select
            If (EditItem Is Nothing) Then
                _drawableItems.Remove(item)
                item.isSelected = True
                SetEditItem(item, True)
            End If
        End Sub

        Private Sub DeSelect() Implements ICanvas.DeSelect
            If (EditItem IsNot Nothing) Then
                EditItem.isSelected = False
                If (Not _drawableItems.Contains(EditItem)) Then
                    _drawableItems.Add(EditItem)
                End If
                DoInvalidate(EditItem)
                ElementChanged(EditItem, CopyOfEditItem)
                SetEditItem(Nothing, False)
            End If
        End Sub

        Private Sub Clear() Implements ICanvas.Clear
            If _drawableItems.Count > 0 Then
                For i As Integer = _drawableItems.Count - 1 To 0 Step -1
                    RemoveItem(_drawableItems.Item(i))
                Next i
            End If

            Invalidate(True)
        End Sub

        Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
            Return True
        End Function



        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            InitialiseDrawingBuffer()
        End Sub

        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            If Not Focused Then
                Focus()
            End If
            MyBase.OnMouseClick(e)
        End Sub

        Protected Overrides Sub OnLostFocus(e As EventArgs)
            MyBase.OnLostFocus(e)
            DeSelect()
        End Sub



        Private Sub handleAdornerPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            Invalidate(_adorner.BoundingBox)
        End Sub



        Private Sub ElementAdded(itm As IDrawableItem)
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itm))
        End Sub

        Private Sub ElementChanged(itm As IDrawableItem, old As IDrawableItem)
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, itm, old))
        End Sub

        Private Sub ElementRemoved(itm As IDrawableItem)
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, itm))
        End Sub



        Private Sub AttachPropertyChangedHandler()
            AddHandler _editItem.PropertyChanged, AddressOf HandleNotifyChanged
        End Sub

        Private Sub DetachPropertyChangedHandler()
            RemoveHandler _editItem.PropertyChanged, AddressOf HandleNotifyChanged
        End Sub

        Private Sub HandleNotifyChanged(sender As Object, e As PropertyChangedEventArgs)
            ElementChanged(_editItem, CopyOfEditItem)
            CopyOfEditItem = New DefaultShapeFactory().DuplicateShape(TryCast(_editItem, IShape))
        End Sub


        Protected Overrides Sub Dispose(isDisposing As Boolean)

            If (isDisposing) Then
                If (_buffer IsNot Nothing) Then
                    _buffer.Dispose()
                    _buffer = Nothing
                End If
            End If
            MyBase.Dispose(Disposing)


        End Sub
    End Class
End Namespace