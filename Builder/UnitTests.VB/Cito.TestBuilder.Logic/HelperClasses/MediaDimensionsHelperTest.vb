
Imports System.Drawing
Imports System.IO
Imports System.Diagnostics
Imports Questify.Builder.Logic
Imports Cito.Tester.Common

<TestClass>
<DeploymentItem("MediaInfo.dll")>
Public Class MediaDimensionsHelperTest

    <TestMethod>
    Public Sub SizeOfImage()
        Dim helper As New MediaDimensionsHelper
        Dim imageBytes As Byte() = CreateImageBytes(400, 300)
        Dim size As Size = helper.GetDimensions("image/png", imageBytes)
        Assert.AreEqual(400, size.Width)
        Assert.AreEqual(300, size.Height)
    End Sub

    ''' <summary>
    ''' Remove the Debug.Listeners for this test because when a file is corrupt, 
    ''' a Debug.Assert will be fired in the try-catch of MediaDimensionsHelper.GetDimensions(...)
    ''' and therefore fails the test. This is unwanted.
    ''' </summary>
    <TestMethod>
    Public Sub SizeOfCorruptFileShouldReturnEmptySize()
        'Arrange
        Dim listeners As TraceListenerCollection = Debug.Listeners

        Try
            Debug.Listeners.Clear()

            Dim helper As New MediaDimensionsHelper
            Dim corruptFile As String = "This is a corrupt file"

            'Act
            Dim size As Size = helper.GetDimensions("image/png", SerializeHelper.XmlSerializeToByteArray(corruptFile))

            'Assert
            Assert.IsTrue(size.IsEmpty)
        Finally
            Debug.Listeners.AddRange(listeners)
        End Try
    End Sub

    <TestMethod>
    Public Sub SizeOfUnkownMediaTypeSouldReturnEmptySize()
        'Arrange
        Dim helper As New MediaDimensionsHelper

        Dim imageBytes As Byte() = CreateImageBytes(400, 300)

        'Act
        Dim size As Size = helper.GetDimensions("zxxxxxxx", imageBytes)

        'Assert
        Assert.IsTrue(size.IsEmpty)
    End Sub

    <TestMethod>
    Public Sub SizeOfSvgFile()
        'Arrange
        Dim helper As New MediaDimensionsHelper

        'Act
        Dim size As Size = helper.GetDimensions("image/svg+xml", My.Resources.samplesvg)

        'Assert
        Assert.AreEqual(1901, size.Width)
        Assert.AreEqual(1018, size.Height)
    End Sub

    <TestMethod>
    Public Sub SizeOfMP4File()
        'Arrange
        Dim helper As New MediaDimensionsHelper

        'Act
        Dim size As Size = helper.GetDimensions("video/mp4", My.Resources.samplemp4)

        'Assert
        Assert.AreEqual(682, size.Width)
        Assert.AreEqual(384, size.Height)
    End Sub

    <TestMethod>
    Public Sub SizeOfCI()
        'Arrange
        Dim helper As New MediaDimensionsHelper

        'Act
        Dim size As Size = helper.GetDimensions("application/x-customInteraction", My.Resources._2_2_4)

        'Assert
        Assert.AreEqual(467, size.Width)
        Assert.AreEqual(525, size.Height)
    End Sub

    Private Function CreateImageBytes(width As Integer, height As Integer) As Byte()
        Dim image As New Bitmap(width, height)
        Using stream As New MemoryStream
            image.Save(stream, Imaging.ImageFormat.Png)
            Return stream.ToArray()
        End Using
    End Function

End Class
