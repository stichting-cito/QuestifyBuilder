Namespace Controls.Canvas.Tools.ToolState
    Public Interface IToolStateHandler(Of T)

        Function Handle(coordinate As Point, commit As Boolean, context As T, state As IToolState(Of T)) As Boolean

    End Interface
End Namespace