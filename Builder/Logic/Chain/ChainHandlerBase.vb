Namespace Chain
    Public MustInherit Class ChainHandlerBase(Of ProcessHandlerType)
        Implements IChainhandler(Of ProcessHandlerType)

        Public Property Name() As String = Me.GetType.ToString Implements IChainhandler(Of ProcessHandlerType).Name

        Public Property LastHandledObject() As String = Nothing Implements IChainhandler(Of ProcessHandlerType).LastHandledObject

        Public MustOverride Function ProcessRequest(ByVal requestData As ProcessHandlerType) As ChainHandlerResult Implements IChainhandler(Of ProcessHandlerType).ProcessRequest
    End Class
End Namespace