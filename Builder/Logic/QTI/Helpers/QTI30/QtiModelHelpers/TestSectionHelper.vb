Imports System.Collections.Concurrent
Imports System.IO
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.Helpers.QTI30.QtiModelHelpers

    Public Class TestSectionHelper

        Protected _testSection As GeneralTestSection

        Public Sub New(testSection As GeneralTestSection)
            _testSection = testSection
        End Sub

        Public Function CreateSection() As AssessmentSectionType
            Dim sectionType As New AssessmentSectionType
            sectionType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_testSection.Identifier, PackageCreatorConstants.TypeOfResource.resource)
            sectionType.title = _testSection.Title
            If TypeOf _testSection Is QTITestSectionBase Then
                Dim qtiSection = DirectCast(_testSection, QTITestSectionBase)
                sectionType.visible = qtiSection.Visible
                sectionType.keeptogether = qtiSection.KeepTogether
                If qtiSection.SectionPart IsNot Nothing Then
                    If TypeOf _testSection Is QTITestSectionBase Then
                        Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(qtiSection.SectionPart.TimeLimits)
                        If timeLimits IsNot Nothing Then
                            sectionType.qtitimelimits = timeLimits
                        End If
                    End If
                End If
            End If

            Return sectionType
        End Function

        Friend Sub AddAdaptiveModuleToSection(sectionType As AssessmentSectionType,
                                              resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                              filesPerEntity As ConcurrentDictionary(Of String, List(Of String)),
                                              testName As String,
                                              tempFolder As String,
                                              adaptiveDirectoryName As String,
                                              resourceTypeDictionary As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType),
                                              packageCreator As PackageCreator)
            If TypeOf _testSection IsNot QTITestSectionBase Then
                Return
            End If

            Dim qtiSection = DirectCast(_testSection, QTITestSectionBase)
            If String.IsNullOrEmpty(qtiSection.DriverHref) AndAlso String.IsNullOrEmpty(qtiSection.ModuleHref) Then
                Return
            End If

            Dim adaptiveSelection As New AdaptiveSelectionType

            Dim modulehRef = GetAdaptiveFile(qtiSection.ModuleHref, resources, filesPerEntity, testName, tempFolder, adaptiveDirectoryName, resourceTypeDictionary(PackageCreator.QTIManifestResourceType.adaptive_module), packageCreator)
            Dim driverhRef = GetAdaptiveFile(qtiSection.DriverHref, resources, filesPerEntity, testName, tempFolder, adaptiveDirectoryName, resourceTypeDictionary(PackageCreator.QTIManifestResourceType.adaptive_driver), packageCreator)

            adaptiveSelection.qtiadaptivesettingsref = New AdaptiveHrefType() With {.identifier = "driver", .href = $"../{driverhRef}"}
            adaptiveSelection.qtiadaptiveengineref = New AdaptiveHrefType() With {.identifier = "module", .href = $"../{modulehRef}"}

            Dim sectionItemsList As List(Of Object)
            If sectionType.Items IsNot Nothing AndAlso Not sectionType.Items.Count = 0 Then
                sectionItemsList = sectionType.Items.ToList()
            Else
                sectionItemsList = New List(Of Object)
            End If
            sectionItemsList.Add(adaptiveSelection)

            sectionType.Items = sectionItemsList.ToArray()
        End Sub

        Public Sub AddTestSectionToAssessmentTestType(ByRef assessmentTestType As AssessmentTestType, testSectionPart As AssessmentSectionType, parentId As String)
            Dim id As String = ChainHandlerHelper.GetIdentifierFromResourceId(parentId, PackageCreatorConstants.TypeOfResource.resource)
            If assessmentTestType.qtitestpart IsNot Nothing AndAlso Not assessmentTestType.qtitestpart.Count = 0 Then
                Dim referencedTestParts = From component In assessmentTestType.qtitestpart
                                          Where component.identifier = id
                If referencedTestParts.Count = 1 Then
                    Dim testPartRef As TestPartType = referencedTestParts(0)
                    If testPartRef.Items IsNot Nothing AndAlso testPartRef.Items.Count > 0 Then
                        Dim sectionList As List(Of Object) = testPartRef.Items.Where(Function(o) TypeOf o Is AssessmentSectionType).ToList()
                        sectionList.Add(testSectionPart)
                        testPartRef.Items = sectionList.ToArray()
                    Else
                        testPartRef.Items = {testSectionPart}
                    End If
                Else
                    Dim referencedTestSection As AssessmentSectionType = Nothing
                    assessmentTestType.qtitestpart.ToList.ForEach(Sub(tp)
                                                                      If referencedTestSection Is Nothing AndAlso tp.Items IsNot Nothing Then
                                                                          referencedTestSection = FindParentSectionById(tp.Items.OfType(Of AssessmentSectionType).ToArray(), id)
                                                                      End If
                                                                  End Sub)
                    If referencedTestSection IsNot Nothing Then
                        Dim sectionList As List(Of Object)
                        If referencedTestSection.Items1 IsNot Nothing AndAlso Not referencedTestSection.Items1.Count = 0 Then
                            sectionList = referencedTestSection.Items1.ToList()
                        Else
                            sectionList = New List(Of Object)
                        End If
                        sectionList.Add(testSectionPart)
                        referencedTestSection.Items1 = sectionList.ToArray()
                    Else
                        Debug.Assert(True, $"Parent: '{parentId}' for TestSection: '{testSectionPart.identifier}' cannot be found")
                    End If
                End If

            Else
                Throw New Exception("Cannot add a section while no testparts are added")
            End If
        End Sub

        Private Function GetAdaptiveFile(resourceName As String,
                                                    resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                                    ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)),
                                                    testName As String,
                                                    tempFolder As String,
                                                    adaptiveDirectoryName As String,
                                                    adaptiveResourceType As ResourceTypeType,
                                                    packageCreator As PackageCreator) As String
            Dim returnValue As String = String.Empty

            Dim eventArgs As New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
            packageCreator.ResourceNeeded(Me, eventArgs)

            If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                If Not Directory.Exists(Path.Combine(tempFolder, adaptiveDirectoryName)) Then
                    Directory.CreateDirectory(Path.Combine(tempFolder, adaptiveDirectoryName))
                End If

                returnValue = String.Concat(adaptiveDirectoryName, "/", resourceName)

                Dim resourcePath As String = Path.Combine(tempFolder, returnValue)
                ChainHandlerHelper.SaveFile(CType(eventArgs.BinaryResource.ResourceObject, Byte()), resourcePath)

                Dim mimeType As String = ChainHandlerHelper.GetMimeTypeFromFile(CType(eventArgs.BinaryResource.ResourceObject, Byte()), resourceName)
                mimeType = ChainHandlerHelper.ConvertMimeType(mimeType, resourceName)

                Dim fileList As New List(Of FileType)
                Dim newFile As New FileType
                newFile.href = returnValue
                fileList.Add(newFile)

                Dim resourceType As New ResourceType
                resourceType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource)
                resourceType.type = adaptiveResourceType
                resourceType.href = returnValue

                PackageCreator.AddResourceToManifest(resources, resourceType, fileList.ToArray)
                PackageCreator.AddDependentResourceToManifest(resources, ChainHandlerHelper.GetIdentifierFromResourceId(testName, PackageCreatorConstants.TypeOfResource.test), ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource))

                If filesPerEntity.ContainsKey(testName) Then
                    Dim listOfFiles As List(Of String) = filesPerEntity(testName)
                    listOfFiles.Add(resourceName)
                End If
            End If

            Return returnValue
        End Function

        Private Function FindParentSectionById(assessmentSectionTypes As AssessmentSectionType(), id As String) As AssessmentSectionType
            Dim referencedTestSection As AssessmentSectionType = Nothing
            assessmentSectionTypes.ToList.ForEach(Sub(st)
                                                      If referencedTestSection Is Nothing Then
                                                          referencedTestSection = FindParentSectionById(st, id)
                                                      End If
                                                  End Sub)
            Return referencedTestSection
        End Function

        Private Function FindParentSectionById(assessmentSectionType As AssessmentSectionType, id As String) As AssessmentSectionType
            Dim referencedTestSection As AssessmentSectionType = Nothing
            If assessmentSectionType.identifier.Equals(id, StringComparison.InvariantCultureIgnoreCase) Then Return assessmentSectionType
            If assessmentSectionType.Items1 IsNot Nothing Then
                assessmentSectionType.Items1.ToList.ForEach(Sub(tc)
                                                                If referencedTestSection Is Nothing Then
                                                                    If TypeOf tc Is AssessmentSectionType Then referencedTestSection = FindParentSectionById(DirectCast(tc, AssessmentSectionType), id)
                                                                End If
                                                            End Sub)
            End If
            Return referencedTestSection
        End Function

    End Class
End Namespace