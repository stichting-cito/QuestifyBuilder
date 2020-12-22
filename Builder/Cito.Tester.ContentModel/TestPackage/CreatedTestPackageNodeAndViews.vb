Public Class CreatedTestPackageNodeAndViews(Of TModel As TestPackageNode, TView As TestPackageComponentBase)

    Private _testNode As TModel
    Private _views As List(Of TestPackageComponentBase)

    Public ReadOnly Property TestNode As TModel
        Get
            Return _testNode
        End Get
    End Property

    Public ReadOnly Property Views As List(Of TestPackageComponentBase)
        Get
            Return _views
        End Get
    End Property

    Public Sub New(testNode As TModel)
        _testNode = testNode
        _views = New List(Of TestPackageComponentBase)
    End Sub

End Class
