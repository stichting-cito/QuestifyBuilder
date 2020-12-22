Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToItemResourceDtoServiceLazy
    Inherits OpenChannelToResourceDtoServiceLazy(Of ItemResourceDto, IItemResourceDtoRepository)
    Implements IItemResourceDtoRepository

    Public Function GetPauseItemList(ByVal bankId As Integer) As IEnumerable(Of ItemResourceDto) Implements IItemResourceDtoRepository.GetPauseItemList
        Return SpecificDecoree.GetPauseItemList(bankId)
    End Function

    Public Function GetItemsListWithSearchOptions(ByVal bankId As Integer,
                                                  ByVal searchKeyWords As String,
                                                  ByVal includeSubbanks As Boolean,
                                                  ByVal searchInBankProperties As Boolean,
                                                  ByVal searchInItemText As Boolean,
                                                  ByVal testContextResourceId As Guid,
                                                  ByVal maxRecords As Integer) As IEnumerable(Of ItemResourceDto) Implements IItemResourceDtoRepository.GetItemsListWithSearchOptions

        Return SpecificDecoree.GetItemsListWithSearchOptions(bankId, searchKeyWords, includeSubbanks, searchInBankProperties, searchInItemText, testContextResourceId, maxRecords)
    End Function

    Public Function GetItemsByCode(ByVal itemCodeList As IEnumerable(Of String), ByVal bankId As Integer, ByVal request As ItemResourceRequestDTO) As IEnumerable(Of ItemResourceDto) Implements IItemResourceDtoRepository.GetItemsByCode
        Return SpecificDecoree.GetItemsByCode(itemCodeList, bankId, request)
    End Function
End Class