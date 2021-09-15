﻿
Imports System.Collections.Concurrent
Imports FakeItEasy
Imports Questify.Builder.Configuration
Imports System.IO
Imports System.Text
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base

Namespace QTI22.Helpers
    <TestClass()>
    Public Class QTI22ResourceHelperTests


        <TestMethod(), TestCategory("FACET Package publish")>
        Public Sub Bug16923_TemplateWithReplacedReourceReferencesWasNotSavedInPackage()
            'arrange
            Dim folderDirectory As New ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
            folderDirectory.TryAdd(PackageCreatorConstants.FileDirectoryType.img, "img")
            folderDirectory.TryAdd(PackageCreatorConstants.FileDirectoryType.ref, "ref")

            Dim fakePackageCreator As New QTI22PackageCreator(New PluginHandlerConfigCollection())
            Dim fakeResourceMgr As ResourceManagerBase

            ' create a fake resource manager to return the template from the unit tests resources (ContentWithResources.xml)
            fakeResourceMgr = A.Fake(Of ResourceManagerBase)()
            A.CallTo(Function() fakeResourceMgr.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, A(Of ResourceRequestDTO).Ignored)).Returns(
                                                                             New BinaryResource(Encoding.UTF8.GetBytes(My.Resources.ContentWithResources_QTI22)))

            fakePackageCreator.ResourceMan = fakeResourceMgr
            Dim helper As New ResourceHelper(folderDirectory, New ConcurrentDictionary(Of QTI22PackageCreator.QTIManifestResourceType, String), fakePackageCreator)

            'act
            helper.ProcessResources("<html href=""resource://package:1/template.html""/>", New ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), New ConcurrentDictionary(Of String, String), True, "ITM-TEST")

            'assert
            'the generated file is written to a temp folder, we use some internal logic about the filename/location     
            Dim resultTemplate As String = File.ReadAllText(Path.Combine(fakePackageCreator.TempWorkingDirectory.FullName, "ref", "template.html"))
            Assert.IsTrue(resultTemplate.Contains("../img"))
            Assert.IsFalse(resultTemplate.Contains("resource://package"))

        End Sub

    End Class
End Namespace
