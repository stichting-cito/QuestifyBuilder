
Imports Cito.Tester.ContentModel

Public Class InlineElementEventArgs
    Inherits EventArgs

    Public Property InlineElement() As InlineElement

    Public Sub New(ByVal inlineElement As InlineElement)
        Me.InlineElement = inlineElement
    End Sub

End Class
