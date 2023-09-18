Imports System.ComponentModel
Imports System.Configuration
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.Facade.QTI30

    Public Class PackageCreatorFacade

        Protected _chain As ChainManager(Of PublicationRequest)
        Protected _setupTestChain As ChainManager(Of PublicationRequest)
        Protected _setupXsdValidationChain As ChainManager(Of PublicationRequest)
        Protected _createItemAndResourcesChain As ChainManager(Of PublicationRequest)
        Protected _createTestChain As ChainManager(Of PublicationRequest)
        Protected _saveTestAndManifestCreationChain As ChainManager(Of PublicationRequest)
        Protected _testAndManifestChain As ChainManager(Of PublicationRequest)
        Protected _packagingChain As ChainManager(Of PublicationRequest)

        Sub New()
            ResetFacade()
        End Sub

        Public ReadOnly Property TestCreationChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _createTestChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property SaveItemAndResourcesChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _createItemAndResourcesChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property PackagingChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _packagingChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property SaveTestAndManifestCreationChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _saveTestAndManifestCreationChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property SetupTestChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _setupTestChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property SetupXsdValidationChain() As IList(Of IChainhandler(Of PublicationRequest))
            Get
                Return _setupXsdValidationChain.HandlerChain
            End Get
        End Property

        Public ReadOnly Property Chain As ChainManager(Of PublicationRequest)
            Get
                Return _chain
            End Get
        End Property

        Private Function GetAsyncValueFromConfig() As Boolean
            Dim appSettings = ConfigurationManager.AppSettings
            Dim async = True
            If appSettings("PublicationParallel") IsNot Nothing Then
                Boolean.TryParse(appSettings("PublicationParallel"), async)
            End If
            Return async
        End Function

        Public Overridable Sub ResetFacade()
            Dim async As Boolean = GetAsyncValueFromConfig()

            _chain = New ChainManager(Of PublicationRequest)("Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, False)
            _testAndManifestChain = New ChainManager(Of PublicationRequest)("Create Package Chain", ProcessStrategyEnum.ProcessEntireChain, False)

            _setupTestChain = New ChainManager(Of PublicationRequest)("Setup Test Creation Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, False)
            _createTestChain = New ChainManager(Of PublicationRequest)("Test Creation Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, False)
            _createItemAndResourcesChain = New ChainManager(Of PublicationRequest)("Item Creation Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, async)
            _saveTestAndManifestCreationChain = New ChainManager(Of PublicationRequest)("Test and manifest Creation Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, False)
            _setupXsdValidationChain = New ChainManager(Of PublicationRequest)("Setup Validation Chain", ProcessStrategyEnum.ProcessEntireChain, False, False, False)
            _packagingChain = New ChainManager(Of PublicationRequest)("Packaging Chain", ProcessStrategyEnum.ProcessEntireChain, False, False)

            With _testAndManifestChain.HandlerChain
                .Add(_createTestChain)
                .Add(_saveTestAndManifestCreationChain)
            End With
            With _chain.HandlerChain
                .Add(_setupTestChain)
                .Add(_setupXsdValidationChain)
                .Add(_createItemAndResourcesChain)
                .Add(_testAndManifestChain)
                .Add(_packagingChain)
            End With
        End Sub

        Public Function CreatePackage(bw As BackgroundWorker, publicationRequest As PublicationRequest) As ChainHandlerResult
            Return ExecuteChain(bw, publicationRequest)
        End Function

        Public Function CreatePackage(publicationRequest As PublicationRequest) As ChainHandlerResult
            Return CreatePackage(Nothing, publicationRequest)
        End Function

        Protected Function ExecuteChain(bw As BackgroundWorker, ByVal request As PublicationRequest) As ChainHandlerResult
            Dim chainResult As ChainHandlerResult
            Do
                _chain.reset()
                _chain.BackGroundWorker = bw
                _chain.ProcessRequest(request)

                chainResult = _chain.ChainProcessingResult

                For Each chainManager As ChainManager(Of PublicationRequest) In _chain.HandlerChain
                    For Each chainex In chainManager.HandlerExceptions
                        _chain.HandlerExceptions.Add(chainex)
                    Next
                Next

                If _chain.HandlerExceptions.Count > 0 Then
                    chainResult = ChainHandlerResult.RequestAborted
                    Exit Do
                ElseIf Not chainResult = ChainHandlerResult.RequestAborted Then
                    chainResult = ChainHandlerResult.RequestHandled
                End If
            Loop Until Not chainResult = ChainHandlerResult.RequestNotHandled
            Return chainResult
        End Function

    End Class
End NameSpace