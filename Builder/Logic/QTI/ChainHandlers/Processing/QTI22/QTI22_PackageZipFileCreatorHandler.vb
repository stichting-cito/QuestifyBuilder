Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath
Imports Microsoft.Ajax.Utilities
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Requests.QTI22

Namespace QTI.ChainHandlers.Processing.QTI22

    Public Class QTI22_PackageZipFileCreatorHandler
        Inherits QTI22_ChainHandlerBase

        Public Sub New(packageCreator As QTI22PackageCreator)
            MyBase.New(packageCreator)
        End Sub

        Public Overrides Function ProcessRequest(requestData As QTI22PublicationRequest) As ChainHandlerResult
            Dim xsdFolder As String = Path.Combine(PackageCreator.TempWorkingDirectory.FullName, requestData.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.controlxsds))
            Directory.Delete(xsdFolder, True)

            For Each fileName As String In Directory.GetFiles(PackageCreator.TempWorkingDirectory.FullName, "*.xsd").Where(Function(f) f.EndsWith(".xsd"))
                File.Delete(fileName)
            Next

            Dim itemFolder As String = Path.Combine(PackageCreator.TempWorkingDirectory.FullName, requestData.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.items))
            For Each file In New DirectoryInfo(itemFolder).GetFiles
                UpdateNameSpacesAndSchemaLocations(file)
            Next

            Dim manifest As New FileInfo(Path.Combine(PackageCreator.TempWorkingDirectory.FullName, $"{PackageCreatorConstants.IMSMANIFEST}.xml"))
            UpdateNameSpacesAndSchemaLocations(manifest)

            ChainHandlerHelper.AddFilesToZip(requestData.TargetPackageFileSystemInfo.ToString, PackageCreator.TempWorkingDirectory.ToString)
            Return ChainHandlerResult.RequestHandled
        End Function

        Private Sub UpdateNameSpacesAndSchemaLocations(file As FileInfo)
            If file IsNot Nothing AndAlso file.Extension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase) Then
                Dim reader = New XmlTextReader(file.FullName)
                Dim doc = XDocument.Load(reader)

                Dim toRemove = doc.Root.Attributes().Where(Function(x) x.IsNamespaceDeclaration AndAlso x.Name.LocalName.Equals("xsd", StringComparison.InvariantCultureIgnoreCase)).ToList()
                toRemove.ForEach(Sub(a)
                                     doc.Root.Attributes(a.Name).Remove()
                                 End Sub)

                Dim xmlNamespaceManager As New XmlNamespaceManager(reader.NameTable)
                xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")
                Dim elements = doc.XPathSelectElements("//*[@xsi:schemaLocation]", xmlNamespaceManager)
                elements.ForEach(Sub(e)
                                     e.Attributes.First(Function(a) a.Name.LocalName.Equals("schemaLocation", StringComparison.InvariantCultureIgnoreCase)).Value = GetSchemaLocations(doc)
                                 End Sub)
                reader.Dispose()
                doc.Save(file.FullName, SaveOptions.DisableFormatting)
            End If
        End Sub

        Private Function GetSchemaLocations(doc As XDocument) As String
            Dim knownLocations As New Dictionary(Of String, String) From {
                    {"http://www.imsglobal.org/xsd/imscp_v1p1", "http://www.imsglobal.org/xsd/qti/qtiv2p2/qtiv2p2_imscpv1p2_v1p0.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqti_v2p2", "http://www.imsglobal.org/xsd/qti/qtiv2p2/imsqti_v2p2p2.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqti_metadata_v2p2", "http://www.imsglobal.org/xsd/qti/qtiv2p2/imsqti_metadata_v2p2.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqtiv2p2_html5_v1p0", "http://www.imsglobal.org/xsd/qti/qtiv2p2/imsqtiv2p2p2_html5_v1p0.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqti_result_v2p2", "http://www.imsglobal.org/xsd/qti/qtiv2p2/imsqti_result_v2p2.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2", "http://www.imsglobal.org/xsd/qti/qtiv2p2/imsqti_usagedata_v2p2.xsd"},
                    {"http://www.w3.org/1998/Math/MathML", "http://www.w3.org/Math/XMLSchema/mathml2/mathml2.xsd"}
            }

            Dim schemaLocations As New StringBuilder
            doc.Root.Attributes().Where(Function(x) x.IsNamespaceDeclaration AndAlso knownLocations.ContainsKey(x.Value)).ForEach(Sub(ns)
                                                                                                                                      schemaLocations.Append($" {ns.Value} {knownLocations(ns.Value)}")
                                                                                                                                  End Sub)
            Return LTrim(schemaLocations.ToString())
        End Function
    End Class
End Namespace