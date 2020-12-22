Public Class TestComponentAddedEventArgs
    Inherits EventArgs

    Private _addedTestComponent As TestComponent2

    Public Sub New(addedTestComponent As TestComponent2)
        _addedTestComponent = addedTestComponent
    End Sub

    Public ReadOnly Property AddedTestComponent As TestComponent2
        Get
            Return _addedTestComponent
        End Get
    End Property
End Class
