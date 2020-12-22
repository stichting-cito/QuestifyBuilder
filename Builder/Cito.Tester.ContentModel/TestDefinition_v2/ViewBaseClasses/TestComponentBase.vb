Imports System.Xml.Serialization

<XmlInclude(GetType(TestPart2))>
Public MustInherit Class TestComponentBase
    Inherits ValidatingEntityBase

    Private _nodeModel As AssessmentTestNode
    Private _parent As TestComponentBase

    Public Property NodeModel As AssessmentTestNode
        Get
            Return _nodeModel
        End Get
        Set
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

    Public ReadOnly Property IsPickable As Boolean
        Get
            Return Me.NodeModel.IsPickable
        End Get
    End Property

    Public Property LockedForEdit As Boolean
        Get
            Return Me.NodeModel.LockedForEdit
        End Get
        Set
            Me.NodeModel.LockedForEdit = value
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

    Public ReadOnly Property SettingsCollection2 As SettingsCollection2
        Get
            Return Me.NodeModel.GetPropertyValue(Of SettingsCollection2)("settingsCollection")
        End Get
    End Property

    Public Property State As ComponentState
        Get
            Return Me.NodeModel.State
        End Get
        Set
            Me.NodeModel.State = value
        End Set
    End Property

    Protected Sub New(nodeModel As AssessmentTestNode)
        _nodeModel = nodeModel
    End Sub

    Protected Sub New()

    End Sub

End Class
