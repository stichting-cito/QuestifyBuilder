Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class ListedPrmFactory
        Inherits FactoryBase(Of ListedParameter, ListedParameterEditorControl)


        Public Overrides Function DoConstruct(ByVal prm As ListedParameter, ByVal editor As ParameterSetsEditor) As ListedParameterEditorControl
            Dim ret As New ListedParameterEditorControl(editor, prm)
            Return ret
        End Function
    End Class

End Namespace
