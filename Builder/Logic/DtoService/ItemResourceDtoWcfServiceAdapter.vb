Imports Questify.Builder.Logic.Service.Decorators

Public Class ItemResourceDtoWcfServiceAdapter
    Inherits BaseItemResourceDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToItemResourceDtoServiceLazy())
    End Sub

End Class