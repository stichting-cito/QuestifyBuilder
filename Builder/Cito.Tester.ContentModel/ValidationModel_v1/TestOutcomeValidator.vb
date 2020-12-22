Imports Cito.Tester.Common

Public Class TestOutcomeValidator
    Inherits EntityValidationBase(Of TestOutcome)

    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As TestOutcome, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Score"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TestOutcomeValidator_Score

            Case "Outcome"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TestOutcomeValidator_Outcome

            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.TestOutcomeValidator_FriendlyName
        End Get
    End Property

End Class
