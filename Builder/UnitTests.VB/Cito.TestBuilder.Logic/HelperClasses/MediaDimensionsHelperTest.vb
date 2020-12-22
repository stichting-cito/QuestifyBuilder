
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

    <TestMethod>
    Public Sub SizeOfCorruptFileShouldReturnEmptySize()
        Dim listeners As TraceListenerCollection = Debug.Listeners

        Try
            Debug.Listeners.Clear()

            Dim helper As New MediaDimensionsHelper
            Dim corruptFile As String = "This is a corrupt file"

            Dim size As Size = helper.GetDimensions("image/png", SerializeHelper.XmlSerializeToByteArray(corruptFile))

            Assert.IsTrue(size.IsEmpty)
        Finally
            Debug.Listeners.AddRange(listeners)
        End Try
    End Sub

    <TestMethod>
    Public Sub SizeOfUnkownMediaTypeSouldReturnEmptySize()
        Dim helper As New MediaDimensionsHelper

        Dim imageBytes As Byte() = CreateImageBytes(400, 300)

        Dim size As Size = helper.GetDimensions("zxxxxxxx", imageBytes)

        Assert.IsTrue(size.IsEmpty)
    End Sub

    <TestMethod>
    Public Sub SizeOfSvgFile()
        Dim helper As New MediaDimensionsHelper

        Dim size As Size = helper.GetDimensions("image/svg+xml", My.Resources.samplesvg)

        Assert.AreEqual(1901, size.Width)
        Assert.AreEqual(1018, size.Height)
    End Sub

    <TestMethod>
    Public Sub SizeOfMP4File()
        Dim helper As New MediaDimensionsHelper

        Dim size As Size = helper.GetDimensions("video/mp4", My.Resources.samplemp4)

        Assert.AreEqual(682, size.Width)
        Assert.AreEqual(384, size.Height)
    End Sub

    <TestMethod>
    Public Sub SizeOfCI()
        Dim helper As New MediaDimensionsHelper

        Dim size As Size = helper.GetDimensions("application/x-customInteraction", My.Resources._2_2_4)

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
