Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory
    Class CustomInteractionResourcePrmFactory
        Inherits FactoryBase(Of CustomInteractionResourceParameter, CustomInteractionResourceParameterEditorControl)

        Public Overrides Function DoConstruct(ByVal prm As CustomInteractionResourceParameter, ByVal editor As ParameterSetsEditor) As CustomInteractionResourceParameterEditorControl
            Dim ret As New CustomInteractionResourceParameterEditorControl(editor, prm, editor.ResourceEntity, editor.ResourceManager)

            Debug.Assert(editor.ResourceEntity IsNot Nothing)

            Dim controlVisible As Boolean = False
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("editbuttonVisible"), controlVisible) Then ret.EditResourceButtonVisible = controlVisible
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("deletebuttonVisible"), controlVisible) Then ret.DeleteResourceButtonVisible = controlVisible
            If Boolean.TryParse(prm.DesignerSettings.GetSettingValueByKey("showDimensions"), controlVisible) Then ret.ForceShowDimensions = controlVisible

            If TypeOf prm Is CustomInteractionResourceParameter AndAlso prm.InlineUsage = False Then

                AddHandler ret.AddingResource, Sub(s As Object, resourceNameArg As ResourceNameEventArgs)
                                                   Dim name = resourceNameArg.ResourceName
                                                   Dim bankId = editor.ResourceEntity.BankId
                                                   Dim parameters = editor.ParameterSets
                                                   Dim solution = editor.Solution

                                                   CustomInteractions.CustomInteraction.AddParameters(name, bankId, parameters, solution)
                                               End Sub

                AddHandler ret.RemovingResource, Sub()
                                                     Dim parameters = editor.ParameterSets
                                                     Dim solution = editor.Solution
                                                     CustomInteractions.CustomInteraction.Remove(parameters, solution)
                                                 End Sub

            End If

            Return ret
        End Function
    End Class
End Namespace
