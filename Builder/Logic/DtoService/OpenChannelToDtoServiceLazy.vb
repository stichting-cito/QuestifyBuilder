Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Decorators

Friend Class OpenChannelToDtoServiceLazy(Of TResource As {Class}, TKey, TServiceToInstantiate As {IDtoRepository(Of TResource, TKey)})
    Inherits BaseDtoServiceDecorator(Of TResource, TKey)

    Private _instance As TServiceToInstantiate = Nothing

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public ReadOnly Property SpecificDecoree() As TServiceToInstantiate
        Get
            Return DirectCast(EnsureRepository(), TServiceToInstantiate)
        End Get
    End Property

    Public Overrides ReadOnly Property Decoree() As IDtoRepository(Of TResource, TKey)
        Get
            Return EnsureRepository()
        End Get
    End Property


    Private Function EnsureRepository() As TServiceToInstantiate
        If (_instance Is Nothing OrElse WcfServiceAdapterHelperClass.IsClientDisposed(_instance)) Then
            _instance = DtoServiceFactory.Instance.CreateService(Of TServiceToInstantiate)()
        End If
        Return _instance
    End Function
End Class
