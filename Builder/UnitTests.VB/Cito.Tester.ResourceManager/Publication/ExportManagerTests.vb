
Imports Cito.Tester.ResourceManager
Imports FakeItEasy
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic
Imports Cito.Tester.Common
Imports FakeItEasy.Core
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ExportManagerTests

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub ExportNothing_Test()
        'Arrange
        Dim fakeResourceManager = A.Fake(Of ResourceManagerBase)()
        Dim exportMngr As New ExportManager(fakeResourceManager, GetTmpFile())
        'Act
        exportMngr.ExportResources(Nothing, Nothing, New ResourceEntryCollection, String.Empty)
        'Assert
        Assert.AreEqual(0, exportMngr.PublishedResources.Count)
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub ExportResource_memoryStreamWasClosed()
        'Arrange
        Dim resourcesColl = New ResourceEntryCollection() : resourcesColl.Add(New ResourceEntry("someName"))

        Dim fakeStream As MyStream = Nothing 'Not inited here
        Dim fakeSourceResourceMngr = A.Fake(Of ResourceManagerBase)()
        Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()

        A.CallTo(Function() fakeSourceResourceMngr.GetResource(A(Of String).Ignored)).ReturnsLazily(
            Function(arg)
                fakeStream = A.Fake(Of MyStream)()
                Dim entity = resourcesColl(0)
                Dim ret = New StreamResource(entity.Name,
                                  entity.Version,
                                  entity.Type,
                                  False,
                                  fakeStream,
                                  New DependentResourceCollection()) 'No stream given, unit test only
                Return ret
            End Function)

        Dim exportMngr As New ExportManager(fakeSourceResourceMngr, GetTmpFile())

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, fakeManifestResourceManager)

        'Assert
        Assert.IsNotNull(fakeStream)
        A.CallTo(Sub() fakeStream.Close()).MustHaveHappened()
        'A.CallTo(Sub() fakeStream.DisposedHasBeenCalled()).MustHaveHappened()
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub Export_1000Resources_PutResourceCalled1000Times()
        'Arrange
        Dim resourcesColl = New ResourceEntryCollection() 'Make 1000 objects
        For i As Integer = 1 To 1000 'Fill some Fake Data
            resourcesColl.Add(New ResourceEntry(i.ToString()))
        Next

        Dim fakeResourceManager = A.Fake(Of ResourceManagerBase)()
        Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()
        Dim exportMngr As New ExportManager(fakeResourceManager, GetTmpFile())

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, fakeManifestResourceManager)

        'Assert
        Assert.AreEqual(1000, resourcesColl.Count) 'The data send had 1000 elements.
        A.CallTo(Sub() fakeManifestResourceManager.PutResource(A(Of StreamResource).Ignored)).MustHaveHappened(Repeated.Exactly.Times(1000))
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub Export_1000ResourcesWith100UniqueNames_PutResourceCalled100Times()
        'Arrange
        Dim resourcesColl = New ResourceEntryCollection() 'Make 1000 objects

        For i As Integer = 1 To 1000 'Fill some Fake Data
            'BTW inheriting form List(of T) is a bad idea, you have to use shadow. Add has logic so it will not add a double resource twice,.. 
            'but since i'm upcasting it,.. i use the add of list(of T)
            Dim id As Integer = (i Mod 100) + 1
            DirectCast(resourcesColl, List(Of ResourceEntry)).Add(New ResourceEntry(id.ToString())) 'Make 100 unique names
        Next

        Dim fakeResourceManager = A.Fake(Of ResourceManagerBase)()
        Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()
        Dim exportMngr As New ExportManager(fakeResourceManager, GetTmpFile())

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, fakeManifestResourceManager)

        'Assert
        Assert.AreEqual(1000, resourcesColl.Count) 'The data send had 1000 elements.
        Assert.AreEqual(100, resourcesColl.Max(Function(r) CInt(r.Name)))
        A.CallTo(Sub() fakeManifestResourceManager.PutResource(A(Of StreamResource).Ignored)).MustHaveHappened(Repeated.Exactly.Times(100))
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub Export_1000ResourcesWith10ResourcesAnd100DependentResources_PutResourceCalled110Times()
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
        FillResourceCollWith_a_j_Resources(resourcesColl)

        Dim fakeSourceResourceMngr = A.Fake(Of ResourceManagerBase)()
        Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()
        Dim exportMngr As New ExportManager(fakeSourceResourceMngr, GetTmpFile())
        'Call Redirects
        A.CallTo(Function() fakeSourceResourceMngr.GetResource(A(Of String).Ignored)).ReturnsLazily(Function(arg) GetStreamForResource_a_z_1_100_Test(arg, resourcesColl, dependentResourcesColl))
        A.CallTo(Function() fakeSourceResourceMngr.GetResourceEntry(A(Of String).Ignored)).ReturnsLazily(Function(arg) GetResourceEntry_WhenAskingForDependentResource(arg, dependentResourcesColl))

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, fakeManifestResourceManager)
        'Assert
        Assert.AreEqual(10, resourcesColl.Count) 'the first 10 elements name start with letter
        Assert.AreEqual(100, dependentResourcesColl.Count) 'the first 10 elements name start with letter, other 100 with number
        Assert.AreEqual(110, exportMngr.PublishedResources.Count)
        A.CallTo(Sub() fakeManifestResourceManager.PutResource(A(Of StreamResource).Ignored)).MustHaveHappened(Repeated.Exactly.Times(110))
    End Sub



    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
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
        FillResourceCollWith_a_j_Resources(resourcesColl)

        Dim fakeSourceResourceMngr = A.Fake(Of ResourceManagerBase)()
        Dim fakeManifestResourceManager = A.Fake(Of ManifestResourceManager)()
        Dim exportMngr As New ExportManager(fakeSourceResourceMngr, GetTmpFile())

        'Call Redirects
        A.CallTo(Function() fakeSourceResourceMngr.GetResource(A(Of String).Ignored)).ReturnsLazily(Function(arg) GetStreamForResource_a_z_1_100_Test(arg, resourcesColl, dependentResourcesColl))
        A.CallTo(Function() fakeSourceResourceMngr.GetResourceEntry(A(Of String).Ignored)).ReturnsLazily(Function(arg) GetResourceEntry_WhenAskingForDependentResource(arg, dependentResourcesColl))

        '!!! ASSERTION DONE HERE !!
        Dim last As Integer = -1
        AddHandler exportMngr.ExportProgress, Sub(sen As Object, e As ProgressEventArgs)
                                                  Dim id() As Char = {"a"c, "b"c, "c"c, "d"c, "e"c, "f"c, "g"c, "h"c, "i"c, "j"c}
                                                  'Format of formatting string :::> Processing resource '{0}'...
                                                  Dim s As String = e.StatusMessage : Dim tmp As String = s.Substring(s.IndexOf("'"c) + 1)
                                                  If (Char.IsLetter(tmp(0))) Then 'Check first Char of processed Status Message
                                                      last = Array.IndexOf(id, tmp(0)) + 1
                                                      Assert.AreEqual(last, e.ProgessValue)
                                                  Else
                                                      'When not a letter then we are exporting dependentResources, these are not used to increase index.
                                                      Assert.AreEqual(last, e.ProgessValue)
                                                  End If
                                              End Sub

        'Act
        exportMngr.DoExportResources(Nothing, Nothing, resourcesColl, fakeManifestResourceManager)
        'Assert
        'ASSERTION IS DONE IN EVENT HANDLER!!
    End Sub

    Public Function ToSteamResource(entity As ResourceEntry) As StreamResource
        Dim strmObj As New StreamResource(entity.Name,
                                   entity.Version,
                                   entity.Type,
                                   False,
                                   Nothing,
                                   New DependentResourceCollection()) 'No stream given, unit test only

        Return strmObj
    End Function

    Private Sub Add100DependentResource(resource As StreamResource)
        For i As Integer = 1 To 100
            resource.DependentResources.Add(New DependentResource(i.ToString()))
        Next
    End Sub

    Private Function GetTmpFile() As String
        Return Path.Combine(Environment.CurrentDirectory, "tmp.export")
    End Function


    Function GetResourceEntry_WhenAskingForDependentResource(arg As IFakeObjectCall, ByRef dependentResource As ResourceEntryCollection) As ResourceEntry
        Dim name = arg.GetArgument(Of String)(0)
        Dim ret As New ResourceEntry(name) 'Just return some resourceEntry.
        dependentResource.Add(ret)
        Return ret
    End Function


    Function GetStreamForResource_a_z_1_100_Test(arg As IFakeObjectCall, resources_a_z As ResourceEntryCollection, resources_1_100 As ResourceEntryCollection) As StreamResource
        Dim name = arg.GetArgument(Of String)(0)
        If Char.IsLetter(name(0)) Then 'only add dependent resources if first char is a letter.
            Dim ret = ToSteamResource(resources_a_z.Item(name))
            'Add 100 DependentResources
            Add100DependentResource(ret)
            Return ret
        Else
            Return ToSteamResource(resources_1_100.Item(name)) 'Make Stream from a dependentResource 
        End If
    End Function

    Private Sub FillResourceCollWith_a_j_Resources(ByRef resourcesColl As ResourceEntryCollection)
        ' Count                 1   2   3     4    5    6   7     8    9    10 
        Dim id() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j"}
        For i As Integer = 0 To 9 'Fill some Fake Data
            resourcesColl.Add(New ResourceEntry(id(i)))
        Next
    End Sub




End Class
