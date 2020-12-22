Imports System.Windows.Forms
Imports Cito.Tester.Common.Controls.Canvas.Drawable.Shapes

Namespace Controls.Canvas.Tools
    Public Class SimpleTool
        Inherits ToolBase(Of ICanvas)

        Public Sub New()
            MyBase.New(New SizeTool(New SelectTool(New WithinBoundsTool(New MoveTool(Nothing)))))
        End Sub

        Public Overrides Sub MouseDown(sender As ICanvas, e As MouseEventArgs)

            If (Control.ModifierKeys = Keys.Alt) Then

                If (sender.EditItem IsNot Nothing) Then
                    sender.AddItem(sender.EditItem)
                    sender.DeSelect()
                End If

                Dim shp = New CircleShape() With {.AnchorPoint = e.Location, .isSelected = True}
                sender.AddItem(shp)
                sender.Select(shp)
            Else
                MyBase.MouseDown(sender, e)
            End If
        End Sub


    End Class
End Namespace