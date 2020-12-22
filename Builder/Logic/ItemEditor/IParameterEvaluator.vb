Imports Cito.Tester.ContentModel
Namespace ItemEditor
    Public Interface IParameterEvaluator
        ReadOnly Property OwningGroupName() As String
        ReadOnly Property IsVisible() As Boolean
        ReadOnly Property Parameter() As ParameterBase
    End Interface
End Namespace
