Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities


Public Class TestTemplateResourceDtoWcfServiceAdapter
    Inherits BaseResourceDtoServiceDecorator(Of AssessmentTestResourceDto)
    Implements ITestTemplateResourceDtoRepository

    Public Sub New()
        MyBase.New(New OpenChannelToResourceDtoServiceLazy(Of AssessmentTestResourceDto, ITestTemplateResourceDtoRepository)())
    End Sub

End Class
