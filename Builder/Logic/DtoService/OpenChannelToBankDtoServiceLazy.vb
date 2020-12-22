
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToBankDtoServiceLazy
    Inherits OpenChannelToDtoServiceLazy(Of BankDto, Integer, IBankDtoRepository)
    Implements IBankDtoRepository

    Public Function All() As IEnumerable(Of BankDto) Implements IBankDtoRepository.All
        Return SpecificDecoree.All()
    End Function

    Public Function GetBankAndParents(id As Integer) As IEnumerable(Of BankDto) Implements IBankDtoRepository.GetBankAndParents
        Return SpecificDecoree.GetBankAndParents(id)
    End Function
End Class