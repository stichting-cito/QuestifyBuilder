
Imports Questify.Builder.UnitTests.Framework.Faketory.impl
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory
    Public Class FakeFactory

        Private Shared _assementMaker As IAssesmentTestMaker
#If Not CONTENTMODEL Then
        Private Shared _datasourceMaker As IDataSourceMaker
        Private Shared _fakeServices As IFakeServices
        Private Shared _entityMaker As IEntityMaker
        Private Shared _fakeDtoRepository As IFakeDtoRepository
#End If

        Public Shared ReadOnly Property AssesmentTest As IAssesmentTestMaker
            Get
                If _assementMaker Is Nothing Then
                    _assementMaker = New AssessmentTestMaker
                End If
                Return _assementMaker
            End Get
        End Property

#If Not CONTENTMODEL Then
        Public Shared ReadOnly Property Datasources As IDataSourceMaker
            Get
                If _datasourceMaker Is Nothing Then
                    _datasourceMaker = New DataSourceMaker
                End If
                Return _datasourceMaker
            End Get
        End Property

        Public Shared ReadOnly Property FakeServices As IFakeServices
            Get
                If _fakeServices Is Nothing Then
                    _fakeServices = New FakeServices
                End If
                Return _fakeServices
            End Get
        End Property

        Public Shared ReadOnly Property ItemEntities As IEntityMaker
            Get
                If _entityMaker Is Nothing Then
                    _entityMaker = New EntityMaker
                End If
                Return _entityMaker
            End Get
        End Property

        Public Shared ReadOnly Property FakeDtoRepository As IFakeDtoRepository
            Get
                If _fakeDtoRepository Is Nothing Then
                    _fakeDtoRepository = New FakeDtoRepository
                End If
                Return _fakeDtoRepository
            End Get
        End Property

#End If

    End Class
End NameSpace