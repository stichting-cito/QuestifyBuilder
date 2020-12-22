Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports System.Linq

Namespace PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

    Class XHtmlPrmFactory
        Inherits FactoryBase(Of XHtmlParameter, XHtmlParameterEditorControl2)

        Private Const DynamicCollAutoScoringIdentifier As String = "autoScoring"

        Public Overrides Function DoConstruct(ByVal prm As XHtmlParameter, ByVal editor As ParameterSetsEditor) As XHtmlParameterEditorControl2
            Dim ret As New XHtmlParameterEditorControl2(editor, prm,
                                                              editor.ResourceEntity,
                                                            editor.ResourceManager,
                                                            editor.HasLoadedOldItemLayoutTemplate,
                                                            editor.ContextIdentifierForEditors)
            ret.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            ret.AutoValidate = AutoValidate.EnableAllowFocusChange

            AddHandler ret.AddedInlineCustomInteraction, Sub(s As Object, inlineElementArg As InlineElementEventArgs)
                                                             Dim name = GetResourceNameFromInlineElement(inlineElementArg.InlineElement)
                                                             Dim bankId = editor.ResourceEntity.BankId
                                                             Dim parameters = editor.ParameterSets
                                                             Dim solution = editor.Solution
                                                             Dim inlineParameters = inlineElementArg.InlineElement.Parameters
                                                             Dim findingOverride = inlineElementArg.InlineElement.InlineFindingOverride
                                                             Dim scoringLabel = GetScoringLabelFromInlineElement(inlineElementArg.InlineElement)
                                                             CustomInteractions.InlineCustomInteraction.AddParameters(name,
                                                                                                                            bankId,
                                                                                                                            parameters,
                                                                                                                            solution,
                                                                                                                            inlineParameters,
                                                                                                                            findingOverride,
                                                                                                                            scoringLabel)
                                                         End Sub

            AddHandler ret.RemovedInlineCustomInteraction, Sub(s As Object, inlineElementArg As InlineElementEventArgs)
                                                               Dim parameters = editor.ParameterSets
                                                               Dim inlineParameters = inlineElementArg.InlineElement.Parameters
                                                               Dim solution = editor.Solution
                                                               CustomInteractions.InlineCustomInteraction.Remove(parameters, inlineParameters, solution)
                                                           End Sub

            AddHandler ret.AddedInlineAspect, Sub(s As Object, inlineElementArg As InlineElementEventArgs)
                                                  Dim parameters = editor.ParameterSets
                                                  Dim inlineElement = inlineElementArg.InlineElement
                                                  Dim solution = editor.Solution
                                                  UpdateAutoScoringPrmAfterAdding(parameters, inlineElement, solution)
                                              End Sub

            AddHandler ret.RemovedInlineAspect, Sub(s As Object, inlineElementArg As InlineElementEventArgs)
                                                    Dim parameters = editor.ParameterSets
                                                    Dim inlineElement = inlineElementArg.InlineElement
                                                    Dim solution = editor.Solution
                                                    UpdateAutoScoringPrmAfterRemoval(parameters, inlineElement, solution)
                                                End Sub

            Return ret
        End Function

        Private Function GetResourceNameFromInlineElement(inlineElement As InlineElement) As String
            If inlineElement.Parameters IsNot Nothing AndAlso inlineElement.Parameters.FlattenParameters().Any(Function(p) p.Name IsNot Nothing AndAlso p.Name.StartsWith("source", StringComparison.OrdinalIgnoreCase)) Then
                Dim prm = inlineElement.Parameters.FlattenParameters().First(Function(p) p.Name IsNot Nothing AndAlso p.Name.StartsWith("source", StringComparison.OrdinalIgnoreCase))
                If TypeOf prm Is ResourceParameter Then Return DirectCast(prm, ResourceParameter).Value
            End If
            Return String.Empty
        End Function

        Private Function GetScoringLabelFromInlineElement(inlineElement As InlineElement) As String
            If inlineElement.Parameters IsNot Nothing AndAlso inlineElement.Parameters.FlattenParameters().Any(Function(p) TypeOf p Is PlainTextParameter AndAlso p.Name IsNot Nothing AndAlso p.Name.EndsWith("label", StringComparison.OrdinalIgnoreCase)) Then
                Dim prm = inlineElement.Parameters.FlattenParameters().First(Function(p) TypeOf p Is PlainTextParameter AndAlso p.Name IsNot Nothing AndAlso p.Name.EndsWith("label", StringComparison.OrdinalIgnoreCase))
                Return DirectCast(prm, PlainTextParameter).Value
            End If
            Return String.Empty
        End Function

        Private Sub UpdateAutoScoringPrmAfterAdding(parameters As ParameterSetCollection, inlineElement As InlineElement, solution As Solution)
            Dim scoringPrms = parameters.DeepFetchInlineScoringParameters()
            Dim inlineElementScoringPrms = inlineElement.Parameters.DeepFetchInlineScoringParameters()
            UpdateAutoScoringPrm(parameters, inlineElement, solution, scoringPrms.Union(inlineElementScoringPrms))
        End Sub

        Private Sub UpdateAutoScoringPrmAfterRemoval(parameters As ParameterSetCollection, inlineElement As InlineElement, solution As Solution)
            Dim scoringPrms = parameters.DeepFetchInlineScoringParameters().Where(Function(sp) String.IsNullOrEmpty(sp.InlineId) OrElse Not sp.InlineId.Equals(inlineElement.Identifier, StringComparison.InvariantCultureIgnoreCase))
            UpdateAutoScoringPrm(parameters, inlineElement, solution, scoringPrms)
        End Sub

        Private Sub UpdateAutoScoringPrm(parameters As ParameterSetCollection, inlineElement As InlineElement, solution As Solution, scoringPrms As IEnumerable(Of ScoringParameter))
            If Not solution.AutoScoring Then
                Dim autoScoringOffPrms = scoringPrms.Where(Function(sp) TypeOf sp Is AspectScoringParameter AndAlso DirectCast(sp, AspectScoringParameter).AutoScoringOffPrm)
                Dim aspectScoringPrms = scoringPrms.Where(Function(sp) TypeOf sp Is AspectScoringParameter AndAlso Not DirectCast(sp, AspectScoringParameter).AutoScoringOffPrm)
                Dim controllerId As String

                If Not aspectScoringPrms.Any() AndAlso Not autoScoringOffPrms.Any() Then
                    If Not parameters.Any(Function(prm) prm.IsDynamicCollection AndAlso prm.Id.Equals(DynamicCollAutoScoringIdentifier, StringComparison.InvariantCultureIgnoreCase)) Then
                        controllerId = "autoScoringOffController"
                        parameters.Add(DynamicAutoScoringOffCollection(controllerId, inlineElement.InlineFindingOverride))

                        UpdateAspectReferenceControllerId(solution, controllerId)
                    End If
                End If

                If aspectScoringPrms.Any() AndAlso autoScoringOffPrms.Any() Then
                    Dim pcToDelete = parameters.Where(Function(prm) prm.IsDynamicCollection AndAlso prm.Id.Equals(DynamicCollAutoScoringIdentifier, StringComparison.InvariantCultureIgnoreCase)).ToList()
                    pcToDelete.ForEach(Sub(pc)
                                           parameters.Remove(pc)
                                       End Sub)

                    UpdateAspectReferenceControllerId(solution, aspectScoringPrms.First().ControllerId)
                End If
            End If
        End Sub

        Private Function DynamicAutoScoringOffCollection(controllerId As String, findingOverride As String) As ParameterCollection
            Dim newPrm = New AspectScoringParameter() With {.AutoScoringOffPrm = True, .ControllerId = controllerId, .FindingOverride = findingOverride, .Name = "aspectScoring", .SingleAspectScoringEditor = True}
            newPrm.Value = New ParameterSetCollection()
            newPrm.Value.Add(New ParameterCollection() With {.Id = "1"})

            Dim prmColl = New ParameterCollection() With {.Id = DynamicCollAutoScoringIdentifier, .IsDynamicCollection = True}
            prmColl.InnerParameters.Add(newPrm)
            Return prmColl
        End Function

        Private Sub UpdateAspectReferenceControllerId(solution As Solution, controllerId As String)
            If solution.AspectReferenceSetCollection.Any() Then
                solution.AspectReferenceSetCollection.ForEach(Sub(ac)
                                                                  ac.Id = controllerId
                                                              End Sub)
            End If
        End Sub
    End Class

End Namespace
