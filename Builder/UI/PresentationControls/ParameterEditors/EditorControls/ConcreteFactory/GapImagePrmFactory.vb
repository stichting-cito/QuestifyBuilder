Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class GapImagePrmFactory
        Inherits FactoryBase(Of GapImageParameter, GapImageParameterEditorControl)


        Public Overrides Function DoConstruct(ByVal prm As GapImageParameter, ByVal editor As ParameterSetsEditor) As GapImageParameterEditorControl
            Dim ret As New GapImageParameterEditorControl(editor, prm, editor.ResourceEntity, editor.ResourceManager)
            Return ret
        End Function
    End Class
End NameSpace