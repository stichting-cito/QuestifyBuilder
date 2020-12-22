Imports System.Collections.Concurrent
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestPackageConstruction.ChainHandlers.Validating

Namespace TestPackageConstruction

    Public Class TestPackageConstructionFacade


        Private _editContextChain As ChainManager(Of TestPackageConstructionRequest)
        Private _testPackageChainResult As ChainHandlerResult
        Private _testPackageProcessChain As ChainManager(Of TestPackageConstructionRequest)
        Private _validationChain As ChainManager(Of TestPackageConstructionRequest)



        Sub New()
            ResetFacade()
        End Sub



        Public Event ResolveValidationError As EventHandler(Of TestConstructionValidationEventArgs)



        Public ReadOnly Property EditContextHandlers() As IList(Of IChainhandler(Of TestPackageConstructionRequest))
            Get
                Return _editContextChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property ValidationHandlers() As IList(Of IChainhandler(Of TestPackageConstructionRequest))
            Get
                Return _validationChain.HandlerChain
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




        Public Function AddTests(ByVal tests As IList(Of Datasources.ResourceRef), ByVal testContext As IList(Of Datasources.ResourceRef)) As ChainHandlerResult
            Dim request As New TestPackageConstructionRequest(TestPackageConstructionRequest.RequestTypeEnum.Add, tests, testContext)
            Dim chainResult As ChainHandlerResult

            chainResult = ExecuteChain(request)

            Return chainResult
        End Function


        Public Function RemoveTests(ByVal tests As IList(Of Datasources.ResourceRef), ByVal testContext As IList(Of Datasources.ResourceRef)) As ChainHandlerResult
            Dim request As New TestPackageConstructionRequest(TestPackageConstructionRequest.RequestTypeEnum.Remove, tests, testContext)
            Dim chainResult As ChainHandlerResult

            chainResult = ExecuteChain(request)

            Return chainResult
        End Function

        Public Sub ResetFacade()

            _testPackageProcessChain = New ChainManager(Of TestPackageConstructionRequest)("TestPackageConstruction Chain", ProcessStrategyEnum.StopOnFirstNothandled, True)
            _testPackageChainResult = ChainHandlerResult.RequestHandled

            _validationChain = New ChainManager(Of TestPackageConstructionRequest)("Validation Chain", ProcessStrategyEnum.ProcessEntireChain, True)
            _validationChain.HandlerChain.Add(New RequestContainsTestsValidationHandler())

            _editContextChain = New ChainManager(Of TestPackageConstructionRequest)("Edit TestPackage", ProcessStrategyEnum.ProcessEntireChain, True)

            With _testPackageProcessChain.HandlerChain
                .Add(_validationChain)
                .Add(_editContextChain)
            End With
        End Sub


        Private Function ExecuteChain(ByVal request As TestPackageConstructionRequest) As ChainHandlerResult
            Dim chainResult As ChainHandlerResult
            Dim editContextChainresult As ChainHandlerResult

            Do
                _testPackageProcessChain.reset()
                chainResult = _testPackageProcessChain.ProcessRequest(request)
                editContextChainresult = _editContextChain.ChainProcessingResult


                If editContextChainresult = ChainHandlerResult.RequestNotHandled Then

                    If _validationChain.HandlerExceptions.Count > 0 Then
                        For Each ex As ChainHandlerException In _validationChain.HandlerExceptions
                            Dim exInternal As ChainHandlerException = ex
                            WhenObject(ex,
                                          IsType(Of RemoveTestIsUsedAsRetryException)(Sub() Handle_TestIsRetryException(exInternal, editContextChainresult)),
                                          IsType(Of NoTestsInRequestException)(Sub(e) Handle_NoTestsInRequestException(e, editContextChainresult)),
                                          Otherwise(Sub() Handle_Default(exInternal, editContextChainresult)))

                            If (editContextChainresult = ChainHandlerResult.RequestAborted) Then
                                Exit For
                            End If
                        Next
                    Else
                        Trace.Assert(request.Tests.Count <> 0, "Internal error while processing request", "Empty Request should have been handled by the 'RequestContainsTestValidationHandler'")
                        Trace.Assert(_editContextChain.HandlerExceptions.Count = 0, "Internal error while processing request",
                                     $"The editContextChain contains {_editContextChain.HandlerExceptions.Count _
                                        } exceptions.")
                        Exit Do
                    End If
                End If
            Loop Until (editContextChainresult = ChainHandlerResult.RequestHandled) OrElse (editContextChainresult = ChainHandlerResult.RequestAborted)

            Return editContextChainresult
        End Function


        Private Sub Resolution_ItemRelVal_Abort()
        End Sub


        Protected Sub Handle_TestIsRetryException(ByRef ex As ChainHandlerException, ByRef editContextChainresult As ChainHandlerResult)
            If TypeOf ex Is RemoveTestIsUsedAsRetryException Then
                Dim validatingEvenArgs As TestConstructionValidationEventArgs
                validatingEvenArgs = New TestConstructionValidationEventArgs(DirectCast(TestConstructionValidationEventArgs.resolutionEnum.RetryFix + TestConstructionValidationEventArgs.resolutionEnum.Abort, TestConstructionValidationEventArgs.resolutionEnum), ex)
                RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

                Select Case validatingEvenArgs.Resolution
                    Case TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                    Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                        Resolution_ItemRelVal_Abort()
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

        Private Sub Handle_NoTestsInRequestException(ByRef ex As NoTestsInRequestException, ByRef editContextChainresult As ChainHandlerResult)
            If TypeOf ex Is NoTestsInRequestException Then
                Dim validatingEvenArgs As TestConstructionValidationEventArgs

                validatingEvenArgs = New TestConstructionValidationEventArgs(TestConstructionValidationEventArgs.resolutionEnum.Abort, ex)
                RaiseEvent ResolveValidationError(Me, validatingEvenArgs)

                Select Case validatingEvenArgs.Resolution
                    Case TestConstructionValidationEventArgs.resolutionEnum.Abort
                        editContextChainresult = ChainHandlerResult.RequestAborted
                End Select
            End If
        End Sub


    End Class
End Namespace