Imports System.ComponentModel
Imports Questify.Builder.UI.Controls.Logic

Namespace Controls

    Public Class TableToggleButton

        Private _pointFrom As ContentAlignment
        Private _pointTo As ContentAlignment


        Public Sub New()

            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            InitializeComponent()

            _pointFrom = ContentAlignment.TopLeft
            _pointTo = ContentAlignment.BottomLeft

        End Sub


        Public Overrides Property Text As String
            Get
                Return String.Empty
            End Get
            Set(value As String)
                MyBase.Text = String.Empty
            End Set
        End Property

        <DefaultValue(False)>
        Public Overrides Property AutoSize As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(value As Boolean)
                MyBase.AutoSize = False
            End Set
        End Property



        Public Property PointFrom() As ContentAlignment
            Get
                Return _pointFrom
            End Get
            Set(ByVal value As ContentAlignment)
                _pointFrom = value
                Invalidate()
            End Set
        End Property


        Public Property PointTo() As ContentAlignment
            Get
                Return _pointTo
            End Get
            Set(ByVal value As ContentAlignment)
                _pointTo = value
                Invalidate()
            End Set
        End Property


        Protected Overrides Sub OnPaint(pevent As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(pevent)
            Dim g = pevent.Graphics


            DrawGrid(g, ClientRectangle)


        End Sub

        Private Sub DrawGrid(g As Graphics, rect As Rectangle)
            If (rect.Width > 15 AndAlso rect.Height > 15) Then
                Dim m As Integer = 4
                Dim r = DrawingMath.GetSmallerRect(rect, m)
                Dim r2 = DrawingMath.GetSmallerRect(r, 1)
                Using bWhite = New SolidBrush(Color.FromArgb(If(Checked, 127, 255), Color.White)), pDot As New Pen(Color.Gray, 1) With {.DashStyle = Drawing2D.DashStyle.Dot}, pPhat = New Pen(Color.Black, 1.5F)
                    g.FillRectangle(bWhite, r)
                    g.DrawRectangle(pDot, r2)
                    g.DrawLine(pDot, DrawingMath.GetPoint(ContentAlignment.TopCenter, r2), DrawingMath.GetPoint(ContentAlignment.BottomCenter, r2))
                    g.DrawLine(pDot, DrawingMath.GetPoint(ContentAlignment.MiddleLeft, r2), DrawingMath.GetPoint(ContentAlignment.MiddleRight, r2))
                    g.DrawLine(pPhat, DrawingMath.GetPoint(_pointFrom, r2), DrawingMath.GetPoint(_pointTo, r2))
                End Using
            End If
        End Sub



    End Class
End Namespace