Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class PlainTextPrmFactory
        Inherits FactoryBase(Of PlainTextParameter, PlainTextParameterEditorControl)


        Public Overrides Function DoConstruct(ByVal prm As PlainTextParameter, ByVal editor As ParameterSetsEditor) As PlainTextParameterEditorControl
            Dim ret As New PlainTextParameterEditorControl(editor, prm)
            Return ret
        End Function
    End Class

End Namespace
