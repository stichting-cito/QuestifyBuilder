Imports FakeItEasy
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace FakeAppTemplate

    Public Class FakeGenericResourceDtoRepositoryHandler : Inherits FakeResourceDtoRepositoryHandler(Of GenericResourceDto)

        Private ReadOnly _fakeGenericResourceDtoRepository As IGenericResourceDtoRepository

        Public Sub New(ByVal fakeGenericResourceDtoRepository As IGenericResourceDtoRepository)
            MyBase.New(fakeGenericResourceDtoRepository)
            _fakeGenericResourceDtoRepository = fakeGenericResourceDtoRepository
        End Sub

        Public Overrides Sub InitFakeItEasyHooks()
            A.CallTo(Function() _fakeGenericResourceDtoRepository.GetListWithFilter(A(Of Integer).Ignored,
                                                                                    A(Of String).Ignored,
                                                                                    A(Of String).Ignored,
                                                                                    A(Of Boolean).Ignored
                                                                                    )
                     ).ReturnsLazily(Function(args) GetListWithFilter(args))
        End Sub

        Private Function GetListWithFilter(args As Core.IFakeObjectCall) As IEnumerable(Of GenericResourceDto)

            Dim filter = args.Arguments.Get(Of String)(1)
            Dim filePrefix = args.Arguments.Get(Of String)(2)
            Dim templatesOnly = args.Arguments.Get(Of Boolean)(3)
            Dim collection = FakeDal.FakeServices.FakeResourceService.GetGenericResourceForBank(Nothing, filter, filePrefix, templatesOnly)

            Return Mapper.GetMany(Of GenericResourceDto)(collection)

        End Function

    End Class
End NameSpace