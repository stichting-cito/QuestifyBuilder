Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class GapTextRichTextParameterEditorFactory
        Inherits FactoryBase(Of GapTextRichTextParameter, GapTextRichTextParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As GapTextRichTextParameter, ByVal editor As ParameterSetsEditor) As GapTextRichTextParameterEditorControl
            Dim ret As New GapTextRichTextParameterEditorControl(editor, prm)
            Return ret

        End Function
    End Class

End Namespace