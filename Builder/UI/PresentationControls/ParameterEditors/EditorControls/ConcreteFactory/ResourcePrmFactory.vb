Imports Cito.Tester.ContentModel
Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class ResourcePrmFactory
        Inherits FactoryBase(Of ResourceParameter, ResourceParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As ResourceParameter, ByVal editor As ParameterSetsEditor) As ResourceParameterEditorControl
            Dim ret As New ResourceParameterEditorControl(editor, prm, editor.ResourceEntity, editor.ResourceManager)

            Dim controlVisible As Boolean = False
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("editbuttonVisible"), controlVisible) Then ret.EditResourceButtonVisible = controlVisible
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("deletebuttonVisible"), controlVisible) Then ret.DeleteResourceButtonVisible = controlVisible
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("showDimensions"), controlVisible) Then ret.ForceShowDimensions = controlVisible

            Return ret
        End Function
    End Class
End Namespace

