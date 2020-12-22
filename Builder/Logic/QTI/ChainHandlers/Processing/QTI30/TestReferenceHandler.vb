
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class TestReferenceHandler
        Inherits ChainHandlerBase(Of PublicationRequest)

        Protected ReadOnly TestReference As TestReference

        Public Sub New(testReference As TestReference)
            MyBase.New()
            Me.TestReference = testReference
        End Sub

        Public Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            AddTestRef(requestData)
            Return ChainHandlerResult.RequestHandled
        End Function

        Protected Overridable Sub AddTestRef(requestData As PublicationRequest)
        End Sub

    End Class
End Namespace