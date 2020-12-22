Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Schema
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30

Namespace QTI.Helpers.QTI30

    Public Class XsdHelper
        Public Overridable Sub InitialiseSettings(ByRef settings As XmlReaderSettings, paths As Dictionary(Of String, String), packageCreator As PackageCreator)
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
            settings.Schemas.Add("http://www.w3.org/1999/xhtml", XmlReader.Create(Path.Combine(tempXsdsPath, "xhtml\xhtml11.xsd")))
            settings.Schemas.Add("http://www.w3.org/2010/Math/MathML", XmlReader.Create(Path.Combine(tempXsdsPath, "mathml3\mathml3.xsd")))

            AddHandler settings.ValidationEventHandler, AddressOf packageCreator.ValidationEventHandler
        End Sub

        Protected Overridable Sub AddQTISchemasToValidate(ByRef settings As XmlReaderSettings, controlxsdsPath As String)
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imsqtiasi_v3p0", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqti_asiv3p0_v1p0.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqtiv3p0_imscpv1p2_v1p0.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqtiv3p0_csmv1p1_v1p0.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/imsqti_metadata_v3p0", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqti_metadatav3p0_v1p0.xsd")))
            settings.Schemas.Add("http://ltsc.ieee.org/xsd/LOM", XmlReader.Create(Path.Combine(controlxsdsPath, "imsmd_loose_v1p3p2.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_extensionv1p2", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqtiv3p0_cpextv1p2_v1p0.xsd")))
            settings.Schemas.Add("http://www.imsglobal.org/xsd/qti/qtiv3p0/imsafa3p0drd_v1p0", XmlReader.Create(Path.Combine(controlxsdsPath, "imsqtiv3p0_afa3p0drd_v1p0.xsd")))

            settings.Schemas.Add("http://www.w3.org/2001/10/synthesis", XmlReader.Create(Path.Combine(controlxsdsPath, "ssmlv1p1-core.xsd")))
        End Sub

    End Class
End Namespace