Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources

Namespace TestPackageConstruction.Requests

    Public Class TestPackageConstructionRequest
        Inherits ChainHandlerRequest

        Private ReadOnly _testContext As IEnumerable(Of ResourceRef)
        Private ReadOnly _tests As New List(Of ResourceRef)


        Public Sub New(ByVal requestType As RequestTypeEnum, ByVal tests As IEnumerable(Of ResourceRef), ByVal testContext As IEnumerable(Of ResourceRef))
            _testContext = testContext
            _tests.AddRange(tests)
            Me.RequestType = requestType
        End Sub

        Public ReadOnly Property TestContext() As IEnumerable(Of ResourceRef)
            Get
                Return _testContext
            End Get
        End Property

        Public ReadOnly Property Tests() As IList(Of ResourceRef)
            Get
                Return _tests
            End Get
        End Property

        Public ReadOnly Property RequestType As RequestTypeEnum

        Public Enum RequestTypeEnum
            Add
            Remove
        End Enum

    End Class
End Namespace