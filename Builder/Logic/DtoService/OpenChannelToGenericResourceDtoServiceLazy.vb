Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToGenericResourceDtoServiceLazy
    Inherits OpenChannelToResourceDtoServiceLazy(Of GenericResourceDto, IGenericResourceDtoRepository)
    Implements IGenericResourceDtoRepository

    Public Function GetListWithFilter(ByVal bankId As Integer, ByVal filter As String, ByVal filePrefix As String, ByVal templatesOnly As Boolean) As IEnumerable(Of GenericResourceDto) Implements IGenericResourceDtoRepository.GetListWithFilter
        Return SpecificDecoree.GetListWithFilter(bankId, filter, filePrefix, templatesOnly)
    End Function
End Class
