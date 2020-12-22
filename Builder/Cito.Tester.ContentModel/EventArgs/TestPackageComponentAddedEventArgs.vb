Public Class TestPackageComponentAddedEventArgs
    Inherits EventArgs

    Private _addedTestPackageComponent As TestPackageComponent

    Public Sub New(addedTestPackageComponent As TestPackageComponent)
        _addedTestPackageComponent = addedTestPackageComponent
    End Sub

    Public ReadOnly Property AddedTestPackageComponent As TestPackageComponent
        Get
            Return _addedTestPackageComponent
        End Get
    End Property
End Class
