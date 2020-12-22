Imports Cito.Tester.Common

Public Class ItemSource


    Public Sub RenderSource(ByVal forceRender As Boolean)
        If Me.AssessmentItem IsNot Nothing AndAlso (forceRender OrElse String.IsNullOrEmpty(ItemSourceTextBox.Text)) Then
            ItemSourceTextBox.Text = SerializeHelper.XmlSerializeToString(Me.AssessmentItem, new SerializationRequestDTO() With {.Indent = True})
            ItemSourceTextBox.ForeColor = System.Drawing.ColorTranslator.FromHtml("#444444")
        End If
    End Sub


    Private Sub handleKeyDown(sender As Object, e As KeyEventArgs) Handles ItemSourceTextBox.KeyDown
        If (e.Control AndAlso e.KeyCode = Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
            e.Handled = True
        End If
    End Sub

End Class
