Public Class CreatedTestNodeAndViews(Of TModel As AssessmentTestNode, TView As TestComponentBase)

    Private _testNode As TModel
    Private _views As List(Of TestComponentBase)

    Public ReadOnly Property TestNode As TModel
        Get
            Return _testNode
        End Get
    End Property

    Public ReadOnly Property Views As List(Of TestComponentBase)
        Get
            Return _views
        End Get
    End Property

    Public Sub New(testNode As TModel)
        _testNode = testNode
        _views = New List(Of TestComponentBase)
    End Sub

End Class
