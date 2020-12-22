
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class TestPackageResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of TestPackageResourceDto)
    Implements ITestPackageResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of TestPackageResourceDto, ITestPackageResourceDtoRepository)())
    End Sub

End Class
