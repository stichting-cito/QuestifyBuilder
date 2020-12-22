
Imports Cito.Tester.Common

Public Class InlineElementValidator
    Inherits EntityValidationBase(Of InlineElement)

    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As InlineElement, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))
            Case "Type"
                If Not ValidationHelper.IsNotEmpty(value) Then
                    Return My.Resources.TypeIsARequiredField
                End If
            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return "Inline element"
        End Get
    End Property
End Class
