Imports Questify.Builder.Logic.Service.Interfaces

Namespace Faketory.interface

    Public Interface IFakeDtoRepository

        Property FakeGenericResourceDtoRepository() As IGenericResourceDtoRepository
        Sub SetupFakeServices()
        Sub CleanFakeServices()

    End Interface
End NameSpace