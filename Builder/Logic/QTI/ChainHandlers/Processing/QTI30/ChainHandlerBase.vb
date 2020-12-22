Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public MustInherit Class ChainHandlerBase
        Inherits ChainHandlerBase(Of PublicationRequest)

        Protected ReadOnly PackageCreator As PackageCreator

        Public Sub New(packageCreator As PackageCreator)
            Me.PackageCreator = packageCreator
        End Sub

    End Class
End Namespace