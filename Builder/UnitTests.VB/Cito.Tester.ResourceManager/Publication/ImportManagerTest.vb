
Imports Cito.Tester.ResourceManager
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.IO
Imports FakeItEasy
Imports System.Collections.Generic
Imports Cito.Tester.Common
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ImportManagerTest

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")> <WorkItem(10118)>
    <DeploymentItem("Cito.Tester.ResourceManager\10118_boom.export")>
    Public Sub CountNrOfPutResources_MustBeEqualToNrOfResources()
        'Arrange
        Dim NrOfResources = -1
        Dim fakeDestResourceMngr = FakeResourceManager.MakeResourceManagerBase()
        Dim sourceResourceMngr = GetManifestResourceManager("10118_boom.export")
        NrOfResources = sourceResourceMngr.Manifest.Resources.Count 'The number of resources
        Dim fakeResourceManagerWrapper = A.Fake(Of IResourceManagerWrapper)()
        A.CallTo(Function() fakeResourceManagerWrapper.ResourceManager).ReturnsLazily(Function() fakeDestResourceMngr)
        Dim fakeResourceFactory = A.Fake(Of IResourceManagerFactory)()
        A.CallTo(Function() fakeResourceFactory.Create()).ReturnsLazily(Function() fakeResourceManagerWrapper)

        Dim imprt As New ImportManager(sourceResourceMngr, fakeResourceFactory)

        'Act
        imprt.ImportResources(sourceResourceMngr.Manifest.Resources, String.Empty)

        'Assert
        A.CallTo(Sub() fakeDestResourceMngr.PutResource(A(Of StreamResource).Ignored)).MustHaveHappened(Repeated.Exactly.Times(NrOfResources))
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")> <WorkItem(10118)>
    <DeploymentItem("Cito.Tester.ResourceManager\10118_boom.export")>
    Public Sub ResourcesShouldNotHaveDependentResourcesNotYetPut()
        'Arrange
        Dim _exported As New HashSet(Of String)
        Dim _missingDependency As New HashSet(Of String)

        Dim NrOfResources = -1
        Dim fakeDestResourceMngr = FakeResourceManager.MakeResourceManagerBase()
        Dim sourceResourceMngr = GetManifestResourceManager("10118_boom.export")
        Dim fakeResourceManagerWrapper = A.Fake(Of IResourceManagerWrapper)()
        A.CallTo(Function() fakeResourceManagerWrapper.ResourceManager).ReturnsLazily(Function() fakeDestResourceMngr)
        Dim fakeResourceFactory = A.Fake(Of IResourceManagerFactory)()
        A.CallTo(Function() fakeResourceFactory.Create()).ReturnsLazily(Function() fakeResourceManagerWrapper)
        NrOfResources = sourceResourceMngr.Manifest.Resources.Count 'The number of resources
        Dim imprt As New ImportManager(sourceResourceMngr, fakeResourceFactory)

        A.CallTo(Sub() fakeDestResourceMngr.PutResource(A(Of StreamResource).Ignored)).Invokes(Sub(arg)
                                                                                                   Dim r = arg.GetArgument(Of StreamResource)(0)
                                                                                                   Dim firstMissing = r.DependentResources.Where(Function(e) Not _exported.Contains(e.Name)).Select(Of String)(Function(e) e.Name).FirstOrDefault()

                                                                                                   If (Not String.IsNullOrEmpty(firstMissing)) Then _missingDependency.Add(firstMissing)
                                                                                                   _exported.Add(r.Name)
                                                                                               End Sub)

        'Act
        imprt.ImportResources(sourceResourceMngr.Manifest.Resources, String.Empty)

        'Assert
        Assert.AreEqual(0, _missingDependency.Count)
        Assert.AreEqual(NrOfResources, _exported.Count)
    End Sub



    Function GetManifestResourceManager(filename As String) As ManifestResourceManager
        Dim _file As String = Path.Combine(Environment.CurrentDirectory, filename) : Assert.IsTrue(File.Exists(_file), "Dependent file missing!")
        Dim manifest = ResourceManifest.PreLoad(New Uri(Path.Combine(_file, "Manifest.xml")))
        'NOTE!! that the uri is file://somePath/10118_boom.export/   with the extra  "/"
        Return New ManifestResourceManager(manifest, New Uri(_file + "/"), Guid.NewGuid.ToString())
    End Function

End Class
