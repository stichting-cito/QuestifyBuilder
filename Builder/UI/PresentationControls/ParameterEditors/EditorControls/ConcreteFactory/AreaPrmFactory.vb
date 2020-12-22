Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class AreaPrmFactory
        Inherits FactoryBase(Of AreaParameter, AreaParameterEditorControl)


        Public Overrides Function DoConstruct(ByVal prm As AreaParameter, ByVal editor As ParameterSetsEditor) As AreaParameterEditorControl
            Dim ret As New AreaParameterEditorControl(editor, prm, editor.ResourceEntity)
            AddHandler ret.ResourceNeeded, AddressOf editor.ParameterEditorControl_ResourceNeeded
            AddHandler ret.EditResource, AddressOf editor.ParameterEditorControl_EditResource
            Return ret
        End Function

    End Class
End Namespace