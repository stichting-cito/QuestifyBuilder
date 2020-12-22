
Imports System.ComponentModel
Imports System.Threading

Namespace Controls.Canvas.Drawable.Shapes

    <DesignerCategory("Code")>
    Friend MustInherit Class DrawableBase(Of T As IShape)
        Implements IDrawableItem
        Implements IDimensionManipulator

        Private Shared _autoID As Integer
        Private _anchor As Point
        Private _isdraggable As Boolean
        Private _isSelected As Boolean
        Private _id As String
        Private _label As String
        Private _painter As IDrawableItemPainter(Of T)


        Sub New(identifier As String, label As String)
            _id = identifier
            _label = label
        End Sub

        Sub New(identifier As String)
            _id = identifier
        End Sub

        Sub New()
            _id = Interlocked.Increment(_autoID).ToString()
        End Sub

        Public MustOverride ReadOnly Property BoundingBox As Rectangle Implements IDrawableItem.BoundingBox

        Public Overridable Sub Draw(g As Graphics, color As Color) Implements IDrawableItem.Draw
            Using pen As New Pen(color)

                If (_painter Is Nothing) Then
                    Dim r = BoundingBox
                    g.DrawRectangle(Pens.Crimson, New Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1))
                    g.DrawLine(pen, New Point(r.Left, r.Top), New Point(r.Right - 1, r.Bottom - 1))
                    g.DrawLine(pen, New Point(r.Left, r.Bottom - 1), New Point(r.Right - 1, r.Top))
                Else
                    _painter.Draw(g, color, Me.Shape)
                End If
            End Using
        End Sub

        Public Property IsDraggable As Boolean Implements IDrawableItem.isDraggable
            Get
                Return _isdraggable
            End Get
            Set(value As Boolean)
                _isdraggable = value
            End Set
        End Property

        Public Overridable Property AnchorPoint As Point Implements IDrawableItem.AnchorPoint
            Get
                Return _anchor
            End Get
            Set(value As Point)
                _anchor = value
                RaisePropertyChanged("AnchorPoint")
            End Set
        End Property

        Public Property IsSelected As Boolean Implements IDrawableItem.isSelected
            Get
                Return _isSelected
            End Get
            Set(value As Boolean)
                _isSelected = value
            End Set
        End Property

        Public Property ID As String Implements IDrawableItem.ID
            Get
                Return _id
            End Get
            Set(value As String)
                _id = value
            End Set
        End Property


        Public MustOverride ReadOnly Property Shape As T

        Private Sub SetPainter(Of T1 As IDrawableItem)(painter As IDrawableItemPainter(Of T1)) Implements IDrawableItem.SetPainter
            If GetType(T1) Is GetType(T) Then
                _painter = DirectCast(painter, IDrawableItemPainter(Of T))
            Else
                Throw New ArgumentException()
            End If
        End Sub



        Public MustOverride Sub DecHeight() Implements IDimensionManipulator.DecHeight

        Public MustOverride Sub DecWidth() Implements IDimensionManipulator.DecWidth

        Public MustOverride Sub IncHeight() Implements IDimensionManipulator.IncHeight

        Public MustOverride Sub IncWidth() Implements IDimensionManipulator.IncWidth


        Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements IDrawableItem.PropertyChanged

        Protected Sub RaisePropertyChanged(name As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        End Sub



    End Class

End Namespace
