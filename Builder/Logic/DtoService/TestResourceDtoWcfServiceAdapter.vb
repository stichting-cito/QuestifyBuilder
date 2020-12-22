Imports Questify.Builder.Logic.Service.Decorators

Public Class TestResourceDtoWcfServiceAdapter
    Inherits BaseTestResourceDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToTestResourceDtoServiceLazy())
    End Sub

End Class