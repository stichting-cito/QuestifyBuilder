Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports Questify.Builder.UI.Controls.Logic


Namespace Controls

    Public Class ColorWheel
        Inherits Control

        Private wheelBitmap As Bitmap
        Private slBitmap As Bitmap
        Private m_hue As Byte
        Private m_saturation As Byte
        Private m_lightness As Byte
        Private m_secondaryHues As Byte()
        Private draggingHue As Boolean
        Private draggingSL As Boolean

        Public Event HueChanged As EventHandler
        Public Event SLChanged As EventHandler

        Public Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)

            DoubleBuffered = True
            TabStop = False

            PrepareWheelBitmap()
        End Sub


        Public Property Hue() As Byte
            Get
                Return m_hue
            End Get
            Set(value As Byte)
                If value <> m_hue Then
                    m_hue = value
                    PrepareSLBitmap()
                    Invalidate()
                End If
            End Set
        End Property

        Public Property Saturation() As Byte
            Get
                Return m_saturation
            End Get
            Set(value As Byte)
                If value <> m_saturation Then
                    m_saturation = value
                    Invalidate()
                End If
            End Set
        End Property

        Public Property Lightness() As Byte
            Get
                Return m_lightness
            End Get
            Set(value As Byte)
                If value <> m_lightness Then
                    m_lightness = value
                    Invalidate()
                End If
            End Set
        End Property

        Public Property SecondaryHues() As Byte()
            Get
                Return m_secondaryHues
            End Get
            Set(value As Byte())
                If (value Is Nothing) <> (m_secondaryHues Is Nothing) OrElse value Is Nothing OrElse value IsNot Nothing AndAlso value.Length <> m_secondaryHues.Length Then
                    m_secondaryHues = value
                    Invalidate()
                ElseIf value IsNot Nothing Then
                    For i As Integer = 0 To value.Length - 1
                        If value(i) <> m_secondaryHues(i) Then
                            m_secondaryHues = value
                            Invalidate()
                            Exit For
                        End If
                    Next
                End If
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property TabStop() As Boolean
            Get
                Return MyBase.TabStop
            End Get
            Set(value As Boolean)
                MyBase.TabStop = False
            End Set
        End Property

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            PrepareWheelBitmap()
            PrepareSLBitmap()
            MyBase.OnSizeChanged(e)
        End Sub


        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            If wheelBitmap IsNot Nothing Then
                pe.Graphics.DrawImage(wheelBitmap, New Point())
            End If

            If slBitmap IsNot Nothing Then
                pe.Graphics.DrawImage(slBitmap, New Point(slBitmap.Width \ 2, slBitmap.Width \ 2))
            End If

            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

            Dim radAngle As Double = CDbl(m_hue) / 128 * Math.PI + Math.PI / 2
            Dim d As Double = 0.89 * wheelBitmap.Width / 2
            Dim x As Integer = CInt(Math.Round(d * Math.Cos(radAngle)))
            Dim y As Integer = CInt(-Math.Round(d * Math.Sin(radAngle)))
            x += wheelBitmap.Width \ 2
            y += wheelBitmap.Width \ 2
            Dim c As Color = If(ColorMath.ToGray(ColorMath.HslToRgb(New HslColor(m_hue, 255, 128))) > 128, Color.Black, Color.White)
            Using p As New Pen(c)
                pe.Graphics.DrawEllipse(p, x - 3, y - 3, 6, 6)
            End Using

            If m_secondaryHues IsNot Nothing Then
                For Each sHue As Byte In m_secondaryHues
                    radAngle = CDbl(sHue) / 128 * Math.PI + Math.PI / 2
                    d = 0.89 * wheelBitmap.Width / 2
                    x = CInt(Math.Round(d * Math.Cos(radAngle)))
                    y = CInt(-Math.Round(d * Math.Sin(radAngle)))
                    x += wheelBitmap.Width \ 2
                    y += wheelBitmap.Width \ 2
                    c = If(ColorMath.ToGray(ColorMath.HslToRgb(New HslColor(sHue, 255, 128))) > 128, Color.Black, Color.White)
                    Using b As Brush = New SolidBrush(Color.FromArgb(128, c))
                        pe.Graphics.FillRectangle(b, x - 2, y - 2, 4, 4)
                    End Using
                Next
            End If

            x = slBitmap.Width \ 2 + m_saturation * (slBitmap.Width - 1) \ 255
            y = slBitmap.Width \ 2 + m_lightness * (slBitmap.Width - 1) \ 255
            c = If(ColorMath.ToGray(ColorMath.HslToRgb(New HslColor(m_hue, m_saturation, m_lightness))) > 128, Color.Black, Color.White)
            Using p As New Pen(c)
                pe.Graphics.DrawEllipse(p, x - 3, y - 3, 6, 6)
            End Using
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                Dim halfSLWidth As Integer = slBitmap.Width \ 2
                If e.X >= halfSLWidth AndAlso e.X < halfSLWidth * 3 AndAlso e.Y >= halfSLWidth AndAlso e.Y < halfSLWidth * 3 Then
                    draggingSL = True
                    OnMouseMove(e)
                Else
                    Dim halfWheelWidth As Integer = wheelBitmap.Width \ 2
                    Dim center As New Point(halfWheelWidth, halfWheelWidth)
                    Dim dist As Double = GetDistance(New Point(e.X, e.Y), center)
                    If dist >= halfWheelWidth * 0.78 AndAlso dist < halfWheelWidth Then
                        draggingHue = True
                        OnMouseMove(e)
                    End If
                End If
            End If

            MyBase.OnMouseDown(e)
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            draggingSL = False
            draggingHue = False

            MyBase.OnMouseUp(e)
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                If draggingSL Then
                    Dim halfSLWidth As Integer = slBitmap.Width \ 2
                    Saturation = CByte(Math.Max(0, Math.Min(255, (e.X - halfSLWidth) * 255 / slBitmap.Width)))
                    Lightness = CByte(Math.Max(0, Math.Min(255, (e.Y - halfSLWidth) * 255 / slBitmap.Width)))
                    OnSLChanged()
                ElseIf draggingHue Then
                    Dim halfWheelWidth As Integer = wheelBitmap.Width \ 2
                    Dim center As New Point(halfWheelWidth, halfWheelWidth)
                    Dim radAngle As Double = Math.Atan2(e.Y - center.Y, e.X - center.X)
                    Dim factor As Double = 128.0 / Math.PI
                    Hue = CByte([Mod](CInt(Math.Truncate(-factor * radAngle + 192)), 256))
                    OnHueChanged()
                End If
            End If

            MyBase.OnMouseMove(e)
        End Sub

        Private Sub PrepareWheelBitmap()
            If wheelBitmap IsNot Nothing Then
                wheelBitmap.Dispose()
            End If

            Dim width As Integer = Math.Min(ClientSize.Width, ClientSize.Height)
            Dim center As New Point(width \ 2, width \ 2)
            If width < 10 Then
                wheelBitmap = Nothing
                Return
            End If

            wheelBitmap = New Bitmap(width, width)

            Dim g As Graphics = Graphics.FromImage(wheelBitmap)
            Using b As Brush = New SolidBrush(Color.Transparent)
                g.FillRectangle(b, 0, 0, width, width)
            End Using

            Dim minDist As Double = (width \ 2) * 0.78
            Dim maxDist As Double = width \ 2 - 1
            Dim factor As Double = 128.0 / Math.PI
            Dim bmData As BitmapData = Nothing
            Dim bytes As Byte() = Nothing
            BitmapReadBytes(wheelBitmap, bytes, bmData)
            For y As Integer = 0 To width - 1
                For x As Integer = 0 To width - 1
                    Dim dist As Double = GetDistance(New Point(x, y), center)
                    Dim alpha As Byte
                    If dist < minDist - 0.5 Then
                        alpha = 0
                    ElseIf dist < minDist + 0.5 Then
                        alpha = CByte(Math.Truncate((0.5 - minDist + dist) * 255))
                    ElseIf dist < maxDist - 0.5 Then
                        alpha = 255
                    ElseIf dist < maxDist + 0.5 Then
                        alpha = CByte(Math.Truncate((0.5 + maxDist - dist) * 255))
                    Else
                        alpha = 0
                    End If

                    If alpha > 0 Then
                        Dim radAngle As Double = Math.Atan2(y - center.Y, x - center.X)
                        Dim hue As Byte = CByte([Mod](CInt(Math.Truncate(-factor * radAngle + 192)), 256))
                        Dim c As Color = Color.FromArgb(alpha, ColorMath.HslToRgb(New HslColor(hue, 255, 128)))
                        BitmapSetPixel(bytes, bmData, x, y, c)
                    End If
                Next
            Next
            BitmapWriteBytes(wheelBitmap, bytes, bmData)
        End Sub

        Private Sub PrepareSLBitmap()
            If slBitmap IsNot Nothing Then
                slBitmap.Dispose()
            End If

            Dim width As Integer = Math.Min(ClientSize.Width, ClientSize.Height) \ 2
            If width < 10 Then
                slBitmap = Nothing
                Return
            End If

            slBitmap = New Bitmap(width, width)

            Dim bmData As BitmapData = Nothing
            Dim bytes As Byte() = Nothing
            BitmapReadBytes(slBitmap, bytes, bmData)
            For y As Integer = 0 To width - 1
                For x As Integer = 0 To width - 1
                    Dim c As Color = ColorMath.HslToRgb(New HslColor(m_hue, CByte(x * 255 \ width), CByte(y * 255 \ width)))
                    BitmapSetPixel(bytes, bmData, x, y, c)
                Next
            Next
            BitmapWriteBytes(slBitmap, bytes, bmData)
        End Sub

        Private Sub BitmapReadBytes(bmp As Bitmap, ByRef bytes As Byte(), ByRef bmData As BitmapData)
            bmData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb)
            Const bpp As Integer = 4
            bytes = New Byte(bmp.Width * bmp.Height * bpp - 1) {}
            Marshal.Copy(bmData.Scan0, bytes, 0, bytes.Length)
        End Sub

        Private Sub BitmapSetPixel(bytes As Byte(), bmData As BitmapData, x As Integer, y As Integer, c As Color)
            Dim i As Integer = y * bmData.Stride + x * 4
            bytes(i) = c.B
            bytes(i + 1) = c.G
            bytes(i + 2) = c.R
            bytes(i + 3) = c.A
        End Sub

        Private Sub BitmapWriteBytes(bmp As Bitmap, bytes As Byte(), bmData As BitmapData)
            Marshal.Copy(bytes, 0, bmData.Scan0, bytes.Length)
            bmp.UnlockBits(bmData)
        End Sub

        Private Function GetDistance(a As Point, b As Point) As Double
            Return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y))
        End Function

        Private Shared Function [Mod](dividend As Integer, divisor As Integer) As Integer
            If divisor <= 0 Then
                Throw New ArgumentOutOfRangeException("divisor", "The divisor cannot be zero or negative.")
            End If
            Dim i As Integer = dividend Mod divisor
            If i < 0 Then
                i += divisor
            End If
            Return i
        End Function

        Protected Sub OnHueChanged()
            RaiseEvent HueChanged(Me, EventArgs.Empty)
        End Sub

        Protected Sub OnSLChanged()
            RaiseEvent SLChanged(Me, EventArgs.Empty)
        End Sub
    End Class

End Namespace
