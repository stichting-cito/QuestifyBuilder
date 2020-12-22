Imports System.ComponentModel
Imports System.Drawing

Namespace Controls.Canvas.Drawable.Ardorner

    Public Class SelectArdorner
        Implements IAdorner


        Private _drawable As IDrawableItem
        Private _bb As Rectangle = Rectangle.Empty


        Public Property AdornedElement As IDrawableItem Implements IAdorner.AdornedElement
            Get
                Return _drawable
            End Get
            Set(value As IDrawableItem)
                _drawable = value
                CalcBB()
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("AdornedElement"))
            End Set
        End Property

        Public ReadOnly Property BoundingBox As Rectangle Implements IAdorner.BoundingBox
            Get
                Return _bb
            End Get
        End Property

        Public Sub Draw(g As Graphics) Implements IAdorner.Draw
            If (_drawable IsNot Nothing) Then

                If (_drawable.isSelected) Then
                    Using pDotted = New Pen(Color.DodgerBlue) With {.DashPattern = New Single() {5, 2}}
                        g.DrawRectangle(pDotted, _drawable.BoundingBox)
                    End Using
                End If

            End If
        End Sub

        Private Sub CalcBB()
            If (_drawable IsNot Nothing) Then

            Else
                _bb = Rectangle.Empty
            End If
        End Sub

        Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace