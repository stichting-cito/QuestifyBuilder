Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Decorators
Imports Questify.Builder.Logic.Service.Model.Entities

Friend Class OpenChannelToResourceDtoServiceLazy(Of TResource As ResourceDto, TServiceToInstantiate As {IDtoRepository(Of TResource, Guid)})
    Inherits BaseResourceDtoServiceDecorator(Of TResource)

    Private _instance As TServiceToInstantiate = Nothing

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public ReadOnly Property SpecificDecoree() As TServiceToInstantiate
        Get
            Return EnsureRepository()
        End Get
    End Property

    Public Overrides ReadOnly Property Decoree() As IDtoRepository(Of TResource, Guid)
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
