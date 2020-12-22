Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace FakeAppTemplate

    Public Class FakeDtoRepositoryHandler(Of TEntity As {ResourceDto}, TKey)

        Private ReadOnly _fake As IDtoRepository(Of TEntity, TKey)

        Public Sub New(ByVal fake As IDtoRepository(Of TEntity, TKey))
            _fake = fake
        End Sub

        Public Overridable Sub InitFakeItEasyHooks()
        End Sub

    End Class
End NameSpace