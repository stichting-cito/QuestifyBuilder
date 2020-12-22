
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToCustomBankPropertyDtoServiceLazy
    Inherits OpenChannelToDtoServiceLazy(Of CustomBankPropertyDto, Guid, ICustomBankPropertyDtoRepository)
    Implements ICustomBankPropertyDtoRepository

    Public Function GetCustomBankPropertiesForBranch(ByVal bankId As Integer) As IEnumerable(Of CustomBankPropertyDto) Implements ICustomBankPropertyDtoRepository.GetCustomBankPropertiesForBranch
        Return SpecificDecoree.GetCustomBankPropertiesForBranch(bankId)
    End Function

    Public Function GetCustomBankPropertiesForBranchWithFilter(ByVal bankId As Integer, ByVal type As String) As IEnumerable(Of CustomBankPropertyDto) Implements ICustomBankPropertyDtoRepository.GetCustomBankPropertiesForBranchWithFilter
        Return SpecificDecoree.GetCustomBankPropertiesForBranchWithFilter(bankId, type)
    End Function

    Public Function GetSelectedValueDisplayValue(ByVal selectedValue As Guid, ByVal bankId As Integer) As String Implements ICustomBankPropertyDtoRepository.GetSelectedValueDisplayValue
        Return SpecificDecoree.GetSelectedValueDisplayValue(selectedValue, bankId)
    End Function

    Public Sub BankChanged(ByVal bankId As Integer) Implements ICustomBankPropertyDtoRepository.BankChanged
        SpecificDecoree.BankChanged(bankId)
    End Sub

    Public Sub BanksChanged(ByVal bankIds As IEnumerable(Of Integer)) Implements ICustomBankPropertyDtoRepository.BanksChanged
        SpecificDecoree.BanksChanged(bankIds)
    End Sub
End Class
