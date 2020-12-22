
Imports Questify.Builder.Logic.Service.Decorators

Public Class ItemLayoutTemplateResourceDtoWcfServiceAdapter
    Inherits BaseItemLayoutTemplateDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToItemlayoutTemplateResourceDtoServiceLazy())
    End Sub

End Class
