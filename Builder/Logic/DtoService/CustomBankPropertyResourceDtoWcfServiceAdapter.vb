
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Public Class CustomBankPropertyResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of CustomBankPropertyResourceDto)
    Implements ICustomBankPropertyResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of CustomBankPropertyResourceDto, ICustomBankPropertyResourceDtoRepository)())
    End Sub

End Class
