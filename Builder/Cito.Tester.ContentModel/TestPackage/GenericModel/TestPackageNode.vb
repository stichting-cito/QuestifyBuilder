
Imports System.Xml.Serialization


<Serializable> _
Public MustInherit Class TestPackageNode
    Inherits TestNodeBase

    Private _lockedOrder As Boolean

    <XmlAttribute("LockedForEdit")> _
    Public Property LockedOrder As Boolean
        Get
            Return _lockedOrder
        End Get
        Set
            _lockedOrder = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.LockedOrder = False
    End Sub

End Class
