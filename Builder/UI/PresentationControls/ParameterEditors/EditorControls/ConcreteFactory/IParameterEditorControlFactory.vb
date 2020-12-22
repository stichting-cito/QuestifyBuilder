Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Interface IParameterEditorControlFactory

        ReadOnly Property CreatedType As Type

        Function Construct(prm As ParameterBase, editor As ParameterSetsEditor) As ParameterEditorControlBase

    End Interface

End Namespace
