Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToDataSourceResourceDtoServiceLazy
    Inherits OpenChannelToResourceDtoServiceLazy(Of DataSourceResourceDto, IDataSourceResourceDtoRepository)
    Implements IDataSourceResourceDtoRepository

    Public Function GetListWithFilter(ByVal bankId As Integer, ByVal isTemplate As Boolean?, ByVal ParamArray behaviours As String()) As IEnumerable(Of DataSourceResourceDto) Implements IDataSourceResourceDtoRepository.GetListWithFilter
        Return SpecificDecoree.GetListWithFilter(bankId, isTemplate, behaviours)
    End Function
End Class
