
Imports System.Xml.Serialization


<Serializable, XmlInclude(GetType(ItemReference2)), XmlInclude(GetType(TestSection2))>
Public MustInherit Class AssessmentTestNode
    Inherits TestNodeBase

    Private _lockedForEdit As Boolean



    Public Sub New()
        MyBase.New()
    End Sub



    Public MustOverride ReadOnly Property IsPickable As Boolean

    <XmlAttribute("LockedForEdit")> _
    Public Property LockedForEdit As Boolean
        Get
            Return _lockedForEdit
        End Get
        Set
            _lockedForEdit = value
        End Set
    End Property

    Public MustOverride ReadOnly Property MaxScore As Double


End Class