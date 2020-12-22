Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports FakeItEasy
Imports Cito.Tester.ResourceManager
Imports System.IO
Imports System.Linq
Imports Cito.Tester.Common
Imports FakeItEasy.Core

<TestClass()>
Public Class ExportmanagerBasicTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")> <WorkItem(9923)>
    <Description("Afhankelijkheden komen niet mee in export bestand")>
    Public Sub Export_Resources_ValidateProgressReport()
        'Arrange

        'Sorry this test is a bit long
        ' Arrange export 10 resource a..j:
        ' resource      with dependent resources
        '   a               1..100
        '   b               1..100
        '   ..
        '   j               1..100
        Dim dependentResourcesColl = New ResourceEntryCollection()
        Dim resourcesColl = New ResourceEntryCollection()

        Dim root = MakeRoot("RootObj")
        Dim child As ResourceEntry = AddDep(root, "Root_A")
        Dim childchild As ResourceEntry = AddDep(child, "Root_A_1")
        resourcesColl.Add(root)

        Dim fakeSourceResourceMngr = A.Fake(Of ResourceManagerBase)()
        'Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()
        Dim manifestResourceManager = New ManifestResourceManager(New ResourceManifest, New Uri(GetTmpFile()), "")
        Dim exportMngr As New ExportManager(fakeSourceResourceMngr, GetTmpFile())

        'Call Redirects
        A.CallTo(Function() fakeSourceResourceMngr.GetResource(A(Of String).Ignored)).ReturnsLazily(Function(arg)
                                                                                                        Dim name = arg.GetArgument(Of String)(0)

                                                                                                        Select Case name
                                                                                                            Case "RootObj"
                                                                                                                Return ToSteamResource(root)
                                                                                                            Case "Root_A"
                                                                                                                Return ToSteamResource(child)
                                                                                                            Case "Root_A_1"
                                                                                                                Return ToSteamResource(childchild)
                                                                                                            Case Else

                                                                                                        End Select
                                                                                                        Assert.Fail()
                                                                                                        Throw New Exception("Assert.Fail should have stopped this.")
                                                                                                    End Function)
        A.CallTo(Function() fakeSourceResourceMngr.GetResourceEntry(A(Of String).Ignored)).ReturnsLazily(Function(arg) GetResourceEntry_WhenAskingForDependentResource(arg, dependentResourcesColl))

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, manifestResourceManager)

        'Assert
        Dim doc = XDocument.Parse(SerializeHelper.XmlSerializeToString(manifestResourceManager.Manifest))
        Dim ret = From n In doc.Descendants("DependentResource") Select n

        Assert.AreEqual(2, ret.Count())

    End Sub






    Private Function MakeRoot(name As String) As ResourceEntry
        Return New ResourceEntry(name)
    End Function

    Private Function AddDep(root As ResourceEntry, name As String) As ResourceEntry
        Dim ret = New ResourceEntry(name)
        root.DependentResources.Add(New DependentResource(ret.Name))
        Return ret
    End Function

    Private Function GetTmpFile() As String
        Return Path.Combine(Environment.CurrentDirectory, "tmp.export")
    End Function

    Public Function ToSteamResource(entity As ResourceEntry) As StreamResource
        Dim strmObj As New StreamResource(entity.Name,
                                   entity.Version,
                                   "someType",
                                   False,
                                   New MemoryStream(Encoding.UTF8.GetBytes("FAKE")),
                                   New DependentResourceCollection(), 0) 'No stream given, unit test only

        For Each d In entity.DependentResources
            strmObj.DependentResources.Add(d)
        Next

        Return strmObj
    End Function

    Function GetResourceEntry_WhenAskingForDependentResource(arg As IFakeObjectCall, ByRef dependentResource As ResourceEntryCollection) As ResourceEntry
        Dim name = arg.GetArgument(Of String)(0)
        Dim ret As New ResourceEntry(name) 'Just return some resourceEntry.
        dependentResource.Add(ret)
        Return ret
    End Function


End Class
