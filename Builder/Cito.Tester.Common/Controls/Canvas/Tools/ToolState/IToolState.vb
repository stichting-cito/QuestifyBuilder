Namespace Controls.Canvas.Tools.ToolState
    Public Interface IToolState(Of T)

        Sub SetState(state As IToolStateHandler(Of T))

        Sub EndState(sender As T)

    End Interface
End Namespace