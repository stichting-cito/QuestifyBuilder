Imports Cito.Tester.ContentModel.Datasources

Public Class DataSourcePreviewExecutedEventArgs
    Inherits EventArgs

    Private _resultItemList As IEnumerable(Of ResourceRef)
    Private _dataSource As IDataSource
    Private _settings As DataSourceSettings

    Public ReadOnly Property ResultItemList() As IEnumerable(Of ResourceRef)
        Get
            Return _resultItemList
        End Get
    End Property

    Public ReadOnly Property DataSource() As IDataSource
        Get
            Return _dataSource
        End Get
    End Property

    Public ReadOnly Property Settings() As DataSourceSettings
        Get
            Return _settings
        End Get
    End Property

    Public Sub New(ByVal settings As DataSourceSettings, ByVal dataSource As IDataSource, ByVal resultItemList As IEnumerable(Of ResourceRef))
        _resultItemList = resultItemList
        _dataSource = dataSource
        _settings = settings
    End Sub


End Class
