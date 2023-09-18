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
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class PackageZipFileCreatorHandler
        Inherits ChainHandlerBase

        Protected knownLocations As New Dictionary(Of String, String) From {
                    {"http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1", "https://purl.imsglobal.org/spec/qti/v3p0/schema/xsd/imsqtiv3p0_imscpv1p2_v1p0.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqtiasi_v3p0", "https://purl.imsglobal.org/spec/qti/v3p0/schema/xsd/imsqti_asiv3p0_v1p0.xsd"},
                    {"http://www.imsglobal.org/xsd/imsqti_metadata_v3p0", "https://purl.imsglobal.org/spec/qti/v3p0/schema/xsd/imsqti_metadatav3p0_v1p0.xsd"},
                    {"http://ltsc.ieee.org/xsd/LOM", "https://purl.imsglobal.org/spec/md/v1p3/schema/xsd/imsmd_loose_v1p3p2.xsd"},
                    {"http://www.w3.org/1998/Math/MathML", "https://purl.imsglobal.org/spec/mathml/v3p0/schema/xsd/mathml3.xsd"},
                    {"http://www.w3.org/2001/10/synthesis", "https://purl.imsglobal.org/spec/ssml/v1p1/schema/xsd/ssmlv1p1-core.xsd"},
                    {"http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_extensionv1p2", "https://purl.imsglobal.org/spec/qti/v3p0/schema/xsd/imsqtiv3p0_cpextv1p2_v1p0.xsd"}
            }

        Public Sub New(packageCreator As PackageCreator)
            MyBase.New(packageCreator)
        End Sub

        Public Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            Dim xsdFolder As String = GetPathToFolder(requestData, PackageCreatorConstants.FileDirectoryType.controlxsds)
            Directory.Delete(xsdFolder, True)

            Dim itemFolder As String = GetPathToFolder(requestData, PackageCreatorConstants.FileDirectoryType.items)
            UpdateNameSpacesAndSchemaLocations(itemFolder)

            Dim manifest As New FileInfo(Path.Combine(PackageCreator.TempWorkingDirectory.FullName, $"{PackageCreatorConstants.IMSMANIFEST}.xml"))
            UpdateNameSpacesAndSchemaLocations(manifest)

            ChainHandlerHelper.AddFilesToZip(requestData.TargetPackageFileSystemInfo.ToString, PackageCreator.TempWorkingDirectory.ToString)
            Return ChainHandlerResult.RequestHandled
        End Function

        Private Function GetPathToFolder(requestData As PublicationRequest, fileDirectoryType As PackageCreatorConstants.FileDirectoryType) As String
            Return Path.Combine(PackageCreator.TempWorkingDirectory.FullName, requestData.FileTypeDictionary(fileDirectoryType))
        End Function

        Private Sub UpdateNameSpacesAndSchemaLocations(path As String)
            For Each file In New DirectoryInfo(path).GetFiles
                UpdateNameSpacesAndSchemaLocations(file)
            Next
        End Sub

        Private Sub UpdateNameSpacesAndSchemaLocations(file As FileInfo)
            If file Is Nothing OrElse Not file.Extension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase) Then
                Return
            End If

            Dim reader = New XmlTextReader(file.FullName)
            Dim doc = XDocument.Load(reader)

            Dim toRemove = doc.Root.Attributes().Where(Function(x) x.IsNamespaceDeclaration AndAlso x.Name.LocalName.Equals("xsd", StringComparison.InvariantCultureIgnoreCase)).ToList()
            toRemove.ForEach(Sub(a)
                                 doc.Root.Attributes(a.Name).Remove()
                             End Sub)

            Dim xmlNamespaceManager As New XmlNamespaceManager(reader.NameTable)
            xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            Dim elements = doc.XPathSelectElements("//*[@xsi:schemaLocation]", xmlNamespaceManager)
            If elements.Any() Then
                elements.ForEach(Sub(e)
                                     e.Attributes.First(Function(a) a.Name.LocalName.Equals("schemaLocation", StringComparison.InvariantCultureIgnoreCase)).Value = GetSchemaLocations(doc)
                                 End Sub)
            Else
                Dim xsiNamespace As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
                Dim schemaLocations = GetSchemaLocations(doc)
                Dim schemaLocationAttribute = New XAttribute(xsiNamespace + "schemaLocation", schemaLocations)
                doc.Root.Add(schemaLocationAttribute)
            End If

            reader.Dispose()
            doc.Save(file.FullName, SaveOptions.DisableFormatting)
        End Sub

        Protected Overridable Function GetSchemaLocations(doc As XDocument) As String
            Dim schemaLocations As New StringBuilder
            doc.Root.Attributes().
                Where(Function(x) x.IsNamespaceDeclaration AndAlso knownLocations.ContainsKey(x.Value)).
                ForEach(Sub(ns)
                            schemaLocations.Append($" {ns.Value} {knownLocations(ns.Value)}")
                        End Sub)
            Return LTrim(schemaLocations.ToString())
        End Function
    End Class
End Namespace