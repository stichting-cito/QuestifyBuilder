Imports System.Xml.Serialization


<Serializable> _
Public MustInherit Class TestPackageComponent
    Inherits TestPackageNode


    Private _parent As TestPackageNode




    Protected Sub New()
        MyBase.New()

    End Sub




    <XmlIgnore> _
    Public Property Parent As TestPackageNode
        Get
            Return _parent
        End Get
        Set
            _parent = value
        End Set
    End Property


End Class
