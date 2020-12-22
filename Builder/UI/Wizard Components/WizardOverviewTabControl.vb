Public Class WizardOverviewTabControl

    Public Property OverviewText() As String
        Get
            Return OverviewRichTextBox.Text
        End Get
        Set(ByVal value As String)
            OverviewRichTextBox.Text = value
        End Set
    End Property

End Class
