Imports Cito.Tester.Common

Namespace Datasources

    Public MustInherit Class DataSource(Of T, C As DataSourceConfig)
        Implements IDataSource(Of T)



        Private _config As C
        Private _resourceManager As ResourceManagerBase



        Public Sub New(config As C)
            Me._config = config
            Me._resourceManager = ResourceManager
        End Sub

        Private Sub New()
        End Sub



        Public Overridable ReadOnly Property Config As C
            Get
                Return _config
            End Get
        End Property

        Public ReadOnly Property ResourceManager As ResourceManagerBase
            Get
                Return Me._resourceManager
            End Get
        End Property

        Public MustOverride ReadOnly Property ShowPreviewControl As Boolean Implements IDataSource(Of T).ShowPreviewControl



        Public Overridable Function IsReturnSetValidated() As Boolean Implements IDataSource(Of T).IsReturnSetValidated
            Return False
        End Function

        Public MustOverride Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of T) Implements IDataSource(Of T).Get

    End Class

End Namespace

