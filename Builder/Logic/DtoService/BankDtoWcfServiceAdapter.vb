Imports Questify.Builder.Logic.Service.Decorators

Public Class BankDtoWcfServiceAdapter
    Inherits BaseBankDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToBankDtoServiceLazy())
    End Sub

End Class
