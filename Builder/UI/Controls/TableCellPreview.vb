Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Controls.Logic

Namespace Controls
    Public Class TableCellPreview
        Inherits Control

        Private _selectRectWidth As Integer = 2
        Private _fakeTxtSize As Integer = 3
        Private _doNotKnowLineSize As Integer = 6
        Private _selectColor As Color = Color.FromArgb(51, 153, 255)
        Private _txtColor As Color = Color.FromArgb(170, 170, 170)
        Private _isSelected As Boolean = False
        Private _dto As TableStyleDto


        Public Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
            _dto = TableStyleDto.ColAndRow()
        End Sub



        Public Property IsSelected() As Boolean
            Get
                Return _isSelected
            End Get
            Set(ByVal value As Boolean)
                _isSelected = value
                Invalidate()
            End Set
        End Property



        Friend Sub SetDto(dto As TableStyleDto)
            _dto = dto
            Invalidate()
        End Sub





        Protected Overrides Sub OnPaintBackground(pevent As System.Windows.Forms.PaintEventArgs)
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim g = e.Graphics
            g.Clear(Color.White)
            CalcAndDrawGrid(g)
        End Sub



        Private Sub CalcAndDrawGrid(g As Graphics)
            Dim workingRect = New Rectangle(ClientRectangle.Location,
                                            New Size(ClientRectangle.Width - (ClientRectangle.Width Mod 2),
                                             ClientRectangle.Height - (ClientRectangle.Height Mod 2)))
            Dim selectRect = DrawingMath.GetSmallerRect(workingRect, 2)
            If (IsSelected) Then DrawSelected(g, selectRect)
            Dim innerRect1 = DrawingMath.GetSmallerRect(workingRect, 4)

            Dim rects = MakeRects(innerRect1)

            For Each r In rects
                DrawFakeText(g, r)
            Next

            DrawInnerLine(g, innerRect1)
        End Sub

        Private Sub DrawSelected(g As Graphics, selectRect As Rectangle)
            Using pen = New Pen(_selectColor, _selectRectWidth)
                g.DrawRectangle(pen, selectRect)
            End Using
        End Sub

        Private Sub drawRect(g As Graphics, rect As Rectangle)
            g.DrawRectangle(Pens.Black, rect)
        End Sub

        Private Function MakeRects(orgRect As Rectangle) As List(Of Rectangle)
            Dim ret As New List(Of Rectangle)
            Dim a = DrawingMath.GetPoint(ContentAlignment.TopLeft, orgRect)
            Dim c = DrawingMath.GetPoint(ContentAlignment.BottomRight, orgRect)

            Dim ab = DrawingMath.GetPoint(ContentAlignment.TopCenter, orgRect)
            Dim bc = DrawingMath.GetPoint(ContentAlignment.MiddleRight, orgRect)
            Dim dc = DrawingMath.GetPoint(ContentAlignment.BottomCenter, orgRect)
            Dim ad = DrawingMath.GetPoint(ContentAlignment.MiddleLeft, orgRect)

            Dim x = DrawingMath.GetPoint(ContentAlignment.MiddleCenter, orgRect)

            Dim oTop = _dto.TopHorizontalWidth
            Dim oMidH = _dto.MidHorizontalWidth \ 2
            Dim oBottom = _dto.BottomHorizontalWidth

            Dim oLeft = _dto.LeftVerticalWidth
            Dim oMidV = _dto.MidVerticalWidth \ 2
            Dim oRight = _dto.RightVerticalWidth

            If _dto.HasMidHorizontal AndAlso _dto.HasMidVertical Then
                ret.Add(DrawingMath.CreateRect(a, x, oLeft, oTop, oMidV, oMidH))
                ret.Add(DrawingMath.CreateRect(ab, bc, oMidV, oTop, oRight, oMidH))
                ret.Add(DrawingMath.CreateRect(x, c, oMidV, oMidH, oRight, oBottom))
                ret.Add(DrawingMath.CreateRect(ad, dc, oLeft, oMidH, oMidV, oBottom))
            ElseIf Not _dto.HasMidHorizontal AndAlso _dto.HasMidVertical Then
                ret.Add(DrawingMath.CreateRect(a, dc, oLeft, oTop, oMidV, oBottom))
                ret.Add(DrawingMath.CreateRect(ab, c, oMidV, oTop, oRight, oBottom))

            ElseIf _dto.HasMidHorizontal AndAlso Not _dto.HasMidVertical Then
                ret.Add(DrawingMath.CreateRect(a, bc, oLeft, oTop, oRight, oMidH))
                ret.Add(DrawingMath.CreateRect(ad, c, oLeft, oMidH, oRight, oBottom))
            ElseIf Not _dto.HasMidHorizontal AndAlso Not _dto.HasMidVertical Then
                ret.Add(DrawingMath.CreateRect(a, c, oLeft, oTop, oRight, oBottom))
            Else
                Debug.Assert(False, "Should Not occur.")
            End If
            Return ret
        End Function

        Private Sub DrawFakeText(g As Graphics, r As Rectangle)
            Dim w = (r.Width - 2)
            Dim h = (r.Height - 2)
            Dim rws = (h \ (_fakeTxtSize * 2) - 1)

            If _dto.BackColor.HasValue Then
                Using Brush = New SolidBrush(_dto.BackColor.Value)
                    g.FillRectangle(Brush, r)
                End Using
            End If

            Using Pen = New Pen(_txtColor, _fakeTxtSize)
                For i = 0 To rws
                    Dim pntStart = New Point(r.Left + 2 + (If(i = 0, w \ 3, 0)), (r.Top + 1) + i * (_fakeTxtSize * 2) + _fakeTxtSize)
                    Dim pntEnd = New Point(r.Right - 2 - (If(i = rws, w \ 3, 0)), (r.Top + 1) + i * (_fakeTxtSize * 2) + _fakeTxtSize)
                    g.DrawLine(Pen, pntStart, pntEnd)
                Next
            End Using

        End Sub

        Private Sub DrawInnerLine(g As Graphics, rect As Rectangle)

            Dim oTop = _dto.TopHorizontalWidth \ 2
            Dim oMidH = _dto.MidHorizontalWidth \ 2
            Dim oBottom = _dto.BottomHorizontalWidth \ 2

            Dim oLeft = _dto.LeftVerticalWidth \ 2
            Dim oMidV = _dto.MidVerticalWidth \ 2
            Dim oRight = _dto.RightVerticalWidth \ 2
            If _dto.HasMidVertical Then
                Using p = PenFactory.Create(_dto.MidVertical, _dto.MidVerticalWidth, _dto.LineColor)
                    g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.TopCenter, rect, 0, oTop),
                                DrawingMath.GetPoint(ContentAlignment.BottomCenter, rect, 0, -oBottom))
                End Using
            End If
            If _dto.HasMidHorizontal Then
                Using p = PenFactory.Create(_dto.MidHorizontal, _dto.MidHorizontalWidth, _dto.LineColor)
                    g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.MiddleLeft, rect, oLeft, 0),
                                DrawingMath.GetPoint(ContentAlignment.MiddleRight, rect, -oRight, 0))
                End Using
            End If
            Using p = PenFactory.Create(_dto.TopHorizontal, _dto.TopHorizontalWidth, _dto.LineColor)
                g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.TopLeft, rect, 0, 0),
                            DrawingMath.GetPoint(ContentAlignment.TopRight, rect, 0, 0))
            End Using
            Using p = PenFactory.Create(_dto.RightVertical, _dto.RightVerticalWidth, _dto.LineColor)
                g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.TopRight, rect, 0, 0),
                            DrawingMath.GetPoint(ContentAlignment.BottomRight, rect, 0, 0))
            End Using
            Using p = PenFactory.Create(_dto.BottomHorizontal, _dto.BottomHorizontalWidth, _dto.LineColor)
                g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.BottomRight, rect, 0, 0),
                            DrawingMath.GetPoint(ContentAlignment.BottomLeft, rect, 0, 0))
            End Using
            Using p = PenFactory.Create(_dto.LeftVertical, _dto.LeftVerticalWidth, _dto.LineColor)
                g.DrawLine(p, DrawingMath.GetPoint(ContentAlignment.BottomLeft, rect, 0, 0),
                            DrawingMath.GetPoint(ContentAlignment.TopLeft, rect, 0, 0))
            End Using

        End Sub



    End Class
End Namespace
