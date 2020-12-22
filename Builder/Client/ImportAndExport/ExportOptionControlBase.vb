Public Class ExportOptionControlBase




    Public ReadOnly Property ConfigurationOptions As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Property ErrorMessage As String

    Public ReadOnly Property ResultDescription() As String
        Get
            Return My.Resources.Succesfull
        End Get
    End Property

End Class