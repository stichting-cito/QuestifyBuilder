Imports Cito.Tester.Common

Public Class GeneralTestPartValidator
    Inherits EntityValidationBase(Of GeneralTestPart)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralTestPart, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.IdentifierIsARequiredField
            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.TestPartViewBase_FriendlyEntityName
        End Get
    End Property
End Class
