
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class AspectResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of AspectResourceDto)
    Implements IAspectResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of AspectResourceDto, IAspectResourceDtoRepository)())
    End Sub

End Class
