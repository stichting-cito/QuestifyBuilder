Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.QTI.Requests.QTI22
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

Namespace QTI.PreviewHandlers.QTI22

    Public MustInherit Class QTI22ItemPreviewHandlerBase
        Implements IStartItemPreview

        Protected _currentFileToValidate As String
        Protected _packageCreator As QTI22PackageCreator

        Public Sub New()
        End Sub

        Public MustOverride Function DoPreviewFromServer(target As String, assessmentItem As AssessmentItem, resourceManager As ResourceManagerBase, exportPath As String, url As String, publicationProperties As List(Of PublicationProperty)) As PublicationResult Implements IStartItemPreview.DoPreviewFromServer

        Protected Sub Validate(ByVal assessmentItem As AssessmentItem, ByRef warnings As String, ByRef errors As String) Implements IStartItemPreview.Validate
            Using fakeRequest = GetPublicationRequest()
                Dim xsdHelper = GetXsdHelper()
                Dim settings As New XmlReaderSettings
                Dim itemFileLocation As String = Path.Combine(Path.Combine(_packageCreator.TempWorkingDirectory.FullName, fakeRequest.GetFileDirectoryName(PackageCreatorConstants.FileDirectoryType.items)),
                                                              $"{assessmentItem.Identifier}.xml")
                Dim itemDepFileLocation As String = Path.Combine(Path.Combine(_packageCreator.TempWorkingDirectory.FullName, fakeRequest.GetFileDirectoryName(PackageCreatorConstants.FileDirectoryType.items)),
                                                                 $"{assessmentItem.Identifier}_extension.xml")
                xsdHelper.InitialiseSettings(settings, _packageCreator.GetXsdFolders(fakeRequest), _packageCreator)
                warnings = String.Join(vbNewLine, _packageCreator.Errors.Select(Function(e) e.Message))
                errors = String.Join(vbNewLine, _packageCreator.Warnings.Select(Function(w) w.Message))

                For Each fileName As String In New String() {itemFileLocation, itemDepFileLocation}
                    _currentFileToValidate = Path.GetFileName(fileName)

                    If File.Exists(fileName) Then
                        SyncLock fakeRequest.ValidationLock
                            Using reader As XmlReader = XmlReader.Create(fileName, settings)
                                While reader.Read()
                                End While
                            End Using
                        End SyncLock
                    End If
                Next
            End Using
        End Sub

        Protected Sub CleanUp() Implements IStartItemPreview.CleanUp
            TempStorageHelper.CleanTempStorage()
        End Sub

        Protected Overridable Function GetXsdHelper() As XsdHelper
            Return New XsdHelper
        End Function

        Protected MustOverride Function GetPackageCreator(ByVal configOfHandler As PluginHandlerConfigCollection) As QTI22PackageCreator

        Protected Overridable Function GetPublicationRequest() As QTI22PublicationRequest
            Return New QTI22PublicationRequest(Nothing, Nothing, Nothing)
        End Function

    End Class
End Namespace