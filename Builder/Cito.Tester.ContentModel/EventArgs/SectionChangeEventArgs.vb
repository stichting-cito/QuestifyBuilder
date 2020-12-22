Public Class SectionChangeEventArgs
    Inherits EventArgs


    Public Property Section As TestSectionViewBase



    Public Sub New(section As TestSectionViewBase)
        MyBase.New()
        Me.Section = section
    End Sub

End Class

