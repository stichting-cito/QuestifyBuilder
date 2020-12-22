Imports Questify.Builder.Logic.Service.Decorators

Public Class CustomBankPropertyDtoWcfServiceAdapter
    Inherits BaseCustomPropertyDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToCustomBankPropertyDtoServiceLazy())
    End Sub

End Class
