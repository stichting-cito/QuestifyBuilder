Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class GapTextPrmFactory
        Inherits FactoryBase(Of GapTextParameter, GapTextParameterEditorControl)


        Public Overrides Function DoConstruct(ByVal prm As GapTextParameter, ByVal editor As ParameterSetsEditor) As GapTextParameterEditorControl
            Dim ret As New GapTextParameterEditorControl(editor, prm)

            Dim controlVisible As Boolean = False
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("showDimensions"), controlVisible) Then ret.ForceShowDimensions = controlVisible

            Return ret
        End Function
    End Class

End Namespace
