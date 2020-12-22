Imports Questify.Builder.Logic.Service.Decorators

Public Class GenericResourceDtoWcfServiceAdapter
    Inherits BaseGenericResourceDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToGenericResourceDtoServiceLazy())
    End Sub

End Class
