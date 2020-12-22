Namespace Chain
    Public Interface IChainhandler(Of ProcessHandlerType)


        Property Name() As String

        Property LastHandledObject() As String




        Function ProcessRequest(ByVal requestData As ProcessHandlerType) As ChainHandlerResult


    End Interface
End Namespace