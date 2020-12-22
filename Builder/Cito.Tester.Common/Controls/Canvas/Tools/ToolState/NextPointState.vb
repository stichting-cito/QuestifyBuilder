Namespace Controls.Canvas.Tools.ToolState
    Friend Class NextPointState(Of T As IShape)
        Inherits CanvasToolState


        Private _shapeConstructor As IShapeConstructor(Of T)

        Public Sub New(shapeConstructor As IShapeConstructor(Of T))
            _shapeConstructor = shapeConstructor
        End Sub

        Public ReadOnly Property ShapeConstructor() As IShapeConstructor(Of T)
            Get
                Return _shapeConstructor
            End Get
        End Property

        Public Overrides Function Handle(coordinate As Point, commit As Boolean, context As ICanvas, state As IToolState(Of ICanvas)) As Boolean
            context.Invalidate(_shapeConstructor.Drawing)
            If commit Then
                Dim ret = _shapeConstructor.CommitPoint(coordinate)
                If ret Then state.EndState(context)
            Else
                _shapeConstructor.SuggestPoint(coordinate)
            End If
            context.Invalidate(_shapeConstructor.Drawing)
        End Function

    End Class
End Namespace