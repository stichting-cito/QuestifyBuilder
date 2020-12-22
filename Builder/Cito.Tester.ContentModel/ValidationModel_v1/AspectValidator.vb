Imports Cito.Tester.Common

Public Class AspectValidator
    Inherits EntityValidationBase(Of Aspect)

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return "Aspect properties"
        End Get
    End Property

    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As Aspect, fieldName As String, value As Object) As String

        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then
                    Return My.Resources.TitleIsARequiredField
                End If
            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function
End Class
