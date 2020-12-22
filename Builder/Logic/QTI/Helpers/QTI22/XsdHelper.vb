Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Schema
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22

Namespace QTI.Helpers.QTI22

    Public Class XsdHelper
        Public Overridable Sub InitialiseSettings(ByRef settings As XmlReaderSettings, paths As Dictionary(Of String, String), packageCreator As QTI22PackageCreator)
            Dim tempXsdsPath As String = paths("tempxsds")
            Dim controlXsdsPath As String = paths("controlxsds")

            If Not Directory.Exists(tempXsdsPath) Then
                Using memoryStream As New IO.MemoryStream(My.Resources.Schemas, 0, My.Resources.Schemas.Count)
                    memoryStream.Position = 0
                    Using streamReader As New StreamReader(memoryStream)
                        ChainHandlerHelper.ExtractZipToDirectory(streamReader, tempXsdsPath)
                    End Using
                End Using
            End If
            packageCreator.CopySchemaFiles(controlXsdsPath)
            settings.ValidationType = ValidationType.Schema

            settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ProcessInlineSchema
            settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ReportValidationWarnings
            settings.Schemas.XmlResolver = New PreventLoadingExternalXsdXmlResolver()

            AddQTISchemasToValidate(settings, controlXsdsPath)

            settings.Schemas.Add("http://www.w3.org/XML/1998/namespace", XmlReader.Create(Path.Combine(tempXsdsPath, "xml\xml.xsd")))
            settings.Schemas.Add("http://www.w3.org/2001/XInclude", XmlReader.Create(Path.Combine(tempXsdsPath, "xinclude\XInclude.xsd")))
            settings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(Path.Combine(tempXsdsPath, "mathml2\mathml2.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imslip_v1p0", XmlReader.Create(Path.Combine(tempXsdsPath, "imslip_v1p0\imslip_v1p0.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/apip/apipv1p0/imsapip_qtiv1p0", XmlReader.Create(Path.Combine(tempXsdsPath, "apip\apipv1p0\imsapip_qtiv1p0\apipv1p0_qtiextv2p1_v1p0.xsd")))
            settings.Schemas.Add("http://www.w3.org/1999/xhtml", XmlReader.Create(Path.Combine(tempXsdsPath, "xhtml\xhtml11.xsd")))
            settings.Schemas.Add("http://www.w3.org/2010/Math/MathML", XmlReader.Create(Path.Combine(tempXsdsPath, "mathml3\mathml3.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imsqtiv2p2_html5_v1p0", XmlReader.Create(Path.Combine(tempXsdsPath, "imsqtiv2p2p1_html5_v1p0.xsd")))
            settings.Schemas.Add("http://www.w3.org/2010/10/synthesis", XmlReader.Create(Path.Combine(tempXsdsPath, "ssmlv1p1-core.xsd")))

            AddHandler settings.ValidationEventHandler, AddressOf packageCreator.ValidationEventHandler
        End Sub

        Protected Overridable Sub AddQTISchemasToValidate(ByRef settings As XmlReaderSettings, controlxsdsPath As String)
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imsqti_v2p2", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqti_v2p2p1.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imscp_v1p1", XmlReader.Create(Path.Combine(controlxsdsPath, "imscp_v1p2.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imsqti_metadata_v2p2", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqti_metadata_v2p2.xsd")))
        End Sub

    End Class
End Namespace