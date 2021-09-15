
Imports System.Text
Imports System.IO
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework


'''<summary>
'''This is a test class for Cito.Tester.Common.SerializeHelper and is intended
'''to contain all Cito.Tester.Common.SerializeHelper Unit Tests
'''</summary>
<TestClass()> _
Public Class SerializeHelperTest

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

    '''<summary>
    '''A test for XmlDeserializeFromByteArray(ByVal Byte(), ByVal System.Type)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromByteArrayTest()
        Dim testModel As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        'Create MD5 Hash to check original and actual
        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(testModel)

        'Serialize to byte array
        Dim data() As Byte = SerializeHelper.XmlSerializeToByteArray(testModel)
        Assert.IsNotNull(data, "Object is not serialized!")

        'Deserialize
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(data, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        'Check hashes
        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    '''<summary>
    '''A test for XmlDeserializeFromFile(ByVal String, ByVal System.Type)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromFileTest()
        Dim testModel As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        'Create MD5 Hash to check original and actual
        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(testModel)

        'Serialize to byte array
        Dim serializeFileName As String = "SerializeTest.xml"
        SerializeHelper.XmlSerializeToFile(serializeFileName, testModel)
        Dim serializeFileInfo As New FileInfo(serializeFileName)
        Assert.IsTrue(serializeFileInfo.Exists, "Object is not serialized!")

        'Deserialize
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromFile(serializeFileName, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        'Check hashes
        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_MD5Check()
        'Arrange
        Dim aspect1 As New Aspect() With {.MaxScore = 4}
        Dim aspect2 As New Aspect() With {.MaxScore = 6}

        'Act
        Dim md51 As Byte() = SerializeHelper.GetMD5Hash(aspect1)
        Dim md52 As Byte() = SerializeHelper.GetMD5Hash(aspect2)

        'Assert
        Assert.IsFalse(ArrayHelper.CompareByteArray(md51, md52), "hashes should differ!")
    End Sub

    '''<summary>
    '''A test for XmlDeserializeFromReader(ByVal System.IO.TextReader, ByVal System.Type)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromReaderTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        'Act
        'Create MD5 Hash to check original and actual
        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)
        Dim sb As New StringBuilder()
        Dim writer As New StringWriter(sb)

        'Serialize to writer
        SerializeHelper.XmlSerializeToWriter(writer, original)
        Assert.IsTrue(sb.Length > 0, "Object is not serialized!")
        writer.Dispose()

        'Deserialize
        Dim reader As New StringReader(sb.ToString())
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        'Check hashes
        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    '''<summary>
    '''A test for XmlDeserializeFromStream(ByVal System.IO.Stream, ByVal System.Type)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromStreamTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        'Create MD5 Hash to check original and actual
        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)


        'Serialize to stream
        Dim memory As New MemoryStream()
        SerializeHelper.XmlSerializeToStream(memory, original)
        Assert.IsTrue(memory.Length > 0, "Object is not serialized!")

        'Deserialize
        memory.Seek(0, SeekOrigin.Begin)
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromStream(memory, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        'Check hashes
        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    '''<summary>
    '''A test for XmlDeserializeFromString(ByVal String, ByVal System.Type)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromStringTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        'Create MD5 Hash to check original and actual
        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)

        'Serialize to writer
        Dim output As String = SerializeHelper.XmlSerializeToString(original)
        Assert.IsTrue(output.Length > 0, "Object is not serialized!")

        'Deserialize
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromString(output, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        'Check hashes
        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

End Class
