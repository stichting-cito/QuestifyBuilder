Imports System.ComponentModel
Namespace Controls.Canvas
    Public Interface IAdorner
        Inherits INotifyPropertyChanged


        Property AdornedElement As IDrawableItem

        ReadOnly Property BoundingBox As Rectangle



        Sub Draw(g As Graphics)


    End Interface

End Namespace
