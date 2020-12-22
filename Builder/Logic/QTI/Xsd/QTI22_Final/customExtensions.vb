Imports System.Xml
Imports System.Xml.Serialization

Namespace QTI.Xsd.QTI22_Final


    Partial Public Class matchInteractionType

        Private anyField() As XmlNode

        <XmlText(),
    XmlAnyElement(Order:=1)>
        Public Property Any() As XmlNode()
            Get
                Return Me.anyField
            End Get
            Set(value As XmlNode())
                Me.anyField = value
            End Set
        End Property
    End Class

    Partial Public Class EqualType
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property toleranceMode() As EqualTypeToleranceMode
            Get
                Return Me._toleranceMode
            End Get
            Set(value As EqualTypeToleranceMode)
                Me._toleranceMode = value
            End Set
        End Property
    End Class


    Partial Class AssessmentItemRefType
        Inherits sectionGroup

        <XmlIgnore()>
        Public Property childIndex As Integer
    End Class

    Partial Public Class AssessmentSectionType
        Inherits sectionGroup


        Private testComponentsField As List(Of sectionGroup)
        <XmlElementAttribute(Type:=GetType(assessmentSectionType), ElementName:="assessmentSection", order:=8)> _
        <XmlElementAttribute(Type:=GetType(assessmentItemRefType), ElementName:="assessmentItemRef", order:=8)> _
        Public Property testComponents As List(Of sectionGroup)
            Get
                Return Me.testComponentsField
            End Get
            Set(value As List(Of sectionGroup))
                Me.testComponentsField = value
            End Set
        End Property

    End Class

    Public Class sectionGroup
    End Class

    Partial Public Class AssessmentTestType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqti_v2p2 ../controlxsds/imsqti_v2p2p1.xsd"
    End Class

    Partial Public Class AssessmentItemType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqti_v2p2 ../controlxsds/imsqti_v2p2p1.xsd"
    End Class

    Partial Public Class ItemBodyType
        <System.Xml.Serialization.XmlAnyElementAttribute()>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = value
            End Set
        End Property
    End Class

    Partial Public Class ManifestType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imscp_v1p1 controlxsds/imscp_v1p2.xsd http://www.imsglobal.org/xsd/imsqti_v2p2 controlxsds/imsqti_v2p2p1.xsd"
    End Class

    Partial Public Class TestPartType

        Private assessmentSectionField() As AssessmentSectionType
        <XmlElementAttribute(Order:=6)>
        Public Property assessmentSection() As AssessmentSectionType()
            Get
                Return Me.assessmentSectionField
            End Get
            Set(value As AssessmentSectionType())
                Me.assessmentSectionField = value
            End Set
        End Property

    End Class
End NameSpace