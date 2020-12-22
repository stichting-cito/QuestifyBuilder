

Imports System.Collections.Generic
Imports Cito.Tester.Common

Namespace Datasources

    Public Class ItemDataSource
        Inherits DataSource(Of ResourceRef, ItemDataSourceConfig)




        Public Sub New(config As ItemDataSourceConfig)
            MyBase.New(config)
        End Sub


        Public Overridable ReadOnly Property ItemCount As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides ReadOnly Property ShowPreviewControl As Boolean
            Get
                Return True
            End Get
        End Property



        Public Overrides Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of ResourceRef)
            Throw New NotImplementedException()
        End Function


    End Class

End Namespace

