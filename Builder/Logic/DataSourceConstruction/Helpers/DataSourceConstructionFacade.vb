Imports System.Collections.Concurrent
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.Common.Filtering
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources

Namespace DataSourceConstruction.Helpers
    Public Class DataSourceConstructionFacade

        Private _postFilteringChain As ChainManager(Of DataSourceConstructionRequest)
        Private _editContextChain As ChainManager(Of DataSourceConstructionRequest)
        Private _filteringChain As ChainManager(Of DataSourceConstructionRequest)
        Private _itemProcessChain As ChainManager(Of DataSourceConstructionRequest)
        Private _validationChain As ChainManager(Of DataSourceConstructionRequest)


        Sub New()
            ResetFacade()
        End Sub



        Public Event ResolveValidationError As EventHandler(Of TestConstructionValidationEventArgs)



        Public ReadOnly Property EditContextHandlers() As IList(Of IChainhandler(Of DataSourceConstructionRequest))
            Get
                Return _editContextChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property ValidationHandlers() As IList(Of IChainhandler(Of DataSourceConstructionRequest))
            Get
                Return _validationChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property PostFilteringHandlers() As IList(Of IChainhandler(Of DataSourceConstructionRequest))
            Get
                Return _postFilteringChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property EditExceptions() As BlockingCollection(Of ChainHandlerException)
            Get
                Return _editContextChain.HandlerExceptions
            End Get
        End Property

        Public ReadOnly Property ValidationExceptions() As BlockingCollection(Of ChainHandlerException)
            Get
                Return _validationChain.HandlerExceptions
            End Get
        End Property



        Public Function AddItems(ByVal items As IList(Of ResourceRef)) As ChainHandlerResult

            Dim request As New DataSourceConstructionRequest(items)
            Dim chainResult As ChainHandlerResult

            Me.ResetFiltering()
            chainResult = ExecuteChain(request)

            Return chainResult
        End Function

        Public Function RemoveItems() As ChainHandlerResult
            Throw New NotImplementedException
        End Function


        Public Sub ResetFacade()

            _itemProcessChain = New ChainManager(Of DataSourceConstructionRequest)("TestConstruction Chain", ProcessStrategyEnum.StopOnFirstNothandled, True)

            _filteringChain = New ChainManager(Of DataSourceConstructionRequest)("Filtering Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _filteringChain.ChainProcessingResultForEmptyChain = ChainHandlerResult.RequestHandled

            _postFilteringChain = New ChainManager(Of DataSourceConstructionRequest)("Post-Filtering Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _postFilteringChain.ChainProcessingResultForEmptyChain = ChainHandlerResult.RequestHandled

            _validationChain = New ChainManager(Of DataSourceConstructionRequest)("Validation Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _validationChain.ChainProcessingResultForEmptyChain = ChainHandlerResult.RequestHandled

            _editContextChain = New ChainManager(Of DataSourceConstructionRequest)("Edit AssessmentTest", ProcessStrategyEnum.ProcessEntireChain, True)

            With _itemProcessChain.HandlerChain
                .Add(_filteringChain)
                .Add(_postFilteringChain)
                .Add(_validationChain)
                .Add(_editContextChain)
            End With
        End Sub



        Private Function ExecuteChain(ByVal request As DataSourceConstructionRequest) As ChainHandlerResult
            Dim editContextChainresult As ChainHandlerResult = ChainHandlerResult.RequestNotHandled

            Do
                _itemProcessChain.reset()
                _itemProcessChain.ProcessRequest(request)
                editContextChainresult = _editContextChain.ChainProcessingResult



                If editContextChainresult = ChainHandlerResult.RequestNotHandled Then

                    If _validationChain.HandlerExceptions.Count > 0 Then
                        For Each ex As ChainHandlerException In _validationChain.HandlerExceptions
                            Dim exInternal As ChainHandlerException = ex
                            WhenObject(ex,
                                          IsType(Of DataSourceValidationException)(Sub(e) editContextChainresult = NotifyUserAndTakeAction(e)),
                                          Otherwise(Sub() Debug.Assert(False,
                                                                       $"Exception {exInternal.GetType()} Not Handled")))
                        Next

                    Else
                        Trace.Assert(request.Items.Count <> 0, "Internal error while processing request", "Empty Request should have been handled by the 'RequestContainsItemsValidationHandler'")
                        Trace.Assert(_editContextChain.HandlerExceptions.Count = 0, "Internal error while processing request",
                                     $"The editContextChain contains {_editContextChain.HandlerExceptions.Count _
                                        } exceptions.")
                        Exit Do
                    End If
                End If
            Loop Until editContextChainresult = ChainHandlerResult.RequestHandled

            Return editContextChainresult
        End Function

        Private Sub ResetFiltering()
            _filteringChain.HandlerChain.Clear()
        End Sub


        Private Function NotifyUserAndTakeAction(ex As DataSourceValidationException) As ChainHandlerResult
            Dim e As New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore, ex)
            e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
            RaiseEvent ResolveValidationError(Me, e)

            If e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore Then
                FilterConflictingItems(ex.ValidationErrors.Keys)
                Return ChainHandlerResult.RequestNotHandled
            Else
                Return (ChainHandlerResult.RequestAborted)
            End If
        End Function

        Private Sub FilterConflictingItems(remove As ICollection(Of String))
            Dim filter As New ModifyDataSourceRequestHandler(FilterRequestTypeEnum.Remove, remove) _
                    With {.Name = "Clearing items that occur in other InclusionGroups"}
            _filteringChain.HandlerChain.Add(filter)
        End Sub

    End Class
End Namespace