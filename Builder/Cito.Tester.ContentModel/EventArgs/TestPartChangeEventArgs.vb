Public Class TestPartChangeEventArgs
    Inherits EventArgs

    Public Property TestPart As TestPartViewBase



    Public Sub New(testPart As TestPartViewBase)
        MyBase.New()
        Me.TestPart = testPart
    End Sub

End Class

