Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Filtering

Namespace TestConstruction.Helpers
    Partial Public Class TestConstructionFacade

        Private Sub Handle_ItemDatasourceUsedElsewhereException(
                ByRef ex As ItemDatasourceUsedElsewhereException,
                ByRef editContextChainresult As ChainHandlerResult)

            Dim validatingEvenArgs As New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.All, ex)

            RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

            Select Case validatingEvenArgs.Resolution
                Case TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                    Resolution_ItemDSVal_RetryFix(ex)

                Case TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                    Resolution_ItemDSVal_RetryIgnore(ex)


                Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                    editContextChainresult = ChainHandlerResult.RequestAborted

            End Select

        End Sub

        Private Sub Resolution_ItemDSVal_RetryFix(ByRef ex As ItemDatasourceUsedElsewhereException)
            Dim filter As IChainhandler(Of TestConstructionRequest)
            filter = New ModifyTargetSection(ex.ConflictingItems, ex.ConflictiongSection)
            _filteringChain.HandlerChain.Add(filter)
        End Sub

        Private Sub Resolution_ItemDSVal_RetryIgnore(ByRef ex As ItemDatasourceUsedElsewhereException)
            Dim filter As IChainhandler(Of TestConstructionRequest)
            filter = New ModifyTargetSection(ex.ConflictingItems, ex.TargetSection)
            _filteringChain.HandlerChain.Add(filter)
        End Sub

    End Class
End Namespace