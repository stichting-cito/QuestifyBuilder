
Namespace ItemEditor
    Friend Interface IGroupConditionalEnabled
        Inherits IGroupInfluence
        Function IsEnabled() As Boolean

        Event NeedsReEvaluation As EventHandler
    End Interface
End Namespace
