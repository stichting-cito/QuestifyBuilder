Imports System.Drawing
Imports System.Text
Namespace PluginExtensibility.Html.Handlers.Logic
    Public Class CssStyleList

        Private _dict As Dictionary(Of String, String)

        Private _BorderLeft As CssBorder
        Private _BorderTop As CssBorder
        Private _BorderRight As CssBorder
        Private _BorderBottom As CssBorder

        Private _BorderCollapse As CssBorderCollapsed


        Public Sub New()
            _dict = New Dictionary(Of String, String)(StringComparer.InvariantCultureIgnoreCase)
        End Sub

        Public Sub New(style As String)
            Me.New()
            If (Not String.IsNullOrEmpty(style)) Then
                Dim styleKVPs = style.Split(";"c)

                For Each kvp In styleKVPs
                    If (Not String.IsNullOrEmpty(kvp.Trim())) Then
                        Dim d = kvp.Split(":"c)
                        Debug.Assert(d.Length = 2)
                        _dict.Add(d(0).Trim, d(1))
                    End If
                Next
            End If
        End Sub


        Public Property Background_color As Color?
            Get
                If _dict.ContainsKey(CssKeywords.Background_color) Then
                    Return ColorTranslator.FromHtml(_dict(CssKeywords.Background_color))
                End If
                Return Nothing
            End Get
            Set(value As Color?)
                If value.HasValue Then
                    _dict.Remove(CssKeywords.Background_color)
                    _dict.Add(CssKeywords.Background_color, ColorTranslator.ToHtml(value.Value))
                Else
                    _dict.Remove(CssKeywords.Background_color)
                End If
            End Set
        End Property

        Public Property BorderLeft As CssBorder
            Get
                If _dict.ContainsKey(CssKeywords.Border_Left) Then
                    If (_BorderLeft Is Nothing) Then _BorderLeft = New CssBorder(_dict(CssKeywords.Border_left))
                    Return _BorderLeft
                End If
                Return Nothing
            End Get
            Set(value As CssBorder)
                If value IsNot Nothing Then

                    _BorderLeft = value
                    _dict.Remove(CssKeywords.Border_left)
                    _dict.Add(CssKeywords.Border_left, value.ToString())
                Else
                    _dict.Remove(CssKeywords.Border_left)
                End If
            End Set
        End Property

        Public Property BorderTop As CssBorder
            Get
                If _dict.ContainsKey(CssKeywords.Border_top) Then
                    If (_BorderTop Is Nothing) Then _BorderTop = New CssBorder(_dict(CssKeywords.Border_top))
                    Return _BorderTop
                End If
                Return Nothing
            End Get
            Set(value As CssBorder)
                If value IsNot Nothing Then
                    _BorderTop = value
                    _dict.Remove(CssKeywords.Border_top)
                    _dict.Add(CssKeywords.Border_top, value.ToString())
                Else
                    _dict.Remove(CssKeywords.Border_top)
                End If
            End Set
        End Property

        Public Property BorderRight As CssBorder
            Get
                If _dict.ContainsKey(CssKeywords.Border_Right) Then
                    If (_BorderRight Is Nothing) Then _BorderRight = New CssBorder(_dict(CssKeywords.Border_right))
                    Return _BorderRight
                End If
                Return Nothing
            End Get
            Set(value As CssBorder)
                If value IsNot Nothing Then
                    _BorderRight = value
                    _dict.Remove(CssKeywords.Border_right)
                    _dict.Add(CssKeywords.Border_right, value.ToString())
                Else
                    _dict.Remove(CssKeywords.Border_right)
                End If
            End Set
        End Property

        Public Property BorderBottom As CssBorder
            Get
                If _dict.ContainsKey(CssKeywords.Border_bottom) Then
                    If (_BorderBottom Is Nothing) Then _BorderBottom = New CssBorder(_dict(CssKeywords.Border_bottom))
                    Return _BorderBottom
                End If
                Return Nothing
            End Get
            Set(value As CssBorder)
                If value IsNot Nothing Then
                    _BorderBottom = value
                    _dict.Remove(CssKeywords.Border_bottom)
                    _dict.Add(CssKeywords.Border_bottom, value.ToString())
                Else
                    _dict.Remove(CssKeywords.Border_bottom)
                End If
            End Set
        End Property

        Public Property BorderCollapse As CssBorderCollapsed
            Get
                If _dict.ContainsKey(CssKeywords.Border_Collapse) Then
                    If (_BorderCollapse Is Nothing) Then _BorderCollapse = New CssBorderCollapsed(_dict(CssKeywords.Border_Collapse))
                    Return _BorderCollapse
                End If
                Return Nothing
            End Get
            Set(value As CssBorderCollapsed)
                If value IsNot Nothing Then
                    _BorderCollapse = value
                    _dict.Remove(CssKeywords.Border_Collapse)
                    _dict.Add(CssKeywords.Border_Collapse, value.ToString())
                Else
                    _dict.Remove(CssKeywords.Border_Collapse)
                End If
            End Set
        End Property

        Public ReadOnly Property hasStyles As Boolean
            Get
                Return _dict.Count > 0
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim sb As New StringBuilder()

            For Each kvp In _dict
                sb.AppendFormat("{0}:{1};", kvp.Key, kvp.Value)
            Next

            Return sb.ToString()
        End Function

        Friend Class CssKeywords

            Public Const Background_color = "background-color"

            Public Const Border_left = "border-left"
            Public Const Border_top = "border-top"
            Public Const Border_right = "border-right"
            Public Const Border_bottom = "border-bottom"

            Public Const Border_Collapse = "border-collapse"

        End Class
    End Class
End Namespace