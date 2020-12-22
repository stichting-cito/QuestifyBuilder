
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ControlTemplateResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of ControlTemplateResourceDto)
    Implements IControlTemplateResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of ControlTemplateResourceDto, IControlTemplateResourceDtoRepository)())
    End Sub

End Class
