
Imports Cito.Tester.Common


Public Class GeneralAssessmentTestValidator
    Inherits EntityValidationBase(Of GeneralAssessmentTest)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralAssessmentTest, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField

        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.AssessmentTestViewBase_FriendlyEntityName
        End Get
    End Property
End Class
