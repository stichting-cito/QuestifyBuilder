Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports Questify.Builder.UI.Controls.Logic


Namespace Controls
    Public Class ColorSelector
        Inherits UserControl
        Private updatingFaders As Boolean
        Private updatingRGBText As Boolean
        Private updatingHexText As Boolean
        Private m_selectedColor As Color
        Private components As IContainer = Nothing
        Private tableLayoutPanel1 As TableLayoutPanel
        Private colorWheel1 As ColorWheel
        Private BlueLabel As Label
        Private GreenLabel As Label
        Private numericBlue As NumericUpDown
        Private numericGreen As NumericUpDown
        Private numericRed As NumericUpDown
        Private RedLabel As Label
        Private HueLabel As Label
        Private SaturationLabel As Label
        Private LightnessLabel As Label
        Private numericHue As NumericUpDown
        Private numericSaturation As NumericUpDown
        Private numericLightness As NumericUpDown
        Private HTMLLabel As Label
        Private textRGB As TextBox
        Private textHex As TextBox
        Private CSSrgbLabel As Label
        Private faderRed As ColorFader
        Private faderGreen As ColorFader
        Private faderBlue As ColorFader
        Private faderHue As ColorFader
        Private faderSaturation As ColorFader
        Private faderLightness As ColorFader
        Private flowLayoutPanel1 As FlowLayoutPanel
        Private pictureBox2 As PictureBox
        Private pictureBox3 As PictureBox
        Private pictureBox4 As PictureBox
        Private pictureBox5 As PictureBox
        Private pictureBox6 As PictureBox
        Private pictureBox7 As PictureBox
        Private pictureBox8 As PictureBox
        Private pictureBox9 As PictureBox
        Private pictureBox10 As PictureBox
        Private pictureBox11 As PictureBox
        Private pictureBox12 As PictureBox
        Private pictureBox13 As PictureBox
        Private pictureBox14 As PictureBox
        Private pictureBox15 As PictureBox
        Private pictureBox16 As PictureBox
        Private pictureBox17 As PictureBox
        Private pictureBox18 As PictureBox
        Private pictureBox19 As PictureBox
        Private pictureBox20 As PictureBox
        Private pictureBox21 As PictureBox
        Private pictureBox22 As PictureBox
        Private pictureBox23 As PictureBox
        Private pictureBox24 As PictureBox
        Private pictureBox25 As PictureBox
        Private pictureBox26 As PictureBox
        Private pictureBox27 As PictureBox
        Private pictureBox28 As PictureBox
        Private pictureBox29 As PictureBox
        Private pictureBox30 As PictureBox
        Private pictureBox31 As PictureBox
        Private pictureBox32 As PictureBox
        Private pictureBox33 As PictureBox
        Private pictureBox34 As PictureBox
        Private pictureBox35 As PictureBox
        Private pictureBox36 As PictureBox
        Private pictureBox37 As PictureBox
        Private pictureBox38 As PictureBox
        Private pictureBox39 As PictureBox
        Private pictureBox40 As PictureBox
        Private pictureBox41 As PictureBox
        Private pictureBox42 As PictureBox
        Private pictureBox43 As PictureBox
        Private pictureBox44 As PictureBox
        Private pictureBox45 As PictureBox
        Private pictureBox46 As PictureBox
        Private pictureBox47 As PictureBox
        Private pictureBox48 As PictureBox
        Private pictureBox49 As PictureBox
        Private RLabel As Label
        Private GLabel As Label
        Private BLabel As Label
        Private HLabel As Label
        Private SLabel As Label
        Private LLabel As Label
        Private ExtendedViewCheck As CheckBox
        Private OKActionButton As Button
        Private ActionsPanel As FlowLayoutPanel
        Private CancelActionButton As Button

        Public Event SelectedColorChanged As System.EventHandler
        Public Event CloseRequested As System.EventHandler
        Public Event CancelRequested As System.EventHandler

        Public Sub New()
            Me.InitializeComponent()
            Me.AutoLocalise()
            Me.RGBfader_RatioChanged(Nothing, Nothing)
        End Sub

        Public Property T_ExtendedView() As String
            Get
                Return Me.ExtendedViewCheck.Text
            End Get
            Set(value As String)
                Me.ExtendedViewCheck.Text = value
            End Set
        End Property
        Public Property T_OK() As String
            Get
                Return Me.OKActionButton.Text
            End Get
            Set(value As String)
                Me.OKActionButton.Text = value
            End Set
        End Property
        Public Property T_Cancel() As String
            Get
                Return Me.CancelActionButton.Text
            End Get
            Set(value As String)
                Me.CancelActionButton.Text = value
            End Set
        End Property
        Public Property T_Red() As String
            Get
                Return Me.RedLabel.Text
            End Get
            Set(value As String)
                Me.RedLabel.Text = value
            End Set
        End Property
        Public Property T_Green() As String
            Get
                Return Me.GreenLabel.Text
            End Get
            Set(value As String)
                Me.GreenLabel.Text = value
            End Set
        End Property
        Public Property T_Blue() As String
            Get
                Return Me.BlueLabel.Text
            End Get
            Set(value As String)
                Me.BlueLabel.Text = value
            End Set
        End Property
        Public Property T_Hue() As String
            Get
                Return Me.HueLabel.Text
            End Get
            Set(value As String)
                Me.HueLabel.Text = value
            End Set
        End Property
        Public Property T_Saturation() As String
            Get
                Return Me.SaturationLabel.Text
            End Get
            Set(value As String)
                Me.SaturationLabel.Text = value
            End Set
        End Property
        Public Property T_Lightness() As String
            Get
                Return Me.LightnessLabel.Text
            End Get
            Set(value As String)
                Me.LightnessLabel.Text = value
            End Set
        End Property
        Public Property T_CSSrgb() As String
            Get
                Return Me.CSSrgbLabel.Text
            End Get
            Set(value As String)
                Me.CSSrgbLabel.Text = value
            End Set
        End Property
        Public Property T_HTML() As String
            Get
                Return Me.HTMLLabel.Text
            End Get
            Set(value As String)
                Me.HTMLLabel.Text = value
            End Set
        End Property
        Public Property SelectedColor() As Color
            Get
                Return Me.m_selectedColor
            End Get
            Set(value As Color)
                If value <> Me.m_selectedColor Then
                    Me.m_selectedColor = value
                    If Not Me.updatingFaders Then
                        Me.faderRed.Ratio = value.R
                        Me.faderGreen.Ratio = value.G
                        Me.faderBlue.Ratio = value.B
                    End If
                    Me.UpdateColors()
                    RaiseEvent SelectedColorChanged(Me, System.EventArgs.Empty)
                End If
            End Set
        End Property
        Public Property RgbText() As String
            Get
                Return Me.textRGB.Text
            End Get
            Set(value As String)
                Me.textRGB.Text = value
            End Set
        End Property
        Public Property HtmlText() As String
            Get
                Return Me.textHex.Text
            End Get
            Set(value As String)
                Me.textHex.Text = value
            End Set
        End Property



        Public Sub AutoLocalise()
            Dim twoLetterISOLanguageName As String = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName

            Me.T_ExtendedView = "Extended options"
            Me.T_OK = "OK"
            Me.T_Cancel = "Cancel"
            Me.T_Red = "Red:"
            Me.T_Green = "Green:"
            Me.T_Blue = "Blue:"
            Me.T_Hue = "Hue:"
            Me.T_Saturation = "Saturation:"
            Me.T_Lightness = "Lightness:"
            Me.T_CSSrgb = "CSS rgb:"
            Me.T_HTML = "HTML:"

            If twoLetterISOLanguageName IsNot Nothing Then
                If twoLetterISOLanguageName = "de" Then
                    Me.T_ExtendedView = "Erweiterte Optionen"
                    Me.T_OK = "OK"
                    Me.T_Cancel = "Abbrechen"
                    Me.T_Red = "Rot:"
                    Me.T_Green = "Grün:"
                    Me.T_Blue = "Blau:"
                    Me.T_Hue = "Farbton:"
                    Me.T_Saturation = "Sättigung:"
                    Me.T_Lightness = "Helligkeit:"
                    Me.T_CSSrgb = "CSS rgb:"
                    Me.T_HTML = "HTML:"
                    Return
                End If
                If Not (twoLetterISOLanguageName = "en") Then
                    If twoLetterISOLanguageName = "es" Then
                        Me.T_ExtendedView = "Opciones extendida"
                        Me.T_OK = "Aceptar"
                        Me.T_Cancel = "Cancelar"
                        Me.T_Red = "Rojo:"
                        Me.T_Green = "Verde:"
                        Me.T_Blue = "Azul:"
                        Me.T_Hue = "Matiz:"
                        Me.T_Saturation = "Saturación:"
                        Me.T_Lightness = "Luminosidad:"
                        Me.T_CSSrgb = "CSS rgb:"
                        Me.T_HTML = "HTML:"
                        Return
                    End If
                    If twoLetterISOLanguageName = "fr" Then
                        Me.T_ExtendedView = "Options étendue"
                        Me.T_OK = "OK"
                        Me.T_Cancel = "Annuler"
                        Me.T_Red = "Rouge:"
                        Me.T_Green = "Vert:"
                        Me.T_Blue = "Bleu:"
                        Me.T_Hue = "Teinte:"
                        Me.T_Saturation = "Saturation:"
                        Me.T_Lightness = "Luminosité:"
                        Me.T_CSSrgb = "CSS rgb:"
                        Me.T_HTML = "HTML:"
                        Return
                    End If
                    If twoLetterISOLanguageName = "nl" Then
                        Me.T_ExtendedView = "Meer opties"
                        Me.T_OK = "OK"
                        Me.T_Cancel = "Annuleren"
                        Me.T_Red = "Rood:"
                        Me.T_Green = "Groen:"
                        Me.T_Blue = "Blauw:"
                        Me.T_Hue = "Tint:"
                        Me.T_Saturation = "Verzadiging:"
                        Me.T_Lightness = "Helderheid:"
                        Me.T_CSSrgb = "CSS rgb:"
                        Me.T_HTML = "HTML:"
                        Return
                    End If
                End If
            End If
        End Sub

        Protected Overrides Sub OnLoad(e As System.EventArgs)
            MyBase.OnLoad(e)
            Me.ExtendedViewCheck_CheckedChanged(Nothing, Nothing)
        End Sub
        Private Sub RGBfader_RatioChanged(sender As Object, e As System.EventArgs)
            If Not Me.updatingFaders Then
                Me.updatingFaders = True
                Dim rgb As Color = Color.FromArgb(CInt(Me.faderRed.Ratio), CInt(Me.faderGreen.Ratio), CInt(Me.faderBlue.Ratio))
                Dim hsl As HslColor = ColorMath.RgbToHsl(rgb)
                Me.faderHue.Ratio = hsl.H
                Me.faderSaturation.Ratio = hsl.S
                Me.faderLightness.Ratio = hsl.L
                Me.SelectedColor = rgb
                Me.updatingFaders = False
            End If
        End Sub
        Private Sub HSLfader_RatioChanged(sender As Object, e As System.EventArgs)
            If Not Me.updatingFaders Then
                Me.updatingFaders = True
                Dim hsl As New HslColor(Me.faderHue.Ratio, Me.faderSaturation.Ratio, Me.faderLightness.Ratio)
                Dim rgb As Color = ColorMath.HslToRgb(hsl)
                Me.faderRed.Ratio = rgb.R
                Me.faderGreen.Ratio = rgb.G
                Me.faderBlue.Ratio = rgb.B
                Me.SelectedColor = rgb
                Me.updatingFaders = False
            End If
        End Sub
        Private Sub colorWheel1_HueChanged(sender As Object, e As System.EventArgs)
            Me.updatingFaders = True
            Me.faderHue.Ratio = Me.colorWheel1.Hue
            Dim hsl As New HslColor(Me.faderHue.Ratio, Me.faderSaturation.Ratio, Me.faderLightness.Ratio)
            Dim rgb As Color = ColorMath.HslToRgb(hsl)
            Me.faderRed.Ratio = rgb.R
            Me.faderGreen.Ratio = rgb.G
            Me.faderBlue.Ratio = rgb.B
            Me.SelectedColor = rgb
            Me.updatingFaders = False
        End Sub
        Private Sub colorWheel1_SLChanged(sender As Object, e As System.EventArgs)
            Me.updatingFaders = True

            Me.faderSaturation.Ratio = Me.colorWheel1.Saturation
            Me.faderLightness.Ratio = Me.colorWheel1.Lightness
            Dim hsl As New HslColor(Me.faderHue.Ratio, Me.faderSaturation.Ratio, Me.faderLightness.Ratio)
            Dim rgb As Color = ColorMath.HslToRgb(hsl)
            Me.faderRed.Ratio = rgb.R
            Me.faderGreen.Ratio = rgb.G
            Me.faderBlue.Ratio = rgb.B
            Me.SelectedColor = rgb
            Me.updatingFaders = False
        End Sub
        Private Sub textRGB_TextChanged(sender As Object, e As System.EventArgs)
            If Not Me.updatingRGBText Then
                Me.updatingRGBText = True
                Try
                    Dim i As Match = Regex.Match(Me.textRGB.Text.Trim().ToLower(), "^rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)$")
                    If Not i.Success Then
                        Throw New System.FormatException()
                    End If
                    Dim r As Integer = Integer.Parse(i.Groups(1).Value)
                    Dim g As Integer = Integer.Parse(i.Groups(2).Value)
                    Dim b As Integer = Integer.Parse(i.Groups(3).Value)
                    If r > 255 Then
                        Throw New System.FormatException()
                    End If
                    If g > 255 Then
                        Throw New System.FormatException()
                    End If
                    If b > 255 Then
                        Throw New System.FormatException()
                    End If
                    Me.faderRed.Ratio = CByte(r)
                    Me.faderGreen.Ratio = CByte(g)
                    Me.faderBlue.Ratio = CByte(b)
                    Me.textRGB.BackColor = Color.Empty
                Catch generatedExceptionName As System.FormatException
                    Me.textRGB.BackColor = ColorMath.Blend(SystemColors.Window, Color.Crimson, 0.3)
                End Try
                Me.updatingRGBText = False
            End If
        End Sub
        Private Sub textHex_TextChanged(sender As Object, e As System.EventArgs)
            If Not Me.updatingHexText Then
                Me.updatingHexText = True
                Try
                    If Me.textHex.Text.Length = 0 Then
                        Throw New System.FormatException()
                    End If
                    If Me.textHex.Text(0) = "#"c AndAlso Me.textHex.Text.Length <> 4 AndAlso Me.textHex.Text.Length <> 7 Then
                        Throw New System.FormatException()
                    End If
                    Dim c As Color = ColorTranslator.FromHtml(Me.textHex.Text)
                    Me.faderRed.Ratio = c.R
                    Me.faderGreen.Ratio = c.G
                    Me.faderBlue.Ratio = c.B
                    Me.textHex.BackColor = Color.Empty
                Catch generatedExceptionName As System.Exception
                    Me.textHex.BackColor = ColorMath.Blend(SystemColors.Window, Color.Crimson, 0.3)
                End Try
                Me.updatingHexText = False
            End If
        End Sub
        Private Sub picturePalette_Click(sender As Object, e As System.EventArgs)
            Dim c As Control = TryCast(sender, Control)
            Dim color As Color = c.BackColor
            Me.faderRed.Ratio = color.R
            Me.faderGreen.Ratio = color.G
            Me.faderBlue.Ratio = color.B
            If Not Me.ExtendedViewCheck.Checked Then
                RaiseEvent CloseRequested(Me, System.EventArgs.Empty)
            End If
        End Sub
        Private Sub ExtendedViewCheck_CheckedChanged(sender As Object, e As System.EventArgs)
            Me.tableLayoutPanel1.SuspendLayout()
            Me.ActionsPanel.Visible = Me.ExtendedViewCheck.Checked
            For Each control As Control In Me.tableLayoutPanel1.Controls
                If Me.tableLayoutPanel1.GetColumn(control) > 0 Then
                    control.Visible = Me.ExtendedViewCheck.Checked
                End If
            Next
            Me.tableLayoutPanel1.ResumeLayout()
        End Sub
        Private Sub OKActionButton_Click(sender As Object, e As System.EventArgs)
            RaiseEvent CloseRequested(Me, System.EventArgs.Empty)
        End Sub
        Private Sub CancelActionButton_Click(sender As Object, e As System.EventArgs)
            RaiseEvent CancelRequested(Me, System.EventArgs.Empty)
        End Sub
        Private Sub UpdateColors()
            Dim c As Color = Color.FromArgb(CInt(Me.faderRed.Ratio), CInt(Me.faderGreen.Ratio), CInt(Me.faderBlue.Ratio))
            Me.faderRed.Color1 = Color.FromArgb(0, CInt(c.G), CInt(c.B))
            Me.faderRed.Color2 = Color.FromArgb(255, CInt(c.G), CInt(c.B))
            Me.faderGreen.Color1 = Color.FromArgb(CInt(c.R), 0, CInt(c.B))
            Me.faderGreen.Color2 = Color.FromArgb(CInt(c.R), 255, CInt(c.B))
            Me.faderBlue.Color1 = Color.FromArgb(CInt(c.R), CInt(c.G), 0)
            Me.faderBlue.Color2 = Color.FromArgb(CInt(c.R), CInt(c.G), 255)
            Dim hsl As New HslColor(Me.faderHue.Ratio, Me.faderSaturation.Ratio, Me.faderLightness.Ratio)
            Me.faderSaturation.Color1 = ColorMath.HslToRgb(New HslColor(hsl.H, 0, hsl.L))
            Me.faderSaturation.Color2 = ColorMath.HslToRgb(New HslColor(hsl.H, 255, hsl.L))
            Me.faderLightness.Color1 = ColorMath.HslToRgb(New HslColor(hsl.H, hsl.S, 0))
            Me.faderLightness.ColorMid = ColorMath.HslToRgb(New HslColor(hsl.H, hsl.S, 128))
            Me.faderLightness.Color2 = ColorMath.HslToRgb(New HslColor(hsl.H, hsl.S, 255))
            If Not Me.updatingRGBText Then
                Me.updatingRGBText = True
                Me.textRGB.Text = String.Concat(New Object() {"rgb(", c.R, ", ", c.G, ", ", c.B, _
                 ")"})
                Me.textRGB.BackColor = Color.Empty
                Me.updatingRGBText = False
            End If
            If Not Me.updatingHexText Then
                Me.updatingHexText = True
                Me.textHex.Text = "#" & c.R.ToString("x2") & c.G.ToString("x2") & c.B.ToString("x2")
                Me.textHex.BackColor = Color.Empty
                Me.updatingHexText = False
            End If
            Me.colorWheel1.Hue = hsl.H
            Me.colorWheel1.Saturation = hsl.S
            Me.colorWheel1.Lightness = hsl.L
        End Sub
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso Me.components IsNot Nothing Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
        Private Sub InitializeComponent()
            Me.tableLayoutPanel1 = New TableLayoutPanel()
            Me.BlueLabel = New Label()
            Me.GreenLabel = New Label()
            Me.numericBlue = New NumericUpDown()
            Me.numericGreen = New NumericUpDown()
            Me.numericRed = New NumericUpDown()
            Me.HueLabel = New Label()
            Me.SaturationLabel = New Label()
            Me.LightnessLabel = New Label()
            Me.numericHue = New NumericUpDown()
            Me.numericSaturation = New NumericUpDown()
            Me.numericLightness = New NumericUpDown()
            Me.HTMLLabel = New Label()
            Me.textRGB = New TextBox()
            Me.textHex = New TextBox()
            Me.CSSrgbLabel = New Label()
            Me.RLabel = New Label()
            Me.GLabel = New Label()
            Me.BLabel = New Label()
            Me.HLabel = New Label()
            Me.SLabel = New Label()
            Me.LLabel = New Label()
            Me.RedLabel = New Label()
            Me.ExtendedViewCheck = New CheckBox()
            Me.flowLayoutPanel1 = New FlowLayoutPanel()
            Me.pictureBox2 = New PictureBox()
            Me.pictureBox3 = New PictureBox()
            Me.pictureBox4 = New PictureBox()
            Me.pictureBox5 = New PictureBox()
            Me.pictureBox6 = New PictureBox()
            Me.pictureBox7 = New PictureBox()
            Me.pictureBox8 = New PictureBox()
            Me.pictureBox9 = New PictureBox()
            Me.pictureBox10 = New PictureBox()
            Me.pictureBox11 = New PictureBox()
            Me.pictureBox12 = New PictureBox()
            Me.pictureBox13 = New PictureBox()
            Me.pictureBox14 = New PictureBox()
            Me.pictureBox15 = New PictureBox()
            Me.pictureBox16 = New PictureBox()
            Me.pictureBox17 = New PictureBox()
            Me.pictureBox18 = New PictureBox()
            Me.pictureBox19 = New PictureBox()
            Me.pictureBox20 = New PictureBox()
            Me.pictureBox21 = New PictureBox()
            Me.pictureBox22 = New PictureBox()
            Me.pictureBox23 = New PictureBox()
            Me.pictureBox24 = New PictureBox()
            Me.pictureBox25 = New PictureBox()
            Me.pictureBox26 = New PictureBox()
            Me.pictureBox27 = New PictureBox()
            Me.pictureBox28 = New PictureBox()
            Me.pictureBox29 = New PictureBox()
            Me.pictureBox30 = New PictureBox()
            Me.pictureBox31 = New PictureBox()
            Me.pictureBox32 = New PictureBox()
            Me.pictureBox33 = New PictureBox()
            Me.pictureBox34 = New PictureBox()
            Me.pictureBox35 = New PictureBox()
            Me.pictureBox36 = New PictureBox()
            Me.pictureBox37 = New PictureBox()
            Me.pictureBox38 = New PictureBox()
            Me.pictureBox39 = New PictureBox()
            Me.pictureBox40 = New PictureBox()
            Me.pictureBox41 = New PictureBox()
            Me.pictureBox42 = New PictureBox()
            Me.pictureBox43 = New PictureBox()
            Me.pictureBox44 = New PictureBox()
            Me.pictureBox45 = New PictureBox()
            Me.pictureBox46 = New PictureBox()
            Me.pictureBox47 = New PictureBox()
            Me.pictureBox48 = New PictureBox()
            Me.pictureBox49 = New PictureBox()
            Me.ActionsPanel = New FlowLayoutPanel()
            Me.OKActionButton = New Button()
            Me.CancelActionButton = New Button()
            Me.faderRed = New ColorFader()
            Me.faderGreen = New ColorFader()
            Me.faderBlue = New ColorFader()
            Me.faderHue = New ColorFader()
            Me.faderSaturation = New ColorFader()
            Me.faderLightness = New ColorFader()
            Me.colorWheel1 = New ColorWheel()
            Me.tableLayoutPanel1.SuspendLayout()
            DirectCast(Me.numericBlue, ISupportInitialize).BeginInit()
            DirectCast(Me.numericGreen, ISupportInitialize).BeginInit()
            DirectCast(Me.numericRed, ISupportInitialize).BeginInit()
            DirectCast(Me.numericHue, ISupportInitialize).BeginInit()
            DirectCast(Me.numericSaturation, ISupportInitialize).BeginInit()
            DirectCast(Me.numericLightness, ISupportInitialize).BeginInit()
            Me.flowLayoutPanel1.SuspendLayout()
            DirectCast(Me.pictureBox2, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox3, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox4, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox5, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox6, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox7, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox8, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox9, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox10, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox11, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox12, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox13, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox14, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox15, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox16, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox17, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox18, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox19, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox20, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox21, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox22, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox23, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox24, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox25, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox26, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox27, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox28, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox29, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox30, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox31, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox32, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox33, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox34, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox35, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox36, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox37, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox38, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox39, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox40, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox41, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox42, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox43, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox44, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox45, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox46, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox47, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox48, ISupportInitialize).BeginInit()
            DirectCast(Me.pictureBox49, ISupportInitialize).BeginInit()
            Me.ActionsPanel.SuspendLayout()
            MyBase.SuspendLayout()
            Me.tableLayoutPanel1.AutoSize = True
            Me.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Me.tableLayoutPanel1.ColumnCount = 6
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            Me.tableLayoutPanel1.Controls.Add(Me.BlueLabel, 2, 3)
            Me.tableLayoutPanel1.Controls.Add(Me.GreenLabel, 2, 2)
            Me.tableLayoutPanel1.Controls.Add(Me.numericBlue, 5, 3)
            Me.tableLayoutPanel1.Controls.Add(Me.numericGreen, 5, 2)
            Me.tableLayoutPanel1.Controls.Add(Me.numericRed, 5, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.HueLabel, 2, 4)
            Me.tableLayoutPanel1.Controls.Add(Me.SaturationLabel, 2, 5)
            Me.tableLayoutPanel1.Controls.Add(Me.LightnessLabel, 2, 6)
            Me.tableLayoutPanel1.Controls.Add(Me.numericHue, 5, 4)
            Me.tableLayoutPanel1.Controls.Add(Me.numericSaturation, 5, 5)
            Me.tableLayoutPanel1.Controls.Add(Me.numericLightness, 5, 6)
            Me.tableLayoutPanel1.Controls.Add(Me.HTMLLabel, 2, 8)
            Me.tableLayoutPanel1.Controls.Add(Me.textRGB, 4, 7)
            Me.tableLayoutPanel1.Controls.Add(Me.textHex, 4, 8)
            Me.tableLayoutPanel1.Controls.Add(Me.CSSrgbLabel, 2, 7)
            Me.tableLayoutPanel1.Controls.Add(Me.faderRed, 4, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.faderGreen, 4, 2)
            Me.tableLayoutPanel1.Controls.Add(Me.faderBlue, 4, 3)
            Me.tableLayoutPanel1.Controls.Add(Me.faderHue, 4, 4)
            Me.tableLayoutPanel1.Controls.Add(Me.faderSaturation, 4, 5)
            Me.tableLayoutPanel1.Controls.Add(Me.faderLightness, 4, 6)
            Me.tableLayoutPanel1.Controls.Add(Me.RLabel, 3, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.GLabel, 3, 2)
            Me.tableLayoutPanel1.Controls.Add(Me.BLabel, 3, 3)
            Me.tableLayoutPanel1.Controls.Add(Me.HLabel, 3, 4)
            Me.tableLayoutPanel1.Controls.Add(Me.SLabel, 3, 5)
            Me.tableLayoutPanel1.Controls.Add(Me.LLabel, 3, 6)
            Me.tableLayoutPanel1.Controls.Add(Me.RedLabel, 2, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.colorWheel1, 1, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.ExtendedViewCheck, 0, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.flowLayoutPanel1, 0, 1)
            Me.tableLayoutPanel1.Controls.Add(Me.ActionsPanel, 0, 8)
            Me.tableLayoutPanel1.Location = New Point(0, 0)
            Me.tableLayoutPanel1.Margin = New Padding(0)
            Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
            Me.tableLayoutPanel1.RowCount = 9
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle())
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
            Me.tableLayoutPanel1.Size = New Size(539, 204)
            Me.tableLayoutPanel1.TabIndex = 0
            Me.BlueLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.BlueLabel.AutoSize = True
            Me.BlueLabel.ForeColor = Color.Blue
            Me.BlueLabel.Location = New Point(311, 48)
            Me.BlueLabel.Margin = New Padding(20, 4, 0, 0)
            Me.BlueLabel.Name = "BlueLabel"
            Me.BlueLabel.Size = New Size(31, 20)
            Me.BlueLabel.TabIndex = 11
            Me.BlueLabel.Text = "Blau:"
            Me.BlueLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.GreenLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.GreenLabel.AutoSize = True
            Me.GreenLabel.ForeColor = Color.Green
            Me.GreenLabel.Location = New Point(311, 24)
            Me.GreenLabel.Margin = New Padding(20, 4, 0, 0)
            Me.GreenLabel.Name = "GreenLabel"
            Me.GreenLabel.Size = New Size(33, 20)
            Me.GreenLabel.TabIndex = 7
            Me.GreenLabel.Text = "Grün:"
            Me.GreenLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.numericBlue.Location = New Point(489, 48)
            Me.numericBlue.Margin = New Padding(4, 4, 0, 0)
            Dim arg_DBB_0 As NumericUpDown = Me.numericBlue
            Dim array As Integer() = New Integer(3) {}
            array(0) = 255
            arg_DBB_0.Maximum = New Decimal(array)
            Me.numericBlue.Name = "numericBlue"
            Me.numericBlue.Size = New Size(50, 20)
            Me.numericBlue.TabIndex = 14
            Me.numericGreen.Location = New Point(489, 24)
            Me.numericGreen.Margin = New Padding(4, 4, 0, 0)
            Dim arg_E3D_0 As NumericUpDown = Me.numericGreen
            array = New Integer(3) {}
            array(0) = 255
            arg_E3D_0.Maximum = New Decimal(array)
            Me.numericGreen.Name = "numericGreen"
            Me.numericGreen.Size = New Size(50, 20)
            Me.numericGreen.TabIndex = 10
            Me.numericRed.Location = New Point(489, 0)
            Me.numericRed.Margin = New Padding(4, 0, 0, 0)
            Dim arg_EBE_0 As NumericUpDown = Me.numericRed
            array = New Integer(3) {}
            array(0) = 255
            arg_EBE_0.Maximum = New Decimal(array)
            Me.numericRed.Name = "numericRed"
            Me.tableLayoutPanel1.SetRowSpan(Me.numericRed, 2)
            Me.numericRed.Size = New Size(50, 20)
            Me.numericRed.TabIndex = 6
            Me.HueLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.HueLabel.AutoSize = True
            Me.HueLabel.Location = New Point(311, 80)
            Me.HueLabel.Margin = New Padding(20, 12, 0, 0)
            Me.HueLabel.Name = "HueLabel"
            Me.HueLabel.Size = New Size(46, 20)
            Me.HueLabel.TabIndex = 15
            Me.HueLabel.Text = "Farbton:"
            Me.HueLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.SaturationLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.SaturationLabel.AutoSize = True
            Me.SaturationLabel.Location = New Point(311, 104)
            Me.SaturationLabel.Margin = New Padding(20, 4, 0, 0)
            Me.SaturationLabel.Name = "SaturationLabel"
            Me.SaturationLabel.Size = New Size(55, 20)
            Me.SaturationLabel.TabIndex = 19
            Me.SaturationLabel.Text = "Sättigung:"
            Me.SaturationLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.LightnessLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.LightnessLabel.AutoSize = True
            Me.LightnessLabel.Location = New Point(311, 128)
            Me.LightnessLabel.Margin = New Padding(20, 4, 0, 0)
            Me.LightnessLabel.Name = "LightnessLabel"
            Me.LightnessLabel.Size = New Size(53, 20)
            Me.LightnessLabel.TabIndex = 23
            Me.LightnessLabel.Text = "Helligkeit:"
            Me.LightnessLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.numericHue.Location = New Point(489, 80)
            Me.numericHue.Margin = New Padding(4, 12, 0, 0)
            Dim arg_1128_0 As NumericUpDown = Me.numericHue
            array = New Integer(3) {}
            array(0) = 256
            arg_1128_0.Maximum = New Decimal(array)
            Me.numericHue.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
            Me.numericHue.Name = "numericHue"
            Me.numericHue.Size = New Size(50, 20)
            Me.numericHue.TabIndex = 18
            Me.numericSaturation.Location = New Point(489, 104)
            Me.numericSaturation.Margin = New Padding(4, 4, 0, 0)
            Dim arg_11CF_0 As NumericUpDown = Me.numericSaturation
            array = New Integer(3) {}
            array(0) = 255
            arg_11CF_0.Maximum = New Decimal(array)
            Me.numericSaturation.Name = "numericSaturation"
            Me.numericSaturation.Size = New Size(50, 20)
            Me.numericSaturation.TabIndex = 22
            Me.numericLightness.Location = New Point(489, 128)
            Me.numericLightness.Margin = New Padding(4, 4, 0, 0)
            Dim arg_1254_0 As NumericUpDown = Me.numericLightness
            array = New Integer(3) {}
            array(0) = 255
            arg_1254_0.Maximum = New Decimal(array)
            Me.numericLightness.Name = "numericLightness"
            Me.numericLightness.Size = New Size(50, 20)
            Me.numericLightness.TabIndex = 26
            Me.HTMLLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.HTMLLabel.AutoSize = True
            Me.tableLayoutPanel1.SetColumnSpan(Me.HTMLLabel, 2)
            Me.HTMLLabel.Location = New Point(311, 184)
            Me.HTMLLabel.Margin = New Padding(20, 4, 0, 0)
            Me.HTMLLabel.Name = "HTMLLabel"
            Me.HTMLLabel.Size = New Size(40, 20)
            Me.HTMLLabel.TabIndex = 29
            Me.HTMLLabel.Text = "HTML:"
            Me.HTMLLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.textRGB.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.tableLayoutPanel1.SetColumnSpan(Me.textRGB, 2)
            Me.textRGB.Location = New Point(385, 160)
            Me.textRGB.Margin = New Padding(4, 12, 0, 0)
            Me.textRGB.Name = "textRGB"
            Me.textRGB.Size = New Size(154, 20)
            Me.textRGB.TabIndex = 28
            AddHandler Me.textRGB.TextChanged, AddressOf Me.textRGB_TextChanged
            Me.textHex.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.tableLayoutPanel1.SetColumnSpan(Me.textHex, 2)
            Me.textHex.Location = New Point(385, 184)
            Me.textHex.Margin = New Padding(4, 4, 0, 0)
            Me.textHex.Name = "textHex"
            Me.textHex.Size = New Size(154, 20)
            Me.textHex.TabIndex = 30
            AddHandler Me.textHex.TextChanged, AddressOf Me.textHex_TextChanged
            Me.CSSrgbLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.CSSrgbLabel.AutoSize = True
            Me.tableLayoutPanel1.SetColumnSpan(Me.CSSrgbLabel, 2)
            Me.CSSrgbLabel.Location = New Point(311, 160)
            Me.CSSrgbLabel.Margin = New Padding(20, 12, 0, 0)
            Me.CSSrgbLabel.Name = "CSSrgbLabel"
            Me.CSSrgbLabel.Size = New Size(49, 20)
            Me.CSSrgbLabel.TabIndex = 27
            Me.CSSrgbLabel.Text = "CSS rgb:"
            Me.CSSrgbLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.RLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.RLabel.AutoSize = True
            Me.RLabel.ForeColor = Color.Firebrick
            Me.RLabel.Location = New Point(366, 0)
            Me.RLabel.Margin = New Padding(0)
            Me.RLabel.Name = "RLabel"
            Me.tableLayoutPanel1.SetRowSpan(Me.RLabel, 2)
            Me.RLabel.Size = New Size(15, 20)
            Me.RLabel.TabIndex = 4
            Me.RLabel.Text = "R"
            Me.RLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.GLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.GLabel.AutoSize = True
            Me.GLabel.ForeColor = Color.Green
            Me.GLabel.Location = New Point(366, 24)
            Me.GLabel.Margin = New Padding(0, 4, 0, 0)
            Me.GLabel.Name = "GLabel"
            Me.GLabel.Size = New Size(15, 20)
            Me.GLabel.TabIndex = 8
            Me.GLabel.Text = "G"
            Me.GLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.BLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.BLabel.AutoSize = True
            Me.BLabel.ForeColor = Color.Blue
            Me.BLabel.Location = New Point(366, 48)
            Me.BLabel.Margin = New Padding(0, 4, 0, 0)
            Me.BLabel.Name = "BLabel"
            Me.BLabel.Size = New Size(15, 20)
            Me.BLabel.TabIndex = 12
            Me.BLabel.Text = "B"
            Me.BLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.HLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.HLabel.AutoSize = True
            Me.HLabel.Location = New Point(366, 80)
            Me.HLabel.Margin = New Padding(0, 12, 0, 0)
            Me.HLabel.Name = "HLabel"
            Me.HLabel.Size = New Size(15, 20)
            Me.HLabel.TabIndex = 16
            Me.HLabel.Text = "H"
            Me.HLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.SLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.SLabel.AutoSize = True
            Me.SLabel.Location = New Point(366, 104)
            Me.SLabel.Margin = New Padding(0, 4, 0, 0)
            Me.SLabel.Name = "SLabel"
            Me.SLabel.Size = New Size(15, 20)
            Me.SLabel.TabIndex = 20
            Me.SLabel.Text = "S"
            Me.SLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.LLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right)
            Me.LLabel.AutoSize = True
            Me.LLabel.Location = New Point(366, 128)
            Me.LLabel.Margin = New Padding(0, 4, 0, 0)
            Me.LLabel.Name = "LLabel"
            Me.LLabel.Size = New Size(15, 20)
            Me.LLabel.TabIndex = 24
            Me.LLabel.Text = "L"
            Me.LLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.RedLabel.Anchor = (AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.RedLabel.AutoSize = True
            Me.RedLabel.ForeColor = Color.Firebrick
            Me.RedLabel.Location = New Point(311, 0)
            Me.RedLabel.Margin = New Padding(20, 0, 0, 0)
            Me.RedLabel.Name = "RedLabel"
            Me.tableLayoutPanel1.SetRowSpan(Me.RedLabel, 2)
            Me.RedLabel.Size = New Size(27, 20)
            Me.RedLabel.TabIndex = 3
            Me.RedLabel.Text = "Rot:"
            Me.RedLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.ExtendedViewCheck.AutoSize = True
            Me.ExtendedViewCheck.Location = New Point(0, 0)
            Me.ExtendedViewCheck.Margin = New Padding(0)
            Me.ExtendedViewCheck.Name = "ExtendedViewCheck"
            Me.ExtendedViewCheck.Size = New Size(111, 17)
            Me.ExtendedViewCheck.TabIndex = 0
            Me.ExtendedViewCheck.Text = "Erweiterte Ansicht"
            Me.ExtendedViewCheck.UseVisualStyleBackColor = True
            AddHandler Me.ExtendedViewCheck.CheckedChanged, AddressOf Me.ExtendedViewCheck_CheckedChanged
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox2)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox3)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox4)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox5)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox6)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox7)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox8)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox9)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox10)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox11)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox12)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox13)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox14)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox15)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox16)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox17)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox18)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox19)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox20)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox21)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox22)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox23)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox24)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox25)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox26)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox27)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox28)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox29)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox30)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox31)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox32)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox33)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox34)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox35)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox36)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox37)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox38)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox39)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox40)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox41)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox42)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox43)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox44)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox45)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox46)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox47)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox48)
            Me.flowLayoutPanel1.Controls.Add(Me.pictureBox49)
            Me.flowLayoutPanel1.Location = New Point(0, 21)
            Me.flowLayoutPanel1.Margin = New Padding(0, 4, 0, 0)
            Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
            Me.tableLayoutPanel1.SetRowSpan(Me.flowLayoutPanel1, 7)
            Me.flowLayoutPanel1.Size = New Size(104, 135)
            Me.flowLayoutPanel1.TabIndex = 1
            Me.pictureBox2.BackColor = Color.Black
            Me.pictureBox2.Location = New Point(0, 0)
            Me.pictureBox2.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox2.Name = "pictureBox2"
            Me.pictureBox2.Size = New Size(16, 16)
            Me.pictureBox2.TabIndex = 7
            Me.pictureBox2.TabStop = False
            AddHandler Me.pictureBox2.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox3.BackColor = Color.FromArgb(64, 64, 64)
            Me.pictureBox3.Location = New Point(17, 0)
            Me.pictureBox3.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox3.Name = "pictureBox3"
            Me.pictureBox3.Size = New Size(16, 16)
            Me.pictureBox3.TabIndex = 7
            Me.pictureBox3.TabStop = False
            AddHandler Me.pictureBox3.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox4.BackColor = Color.Gray
            Me.pictureBox4.Location = New Point(34, 0)
            Me.pictureBox4.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox4.Name = "pictureBox4"
            Me.pictureBox4.Size = New Size(16, 16)
            Me.pictureBox4.TabIndex = 7
            Me.pictureBox4.TabStop = False
            AddHandler Me.pictureBox4.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox5.BackColor = Color.Silver
            Me.pictureBox5.Location = New Point(51, 0)
            Me.pictureBox5.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox5.Name = "pictureBox5"
            Me.pictureBox5.Size = New Size(16, 16)
            Me.pictureBox5.TabIndex = 7
            Me.pictureBox5.TabStop = False
            AddHandler Me.pictureBox5.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox6.BackColor = Color.FromArgb(224, 224, 224)
            Me.pictureBox6.Location = New Point(68, 0)
            Me.pictureBox6.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox6.Name = "pictureBox6"
            Me.pictureBox6.Size = New Size(16, 16)
            Me.pictureBox6.TabIndex = 7
            Me.pictureBox6.TabStop = False
            AddHandler Me.pictureBox6.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox7.BackColor = Color.White
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox7, True)
            Me.pictureBox7.Location = New Point(85, 0)
            Me.pictureBox7.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox7.Name = "pictureBox7"
            Me.pictureBox7.Size = New Size(16, 16)
            Me.pictureBox7.TabIndex = 7
            Me.pictureBox7.TabStop = False
            AddHandler Me.pictureBox7.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox8.BackColor = Color.FromArgb(64, 0, 0)
            Me.pictureBox8.Location = New Point(0, 17)
            Me.pictureBox8.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox8.Name = "pictureBox8"
            Me.pictureBox8.Size = New Size(16, 16)
            Me.pictureBox8.TabIndex = 11
            Me.pictureBox8.TabStop = False
            AddHandler Me.pictureBox8.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox9.BackColor = Color.Maroon
            Me.pictureBox9.Location = New Point(17, 17)
            Me.pictureBox9.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox9.Name = "pictureBox9"
            Me.pictureBox9.Size = New Size(16, 16)
            Me.pictureBox9.TabIndex = 12
            Me.pictureBox9.TabStop = False
            AddHandler Me.pictureBox9.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox10.BackColor = Color.FromArgb(192, 0, 0)
            Me.pictureBox10.Location = New Point(34, 17)
            Me.pictureBox10.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox10.Name = "pictureBox10"
            Me.pictureBox10.Size = New Size(16, 16)
            Me.pictureBox10.TabIndex = 13
            Me.pictureBox10.TabStop = False
            AddHandler Me.pictureBox10.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox11.BackColor = Color.Red
            Me.pictureBox11.Location = New Point(51, 17)
            Me.pictureBox11.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox11.Name = "pictureBox11"
            Me.pictureBox11.Size = New Size(16, 16)
            Me.pictureBox11.TabIndex = 8
            Me.pictureBox11.TabStop = False
            AddHandler Me.pictureBox11.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox12.BackColor = Color.FromArgb(255, 128, 128)
            Me.pictureBox12.Location = New Point(68, 17)
            Me.pictureBox12.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox12.Name = "pictureBox12"
            Me.pictureBox12.Size = New Size(16, 16)
            Me.pictureBox12.TabIndex = 9
            Me.pictureBox12.TabStop = False
            AddHandler Me.pictureBox12.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox13.BackColor = Color.FromArgb(255, 192, 192)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox13, True)
            Me.pictureBox13.Location = New Point(85, 17)
            Me.pictureBox13.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox13.Name = "pictureBox13"
            Me.pictureBox13.Size = New Size(16, 16)
            Me.pictureBox13.TabIndex = 10
            Me.pictureBox13.TabStop = False
            AddHandler Me.pictureBox13.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox14.BackColor = Color.FromArgb(128, 64, 64)
            Me.pictureBox14.Location = New Point(0, 34)
            Me.pictureBox14.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox14.Name = "pictureBox14"
            Me.pictureBox14.Size = New Size(16, 16)
            Me.pictureBox14.TabIndex = 17
            Me.pictureBox14.TabStop = False
            AddHandler Me.pictureBox14.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox15.BackColor = Color.FromArgb(128, 64, 0)
            Me.pictureBox15.Location = New Point(17, 34)
            Me.pictureBox15.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox15.Name = "pictureBox15"
            Me.pictureBox15.Size = New Size(16, 16)
            Me.pictureBox15.TabIndex = 18
            Me.pictureBox15.TabStop = False
            AddHandler Me.pictureBox15.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox16.BackColor = Color.FromArgb(192, 64, 0)
            Me.pictureBox16.Location = New Point(34, 34)
            Me.pictureBox16.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox16.Name = "pictureBox16"
            Me.pictureBox16.Size = New Size(16, 16)
            Me.pictureBox16.TabIndex = 19
            Me.pictureBox16.TabStop = False
            AddHandler Me.pictureBox16.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox17.BackColor = Color.FromArgb(255, 128, 0)
            Me.pictureBox17.Location = New Point(51, 34)
            Me.pictureBox17.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox17.Name = "pictureBox17"
            Me.pictureBox17.Size = New Size(16, 16)
            Me.pictureBox17.TabIndex = 14
            Me.pictureBox17.TabStop = False
            AddHandler Me.pictureBox17.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox18.BackColor = Color.FromArgb(255, 192, 128)
            Me.pictureBox18.Location = New Point(68, 34)
            Me.pictureBox18.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox18.Name = "pictureBox18"
            Me.pictureBox18.Size = New Size(16, 16)
            Me.pictureBox18.TabIndex = 15
            Me.pictureBox18.TabStop = False
            AddHandler Me.pictureBox18.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox19.BackColor = Color.FromArgb(255, 224, 192)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox19, True)
            Me.pictureBox19.Location = New Point(85, 34)
            Me.pictureBox19.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox19.Name = "pictureBox19"
            Me.pictureBox19.Size = New Size(16, 16)
            Me.pictureBox19.TabIndex = 16
            Me.pictureBox19.TabStop = False
            AddHandler Me.pictureBox19.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox20.BackColor = Color.FromArgb(64, 64, 0)
            Me.pictureBox20.Location = New Point(0, 51)
            Me.pictureBox20.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox20.Name = "pictureBox20"
            Me.pictureBox20.Size = New Size(16, 16)
            Me.pictureBox20.TabIndex = 23
            Me.pictureBox20.TabStop = False
            AddHandler Me.pictureBox20.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox21.BackColor = Color.Olive
            Me.pictureBox21.Location = New Point(17, 51)
            Me.pictureBox21.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox21.Name = "pictureBox21"
            Me.pictureBox21.Size = New Size(16, 16)
            Me.pictureBox21.TabIndex = 24
            Me.pictureBox21.TabStop = False
            AddHandler Me.pictureBox21.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox22.BackColor = Color.FromArgb(192, 192, 0)
            Me.pictureBox22.Location = New Point(34, 51)
            Me.pictureBox22.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox22.Name = "pictureBox22"
            Me.pictureBox22.Size = New Size(16, 16)
            Me.pictureBox22.TabIndex = 25
            Me.pictureBox22.TabStop = False
            AddHandler Me.pictureBox22.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox23.BackColor = Color.Yellow
            Me.pictureBox23.Location = New Point(51, 51)
            Me.pictureBox23.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox23.Name = "pictureBox23"
            Me.pictureBox23.Size = New Size(16, 16)
            Me.pictureBox23.TabIndex = 20
            Me.pictureBox23.TabStop = False
            AddHandler Me.pictureBox23.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox24.BackColor = Color.FromArgb(255, 255, 128)
            Me.pictureBox24.Location = New Point(68, 51)
            Me.pictureBox24.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox24.Name = "pictureBox24"
            Me.pictureBox24.Size = New Size(16, 16)
            Me.pictureBox24.TabIndex = 21
            Me.pictureBox24.TabStop = False
            AddHandler Me.pictureBox24.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox25.BackColor = Color.FromArgb(255, 255, 192)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox25, True)
            Me.pictureBox25.Location = New Point(85, 51)
            Me.pictureBox25.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox25.Name = "pictureBox25"
            Me.pictureBox25.Size = New Size(16, 16)
            Me.pictureBox25.TabIndex = 22
            Me.pictureBox25.TabStop = False
            AddHandler Me.pictureBox25.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox26.BackColor = Color.FromArgb(0, 64, 0)
            Me.pictureBox26.Location = New Point(0, 68)
            Me.pictureBox26.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox26.Name = "pictureBox26"
            Me.pictureBox26.Size = New Size(16, 16)
            Me.pictureBox26.TabIndex = 29
            Me.pictureBox26.TabStop = False
            AddHandler Me.pictureBox26.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox27.BackColor = Color.Green
            Me.pictureBox27.Location = New Point(17, 68)
            Me.pictureBox27.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox27.Name = "pictureBox27"
            Me.pictureBox27.Size = New Size(16, 16)
            Me.pictureBox27.TabIndex = 30
            Me.pictureBox27.TabStop = False
            AddHandler Me.pictureBox27.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox28.BackColor = Color.FromArgb(0, 192, 0)
            Me.pictureBox28.Location = New Point(34, 68)
            Me.pictureBox28.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox28.Name = "pictureBox28"
            Me.pictureBox28.Size = New Size(16, 16)
            Me.pictureBox28.TabIndex = 31
            Me.pictureBox28.TabStop = False
            AddHandler Me.pictureBox28.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox29.BackColor = Color.Lime
            Me.pictureBox29.Location = New Point(51, 68)
            Me.pictureBox29.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox29.Name = "pictureBox29"
            Me.pictureBox29.Size = New Size(16, 16)
            Me.pictureBox29.TabIndex = 26
            Me.pictureBox29.TabStop = False
            AddHandler Me.pictureBox29.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox30.BackColor = Color.FromArgb(128, 255, 128)
            Me.pictureBox30.Location = New Point(68, 68)
            Me.pictureBox30.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox30.Name = "pictureBox30"
            Me.pictureBox30.Size = New Size(16, 16)
            Me.pictureBox30.TabIndex = 27
            Me.pictureBox30.TabStop = False
            AddHandler Me.pictureBox30.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox31.BackColor = Color.FromArgb(192, 255, 192)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox31, True)
            Me.pictureBox31.Location = New Point(85, 68)
            Me.pictureBox31.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox31.Name = "pictureBox31"
            Me.pictureBox31.Size = New Size(16, 16)
            Me.pictureBox31.TabIndex = 28
            Me.pictureBox31.TabStop = False
            AddHandler Me.pictureBox31.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox32.BackColor = Color.FromArgb(0, 64, 64)
            Me.pictureBox32.Location = New Point(0, 85)
            Me.pictureBox32.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox32.Name = "pictureBox32"
            Me.pictureBox32.Size = New Size(16, 16)
            Me.pictureBox32.TabIndex = 35
            Me.pictureBox32.TabStop = False
            AddHandler Me.pictureBox32.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox33.BackColor = Color.Teal
            Me.pictureBox33.Location = New Point(17, 85)
            Me.pictureBox33.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox33.Name = "pictureBox33"
            Me.pictureBox33.Size = New Size(16, 16)
            Me.pictureBox33.TabIndex = 36
            Me.pictureBox33.TabStop = False
            AddHandler Me.pictureBox33.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox34.BackColor = Color.FromArgb(0, 192, 192)
            Me.pictureBox34.Location = New Point(34, 85)
            Me.pictureBox34.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox34.Name = "pictureBox34"
            Me.pictureBox34.Size = New Size(16, 16)
            Me.pictureBox34.TabIndex = 37
            Me.pictureBox34.TabStop = False
            AddHandler Me.pictureBox34.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox35.BackColor = Color.Cyan
            Me.pictureBox35.Location = New Point(51, 85)
            Me.pictureBox35.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox35.Name = "pictureBox35"
            Me.pictureBox35.Size = New Size(16, 16)
            Me.pictureBox35.TabIndex = 32
            Me.pictureBox35.TabStop = False
            AddHandler Me.pictureBox35.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox36.BackColor = Color.FromArgb(128, 255, 255)
            Me.pictureBox36.Location = New Point(68, 85)
            Me.pictureBox36.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox36.Name = "pictureBox36"
            Me.pictureBox36.Size = New Size(16, 16)
            Me.pictureBox36.TabIndex = 33
            Me.pictureBox36.TabStop = False
            AddHandler Me.pictureBox36.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox37.BackColor = Color.FromArgb(192, 255, 255)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox37, True)
            Me.pictureBox37.Location = New Point(85, 85)
            Me.pictureBox37.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox37.Name = "pictureBox37"
            Me.pictureBox37.Size = New Size(16, 16)
            Me.pictureBox37.TabIndex = 34
            Me.pictureBox37.TabStop = False
            AddHandler Me.pictureBox37.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox38.BackColor = Color.FromArgb(0, 0, 64)
            Me.pictureBox38.Location = New Point(0, 102)
            Me.pictureBox38.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox38.Name = "pictureBox38"
            Me.pictureBox38.Size = New Size(16, 16)
            Me.pictureBox38.TabIndex = 41
            Me.pictureBox38.TabStop = False
            AddHandler Me.pictureBox38.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox39.BackColor = Color.Navy
            Me.pictureBox39.Location = New Point(17, 102)
            Me.pictureBox39.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox39.Name = "pictureBox39"
            Me.pictureBox39.Size = New Size(16, 16)
            Me.pictureBox39.TabIndex = 42
            Me.pictureBox39.TabStop = False
            AddHandler Me.pictureBox39.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox40.BackColor = Color.FromArgb(0, 0, 192)
            Me.pictureBox40.Location = New Point(34, 102)
            Me.pictureBox40.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox40.Name = "pictureBox40"
            Me.pictureBox40.Size = New Size(16, 16)
            Me.pictureBox40.TabIndex = 43
            Me.pictureBox40.TabStop = False
            AddHandler Me.pictureBox40.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox41.BackColor = Color.Blue
            Me.pictureBox41.Location = New Point(51, 102)
            Me.pictureBox41.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox41.Name = "pictureBox41"
            Me.pictureBox41.Size = New Size(16, 16)
            Me.pictureBox41.TabIndex = 38
            Me.pictureBox41.TabStop = False
            AddHandler Me.pictureBox41.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox42.BackColor = Color.FromArgb(128, 128, 255)
            Me.pictureBox42.Location = New Point(68, 102)
            Me.pictureBox42.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox42.Name = "pictureBox42"
            Me.pictureBox42.Size = New Size(16, 16)
            Me.pictureBox42.TabIndex = 39
            Me.pictureBox42.TabStop = False
            AddHandler Me.pictureBox42.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox43.BackColor = Color.FromArgb(192, 192, 255)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox43, True)
            Me.pictureBox43.Location = New Point(85, 102)
            Me.pictureBox43.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox43.Name = "pictureBox43"
            Me.pictureBox43.Size = New Size(16, 16)
            Me.pictureBox43.TabIndex = 40
            Me.pictureBox43.TabStop = False
            AddHandler Me.pictureBox43.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox44.BackColor = Color.FromArgb(64, 0, 64)
            Me.pictureBox44.Location = New Point(0, 119)
            Me.pictureBox44.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox44.Name = "pictureBox44"
            Me.pictureBox44.Size = New Size(16, 16)
            Me.pictureBox44.TabIndex = 47
            Me.pictureBox44.TabStop = False
            AddHandler Me.pictureBox44.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox45.BackColor = Color.Purple
            Me.pictureBox45.Location = New Point(17, 119)
            Me.pictureBox45.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox45.Name = "pictureBox45"
            Me.pictureBox45.Size = New Size(16, 16)
            Me.pictureBox45.TabIndex = 48
            Me.pictureBox45.TabStop = False
            AddHandler Me.pictureBox45.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox46.BackColor = Color.FromArgb(192, 0, 192)
            Me.pictureBox46.Location = New Point(34, 119)
            Me.pictureBox46.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox46.Name = "pictureBox46"
            Me.pictureBox46.Size = New Size(16, 16)
            Me.pictureBox46.TabIndex = 49
            Me.pictureBox46.TabStop = False
            AddHandler Me.pictureBox46.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox47.BackColor = Color.Fuchsia
            Me.pictureBox47.Location = New Point(51, 119)
            Me.pictureBox47.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox47.Name = "pictureBox47"
            Me.pictureBox47.Size = New Size(16, 16)
            Me.pictureBox47.TabIndex = 44
            Me.pictureBox47.TabStop = False
            AddHandler Me.pictureBox47.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox48.BackColor = Color.FromArgb(255, 128, 255)
            Me.pictureBox48.Location = New Point(68, 119)
            Me.pictureBox48.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox48.Name = "pictureBox48"
            Me.pictureBox48.Size = New Size(16, 16)
            Me.pictureBox48.TabIndex = 45
            Me.pictureBox48.TabStop = False
            AddHandler Me.pictureBox48.Click, AddressOf Me.picturePalette_Click
            Me.pictureBox49.BackColor = Color.FromArgb(255, 192, 255)
            Me.flowLayoutPanel1.SetFlowBreak(Me.pictureBox49, True)
            Me.pictureBox49.Location = New Point(85, 119)
            Me.pictureBox49.Margin = New Padding(0, 0, 1, 1)
            Me.pictureBox49.Name = "pictureBox49"
            Me.pictureBox49.Size = New Size(16, 16)
            Me.pictureBox49.TabIndex = 46
            Me.pictureBox49.TabStop = False
            AddHandler Me.pictureBox49.Click, AddressOf Me.picturePalette_Click
            Me.ActionsPanel.Anchor = (AnchorStyles.Bottom Or AnchorStyles.Left)
            Me.ActionsPanel.AutoSize = True
            Me.ActionsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Me.tableLayoutPanel1.SetColumnSpan(Me.ActionsPanel, 2)
            Me.ActionsPanel.Controls.Add(Me.OKActionButton)
            Me.ActionsPanel.Controls.Add(Me.CancelActionButton)
            Me.ActionsPanel.Location = New Point(0, 182)
            Me.ActionsPanel.Margin = New Padding(0)
            Me.ActionsPanel.Name = "ActionsPanel"
            Me.ActionsPanel.Size = New Size(156, 22)
            Me.ActionsPanel.TabIndex = 31
            Me.OKActionButton.AutoSize = True
            Me.OKActionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Me.OKActionButton.FlatStyle = FlatStyle.System
            Me.OKActionButton.Location = New Point(0, 0)
            Me.OKActionButton.Margin = New Padding(0)
            Me.OKActionButton.MinimumSize = New Size(75, 0)
            Me.OKActionButton.Name = "OKActionButton"
            Me.OKActionButton.Size = New Size(75, 22)
            Me.OKActionButton.TabIndex = 0
            Me.OKActionButton.Text = "OK"
            Me.OKActionButton.UseVisualStyleBackColor = False
            AddHandler Me.OKActionButton.Click, AddressOf Me.OKActionButton_Click
            Me.CancelActionButton.AutoSize = True
            Me.CancelActionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Me.CancelActionButton.FlatStyle = FlatStyle.System
            Me.CancelActionButton.Location = New Point(81, 0)
            Me.CancelActionButton.Margin = New Padding(6, 0, 0, 0)
            Me.CancelActionButton.MinimumSize = New Size(75, 0)
            Me.CancelActionButton.Name = "CancelActionButton"
            Me.CancelActionButton.Size = New Size(75, 22)
            Me.CancelActionButton.TabIndex = 1
            Me.CancelActionButton.Text = "Abbrechen"
            Me.CancelActionButton.UseVisualStyleBackColor = False
            AddHandler Me.CancelActionButton.Click, AddressOf Me.CancelActionButton_Click
            Me.faderRed.BackColor = Color.Transparent
            Me.faderRed.ColorMid = Color.Empty
            Me.faderRed.Location = New Point(385, 6)
            Me.faderRed.Margin = New Padding(4, 6, 0, 0)
            Me.faderRed.Name = "faderRed"
            Me.faderRed.NumericControl = Me.numericRed
            Me.faderRed.Ratio = 0
            Me.tableLayoutPanel1.SetRowSpan(Me.faderRed, 2)
            Me.faderRed.Size = New Size(100, 14)
            Me.faderRed.TabIndex = 5
            AddHandler Me.faderRed.RatioChanged, AddressOf Me.RGBfader_RatioChanged
            Me.faderGreen.BackColor = Color.Transparent
            Me.faderGreen.ColorMid = Color.Empty
            Me.faderGreen.Location = New Point(385, 30)
            Me.faderGreen.Margin = New Padding(4, 10, 0, 0)
            Me.faderGreen.Name = "faderGreen"
            Me.faderGreen.NumericControl = Me.numericGreen
            Me.faderGreen.Ratio = 0
            Me.faderGreen.Size = New Size(100, 14)
            Me.faderGreen.TabIndex = 9
            AddHandler Me.faderGreen.RatioChanged, AddressOf Me.RGBfader_RatioChanged
            Me.faderBlue.BackColor = Color.Transparent
            Me.faderBlue.ColorMid = Color.Empty
            Me.faderBlue.Location = New Point(385, 54)
            Me.faderBlue.Margin = New Padding(4, 10, 0, 0)
            Me.faderBlue.Name = "faderBlue"
            Me.faderBlue.NumericControl = Me.numericBlue
            Me.faderBlue.Ratio = 0
            Me.faderBlue.Size = New Size(100, 14)
            Me.faderBlue.TabIndex = 13
            AddHandler Me.faderBlue.RatioChanged, AddressOf Me.RGBfader_RatioChanged
            Me.faderHue.BackColor = Color.Transparent
            Me.faderHue.ColorMid = Color.Empty
            Me.faderHue.HueMode = True
            Me.faderHue.Location = New Point(385, 86)
            Me.faderHue.Margin = New Padding(4, 18, 0, 0)
            Me.faderHue.Name = "faderHue"
            Me.faderHue.NumericControl = Me.numericHue
            Me.faderHue.Ratio = 0
            Me.faderHue.Size = New Size(100, 14)
            Me.faderHue.TabIndex = 17
            AddHandler Me.faderHue.RatioChanged, AddressOf Me.HSLfader_RatioChanged
            Me.faderSaturation.BackColor = Color.Transparent
            Me.faderSaturation.ColorMid = Color.Empty
            Me.faderSaturation.Location = New Point(385, 110)
            Me.faderSaturation.Margin = New Padding(4, 10, 0, 0)
            Me.faderSaturation.Name = "faderSaturation"
            Me.faderSaturation.NumericControl = Me.numericSaturation
            Me.faderSaturation.Ratio = 0
            Me.faderSaturation.Size = New Size(100, 14)
            Me.faderSaturation.TabIndex = 21
            AddHandler Me.faderSaturation.RatioChanged, AddressOf Me.HSLfader_RatioChanged
            Me.faderLightness.BackColor = Color.Transparent
            Me.faderLightness.ColorMid = Color.Empty
            Me.faderLightness.Location = New Point(385, 134)
            Me.faderLightness.Margin = New Padding(4, 10, 0, 0)
            Me.faderLightness.Name = "faderLightness"
            Me.faderLightness.NumericControl = Me.numericLightness
            Me.faderLightness.Ratio = 0
            Me.faderLightness.Size = New Size(100, 14)
            Me.faderLightness.TabIndex = 25
            AddHandler Me.faderLightness.RatioChanged, AddressOf Me.HSLfader_RatioChanged
            Me.colorWheel1.BackColor = Color.Transparent
            Me.colorWheel1.Hue = 42
            Me.colorWheel1.Lightness = 255
            Me.colorWheel1.Location = New Point(131, 8)
            Me.colorWheel1.Margin = New Padding(20, 8, 0, 0)
            Me.colorWheel1.Name = "colorWheel1"
            Me.tableLayoutPanel1.SetRowSpan(Me.colorWheel1, 8)
            Me.colorWheel1.Saturation = 255
            Me.colorWheel1.SecondaryHues = Nothing
            Me.colorWheel1.Size = New Size(160, 160)
            Me.colorWheel1.TabIndex = 2
            Me.colorWheel1.Text = "colorWheel1"
            AddHandler Me.colorWheel1.HueChanged, AddressOf Me.colorWheel1_HueChanged
            AddHandler Me.colorWheel1.SLChanged, AddressOf Me.colorWheel1_SLChanged
            MyBase.AutoScaleMode = AutoScaleMode.None
            Me.AutoSize = True
            MyBase.AutoSizeMode = AutoSizeMode.GrowAndShrink
            MyBase.Controls.Add(Me.tableLayoutPanel1)
            MyBase.Name = "ColorSelector"
            MyBase.Size = New Size(539, 204)
            Me.tableLayoutPanel1.ResumeLayout(False)
            Me.tableLayoutPanel1.PerformLayout()
            DirectCast(Me.numericBlue, ISupportInitialize).EndInit()
            DirectCast(Me.numericGreen, ISupportInitialize).EndInit()
            DirectCast(Me.numericRed, ISupportInitialize).EndInit()
            DirectCast(Me.numericHue, ISupportInitialize).EndInit()
            DirectCast(Me.numericSaturation, ISupportInitialize).EndInit()
            DirectCast(Me.numericLightness, ISupportInitialize).EndInit()
            Me.flowLayoutPanel1.ResumeLayout(False)
            DirectCast(Me.pictureBox2, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox3, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox4, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox5, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox6, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox7, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox8, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox9, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox10, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox11, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox12, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox13, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox14, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox15, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox16, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox17, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox18, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox19, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox20, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox21, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox22, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox23, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox24, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox25, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox26, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox27, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox28, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox29, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox30, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox31, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox32, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox33, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox34, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox35, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox36, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox37, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox38, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox39, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox40, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox41, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox42, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox43, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox44, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox45, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox46, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox47, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox48, ISupportInitialize).EndInit()
            DirectCast(Me.pictureBox49, ISupportInitialize).EndInit()
            Me.ActionsPanel.ResumeLayout(False)
            Me.ActionsPanel.PerformLayout()
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub
    End Class

End Namespace