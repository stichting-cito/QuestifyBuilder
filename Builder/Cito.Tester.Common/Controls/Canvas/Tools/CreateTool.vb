Imports Cito.Tester.Common.Controls.Canvas.Tools.ToolState
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory
Imports System.Linq
Imports System.Windows.Forms
Imports Cito.Tester.Common.Controls.Canvas.ShapeConstructor

Namespace Controls.Canvas.Tools
    Public Class CreateTool(Of T As IShape)
        Inherits CanvasTool
        Implements IToolState(Of ICanvas)

        Private _state As IToolStateHandler(Of ICanvas)
        Private _ShapeFactory As IDrawableShapeFactory = New DefaultShapeFactory

        Public Sub New(firstState As IToolStateHandler(Of ICanvas))
            MyBase.New(Nothing)
            _state = firstState
        End Sub

        Public Sub New(firstState As IToolStateHandler(Of ICanvas), shapeFactory As IDrawableShapeFactory)
            Me.New(firstState)
            _ShapeFactory = shapeFactory
        End Sub

        Public Sub SetState(state As IToolStateHandler(Of ICanvas)) Implements IToolState(Of ICanvas).SetState
            _state = state
        End Sub

        Public Sub EndState(sender As ICanvas) Implements IToolState(Of ICanvas).EndState
            Dim shape As IShape = DirectCast(sender.EditItem, IShape)

            If shape IsNot Nothing Then
                Dim newShape As IDrawableItem = _ShapeFactory.CreateShape(shape)
                sender.AddItem(newShape)
                sender.SetAsTemporaryEditedDrawing(Nothing)
                sender.Select(newShape)
                sender.Invalidate(sender.EditItem)
            Else
                sender.Invalidate(sender.Items.ToArray())
            End If

            sender.Tool = New SimpleTool()
            _state = Nothing
        End Sub


        Public Overrides Sub MouseMove(sender As ICanvas, e As MouseEventArgs)
            If _state IsNot Nothing Then
                If TypeOf _state Is NextPointState(Of T) AndAlso TypeOf DirectCast(_state, NextPointState(Of T)).ShapeConstructor Is TwoPointConstructorbase(Of T) Then
                    Dim constr = DirectCast(DirectCast(_state, NextPointState(Of T)).ShapeConstructor, TwoPointConstructorbase(Of T))
                    If constr.p1.HasValue AndAlso Not constr.p2.HasValue Then
                        Dim prevPoint = constr.p1.Value
                        _state.Handle(e.Location, False, sender, Me)

                        Dim rect = constr.Drawing.BoundingBox
                        If Not sender.BackgroundImageBounds.Contains(rect) Then
                            _state.Handle(prevPoint, False, sender, Me)
                        End If
                        Return
                    End If
                End If
                _state.Handle(e.Location, False, sender, Me)
            End If
        End Sub

        Public Overrides Sub MouseUp(sender As ICanvas, e As MouseEventArgs)
            If _state IsNot Nothing Then _state.Handle(e.Location, True, sender, Me)
        End Sub

        Public Overrides Sub EndTool(sender As ICanvas)
            If (sender.EditItem IsNot Nothing) Then
                sender.Invalidate(sender.EditItem)
            End If
            MyBase.EndTool(sender)
        End Sub

    End Class
End Namespace