Public Class RemoveCustomPropertyWithConnectedResourcesDialog

    Private Shared ReadOnly _splitCharacter As Char = ";"c

    Private Sub New()
        InitializeComponent()

    End Sub

    Public Shared ReadOnly Property SplitCharacter As Char
        Get
            Return _splitCharacter
        End Get
    End Property

    Public Sub New(ByVal labelText As String, ByVal scrollableText As String)
        Me.New()
        Label1.Text = labelText
        DummyColumnHeader.Width = ListView1.Width - 5

        For Each item As String In scrollableText.Split(_splitCharacter)
            ListView1.Items.Add(New ListViewItem(item))

            If (DummyColumnHeader.Width < TextRenderer.MeasureText(item, New Font(ListView1.Font.Name, ListView1.Font.Size)).Width) Then
                ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If
        Next
    End Sub

End Class