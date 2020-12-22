Public MustInherit Class TestPackageComponentBase
    Inherits ValidatingEntityBase

    Private _nodeModel As TestPackageNode


    Public Property NodeModel As TestPackageNode
        Get
            Return _nodeModel
        End Get
        Friend Set
            _nodeModel = value
        End Set
    End Property

    Public Property Identifier As String
        Get
            Return Me.NodeModel.Identifier
        End Get
        Set
            Me.NodeModel.Identifier = value
            Me.Validate("Identifier")
        End Set
    End Property



    Public Property Title As String
        Get
            Return Me.NodeModel.Title
        End Get
        Set
            Me.NodeModel.Title = value
            Me.Validate("Title")
        End Set
    End Property

    Public ReadOnly Property SettingsCollection As SettingsCollection2
        Get
            Return Me.NodeModel.GetPropertyValue(Of SettingsCollection2)("settingsCollection")
        End Get
    End Property


    Protected Sub New(nodeModel As TestPackageNode)
        _nodeModel = nodeModel
    End Sub

    Protected Sub New()

    End Sub

End Class

