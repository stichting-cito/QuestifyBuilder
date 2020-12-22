Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class HotTextScoringParameterEditorFactory
        Inherits FactoryBase(Of HotTextScoringParameter, HotTextScoringParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As HotTextScoringParameter, ByVal editor As ParameterSetsEditor) As HotTextScoringParameterEditorControl
            Dim ret As New HotTextScoringParameterEditorControl(editor, prm,
                                                              editor.ResourceEntity,
                                                            editor.ResourceManager)
            ret.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            ret.AutoValidate = AutoValidate.EnableAllowFocusChange
            Return ret
        End Function
    End Class

End Namespace
