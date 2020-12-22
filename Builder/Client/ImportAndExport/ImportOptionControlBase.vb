Public Class ImportOptionControlBase

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Property ChangesMade As Boolean = False

    Public ReadOnly Property ConfigurationOptions As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Property ErrorMessage As String

End Class