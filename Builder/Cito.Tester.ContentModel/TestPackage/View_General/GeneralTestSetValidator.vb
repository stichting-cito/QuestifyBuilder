Imports Cito.Tester.Common

Public Class GeneralTestSetValidator
    Inherits EntityValidationBase(Of GeneralTestSet)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralTestSet, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))
            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField
            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.TestSetViewBase_FriendlyEntityName
        End Get
    End Property
End Class
