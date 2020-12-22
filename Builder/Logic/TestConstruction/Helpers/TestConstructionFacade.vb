Imports System.Collections.Concurrent
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Filtering

Namespace TestConstruction.Helpers
    Public Class TestConstructionFacade


        Private _postFilteringChain As ChainManager(Of TestConstructionRequest)
        Private _editContextChain As ChainManager(Of TestConstructionRequest)
        Private _filteringChain As ChainManager(Of TestConstructionRequest)
        Private _itemChainResult As ChainHandlerResult
        Private _itemProcessChain As ChainManager(Of TestConstructionRequest)
        Private _validationChain As ChainManager(Of TestConstructionRequest)



        Sub New()
            ResetFacade()
        End Sub



        Public Event ResolveValidationError As EventHandler(Of TestConstructionValidationEventArgs)



        Public ReadOnly Property EditContextHandlers() As IList(Of IChainhandler(Of TestConstructionRequest))
            Get
                Return _editContextChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property ValidationHandlers() As IList(Of IChainhandler(Of TestConstructionRequest))
            Get
                Return _validationChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property PostFilteringHandlers() As IList(Of IChainhandler(Of TestConstructionRequest))
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



        Public Function AddItems(ByVal items As IEnumerable(Of Datasources.ResourceRef),
                         ByVal itemContext As IList(Of Datasources.ResourceRef)) As ChainHandlerResult
            Dim request As New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Add, items, itemContext)
            Dim chainResult As ChainHandlerResult

            Me.ResetFiltering()
            chainResult = ExecuteChain(request)

            Return chainResult
        End Function

        Public Function RemoveItems(ByVal items As IList(Of Datasources.ResourceRef), ByVal itemContext As IList(Of Datasources.ResourceRef)) As ChainHandlerResult
            Dim request As New TestConstructionRequest(TestConstructionRequest.RequestTypeEnum.Remove, items, itemContext)
            Dim chainResult As ChainHandlerResult

            Me.ResetFiltering()
            chainResult = ExecuteChain(request)

            Return chainResult
        End Function

        Public Sub ResetFacade()

            _itemProcessChain = New ChainManager(Of TestConstructionRequest)("TestConstruction Chain", ProcessStrategyEnum.StopOnFirstNothandled, True)
            _itemChainResult = ChainHandlerResult.RequestHandled

            _filteringChain = New ChainManager(Of TestConstructionRequest)("Filtering Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _filteringChain.ChainProcessingResultForEmptyChain = ChainHandlerResult.RequestHandled

            _postFilteringChain = New ChainManager(Of TestConstructionRequest)("Post-Filtering Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _postFilteringChain.ChainProcessingResultForEmptyChain = ChainHandlerResult.RequestHandled

            _validationChain = New ChainManager(Of TestConstructionRequest)("Validation Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _validationChain.HandlerChain.Add(New RequestContainsItemsValidationHandler())

            _editContextChain = New ChainManager(Of TestConstructionRequest)("Edit AssessmentTest", ProcessStrategyEnum.ProcessEntireChain, True)

            With _itemProcessChain.HandlerChain
                .Add(_filteringChain)
                .Add(_postFilteringChain)
                .Add(_validationChain)
                .Add(_editContextChain)
            End With
        End Sub


        Private Function ExecuteChain(ByVal request As TestConstructionRequest) As ChainHandlerResult
            Dim chainResult As ChainHandlerResult
            Dim editContextChainresult As ChainHandlerResult

            Do
                _itemProcessChain.reset()
                chainResult = _itemProcessChain.ProcessRequest(request)
                editContextChainresult = _editContextChain.ChainProcessingResult


                If editContextChainresult = ChainHandlerResult.RequestNotHandled Then

                    If _validationChain.HandlerExceptions.Count > 0 Then
                        For Each ex As ChainHandlerException In _validationChain.HandlerExceptions
                            Dim exInternal As ChainHandlerException = ex
                            WhenObject(ex,
                                          IsType(Of ItemRelationshipException)(Sub(e) Handle_ItemRelationShipException(e, editContextChainresult, request)),
                                          IsType(Of ItemDatasourceUsedElsewhereException)(Sub(e) Handle_ItemDatasourceUsedElsewhereException(e, editContextChainresult)),
                                          IsType(Of SuggestDatasourceBindingException)(Sub(e) Handle_SuggestDatasourceBindingException(e, editContextChainresult)),
                                          IsType(Of InvalidRequestException)(Sub(e) Handle_InvalidRequestException(e, editContextChainresult)),
                                          IsType(Of NoItemsInRequestException)(Sub(e) Handle_NoItemsInRequestException(e, editContextChainresult)),
                                          Otherwise(Sub() Handle_Default(exInternal, editContextChainresult)))

                            If (editContextChainresult = ChainHandlerResult.RequestAborted) Then
                                Exit For
                            End If
                        Next
                    Else
                        Trace.Assert(request.Items.Count <> 0, "Internal error while processing request", "Empty Request should have been handled by the 'RequestContainsItemsValidationHandler'")
                        Trace.Assert(_editContextChain.HandlerExceptions.Count = 0, "Internal error while processing request",
                                     $"The editContextChain contains {_editContextChain.HandlerExceptions.Count _
                                        } exceptions.")
                        Exit Do
                    End If
                End If
            Loop Until (editContextChainresult = ChainHandlerResult.RequestHandled) OrElse (editContextChainresult = ChainHandlerResult.RequestAborted)

            Return editContextChainresult
        End Function

        Sub Handle_ItemRelationShipException(ByRef ex As ItemRelationshipException, ByRef editContextChainresult As ChainHandlerResult, ByRef request As TestConstructionRequest)
            If TypeOf ex Is ItemRelationshipException Then
                Dim validatingEvenArgs As TestConstructionValidationEventArgs
                validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.All, ex)

                RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

                Select Case validatingEvenArgs.Resolution
                    Case TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                        Resolution_ItemRelVal_RetryFix(request)

                    Case TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                        Resolution_ItemRelVal_RetryIgnore()

                    Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                        Resolution_ItemRelVal_Abort()
                        editContextChainresult = ChainHandlerResult.RequestAborted

                End Select
            End If
        End Sub

        Private Sub Handle_InvalidRequestException(ByRef ex As InvalidRequestException, ByRef editContextChainresult As ChainHandlerResult)
            If TypeOf ex Is InvalidRequestException Then
                Dim validatingEvenArgs As TestConstructionValidationEventArgs
                Dim reqEx As InvalidRequestException = CType(ex, InvalidRequestException)

                If reqEx.conflictingResourceRefs.Count > 0 Then
                    validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.Abort Or TestConstructionValidationEventArgs.resolutionEnum.RetryFix, ex)
                Else
                    validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.Abort, ex)
                End If

                RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

                Select Case validatingEvenArgs.Resolution
                    Case TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                        Resolution_ReqVal_RetryFix(CType(ex, InvalidRequestException))

                    Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                        editContextChainresult = ChainHandlerResult.RequestAborted
                End Select
            End If
        End Sub


        Private Sub Handle_NoItemsInRequestException(ByRef ex As NoItemsInRequestException, ByRef editContextChainresult As ChainHandlerResult)
            If TypeOf ex Is NoItemsInRequestException Then
                Dim validatingEvenArgs As TestConstructionValidationEventArgs

                validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.Abort, ex)
                RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

                Select Case validatingEvenArgs.Resolution
                    Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                        editContextChainresult = ChainHandlerResult.RequestAborted
                End Select
            End If
        End Sub


        Sub Handle_Default(ByRef ex As ChainHandlerException, ByRef editContextChainresult As ChainHandlerResult)
            Dim validatingEvenArgs As TestConstructionValidationEventArgs
            validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.Abort, ex)
            RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

            Select Case validatingEvenArgs.Resolution
                Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                    editContextChainresult = ChainHandlerResult.RequestAborted
            End Select
        End Sub


        Private Sub ResetFiltering()
            _filteringChain.HandlerChain.Clear()
        End Sub

        Private Sub Resolution_ReqVal_RetryFix(ByVal ex As InvalidRequestException)
            Dim filterName As String =
                    $"Clearing {ex.conflictingResourceRefs.Count _
                    } conflicting items from request because they are already present in context."
            Dim filter As IChainhandler(Of TestConstructionRequest)

            filter = New ModifyItemsInRequestHandler(filterName, ModifyItemsInRequestHandler.RequestTypeEnum.Remove, ex.conflictingResourceRefs)

            _filteringChain.HandlerChain.Add(filter)
        End Sub

        Private Sub Resolution_ItemRelVal_Abort()
        End Sub

        Private Sub Resolution_ItemRelVal_RetryFix(ByVal request As TestConstructionRequest)
            For Each relEx As ItemRelationshipException In _validationChain.HandlerExceptions
                Dim filter As IChainhandler(Of TestConstructionRequest)

                Select Case relEx.Behaviour
                    Case Datasources.DataSourceBehaviourEnum.Inclusion
                        If request.RequestType = TestConstructionRequest.RequestTypeEnum.Add Then
                            Dim filterName As String =
        $"Completing with items from inclusion group '{relEx.IdentifierOfSelection}'"
                            filter = New ModifyItemsInRequestHandler(filterName, ModifyItemsInRequestHandler.RequestTypeEnum.Add, relEx.ConflictingResourceRefs)
                        Else
                            Dim filterName As String =
        $"Completing with items from inclusion group '{relEx.IdentifierOfSelection}'"
                            filter = New ModifyItemsInRequestHandler(filterName, ModifyItemsInRequestHandler.RequestTypeEnum.Remove, relEx.ConflictingResourceRefs)

                        End If

                    Case Datasources.DataSourceBehaviourEnum.Exclusion
                        Dim filterName As String =
        $"Clearing conflicting from exclusion group '{relEx.IdentifierOfSelection}'"
                        filter = New ModifyItemsInRequestHandler(filterName, ModifyItemsInRequestHandler.RequestTypeEnum.Remove, relEx.ConflictingResourceRefs)

                    Case Else
                        Throw New NotSupportedException()
                End Select

                _filteringChain.HandlerChain.Add(filter)
            Next
        End Sub

        Private Sub Resolution_ItemRelVal_RetryIgnore()
            For Each relEx As ItemRelationshipException In _validationChain.HandlerExceptions
                For Each handler As IChainhandler(Of TestConstructionRequest) In _validationChain.HandlerChain
                    If TypeOf handler Is ItemRelationshipValidationHandler Then
                        Dim itemRelValidationHandler As ItemRelationshipValidationHandler = DirectCast(handler, ItemRelationshipValidationHandler)

                        Dim groupsToRemove As New List(Of Datasources.DataSourceSettings)
                        For Each group As KeyValuePair(Of Datasources.DataSourceSettings, IEnumerable(Of Datasources.ResourceRef)) In itemRelValidationHandler.RelatedItems
                            If group.Key.Behaviour = relEx.Behaviour AndAlso group.Key.Identifier = relEx.IdentifierOfSelection Then
                                groupsToRemove.Add(group.Key)
                            End If
                        Next

                        For Each groupToRemove As Datasources.DataSourceSettings In groupsToRemove
                            itemRelValidationHandler.RelatedItems.Remove(groupToRemove)
                        Next
                    End If
                Next
            Next
        End Sub


    End Class
End Namespace