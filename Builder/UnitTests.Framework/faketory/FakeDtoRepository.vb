Imports FakeItEasy
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory

    Class FakeDtoRepository : Implements IFakeDtoRepository

        Public Property FakeGenericResourceDtoRepository() As IGenericResourceDtoRepository Implements IFakeDtoRepository.FakeGenericResourceDtoRepository

        Public Sub SetupFakeServices() Implements IFakeDtoRepository.SetupFakeServices

            FakeGenericResourceDtoRepository = A.Fake(Of IGenericResourceDtoRepository)()


            DtoFactory.Instantiate(Nothing, Nothing, Nothing, Nothing, Nothing, FakeGenericResourceDtoRepository, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End Sub

        Public Sub CleanFakeServices() Implements IFakeDtoRepository.CleanFakeServices
            DtoFactory.Destroy()
        End Sub

    End Class
End NameSpace