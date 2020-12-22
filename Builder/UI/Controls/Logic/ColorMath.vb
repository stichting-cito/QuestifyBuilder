
Namespace Controls.Logic
    Public Module ColorMath
        Public Function Blend(color1 As Color, color2 As Color, ratio As Double) As Color
            Dim a As Integer = CInt(System.Math.Round(CDec(color1.A) * (1.0 - ratio) + CDec(color2.A) * ratio))
            Dim r As Integer = CInt(System.Math.Round(CDec(color1.R) * (1.0 - ratio) + CDec(color2.R) * ratio))
            Dim g As Integer = CInt(System.Math.Round(CDec(color1.G) * (1.0 - ratio) + CDec(color2.G) * ratio))
            Dim b As Integer = CInt(System.Math.Round(CDec(color1.B) * (1.0 - ratio) + CDec(color2.B) * ratio))
            Return Color.FromArgb(a, r, g, b)
        End Function

        Public Function Darken(color As Color, ratio As Double) As Color
            Return ColorMath.Blend(color, color.Black, ratio)
        End Function

        Public Function Lighten(color As Color, ratio As Double) As Color
            Return ColorMath.Blend(color, color.White, ratio)
        End Function

        Public Function RgbToHsl(rgb As Color) As HslColor
            Dim r As Double = CDec(rgb.R) / 255.0
            Dim g As Double = CDec(rgb.G) / 255.0
            Dim b As Double = CDec(rgb.B) / 255.0
            Dim min As Double = System.Math.Min(System.Math.Min(r, g), b)
            Dim max As Double = System.Math.Max(System.Math.Max(r, g), b)
            Dim i As Double = (max + min) / 2.0
            Dim h As Double
            If max = min Then
                h = 0.0
            Else
                If max = r Then
                    h = 60.0 * (g - b) / (max - min) Mod 360.0
                Else
                    If max = g Then
                        h = (60.0 * (b - r) / (max - min) + 120.0) Mod 360.0
                    Else
                        h = (60.0 * (r - g) / (max - min) + 240.0) Mod 360.0
                    End If
                End If
            End If
            If h < 0.0 Then
                h += 360.0
            End If
            Dim s As Double
            If max = min Then
                s = 0.0
            Else
                If i <= 0.5 Then
                    s = (max - min) / (2.0 * i)
                Else
                    s = (max - min) / (2.0 - 2.0 * i)
                End If
            End If
            Return New HslColor(CByte(System.Math.Round(h / 360.0 * 256.0 Mod 256.0)), CByte(System.Math.Round(s * 255.0)), CByte(System.Math.Round(i * 255.0)))
        End Function

        Public Function HslToRgb(hsl As HslColor) As Color
            Dim h As Double = CDec(hsl.H) / 256.0
            Dim s As Double = CDec(hsl.S) / 255.0
            Dim l As Double = CDec(hsl.L) / 255.0
            Dim q As Double
            If l < 0.5 Then
                q = l * (1.0 + s)
            Else
                q = l + s - l * s
            End If
            Dim p As Double = 2.0 * l - q
            Dim t As Double() = {h + 1.0 / 3, h, h - 1.0 / 3}
            Dim rgb(3) As Byte
            For i As Integer = 0 To 3 - 1
                If t(i) < 0.0 Then t(i) += 1.0
                If t(i) > 1.0 Then t(i) -= 1.0
                If t(i) < 0.16666666666666666 Then
                    rgb(i) = CByte(System.Math.Round((p + (q - p) * 6.0 * t(i)) * 255.0))
                Else
                    If t(i) < 0.5 Then
                        rgb(i) = CByte(System.Math.Round(q * 255.0))
                    Else
                        If t(i) < 0.66666666666666663 Then
                            rgb(i) = CByte(System.Math.Round((p + (q - p) * 6.0 * (0.66666666666666663 - t(i))) * 255.0))
                        Else
                            rgb(i) = CByte(System.Math.Round(p * 255.0))
                        End If
                    End If
                End If
            Next
            Return Color.FromArgb(CInt(rgb(0)), CInt(rgb(1)), CInt(rgb(2)))
        End Function

        Private Function [Mod](dividend As Integer, divisor As Integer) As Integer
            If divisor <= 0 Then
                Throw New System.ArgumentOutOfRangeException("divisor", "The divisor cannot be zero or negative.")
            End If
            Dim i As Integer = dividend Mod divisor
            If i < 0 Then
                i += divisor
            End If
            Return i
        End Function

        Public Function ToGray(c As Color) As Byte
            Return CByte((CDec(c.R) * 0.3 + CDec(c.G) * 0.59 + CDec(c.B) * 0.11))
        End Function

        Public Function IsDarkColor(c As Color) As Boolean
            Return ColorMath.ToGray(c) < 144
        End Function
    End Module
End Namespace
