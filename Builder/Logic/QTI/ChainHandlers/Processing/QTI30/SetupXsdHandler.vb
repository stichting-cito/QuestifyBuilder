
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class SetupXsdHandler
        Inherits ChainHandlerBase

        Public Sub New(packageCreator As PackageCreator)
            MyBase.New(packageCreator)
        End Sub

        Public Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            Dim xsdHelper = PackageCreator.GetXsdHelper
            xsdHelper.InitialiseSettings(requestData.Settings, PackageCreator.GetXsdFolders(requestData), PackageCreator)
            Return ChainHandlerResult.RequestHandled
        End Function
    End Class
End Namespace