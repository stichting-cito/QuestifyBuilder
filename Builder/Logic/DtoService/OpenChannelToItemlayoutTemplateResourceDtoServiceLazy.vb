Imports Enums
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToItemlayoutTemplateResourceDtoServiceLazy
    Inherits OpenChannelToResourceDtoServiceLazy(Of ItemLayoutTemplateResourceDto, IItemlayoutTemplateResourceDtoRepository)
    Implements IItemlayoutTemplateResourceDtoRepository

    Public Function GetListWithItemTypeFilter(ByVal bankId As Integer, ByVal itemTypes As IEnumerable(Of ItemTypeEnum), ByVal excluded As Boolean) As IEnumerable(Of ItemLayoutTemplateResourceDto) Implements IItemlayoutTemplateResourceDtoRepository.GetListWithItemTypeFilter
        Return SpecificDecoree.GetListWithItemTypeFilter(bankId, itemTypes, excluded)
    End Function
End Class
