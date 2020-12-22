Namespace Controls.Canvas.Tools.ToolState
    Friend MustInherit Class CanvasToolState
        Implements IToolStateHandler(Of ICanvas)

        Public MustOverride Function Handle(coordinate As Point, commit As Boolean, context As ICanvas, state As IToolState(Of ICanvas)) As Boolean Implements IToolStateHandler(Of ICanvas).Handle

    End Class
End Namespace