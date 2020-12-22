Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace ContentModel

    Public Class ResourceAndCustomBankPropertyMoveResult
        Public Sub New(ByVal sourceBankId As Integer, ByVal destinationBankId As Integer)
            SourceBank = BankFactory.Instance.GetBank(sourceBankId)
            DestinationBank = BankFactory.Instance.GetBank(destinationBankId)
            AllCanBeMoved = True
        End Sub

        Public Property SourceBank As BankEntity
        Public Property DestinationBank As BankEntity
        Public Property AllCanBeMoved As Boolean

        Public ReadOnly Property Details As List(Of ResourceAndCustomBankPropertyMoveResultDetail) = New List(Of ResourceAndCustomBankPropertyMoveResultDetail)
    End Class

End Namespace
