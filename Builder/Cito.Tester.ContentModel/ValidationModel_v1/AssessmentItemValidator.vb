
Imports Cito.Tester.Common

Public Class AssessmentItemValidator
    Inherits EntityValidationBase(Of AssessmentItem)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As AssessmentItem, fieldName As String, value As Object) As String
        Select Case fieldName

            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then
                    Return My.Resources.TitleIsARequiredField
                End If

            Case "LayoutTemplateSourceName"
                If Not ValidationHelper.IsNotEmpty(value) Then
                    Return "'Layout Template' is a required field."
                End If

            Case Else
                Return String.Empty
        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return "General item properties"
        End Get
    End Property
End Class
