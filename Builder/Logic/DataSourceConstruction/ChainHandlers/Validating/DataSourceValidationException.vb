Imports Questify.Builder.Logic.Chain

Public Class DataSourceValidationException
    Inherits ChainHandlerException

    Private ReadOnly _validationErrors As New Dictionary(Of String, String)

    Sub AddValidationError(item As String, location As String)
        _validationErrors.Add(item, location)
    End Sub

    Public ReadOnly Property ValidationErrors() As IDictionary(Of String, String)
        Get
            Return _validationErrors
        End Get
    End Property

End Class
