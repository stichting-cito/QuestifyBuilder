Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic
    Public Class CssBorder

        Private _Style As String = String.Empty
        Private _CssName As String = String.Empty

        Private _bordersStyle As String
        Private _width As Integer?
        Private _color As Color?
        Private _interperterRan As Boolean

        Private Shared _validBorderStyles As HashSet(Of String)
        Private Shared _validColorNames As HashSet(Of String)

        Shared Sub New()
            _validBorderStyles = New HashSet(Of String)(New String() {"none", "hidden", "dotted", "dashed", "solid", "double", "groove", "ridge", "inset", "outset", "inherit"},
    StringComparer.CurrentCultureIgnoreCase)

            _validColorNames = New HashSet(Of String)(New String() {"AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine ", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond",
                                                                    "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue",
                                                                    "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenRod", "DarkGray", "DarkGreen", "DarkKhaki",
                                                                    "DarkMagenta", "DarkOliveGreen", "Darkorange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen",
                                                                    "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray",
                                                                    "DimGrey", "DodgerBlue", "FireBrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite",
                                                                    "Gold", "GoldenRod", "Gray", "Green", "GreenYellow", "HoneyDew", "HotPink", "IndianRed", "Indigo", "Ivory",
                                                                    "Khaki", "Lavender", "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan",
                                                                    "LightGoldenRodYellow", "LightGray", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue",
                                                                    "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon",
                                                                    "MediumAquaMarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue",
                                                                    "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "MintCream", "MistyRose",
                                                                    "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid",
                                                                    "PaleGoldenRod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff ", "Peru", "Pink",
                                                                    "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown ",
                                                                    "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen",
                                                                    "SteelBlue", "Tan", "Teal", "Thistle", "Tomato", "Turquoise", "Violet", "Wheat", "White", "WhiteSmoke",
                                                                    "Yellow", "YellowGreen"}, StringComparer.CurrentCultureIgnoreCase)
        End Sub




        Public Sub New()
            _interperterRan = True
        End Sub

        Public Sub New(style As String)
            _interperterRan = False
            _Style = style.Trim()
        End Sub


        Public Property Width As Integer?
            Get
                If (Not _interperterRan) Then Interpert()
                Return _width
            End Get
            Set(value As Integer?)
                _width = value
            End Set
        End Property

        Public Property Color As Color?
            Get
                If (Not _interperterRan) Then Interpert()
                Return _color
            End Get
            Set(value As Color?)
                _color = value
            End Set
        End Property

        Public Property BorderStyle As String
            Get
                If (Not _interperterRan) Then Interpert()
                Return _bordersStyle
            End Get
            Set(value As String)
                If (_validBorderStyles.Contains(value)) Then
                    _bordersStyle = value
                End If
            End Set
        End Property

        Public ReadOnly Property hasBorderStyle As Boolean
            Get
                Dim isEmpty = (String.IsNullOrEmpty(BorderStyle) OrElse (BorderStyle.Equals("none", StringComparison.InvariantCultureIgnoreCase)))
                Return Not isEmpty
            End Get
        End Property

        Private Sub Interpert()
            For Each s In _Style.Split(" "c)
                If (Not String.IsNullOrEmpty(s)) Then
                    If (Not InterpetedAsColor(s)) Then
                        If (Not InterpetedAsWidth(s)) Then
                            If (Not InterpetedAsBorderStyle(s)) Then

                                Debug.Assert(False, String.Format("Unable to determine what [{0}] is", s))

                            End If
                        End If
                    End If
                End If
            Next
            _interperterRan = True
        End Sub

        Private Function InterpetedAsColor(s As String) As Boolean
            If (s.StartsWith("#") OrElse _validColorNames.Contains(s)) Then
                Dim result = ColorTranslator.FromHtml(s)
                _color = result
                Return True
            End If
            Return False
        End Function

        Private Function InterpetedAsWidth(s As String) As Boolean
            If (s.EndsWith("px", StringComparison.InvariantCultureIgnoreCase)) Then
                Dim result As Integer = -1
                If (Integer.TryParse(s.Substring(0, s.Length - 2), result)) Then
                    _width = result
                    Return True
                End If
            End If
            Return False
        End Function

        Private Function InterpetedAsBorderStyle(s As String) As Boolean
            If (_validBorderStyles.Contains(s)) Then
                _bordersStyle = s
                Return True
            End If
            Return False
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} {2}",
                                 If(Color.HasValue, System.Drawing.ColorTranslator.ToHtml(Color.Value), String.Empty),
                                 If(Width.HasValue, String.Format("{0}px", Width.Value), String.Empty),
                                 BorderStyle)
        End Function

        Public Overrides Function Equals(o As Object) As Boolean
            If TypeOf o Is CssBorder Then
                Dim other As CssBorder = DirectCast(o, CssBorder)
                Dim r1 = (other.Width.HasValue = Width.HasValue)
                If (r1 AndAlso Width.HasValue) Then r1 = (other.Width.Value = Width.Value)

                Dim r2 = other.BorderStyle.Equals(BorderStyle, StringComparison.InvariantCultureIgnoreCase)

                Dim r3 = (other.Color.HasValue = Color.HasValue)
                If (r3 AndAlso Color.HasValue) Then r3 = (other.Color.Value.R = Color.Value.R) AndAlso (other.Color.Value.G = Color.Value.G) AndAlso (other.Color.Value.B = Color.Value.B)
                Return r1 AndAlso r2 AndAlso r3
            End If
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _width.GetHashCode() Xor _bordersStyle.GetHashCode() Xor Color.GetHashCode()
        End Function

    End Class
End Namespace