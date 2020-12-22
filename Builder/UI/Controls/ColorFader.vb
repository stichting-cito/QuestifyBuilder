Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports Questify.Builder.UI.Controls.Logic


Namespace Controls


    Public Class ColorFader
        Inherits Control
        Private m_color1 As Color
        Private m_color2 As Color
        Private m_colorMid As Color
        Private m_hueMode As Boolean
        Private m_ratio As Byte
        Private m_numericControl As NumericUpDown
        Private marginBottom As Integer = 3
        Private paddingH As Integer = 4

        Public Event RatioChanged As EventHandler

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "Black")> _
        Public Property Color1() As Color
            Get
                Return m_color1
            End Get
            Set(value As Color)
                m_color1 = value
                Invalidate()
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "White")> _
        Public Property Color2() As Color
            Get
                Return m_color2
            End Get
            Set(value As Color)
                m_color2 = value
                Invalidate()
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "Empty")> _
        Public Property ColorMid() As Color
            Get
                Return m_colorMid
            End Get
            Set(value As Color)
                m_colorMid = value
                Invalidate()
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(False)> _
        Public Property HueMode() As Boolean
            Get
                Return m_hueMode
            End Get
            Set(value As Boolean)
                m_hueMode = value
                Invalidate()
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(0)> _
        Public Property Ratio() As Byte
            Get
                Return m_ratio
            End Get
            Set(value As Byte)
                m_ratio = value
                Invalidate()
                If m_numericControl IsNot Nothing Then
                    m_numericControl.Value = m_ratio
                End If
                OnRatioChanged()
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property MixedColor() As Color
            Get
                Dim d As Double = CDbl(m_ratio) / 255
                Dim r As Integer = CInt(m_color1.R * (1 - d) + m_color2.R * d)
                Dim g As Integer = CInt(m_color1.G * (1 - d) + m_color2.G * d)
                Dim b As Integer = CInt(m_color1.B * (1 - d) + m_color2.B * d)
                Return Color.FromArgb(r, g, b)
            End Get
        End Property

        Public Property NumericControl() As NumericUpDown
            Get
                Return m_numericControl
            End Get
            Set(value As NumericUpDown)
                If m_numericControl IsNot Nothing Then
                    RemoveHandler m_numericControl.ValueChanged, AddressOf numericControl_ValueChanged
                End If
                m_numericControl = value
                If m_numericControl IsNot Nothing Then
                    AddHandler m_numericControl.ValueChanged, AddressOf numericControl_ValueChanged
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

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property BackgroundImage() As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property BackgroundImageLayout() As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property AutoScrollOffset() As Point
            Get
                Return MyBase.AutoScrollOffset
            End Get
            Set(value As Point)
                MyBase.AutoScrollOffset = value
            End Set
        End Property

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property

        Public Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)

            DoubleBuffered = True
            TabStop = False
            m_color1 = Color.Black
            m_color2 = Color.White
            m_ratio = 0
        End Sub

        Private Sub numericControl_ValueChanged(sender As Object, e As EventArgs)
            If m_numericControl.Value = -1 Then
                m_numericControl.Value = 255
                Return
            End If
            If m_numericControl.Value = 256 Then
                m_numericControl.Value = 0
                Return
            End If

            Ratio = CByte(m_numericControl.Value)
        End Sub


        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            If Not m_hueMode Then
                If m_color1.A < 255 OrElse m_color2.A < 255 Then
                    Dim backColor1 As Color = Color.WhiteSmoke
                    Dim backColor2 As Color = Color.Silver

                    Dim size As Integer = 2 * 3
                    Using bmp As New Bitmap(size, size)
                        For x As Integer = 0 To bmp.Width - 1
                            For y As Integer = 0 To bmp.Height - 1
                                If x < bmp.Width / 2 AndAlso y < bmp.Height / 2 OrElse x >= bmp.Width / 2 AndAlso y >= bmp.Height / 2 Then
                                    bmp.SetPixel(x, y, backColor1)
                                Else
                                    bmp.SetPixel(x, y, backColor2)
                                End If
                            Next
                        Next
                        Using b As Brush = New TextureBrush(bmp)
                            pe.Graphics.FillRectangle(b, New Rectangle(paddingH, 0, Width - 2 * paddingH, Height - marginBottom))
                        End Using
                    End Using
                End If

                If m_colorMid.IsEmpty Then
                    Dim gradient As New LinearGradientBrush(New Point(), New Point(Width, 0), m_color1, m_color2)
                    pe.Graphics.FillRectangle(gradient, New Rectangle(paddingH, 0, Width - 2 * paddingH, Height - marginBottom))
                    gradient.Dispose()
                Else
                    Dim gradient As New LinearGradientBrush(New Point(), New Point(Width \ 2, 0), m_color1, m_colorMid)
                    pe.Graphics.FillRectangle(gradient, New Rectangle(paddingH, 0, Width \ 2 - paddingH, Height - marginBottom))
                    gradient.Dispose()

                    gradient = New LinearGradientBrush(New Point(Width \ 2, 0), New Point(Width, 0), m_colorMid, m_color2)
                    pe.Graphics.FillRectangle(gradient, New Rectangle(Width \ 2, 0, Width \ 2 - paddingH, Height - marginBottom))
                    gradient.Dispose()
                End If
            Else
                For x As Integer = paddingH To Width - paddingH - 1
                    Dim h As Byte = CByte(Math.Round(CDbl(x - paddingH) / (Width - paddingH) * 255))
                    Using p As New Pen(ColorMath.HslToRgb(New HslColor(h, 255, 128)))
                        pe.Graphics.DrawLine(p, x, 0, x, Height - marginBottom)
                    End Using
                Next
            End If

            pe.Graphics.InterpolationMode = InterpolationMode.High
            pe.Graphics.SmoothingMode = SmoothingMode.HighQuality

            Dim d As Double = CDbl(m_ratio) / 255
            Dim x0 As Integer = CInt(Math.Truncate((Width - 1 - 2 * paddingH) * d)) + paddingH
            Dim y0 As Integer = Height - 7
            Dim triangleWidth As Integer = 5
            Dim triangleHeight As Integer = 9
            Dim trianglePath As New GraphicsPath()
            trianglePath.AddLine(x0 - triangleWidth, y0 + triangleHeight, x0 + 0, y0 + 0)
            trianglePath.AddLine(x0 + 0, y0 + 0, x0 + triangleWidth, y0 + triangleHeight)
            trianglePath.CloseFigure()
            Dim triangleBrush As New SolidBrush(Color.Black)
            pe.Graphics.FillPath(triangleBrush, trianglePath)
            triangleBrush.Dispose()
            Dim trianglePen As New Pen(SystemColors.Control)
            pe.Graphics.DrawPath(trianglePen, trianglePath)
            trianglePen.Dispose()
            trianglePath.Dispose()
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                Dim x As Integer = e.X
                If x < paddingH Then
                    x = paddingH
                End If
                If x > Width - paddingH Then
                    x = Width - paddingH
                End If
                Ratio = CByte(Math.Truncate(CDbl(x - paddingH) / (Width - 2 * paddingH) * 255))

                If m_numericControl IsNot Nothing Then
                    m_numericControl.Focus()
                End If
            End If

            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                Dim x As Integer = e.X
                If x < paddingH Then
                    x = paddingH
                End If
                If x > Width - paddingH Then
                    x = Width - paddingH
                End If
                Ratio = CByte(Math.Truncate(CDbl(x - paddingH) / (Width - 2 * paddingH) * 255))
            End If

            MyBase.OnMouseMove(e)
        End Sub

        Protected Sub OnRatioChanged()
            RaiseEvent RatioChanged(Me, EventArgs.Empty)
        End Sub
    End Class

End Namespace