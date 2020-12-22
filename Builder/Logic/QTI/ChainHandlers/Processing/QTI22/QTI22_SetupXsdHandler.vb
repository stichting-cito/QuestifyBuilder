
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Requests.QTI22

Namespace QTI.ChainHandlers.Processing.QTI22

    Public Class QTI22_SetupXsdHandler
        Inherits QTI22_ChainHandlerBase

        Public Sub New(packageCreator As QTI22PackageCreator)
            MyBase.New(packageCreator)
        End Sub

        Public Overrides Function ProcessRequest(requestData As QTI22PublicationRequest) As ChainHandlerResult
            Dim xsdHelper = PackageCreator.GetXsdHelper
            xsdHelper.InitialiseSettings(requestData.Settings, PackageCreator.GetXsdFolders(requestData), PackageCreator)
            Return ChainHandlerResult.RequestHandled
        End Function
    End Class
End Namespace