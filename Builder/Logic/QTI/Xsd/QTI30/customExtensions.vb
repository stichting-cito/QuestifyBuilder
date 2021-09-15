﻿Imports System.Xml.Serialization

Namespace QTI.Xsd.QTI30

    Partial Public Class ItemBodyType
        <XmlAnyElement()>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property
    End Class

    Partial Public Class StimulusBodyType
        <XmlAnyElement()>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property
    End Class

    Partial Public Class AssessmentItemRefType

        <XmlIgnore()>
        Public Property childIndex As Integer
    End Class

    Partial Public Class AssessmentStimulusType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqtiasi_v3p0 https://purl.imsglobal.org/spec/qti/v3p0/schema/xsd/imsqti_stimulusv3p0_v1p0.xsd"
    End Class

    Partial Public Class AssessmentTestType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqtiasi_v3p0 ../controlxsds/imsqti_asiv3p0_v1p0.xsd"
    End Class

    Partial Public Class AssessmentItemType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqtiasi_v3p0 ../controlxsds/imsqti_asiv3p0_v1p0.xsd http://www.duo.nl/schema/dep_extension ../dep_extension.xsd"
    End Class

    Partial Public Class ManifestType
        <XmlAttribute("schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
        Public xsiSchemaLocation As String = "http://www.imsglobal.org/xsd/imsqtiasi_v3p0 controlxsds/imsqti_asiv3p0_v1p0.xsd http://ltsc.ieee.org/xsd/LOM https://purl.imsglobal.org/spec/md/v1p3/schema/xsd/imsmd_loose_v1p3p2.xsd
         http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1 controlxsds/imsqtiv3p0_imscpv1p2_v1p0.xsd http://www.imsglobal.org/xsd/imsqti_metadata_v3p0 controlxsds/imsqti_metadatav3p0_v1p0.xsd http://www.duo.nl/schema/dep_extension dep_extension.xsd"
    End Class

End Namespace