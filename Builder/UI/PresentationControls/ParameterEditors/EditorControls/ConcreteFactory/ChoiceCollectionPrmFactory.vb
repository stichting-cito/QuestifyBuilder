Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class ChoiceCollectionPrmFactory
        Inherits FactoryBase(Of ChoiceCollectionParameter, ChoicesParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As ChoiceCollectionParameter, ByVal editor As ParameterSetsEditor) As ChoicesParameterEditorControl
            Dim ret As New ChoicesParameterEditorControl(editor, prm)
            Return ret
        End Function
    End Class
End Namespace
