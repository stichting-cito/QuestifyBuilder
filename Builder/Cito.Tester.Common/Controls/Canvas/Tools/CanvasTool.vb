Imports System.Drawing
Namespace Controls.Canvas.Tools
    Public Class CanvasTool
        Inherits ToolBase(Of ICanvas)

        Public Sub New(decoree As ITool(Of ICanvas))
            MyBase.new(decoree)
        End Sub


        Protected Function SelectItem(point As Point, c As ICanvas) As Boolean
            Dim r As IDrawableItem
            Dim l = c.HitTest(point)
            r = If(l.Count > 0, l(0), Nothing)
            If (r IsNot Nothing) Then
                c.Select(r)
                Return True
            End If
            Return False
        End Function

        Protected Function DeSelect(c As ICanvas) As Boolean
            If (c.EditItem IsNot Nothing) Then
                c.DeSelect()
                Return True
            End If
            Return False
        End Function


    End Class
End Namespace