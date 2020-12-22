Imports System.Xml.Serialization


<Serializable, XmlInclude(GetType(ItemReference2)), XmlInclude(GetType(TestSection2))> _
Public MustInherit Class TestComponent2
    Inherits AssessmentTestNode


    <NonSerialized>
    Private _parent As AssessmentTestNode




    Protected Sub New()
        MyBase.New()

    End Sub




    <XmlIgnore>
    Public Property Parent As AssessmentTestNode
        Get
            Return _parent
        End Get
        Set
            _parent = value
        End Set
    End Property


End Class
