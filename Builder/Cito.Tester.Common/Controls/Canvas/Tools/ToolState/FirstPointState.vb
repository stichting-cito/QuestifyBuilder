Namespace Controls.Canvas.Tools.ToolState
    Friend Class FirstPointState(Of T As IShape)
        Inherits CanvasToolState

        Private _shapeConstructor As IShapeConstructor(Of T)

        Public Sub New(shapeConstructor As IShapeConstructor(Of T))
            _shapeConstructor = shapeConstructor
            Debug.Assert(_shapeConstructor IsNot Nothing)
        End Sub

        Public Overrides Function Handle(coordinate As Point, commit As Boolean, context As ICanvas, state As IToolState(Of ICanvas)) As Boolean
            context.Invalidate(_shapeConstructor.Drawing)
            If commit Then
                Dim ret = _shapeConstructor.CommitPoint(coordinate)
                Debug.Assert(ret = False)
                state.SetState(New NextPointState(Of T)(_shapeConstructor))
            Else
                _shapeConstructor.SuggestPoint(coordinate)
            End If
            If (context.EditItem Is Nothing) Then context.SetAsTemporaryEditedDrawing(_shapeConstructor.Drawing)
            context.Invalidate(context.EditItem)
        End Function

    End Class
End Namespace