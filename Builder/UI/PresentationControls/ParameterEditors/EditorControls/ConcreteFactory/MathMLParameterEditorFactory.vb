Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Friend Class MathMLParameterEditorFactory
        Inherits FactoryBase(Of MathMLParameter, MathMLParameterEditorControl)

        Public Overrides Function DoConstruct(prm As MathMLParameter, editor As ParameterSetsEditor) As MathMLParameterEditorControl
            Dim ret As New MathMLParameterEditorControl(prm)
            Return ret
        End Function

    End Class
End Namespace