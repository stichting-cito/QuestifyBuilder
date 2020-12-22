Imports System.Collections.Concurrent
Imports System.ComponentModel

Namespace Chain
    Public Class ChainManager(Of HandlerRequestType)
        Inherits ChainHandlerBase(Of HandlerRequestType)


        Private _chainProcessingResult As ChainHandlerResult = ChainHandlerResult.RequestNotHandled
        Private ReadOnly _handlerChain As IList(Of IChainhandler(Of HandlerRequestType)) = New List(Of IChainhandler(Of HandlerRequestType))
        Private ReadOnly _continueOnKnownErrorsOnly As Boolean = True



        Public Sub New(strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean)
            Me.Strategy = strategy
            Me.StopOnFirstHandlerException = stopOnFirstHandlerException
        End Sub

        Public Sub New(strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean, continueOnKnownErrorsOnly As Boolean)
            Me.New(strategy, stopOnFirstHandlerException)
            Me._continueOnKnownErrorsOnly = continueOnKnownErrorsOnly
        End Sub

        Public Sub New(strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean, continueOnKnownErrorsOnly As Boolean, async As Boolean)
            Me.New(strategy, stopOnFirstHandlerException, continueOnKnownErrorsOnly)
            Me.Async = async
        End Sub

        Public Sub New(name As String, strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean, continueOnKnownErrorsOnly As Boolean)
            Me.New(strategy, stopOnFirstHandlerException, continueOnKnownErrorsOnly)
            Me.Name = name
        End Sub

        Public Sub New(name As String, strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean, continueOnKnownErrorsOnly As Boolean, async As Boolean)
            Me.New(name, strategy, stopOnFirstHandlerException, continueOnKnownErrorsOnly)
            Me.Async = async
        End Sub

        Public Sub New(name As String, strategy As ProcessStrategyEnum, stopOnFirstHandlerException As Boolean)
            Me.New(strategy, stopOnFirstHandlerException, stopOnFirstHandlerException)
            Me.Name = name
        End Sub



        Public Property BackGroundWorker As BackgroundWorker

        Public ReadOnly Property ChainProcessingResult() As ChainHandlerResult
            Get
                Return _chainProcessingResult
            End Get
        End Property

        Public Property ChainProcessingResultForEmptyChain As ChainHandlerResult = ChainHandlerResult.RequestNotHandled

        Public ReadOnly Property HandlerChain() As IList(Of IChainhandler(Of HandlerRequestType))
            Get
                Return _handlerChain
            End Get
        End Property

        Public ReadOnly Property HandlerExceptions As BlockingCollection(Of ChainHandlerException) = New BlockingCollection(Of ChainHandlerException)

        Public ReadOnly Property StopOnFirstHandlerException As Boolean = True

        Public ReadOnly Property Async As Boolean = False

        Public ReadOnly Property Strategy As ProcessStrategyEnum = ProcessStrategyEnum.StopOnFirstHandled



        Private Function ChainStopCondition(handler As IChainhandler(Of HandlerRequestType), handlerResult As ChainHandlerResult) As Boolean
            Dim stopOnFirstHandled As Boolean = (handlerResult = ChainHandlerResult.RequestHandled AndAlso Strategy = ProcessStrategyEnum.StopOnFirstHandled)
            Dim stopOnFirstNotHandled As Boolean = (handlerResult = ChainHandlerResult.RequestNotHandled AndAlso Strategy = ProcessStrategyEnum.StopOnFirstNothandled)
            Dim stopOnFirstExceptionInCompositeHandler As Boolean = StopOnFirstHandlerException AndAlso TypeOf handler Is ChainManager(Of HandlerRequestType) AndAlso
                (CType(handler, ChainManager(Of HandlerRequestType)).HandlerExceptions.Count > 0)

            If stopOnFirstHandled Then
                Debug.Print("*Stop Condition: [stopOnFirstHandled]")
            End If

            If stopOnFirstNotHandled Then
                Debug.Print("*Stop Condition: [stopOnFirstNotHandled]")
            End If

            If stopOnFirstExceptionInCompositeHandler Then
                Dim subChain = CType(handler, ChainManager(Of HandlerRequestType))
                Debug.Print("*Stop Condition: [stopOnFirstExceptionInCompositeHandler], sub chain '{0}' contains exceptions", subChain.Name)
            End If

            Return stopOnFirstHandled OrElse stopOnFirstNotHandled OrElse stopOnFirstExceptionInCompositeHandler
        End Function

        Private Function ExecuteChain(requestData As HandlerRequestType) As ChainHandlerResult
            Debug.Print("Start ProcessRequest (ChainManager: {0} RequestType: {1}", Name, GetType(HandlerRequestType).ToString)

            HandlerExceptions.Clear()


            If Async Then
                Threading.Tasks.Parallel.ForEach(_handlerChain, Sub(handler As IChainhandler(Of HandlerRequestType))
                                                                    If BackGroundWorker IsNot Nothing AndAlso BackGroundWorker.CancellationPending Then
                                                                        _chainProcessingResult = ChainHandlerResult.RequestAborted
                                                                        Exit Sub
                                                                    End If
                                                                    Dim shouldContinue = ExecuteHandler(handler, requestData)
                                                                    If Not shouldContinue Then
                                                                        Exit Sub
                                                                    End If
                                                                End Sub)
            Else
                For Each handler As IChainhandler(Of HandlerRequestType) In _handlerChain
                    If BackGroundWorker IsNot Nothing AndAlso BackGroundWorker.CancellationPending Then
                        _chainProcessingResult = ChainHandlerResult.RequestAborted
                        Exit For
                    End If
                    Dim shouldContinue = ExecuteHandler(handler, requestData)
                    If Not shouldContinue Then
                        Exit For
                    End If
                Next
            End If
            Debug.Print("End ProcessRequest (ChainManager: {0} RequestType: {1} ChainResult: {2}", Name, GetType(HandlerRequestType).ToString, _chainProcessingResult)
            Return _chainProcessingResult
        End Function


        Private Function ExecuteHandler(handler As IChainhandler(Of HandlerRequestType), requestData As HandlerRequestType) As Boolean
            Try
                Dim handlerResult As ChainHandlerResult = handler.ProcessRequest(requestData)

                If handlerResult = ChainHandlerResult.RequestHandled Then
                    _chainProcessingResult = ChainHandlerResult.RequestHandled
                End If

                Debug.Print("*Executed Handler (Handler: {0} HandlerResult: {1} ChainResult: {2}", handler.Name, handlerResult.ToString, _chainProcessingResult)

                If ChainStopCondition(handler, handlerResult) Then
                    Return False
                End If
            Catch ex As Exception
                Debug.Print("*Executed Handler (Handler: {0} HandlerResult: Exception [{1}] ChainResult: {2}", handler.Name, ex.Message, _chainProcessingResult)

                If TypeOf ex Is ChainHandlerException OrElse Not _continueOnKnownErrorsOnly Then
                    Dim exceptionToPass As ChainHandlerException

                    If TypeOf ex Is ChainHandlerException Then
                        exceptionToPass = DirectCast(ex, ChainHandlerException)
                    ElseIf handler.LastHandledObject IsNot Nothing Then
                        exceptionToPass = New ChainHandlerException(
                            $"object:{handler.LastHandledObject} message:{ex.Message} trace:{ex.StackTrace}")
                    Else
                        exceptionToPass = New ChainHandlerException($"message:{ex.Message} trace:{ex.StackTrace}")
                    End If


                    HandlerExceptions.Add(exceptionToPass)

                    If StopOnFirstHandlerException Then
                        Debug.Print("*Stop Condition: [first handler exception]")

                        Return False
                    End If
                Else
                    Debug.Print("*Stop Condition: Previous exception type was not expected")

                    Return False
                End If
            End Try

            Return True
        End Function





        Public Overrides Function ProcessRequest(requestData As HandlerRequestType) As ChainHandlerResult
            If HandlerChain.Count <> 0 Then
                Return ExecuteChain(requestData)
            Else
                Return ChainProcessingResultForEmptyChain
            End If
        End Function

        Public Sub Reset()
            _chainProcessingResult = ChainHandlerResult.RequestNotHandled
            HandlerExceptions.Clear()
            For Each handler As IChainhandler(Of HandlerRequestType) In HandlerChain
                If TypeOf handler Is ChainManager(Of HandlerRequestType) Then
                    CType(handler, ChainManager(Of HandlerRequestType)).reset()
                End If
            Next
        End Sub


    End Class

End Namespace