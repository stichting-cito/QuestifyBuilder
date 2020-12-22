Imports Cito.Tester.ContentModel

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class XHtmlResourcePrmFactory
        Inherits FactoryBase(Of XhtmlResourceParameter, XhtmlResourceParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As XhtmlResourceParameter, ByVal editor As ParameterSetsEditor) As XhtmlResourceParameterEditorControl
            Dim ret As New XhtmlResourceParameterEditorControl(editor, prm,
                                                               editor.ResourceEntity,
                                                               editor.ResourceManager)

            AddHandler ret.ResourceNeeded, AddressOf editor.ParameterEditorControl_ResourceNeeded
            AddHandler ret.EditResource, AddressOf editor.ParameterEditorControl_EditResource

            Dim buttonVisible As Boolean = False
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("editbuttonVisible"), buttonVisible) Then ret.EditResourceButtonVisible = buttonVisible
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("deletebuttonVisible"), buttonVisible) Then ret.DeleteResourceButtonVisible = buttonVisible
            Return ret
        End Function

    End Class

End Namespace
