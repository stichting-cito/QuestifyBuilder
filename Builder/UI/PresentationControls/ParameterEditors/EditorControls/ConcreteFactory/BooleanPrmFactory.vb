Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class BooleanPrmFactory
        Inherits FactoryBase(Of BooleanParameter, BooleanParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As BooleanParameter, ByVal editor As ParameterSetsEditor) As BooleanParameterEditorControl
            Dim ret As New BooleanParameterEditorControl(editor, prm)
            Return ret
        End Function
    End Class
End Namespace
