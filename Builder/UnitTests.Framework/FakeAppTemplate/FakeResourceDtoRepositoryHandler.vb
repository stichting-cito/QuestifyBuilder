Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace FakeAppTemplate

    Public Class FakeResourceDtoRepositoryHandler(Of TEntity As {ResourceDto}) : Inherits FakeDtoRepositoryHandler(Of TEntity, Guid)

        Private ReadOnly _fake As IResourceDtoRepository(Of GenericResourceDto)

        Public Sub New(fake As IResourceDtoRepository(Of GenericResourceDto))
            MyBase.New(CType(fake, IDtoRepository(Of TEntity, Global.System.Guid)))
            _fake = fake
        End Sub

    End Class
End NameSpace