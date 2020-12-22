Imports Cito.Tester.Common

Public Class GeneralTestSectionValidator
    Inherits EntityValidationBase(Of GeneralTestSection)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralTestSection, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.IdentifierIsARequiredField

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField

            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.TestSectionViewBase_FriendlyEntityName
        End Get
    End Property
End Class
