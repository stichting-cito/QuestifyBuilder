Imports Questify.Builder.UI.PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
Imports Cito.Tester.ContentModel
Imports System.Linq

Friend Class ScoringParameterMultiEditorFactory(Of TScoreParam As ScoringParameter)
    Inherits FactoryBase(Of TScoreParam, ParameterCollectionEditorControl)

    Public Overrides Function DoConstruct(ByVal prm As TScoreParam, ByVal editor As ParameterSetsEditor) As ParameterCollectionEditorControl

        Dim ret As New ParameterCollectionEditorControl(editor, prm,
                                                  editor.ResourceEntity,
                                                  editor.ResourceManager,
                                                  editor.HasLoadedOldItemLayoutTemplate,
                                                  editor.ContextIdentifierForEditors,
                                                  GetInteractionParameters(prm))

        AddHandler ret.ResourceNeeded, AddressOf editor.ParameterEditorControl_ResourceNeeded
        AddHandler ret.EditResource, AddressOf editor.ParameterEditorControl_EditResource

        Return ret
    End Function

    Private Function GetInteractionParameters(ByVal scoreParam As TScoreParam) As IEnumerable(Of ParameterBase)
        Dim properties = scoreParam.GetType().GetProperties().Where(Function(prop) prop.IsDefined(GetType(ParameterControlAttribute), False))
        Return (From propertyInfo In properties Select CType(propertyInfo.GetValue(scoreParam), ParameterBase)).ToList()
    End Function
End Class