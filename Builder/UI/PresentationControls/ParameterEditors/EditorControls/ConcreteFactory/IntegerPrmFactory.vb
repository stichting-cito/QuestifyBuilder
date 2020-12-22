Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class IntegerPrmFactory
        Inherits FactoryBase(Of IntegerParameter, IntegerParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As IntegerParameter, ByVal editor As ParameterSetsEditor) As IntegerParameterEditorControl
            Dim ret As New IntegerParameterEditorControl(editor, prm)
            Return ret
        End Function
    End Class

End Namespace
