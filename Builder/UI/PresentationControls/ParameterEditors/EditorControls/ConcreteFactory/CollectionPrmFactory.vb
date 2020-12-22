Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class CollectionPrmFactory
        Inherits FactoryBase(Of CollectionParameter, ParameterCollectionEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As CollectionParameter, ByVal editor As ParameterSetsEditor) As ParameterCollectionEditorControl
            Dim ret As New ParameterCollectionEditorControl(editor, prm,
                                                            editor.ResourceEntity,
                                                            editor.ResourceManager,
                                                            editor.HasLoadedOldItemLayoutTemplate,
                                                            editor.ContextIdentifierForEditors)

            AddHandler ret.ResourceNeeded, AddressOf editor.ParameterEditorControl_ResourceNeeded
            AddHandler ret.EditResource, AddressOf editor.ParameterEditorControl_EditResource

            Return ret
        End Function

    End Class
End Namespace
