Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Helpers.QTI30

    Public Class QTI30NamespaceHelper
        Inherits NamespaceHelper

        Public Overrides Function GetImsManifestXmlSerializerNamespaces() As XmlSerializerNamespaces
            Dim ns As New XmlSerializerNamespaces
            ns.Add("", "http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            Return ns
        End Function

        Public Overrides Function GetImsQtiNamespace() As XNamespace
            Dim imsmetadata As XNamespace = "http://www.imsglobal.org/xsd/imsqtiasi_v3p0"
            Return imsmetadata
        End Function

        Public Overrides Function GetImsMetadataNamespace() As XNamespace
            Dim imsmetadata As XNamespace = "http://www.imsglobal.org/xsd/imsqti_metadata_v3p0"
            Return imsmetadata
        End Function

        Public Overrides Function GetSSMLNamespace() As XNamespace
            Dim ssmlNamespace As XNamespace = "http://www.w3.org/2001/10/synthesis"
            Return ssmlNamespace
        End Function

    End Class

End Namespace