Imports Cito.Tester.Common

Namespace Datasources

    Public Class ItemDataSourceManyOutput
        Inherits ItemDataSource
        Implements IDataSourceWithManyOutputSupport(Of ResourceRef)


        Public Sub New(config As ItemDataSourceConfig)
            MyBase.New(config)
        End Sub



        Public Overrides ReadOnly Property ShowPreviewControl As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property ClearSectionWhenProposing As Boolean
            Get
                Return True
            End Get
        End Property



        Public Overridable Function GetMany(resourceManager As ResourceManagerBase, numberOfRequests As Integer) As IList(Of IEnumerable(Of ResourceRef)) Implements IDataSourceWithManyOutputSupport(Of ResourceRef).GetMany
            Throw New NotImplementedException()
        End Function

        Public Overridable Function GetAllItemcodes(resourceManager As ResourceManagerBase) As IEnumerable(Of String) Implements IDataSourceWithManyOutputSupport(Of ResourceRef).GetAllItemcodes
            Throw New NotImplementedException()
        End Function

        Public Overrides Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of ResourceRef)
            Throw New NotImplementedException()
        End Function


    End Class

End Namespace

