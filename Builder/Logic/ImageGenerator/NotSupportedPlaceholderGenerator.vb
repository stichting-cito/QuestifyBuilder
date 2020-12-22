Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace ImageGenerator

    Public Class NotSupportedPlaceholderGenerator
        Inherits Global.Questify.Builder.Logic.ImageGenerator.ImageGenerator

        Public Sub New()
        End Sub

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String) As Byte()
            Return CreateImage(width, height, text, New Pen(Color.Red, 2), 255)
        End Function

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String, percTransparency As Integer) As Byte()
            Return CreateImage(width, height, text, New Pen(Color.Black, 1), percTransparency)
        End Function

        Public Overrides Function CreateImage(width As Integer, height As Integer, text As String, pen As Pen, percTransparency As Integer) As Byte()
            If width <= 0 Then Throw New ArgumentOutOfRangeException("width", "Width must be larger than 0")
            If height <= 0 Then Throw New ArgumentOutOfRangeException("height", "Height must be larger than 0")

            Dim drawformat As StringFormat = New StringFormat()
            Dim font As Font = New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim result As Byte()

            Using bitmap As Bitmap = New Bitmap(width, height, PixelFormat.Format32bppArgb)
                Using graphics As Graphics = graphics.FromImage(bitmap)
                    graphics.Clear(Color.White)
                    graphics.DrawRectangle(pen, pen.Width / 2, pen.Width / 2, width - pen.Width, height - pen.Width)

                    pen.Color = Color.Black
                    drawformat.Alignment = StringAlignment.Center
                    Dim textDrawingRectangle As New RectangleF(pen.Width / 2, ((height - pen.Width) - font.Size) / 2, width - pen.Width, height - pen.Width)
                    graphics.DrawString(text, font, New SolidBrush(pen.Color), textDrawingRectangle, drawformat)

                    Using memoryStream As New MemoryStream()
                        bitmap.Save(memoryStream, ImageFormat.Jpeg)
                        result = memoryStream.GetBuffer()
                    End Using

                End Using
            End Using

            Return result
        End Function
    End Class
End NameSpace