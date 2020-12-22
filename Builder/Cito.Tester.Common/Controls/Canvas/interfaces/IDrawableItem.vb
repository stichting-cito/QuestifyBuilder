Imports System.ComponentModel

Namespace Controls.Canvas
    Public Interface IDrawableItem
        Inherits INotifyPropertyChanged

        Property AnchorPoint As Point

        ReadOnly Property BoundingBox As Rectangle

        Property isDraggable As Boolean

        Property isSelected As Boolean

        Property ID As String


        Sub Draw(g As Graphics, color As Color)

        Sub SetPainter(Of T As IDrawableItem)(painter As IDrawableItemPainter(Of T))


    End Interface


End Namespace
