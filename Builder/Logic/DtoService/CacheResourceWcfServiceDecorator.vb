Imports System.ServiceModel
Imports Questify.Builder.Logic.Service.Cache
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Interfaces

Public Class CacheResourceWcfServiceDecorator
    Inherits BaseCacheServiceDecorator

    Private _decoree As ICacheService
    Public Sub New()
        MyBase.New(New CacheDtoService())
    End Sub
    Public Overrides Sub FlushAllCachePermissionsForCurrentUser()
        Try
            If WcfServiceAdapterHelperClass.IsClientDisposed(_decoree) Then
                Dim factory As New ChannelFactory(Of ICacheService)("BasicHttpBinding_ICacheServiceDto")
                Dim wcfClientChannel As ICacheService = factory.CreateChannel()
                _decoree = wcfClientChannel
                Decoree = _decoree
            Else
                Decoree = _decoree
            End If
        Finally

        End Try
        MyBase.FlushAllCachePermissionsForCurrentUser()
    End Sub
End Class
