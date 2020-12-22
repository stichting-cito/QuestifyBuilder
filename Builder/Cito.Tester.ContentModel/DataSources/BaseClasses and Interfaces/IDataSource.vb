Imports Cito.Tester.Common

Namespace Datasources


    Public Interface IDataSource(Of T)
        Inherits IDataSource



        Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of T)

        Function IsReturnSetValidated() As Boolean

        ReadOnly Property ShowPreviewControl As Boolean



    End Interface

    Public Interface IDataSource

    End Interface

End Namespace

