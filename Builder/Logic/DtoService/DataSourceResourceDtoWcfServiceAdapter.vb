Imports Questify.Builder.Logic.Service.Decorators

Public Class DataSourceResourceDtoWcfServiceAdapter
    Inherits BaseDatasourceResourceDtoServiceDecorator

    Public Sub New()
        MyBase.New(New OpenChannelToDataSourceResourceDtoServiceLazy())
    End Sub

End Class
