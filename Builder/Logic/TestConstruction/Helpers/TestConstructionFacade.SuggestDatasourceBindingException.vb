Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Filtering

Namespace TestConstruction.Helpers
    Partial Public Class TestConstructionFacade

        Sub Handle_SuggestDatasourceBindingException(
                ByRef ex As SuggestDatasourceBindingException,
                ByRef editContextChainresult As ChainHandlerResult)

            Dim validatingEvenArgs As New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.All, ex)

            RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

            Select Case validatingEvenArgs.Resolution
                Case TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                    Resolution_SuggestedSection_RetryFix(ex)

                Case TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                    Resolution_SuggestedSection_RetryIgnore(ex)


                Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                    editContextChainresult = ChainHandlerResult.RequestAborted

            End Select
        End Sub

        Private Sub Resolution_SuggestedSection_RetryFix(ByRef ex As SuggestDatasourceBindingException)

            Dim filter As IChainhandler(Of TestConstructionRequest)
            If (ex.IsSuggestingNestedSection) Then
                filter = New ModifyTargetSection(ex.ItemsOfDatasource, ex.SuggestedTargetSection, ex.TargetSection, ex.SetBindingToDatasource)
            Else
                filter = New ModifyTargetSection(ex.ItemsOfDatasource, ex.TargetSection, ex.SetBindingToDatasource)
            End If

            _filteringChain.HandlerChain.Add(filter)
        End Sub

        Private Sub Resolution_SuggestedSection_RetryIgnore(ByRef ex As SuggestDatasourceBindingException)

            Dim filter As IChainhandler(Of TestConstructionRequest)
            filter = New ModifyTargetSection(ex.ItemsOfDatasource, ex.TargetSection)
            _filteringChain.HandlerChain.Add(filter)
        End Sub
    End Class
End Namespace