Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    MustInherit Class FactoryBase(Of T As ParameterBase, R As ParameterEditorControlBase)
        Implements IParameterEditorControlFactory

        Public MustOverride Function DoConstruct(ByVal prm As T, ByVal editor As ParameterSetsEditor) As R


        Private Function Construct(prm As ParameterBase, editor As ParameterSetsEditor) As ParameterEditorControlBase Implements IParameterEditorControlFactory.Construct
            Return DoConstruct(DirectCast(prm, T), editor)
        End Function

        Public ReadOnly Property CreatedType As System.Type Implements IParameterEditorControlFactory.CreatedType
            Get
                Return GetType(T)
            End Get
        End Property


    End Class

End Namespace
