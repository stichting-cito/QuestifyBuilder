Imports System.IO


Public Class ImageGenericResourceViewer

    Private _sourceImage As Image


    Protected Overrides Sub DataBind()
        If IsResourceDataAvailable Then
            Dim image As Image = Nothing

            If (DataSource.ResourceData.BinData.Length > 0) Then
                Using memStream = New MemoryStream(Me.DataSource.ResourceData.BinData)
                    image = image.FromStream(memStream)
                End Using
            End If

            _sourceImage = image
        Else
            _sourceImage = Nothing
        End If

        Refresh()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        If _sourceImage IsNot Nothing Then
            e.Graphics.Clear(SystemColors.Control)

            Dim paintRectangle As Rectangle = ScaleToFit(Me.ClientRectangle, _sourceImage)
            e.Graphics.DrawImage(Me._sourceImage, paintRectangle)
        End If

    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        Me.Invalidate()

        MyBase.OnResize(e)
    End Sub



    Private Shared Function ScaleToFit(ByVal targetArea As Rectangle, ByVal img As Image) As Rectangle
        If img Is Nothing Then
            Throw New ArgumentNullException("img")
        End If

        Dim r As New Rectangle(targetArea.Location, targetArea.Size)

        If r.Height < img.Width OrElse r.Width < img.Width Then
            If r.Height * img.Width > r.Width * img.Height Then
                r.Height = CInt(r.Width * img.Height / img.Width)
                r.Y = CInt((targetArea.Height - r.Height) / 2)
            Else
                r.Width = CInt(r.Height * img.Width / img.Height)
                r.X = CInt((targetArea.Width - r.Width) / 2)
            End If
        Else
            r.Y = CInt((r.Height - img.Height) / 2)
            r.X = CInt((r.Width - img.Width) / 2)

            r.Width = img.Width
            r.Height = img.Height
        End If

        Return r
    End Function


End Class
