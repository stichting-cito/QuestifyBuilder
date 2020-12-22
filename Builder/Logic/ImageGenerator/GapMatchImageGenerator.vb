Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace ImageGenerator

    Public Class GapMatchImageGenerator
        Inherits ImageGenerator

        Public Sub New()
        End Sub

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String) As Byte()
            Return CreateImage(width, height, text, New Pen(Color.Black, 1), 255)
        End Function

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String, percTransparency As Integer) As Byte()
            Return CreateImage(width, height, text, New Pen(Color.Black, 1), percTransparency)
        End Function

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String, pen As Pen, percTransparency As Integer) As Byte()
            If width <= 0 Then Throw New ArgumentOutOfRangeException("width", "Width must be larger than 0")
            If height <= 0 Then Throw New ArgumentOutOfRangeException("height", "Height must be larger than 0")

            Dim drawformat As StringFormat = New StringFormat()
            Dim font As Font = New Font("Verdana", 13, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim result As Byte()

            Using bitmap As Bitmap = New Bitmap(width, height, PixelFormat.Format32bppArgb)
                Using graphics As Graphics = graphics.FromImage(bitmap)
                    Dim alpha As Integer = 255 - CInt(Math.Round((255 * (percTransparency / 100))))
                    Dim brush As New SolidBrush(Color.FromArgb(alpha, 255, 255, 255))
                    graphics.FillRectangle(brush, pen.Width / 2, pen.Width / 2, width - pen.Width, height - pen.Width)

                    drawformat.Alignment = StringAlignment.Center
                    drawformat.LineAlignment = StringAlignment.Center
                    Dim textDrawingRectangle As New RectangleF(pen.Width / 2, pen.Width / 2, width - pen.Width, height - pen.Width)
                    graphics.DrawString(text, font, New SolidBrush(pen.Color), textDrawingRectangle, drawformat)

                    Using memoryStream As New MemoryStream()
                        bitmap.Save(memoryStream, ImageFormat.Png)
                        result = memoryStream.GetBuffer()
                    End Using

                End Using
            End Using

            Return result
        End Function

        Public Function CreateTransparentImageFromMathMLImage(width As Integer, height As Integer, mathMLImg As Byte(), percTransparency As Integer) As Byte()
            Return CreateTransparentImageFromMathMLImage(width, height, mathMLImg, New Pen(Color.Black, 1), percTransparency)
        End Function

        Private Function CreateTransparentImageFromMathMLImage(width As Integer, height As Integer, mathMLimg As Byte(), pen As Pen, percTransparency As Integer) As Byte()
            Dim result As Byte()

            Dim stream As New MemoryStream(mathMLimg)
            Dim mathMlImage As Image = Image.FromStream(stream)
            If width = 0 Then width = mathMlImage.Width + 6
            If height = 0 Then height = mathMlImage.Height + 6

            Using bitmap As Bitmap = New Bitmap(width, height, PixelFormat.Format32bppArgb)
                Using graphics As Graphics = Graphics.FromImage(bitmap)
                    Dim alpha As Integer = 255 - CInt(Math.Round((255 * (percTransparency / 100))))
                    Dim brush As New SolidBrush(Color.FromArgb(alpha, 255, 255, 255))
                    graphics.FillRectangle(brush, pen.Width / 2, pen.Width / 2, width - pen.Width, height - pen.Width)
                    graphics.DrawImage(mathMlImage, GetLocationForMathMLImage(width, height, mathMlImage.Width, mathMlImage.Height))

                    Using memoryStream As New MemoryStream()
                        bitmap.Save(memoryStream, ImageFormat.Png)
                        result = memoryStream.GetBuffer()
                    End Using

                End Using
            End Using

            Return result
        End Function

        Private Function GetLocationForMathMLImage(containerWidth As Integer, containerHeight As Integer, imageWidth As Integer, imageHeight As Integer) As Point
            Dim x As Integer = 3
            Dim y As Integer = 3
            If imageWidth <= containerWidth Then x = CInt((containerWidth / 2) - (imageWidth / 2))
            If imageHeight <= containerHeight Then y = CInt((containerHeight / 2) - (imageHeight / 2))
            Return New Point(x, y)
        End Function

    End Class
End NameSpace