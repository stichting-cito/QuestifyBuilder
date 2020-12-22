Imports Cito.Tester.Common

Public Class GeneralTestPackageValidator
    Inherits EntityValidationBase(Of GeneralTestPackage)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralTestPackage, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))
            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField
            Case "Password"
                If ValidationHelper.IsNotEmpty(value) AndAlso Not ValidationHelper.IsValidPassword(CType(value, String)) Then
                    Return My.Resources.PasswordRules
                ElseIf Not ValidationHelper.IsNotEmpty(value) Then
                    Return My.Resources.PasswordIsARequiredField
                End If

            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.TestPackageViewBase_FriendlyEntityName
        End Get
    End Property
End Class
