Public Class SourceGenericResourceEditor


    Private Sub SourceTextEditorControl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SourceTextEditorControl.Validating
        Dim sourceText As String
        Dim sourceBytes() As Byte

        sourceText = SourceTextEditorControl.Text
        sourceBytes = (New System.Text.UTF8Encoding()).GetBytes(sourceText)
        Me.DataSource.ResourceData.BinData = sourceBytes
    End Sub



    Protected Overrides Sub DataBind()
        Dim sourceText As String
        sourceText = System.Text.Encoding.UTF8.GetString(Me.DataSource.ResourceData.BinData())

        SourceTextEditorControl.Text = sourceText
    End Sub



    Public Overrides Sub PreSave()
    End Sub


End Class