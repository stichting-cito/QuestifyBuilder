
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging
Imports Cito.Tester.Common

'''<summary>
'''This is a test class for Cito.Tester.Common.FileHelper and is intended
'''to contain all Cito.Tester.Common.FileHelper Unit Tests
'''</summary>
<TestClass()> _
Public Class TesterFileHelperTest

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext

    '''<summary>
    '''A test for MakeByteArrayFromFile(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub FileHelper_MakeByteArrayFromFileTest()
        'Arrange
        Dim file As String = "SampleFile"
        My.Resources.SampleImage.Save(file, ImageFormat.Jpeg)
        Dim info As New FileInfo(file)

        'Act
        Dim fileBytes() As Byte = FileHelper.MakeByteArrayFromFile(file)

        'Assert
        Assert.AreEqual(CInt(info.Length), fileBytes.Length, "File length is different then stream!")
    End Sub

    '''<summary>
    '''A test for MakeFileFromByteArray(ByVal String, ByVal Byte())
    '''</summary>
    <TestMethod()> _
    Public Sub FileHelper_MakeFileFromByteArrayTest()
        'Create file
        Dim file As String = "SampleFile"
        Dim imageStream As New MemoryStream
        My.Resources.SampleImage.Save(imageStream, ImageFormat.Jpeg)
        Dim fileByteArray() As Byte = DirectCast(Array.CreateInstance(GetType(Byte), imageStream.Length), Byte())
        imageStream.Read(fileByteArray, 0, CInt(imageStream.Length))

        Dim result As Boolean = FileHelper.MakeFileFromByteArray(file, fileByteArray)
        Assert.IsTrue(result, "MakeFileFromByteArray returns false!")

        'Open file and read contents
        Using fileStr As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim fileByteReadArray() As Byte = DirectCast(Array.CreateInstance(GetType(Byte), fileStr.Length), Byte())

            CollectionAssert.AreEqual(fileByteArray, fileByteReadArray, "Cito.Tester.Common.FileHelper.MakeFileFromByteArray did not create the right file.")
        End Using
    End Sub

    <TestMethod()>
    Public Sub GetSizeFromByteArray_FeedNull_Expect_Size0_0()
        'Arrange
        
        'Act
        Dim result As Size = FileHelper.GetSizeFromByteArray(Nothing)
        
        'Assert
        Assert.AreEqual(0, result.Width)
        Assert.AreEqual(0, result.Height)
    End Sub

    <TestMethod()>
    Public Sub GetSizeFromByteArray_FeedEmptyArray_Expect_Size0_0()
        'Arrange
        Dim empty(-1) As Byte
       
        'Act
        Dim result As Size = FileHelper.GetSizeFromByteArray(empty)
        
        'Assert
        Assert.AreEqual(0, result.Width)
        Assert.AreEqual(0, result.Height)
    End Sub


    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub GetSizeFromByteArray_FeedNoImage_Expect_Exception()
        'Arrange
        Dim empty(10) As Byte
       
        'Act
        Dim result As Size = FileHelper.GetSizeFromByteArray(empty)
       
        'Assert
        Assert.Fail()
    End Sub

End Class
