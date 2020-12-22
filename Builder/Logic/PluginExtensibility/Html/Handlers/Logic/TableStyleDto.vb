Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Class TableStyleDto

        Property TopHorizontal As LineStyle?
        Property MidHorizontal As LineStyle?
        Property BottomHorizontal As LineStyle?

        Property LeftVertical As LineStyle?
        Property MidVertical As LineStyle?
        Property RightVertical As LineStyle?

        Property TopHorizontalWidth As Integer
        Property MidHorizontalWidth As Integer
        Property BottomHorizontalWidth As Integer

        Property LeftVerticalWidth As Integer
        Property MidVerticalWidth As Integer
        Property RightVerticalWidth As Integer

        Property HasMidHorizontal As Boolean
        Property HasMidVertical As Boolean

        Property BackColor As Color?
        Property LineColor As Color?

        Public Sub Box(style As LineStyle, width As Integer)
            TopHorizontal = style
            RightVertical = style
            BottomHorizontal = style
            LeftVertical = style

            TopHorizontalWidth = width
            RightVerticalWidth = width
            BottomHorizontalWidth = width
            LeftVerticalWidth = width
        End Sub

        Public Sub Inner(style As LineStyle, width As Integer)
            If HasMidHorizontal Then
                MidHorizontal = style
                MidHorizontalWidth = width
            End If
            If HasMidVertical Then
                MidVertical = style
                MidVerticalWidth = width
            End If
        End Sub

        Friend Shared Function SingleCell() As TableStyleDto
            Dim ret = New TableStyleDto
            ret.Box(LineStyle.Solid, 1)
            Return ret
        End Function

        Friend Shared Function Row() As TableStyleDto
            Dim ret = New TableStyleDto
            ret.HasMidHorizontal = True
            ret.Box(LineStyle.Solid, 1)
            ret.Inner(LineStyle.Solid, 1)
            Return ret
        End Function

        Friend Shared Function Column() As TableStyleDto
            Dim ret = New TableStyleDto
            ret.HasMidVertical = True
            ret.Box(LineStyle.Solid, 1)
            ret.Inner(LineStyle.Solid, 1)
            Return ret
        End Function

        Public Shared Function ColAndRow() As TableStyleDto
            Dim ret = New TableStyleDto
            ret.HasMidVertical = True
            ret.HasMidHorizontal = True
            ret.Box(LineStyle.Solid, 1)
            ret.Inner(LineStyle.Solid, 1)
            Return ret
        End Function



        Function GetLeftVert(borders As Boolean, Optional lineColor As Color? = Nothing) As CssBorder
            Return New CssBorder() With {.Color = If(lineColor, Color.Black),
                                         .Width = If(borders, LeftVerticalWidth, If(HasMidVertical, MidVerticalWidth, LeftVerticalWidth)),
                                         .BorderStyle = TranslateLineStyle(If(borders, LeftVertical, If(HasMidVertical, MidVertical, LeftVertical)))
                                          }
        End Function


        Function GetTopHorizontal(borders As Boolean, Optional lineColor As Color? = Nothing) As CssBorder
            Return New CssBorder() With {.Color = If(lineColor, Color.Black),
                                     .Width = If(borders, TopHorizontalWidth, If(HasMidHorizontal, MidHorizontalWidth, TopHorizontalWidth)),
                                     .BorderStyle = TranslateLineStyle(If(borders, TopHorizontal, If(HasMidHorizontal, MidHorizontal, TopHorizontal)))
                                      }
        End Function

        Function GetRightVert(borders As Boolean, Optional lineColor As Color? = Nothing) As CssBorder
            Return New CssBorder() With {.Color = If(lineColor, Color.Black),
                                        .Width = If(borders, RightVerticalWidth, If(HasMidVertical, MidVerticalWidth, RightVerticalWidth)),
                                        .BorderStyle = TranslateLineStyle(If(borders, RightVertical, If(HasMidVertical, MidVertical, RightVertical)))
                                         }
        End Function

        Function GetBottomHorizontal(borders As Boolean, Optional lineColor As Color? = Nothing) As CssBorder
            Return New CssBorder() With {.Color = If(lineColor, Color.Black),
                         .Width = If(borders, BottomHorizontalWidth, If(HasMidHorizontal, MidHorizontalWidth, BottomHorizontalWidth)),
                         .BorderStyle = TranslateLineStyle(If(borders, BottomHorizontal, If(HasMidHorizontal, MidHorizontal, BottomHorizontal)))
                          }
        End Function


        Private Function TranslateLineStyle(style As LineStyle?) As String
            If style.HasValue Then
                Return style.ToString()
            End If
            Return String.Empty
        End Function

        Public Function Clone() As TableStyleDto
            Dim ret As New TableStyleDto
            ret.TopHorizontal = TopHorizontal
            ret.MidHorizontal = MidHorizontal
            ret.BottomHorizontal = BottomHorizontal

            ret.LeftVertical = LeftVertical
            ret.MidVertical = MidVertical
            ret.RightVertical = RightVertical

            ret.TopHorizontalWidth = TopHorizontalWidth
            ret.MidHorizontalWidth = MidHorizontalWidth
            ret.BottomHorizontalWidth = BottomHorizontalWidth

            ret.LeftVerticalWidth = LeftVerticalWidth
            ret.MidVerticalWidth = MidVerticalWidth
            ret.RightVerticalWidth = RightVerticalWidth

            ret.HasMidHorizontal = HasMidHorizontal
            ret.HasMidVertical = HasMidVertical

            ret.BackColor = BackColor
            ret.LineColor = LineColor
            Return ret
        End Function

        Function BoxIsSet() As Boolean
            Return LeftVertical.HasValue AndAlso TopHorizontal.HasValue AndAlso RightVertical.HasValue AndAlso BottomHorizontal.HasValue
        End Function

        Function InnerIsSet() As Boolean
            Return (MidHorizontal.HasValue Xor MidVertical.HasValue)
        End Function

        Function HasInnser() As Boolean
            Return HasMidHorizontal OrElse HasMidVertical
        End Function

        Function BoxIsSameStyle() As Boolean
            Dim allBoxIsSet As Boolean = BoxIsSet()

            If allBoxIsSet Then
                Dim line As Boolean = (LeftVertical.Value = TopHorizontal.Value AndAlso TopHorizontal.Value = RightVertical.Value AndAlso RightVertical.Value = BottomHorizontal.Value)
                Dim linew As Boolean = (LeftVerticalWidth = TopHorizontalWidth AndAlso TopHorizontalWidth = RightVerticalWidth AndAlso RightVerticalWidth = BottomHorizontalWidth)

                If line Then
                    If (LeftVertical.Value = LineStyle.None OrElse LeftVertical.Value = LineStyle.Hidden) Then
                        Return True
                    End If

                    Return linew
                End If
            Else
                Return False
            End If
        End Function

        Function InnerSameStyle() As Boolean
            Dim both As Boolean = (MidHorizontal.HasValue AndAlso MidVertical.HasValue)
            Dim any As Boolean = (HasMidHorizontal Xor HasMidVertical)
            If both Then
                Return MidHorizontal.Value = MidVertical.Value AndAlso ((MidHorizontalWidth = MidVerticalWidth) OrElse (MidHorizontal.Value = LineStyle.None) OrElse
                    (MidHorizontal.Value = LineStyle.Hidden))
            End If
            Return any
        End Function

        Function GetInnserstyle() As LineStyle?
            If (HasInnser()) Then
                If (InnerSameStyle()) Then
                    Return If(HasMidHorizontal, MidHorizontal, MidVertical)
                End If
            End If
            Return Nothing
        End Function

        Function GetInnserstyleWidth() As Integer
            If (HasInnser()) Then
                If (InnerSameStyle()) Then
                    Return If(HasMidHorizontal, MidHorizontalWidth, MidVerticalWidth)
                End If
            End If
            Return -1
        End Function

        Function BoxAndInnerSameStyle() As Boolean
            If BoxIsSet() AndAlso BoxIsSameStyle() Then

                If (HasInnser()) Then
                    If InnerSameStyle() Then
                        Dim inner = GetInnserstyle()
                        Dim innerW = GetInnserstyleWidth()
                        Return If(inner.HasValue, inner.Value = LeftVertical.Value AndAlso
                                  (inner.Value = LineStyle.None OrElse inner.Value = LineStyle.Hidden OrElse LeftVerticalWidth = innerW), False)
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If

            End If
        End Function

    End Class

End Namespace