
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cito.Tester.ResourceManager
Imports System.IO
Imports System.Diagnostics
Imports Cito.Tester.Common

<TestClass()>
Public Class ManifestResourceManagerTests
    
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

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    Public Sub TestExportMetaDataConversion()
        'Test conversion from "faked export from resource manager" to serialized data.

        'Arrange
        Dim strmData As New StreamResource(New MemoryStream(New Byte() {1}), 1) 'Fake resourceStream
        Dim strmObj As New StreamResource("Mijn Obj",
                                          2,
                                          "mijn Type",
                                          False,
                                          strmData.ResourceObject,
                                          New DependentResourceCollection(), 0)

        '--<Move To Factory>

        'This is just some fake data...

        'For MultiValues.
        Dim meta As New MetaDataMultiValue() With {.Name = "soort", .Title = "title",
                                                   .MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty,
                                                   .ApplicableTo = 2, .Publishable = True,
                                                   .MetaDataSubType = MetaDataMultiValue.enumMetaDataSubType.SingleSelect, .Code = New Guid("A6DBB1E1-B9D6-450D-B8A5-78B6DB988025")
                                                  }
        meta.ListValues.Add(New MetaDataCode() With {.Name = "1", .Title = "1", .Code = New Guid("E6DBB1E1-B9D6-450D-B8A5-78B6DB988025"), .IsSelected = True})
        meta.ListValues.Add(New MetaDataCode() With {.Name = "2", .Title = "2", .Code = New Guid("E7DBB1E1-B9D6-450D-B8A5-78B6DB988025")})

        strmObj.MetaData.Add(meta)
        '---
        'For Free Value.
        Dim meta2 As New MetaData() With {.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty,
                                              .Name = "Free", .Title = "Free",
                                              .ApplicableTo = 2, .Publishable = True}
        strmObj.MetaData.Add(meta2)

        '--</Move To Factory>

        Dim metacol As MetaDataCollection = New MetaDataCollection()
        metacol.Add(meta)
        metacol.Add(meta2)

        Dim rm As New ResourceManifest()
        Dim tmp As String = Path.Combine(Environment.CurrentDirectory, "tmp.export")
        Debug.WriteLine($"Path = {tmp}")
        Dim mrm As New ManifestResourceManager(rm, metacol, New Uri($"file:\\{tmp}"), "tst")


        'Act
        mrm.PutResource(strmObj)
        mrm.UpdateManifest() 'If you want to save to disk. I'm leaving it in place since the serialization is called here as well.
        'maybe in the future create ordered test/.

        'Assert
        Assert.AreEqual(2, mrm.ManifestMetaData.CustomPropertyDefinitions.Count)
        Assert.AreEqual(mrm.ManifestMetaData.CustomPropertyDefinitions(0).GetType(), GetType(ResourceManifestMetadataMultiValueDefinition))
        Assert.AreEqual(mrm.ManifestMetaData.CustomPropertyDefinitions(1).GetType(), GetType(ResourceManifestMetadataSingleValueDefinition))

        Dim _multi As ResourceManifestMetadataMultiValueDefinition = DirectCast(mrm.ManifestMetaData.CustomPropertyDefinitions(0), ResourceManifestMetadataMultiValueDefinition)
        Dim _single As ResourceManifestMetadataSingleValueDefinition = DirectCast(mrm.ManifestMetaData.CustomPropertyDefinitions(1), ResourceManifestMetadataSingleValueDefinition)

        'Assert MultiValue part.
        Assert.AreEqual(New Guid("a6dbb1e1-b9d6-450d-b8a5-78b6db988025"), _multi.Code)
        Assert.AreEqual(New Guid("E6DBB1E1-B9D6-450D-B8A5-78B6DB988025"), _multi.ListValues(0).Code)
        Assert.AreEqual("1", _multi.ListValues(0).Name)
        Assert.AreEqual(New Guid("E7DBB1E1-B9D6-450D-B8A5-78B6DB988025"), _multi.ListValues(1).Code)
        
    End Sub

End Class
