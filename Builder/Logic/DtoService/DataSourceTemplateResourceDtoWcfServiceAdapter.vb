
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class DataSourceTemplateResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of DataSourceResourceDto)
    Implements IDataSourceTemplateResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of DataSourceResourceDto, IDataSourceTemplateResourceDtoRepository)())
    End Sub

End Class
