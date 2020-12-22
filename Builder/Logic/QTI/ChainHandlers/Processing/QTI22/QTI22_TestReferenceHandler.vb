
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Requests.QTI22

Namespace QTI.ChainHandlers.Processing.QTI22

    Public Class QTI22_TestReferenceHandler
        Inherits ChainHandlerBase(Of QTI22PublicationRequest)

        Protected ReadOnly TestReference As TestReference

        Public Sub New(testReference As TestReference)
            MyBase.New()
            Me.TestReference = testReference
        End Sub

        Public Overrides Function ProcessRequest(requestData As QTI22PublicationRequest) As ChainHandlerResult
            AddTestRef(requestData)
            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Sub AddTestRef(requestData As QTI22PublicationRequest)
        End Sub

    End Class
End Namespace