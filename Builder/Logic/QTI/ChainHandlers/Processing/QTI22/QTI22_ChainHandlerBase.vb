Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Requests.QTI22

Namespace QTI.ChainHandlers.Processing.QTI22

    Public MustInherit Class QTI22_ChainHandlerBase
        Inherits ChainHandlerBase(Of QTI22PublicationRequest)

        Protected ReadOnly PackageCreator As QTI22PackageCreator

        Public Sub New(packageCreator As QTI22PackageCreator)
            Me.PackageCreator = packageCreator
        End Sub

    End Class
End Namespace