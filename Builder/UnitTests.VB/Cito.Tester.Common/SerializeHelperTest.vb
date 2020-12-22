
Imports System.Text
Imports System.IO
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework


<TestClass()> _
Public Class SerializeHelperTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromByteArrayTest()
        Dim testModel As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(testModel)

        Dim data() As Byte = SerializeHelper.XmlSerializeToByteArray(testModel)
        Assert.IsNotNull(data, "Object is not serialized!")

        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(data, GetType(AssessmentTest2)), _
    AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromFileTest()
        Dim testModel As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(testModel)

        Dim serializeFileName As String = "SerializeTest.xml"
        SerializeHelper.XmlSerializeToFile(serializeFileName, testModel)
        Dim serializeFileInfo As New FileInfo(serializeFileName)
        Assert.IsTrue(serializeFileInfo.Exists, "Object is not serialized!")

        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromFile(serializeFileName, GetType(AssessmentTest2)), _
    AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_MD5Check()
        Dim aspect1 As New Aspect() With {.MaxScore = 4}
        Dim aspect2 As New Aspect() With {.MaxScore = 6}

        Dim md51 As Byte() = SerializeHelper.GetMD5Hash(aspect1)
        Dim md52 As Byte() = SerializeHelper.GetMD5Hash(aspect2)

        Assert.IsFalse(ArrayHelper.CompareByteArray(md51, md52), "hashes should differ!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromReaderTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)
        Dim sb As New StringBuilder()
        Dim writer As New StringWriter(sb)

        SerializeHelper.XmlSerializeToWriter(writer, original)
        Assert.IsTrue(sb.Length > 0, "Object is not serialized!")
        writer.Dispose()

        Dim reader As New StringReader(sb.ToString())
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromStreamTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)


        Dim memory As New MemoryStream()
        SerializeHelper.XmlSerializeToStream(memory, original)
        Assert.IsTrue(memory.Length > 0, "Object is not serialized!")

        memory.Seek(0, SeekOrigin.Begin)
        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromStream(memory, GetType(AssessmentTest2)), _
            AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

    <TestMethod()> _
    Public Sub SerializeHelper_XmlSerializeAndDeserializeFromStringTest()
        Dim original As AssessmentTest2 = CTContentSampleDataHelper.GetAssessmentTestModel()

        Dim originalHash() As Byte = SerializeHelper.GetMD5Hash(original)

        Dim output As String = SerializeHelper.XmlSerializeToString(original)
        Assert.IsTrue(output.Length > 0, "Object is not serialized!")

        Dim actual As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromString(output, GetType(AssessmentTest2)), _
    AssessmentTest2)
        Assert.IsNotNull(actual, "Object is not deserialized!")

        Dim actualHash() As Byte = SerializeHelper.GetMD5Hash(actual)
        Assert.IsTrue(ArrayHelper.CompareByteArray(originalHash, actualHash), "Original and deserialized object not the same!")
    End Sub

End Class
