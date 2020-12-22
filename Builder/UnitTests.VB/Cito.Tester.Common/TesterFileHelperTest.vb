
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging
Imports Cito.Tester.Common

<TestClass()> _
Public Class TesterFileHelperTest

    Public Property TestContext() As TestContext

    <TestMethod()> _
    Public Sub FileHelper_MakeByteArrayFromFileTest()
        Dim file As String = "SampleFile"
        My.Resources.SampleImage.Save(file, ImageFormat.Jpeg)
        Dim info As New FileInfo(file)

        Dim fileBytes() As Byte = FileHelper.MakeByteArrayFromFile(file)

        Assert.AreEqual(CInt(info.Length), fileBytes.Length, "File length is different then stream!")
    End Sub

    <TestMethod()> _
    Public Sub FileHelper_MakeFileFromByteArrayTest()
        Dim file As String = "SampleFile"
        Dim imageStream As New MemoryStream
        My.Resources.SampleImage.Save(imageStream, ImageFormat.Jpeg)
        Dim fileByteArray() As Byte = DirectCast(Array.CreateInstance(GetType(Byte), imageStream.Length), Byte())
        imageStream.Read(fileByteArray, 0, CInt(imageStream.Length))

        Dim result As Boolean = FileHelper.MakeFileFromByteArray(file, fileByteArray)
        Assert.IsTrue(result, "MakeFileFromByteArray returns false!")

        Using fileStr As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim fileByteReadArray() As Byte = DirectCast(Array.CreateInstance(GetType(Byte), fileStr.Length), Byte())

            CollectionAssert.AreEqual(fileByteArray, fileByteReadArray, "Cito.Tester.Common.FileHelper.MakeFileFromByteArray did not create the right file.")
        End Using
    End Sub

    <TestMethod()>
    Public Sub GetSizeFromByteArray_FeedNull_Expect_Size0_0()

        Dim result As Size = FileHelper.GetSizeFromByteArray(Nothing)

        Assert.AreEqual(0, result.Width)
        Assert.AreEqual(0, result.Height)
    End Sub

    <TestMethod()>
    Public Sub GetSizeFromByteArray_FeedEmptyArray_Expect_Size0_0()
        Dim empty(-1) As Byte

        Dim result As Size = FileHelper.GetSizeFromByteArray(empty)

        Assert.AreEqual(0, result.Width)
        Assert.AreEqual(0, result.Height)
    End Sub


    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub GetSizeFromByteArray_FeedNoImage_Expect_Exception()
        Dim empty(10) As Byte

        Dim result As Size = FileHelper.GetSizeFromByteArray(empty)

        Assert.Fail()
    End Sub

End Class
