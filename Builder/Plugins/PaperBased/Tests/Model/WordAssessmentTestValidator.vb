Imports System.IO
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class WordAssessmentTestValidator
    Inherits EntityValidationBase(Of WordAssessmentTest)

    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As WordAssessmentTest, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                Return ValidationHelper.IsValidResourceCode(DirectCast(value, String))

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField

            Case "PrintForm"
                If TypeOf value Is PrintFormCollection Then
                    Dim pfCollection = DirectCast(value, PrintFormCollection)
                    If pfCollection.Count = 0 Then
                        Return My.Resources.PrintFormIsARequiredField
                    ElseIf Not pfCollection.Any(Function(pf) Not String.IsNullOrEmpty(pf.ResourceName)) Then
                        Return My.Resources.PrintFormIsARequiredField
                    Else
                        Dim allNonEmptyLabelCount = pfCollection.Where(Function(x) Not String.IsNullOrEmpty(x.TypeLabel)).Count
                        Dim distinctNonEmptyLabelCount = pfCollection.Where(Function(x) Not String.IsNullOrEmpty(x.TypeLabel)).Select(Function(x) x.TypeLabel).Distinct().Count

                        If allNonEmptyLabelCount <> distinctNonEmptyLabelCount Then
                            Return My.Resources.PrintFormLabelsMustBeUnique
                        ElseIf pfCollection.Where(Function(pf) pf.Id <> Guid.Empty AndAlso String.IsNullOrEmpty(pf.TypeLabel)).Count > 0 Then
                            Return My.Resources.PrintFormLabelsAreRequired
                        ElseIf pfCollection.Where(Function(pf) pf.Id <> Guid.Empty AndAlso String.IsNullOrEmpty(pf.ResourceName)).Any() Then
                            Return My.Resources.PrintFormWordTemplatesAreRequired
                        ElseIf pfCollection.Where(Function(pf) pf.Id <> Guid.Empty AndAlso pf.TypeLabel.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0).Count > 0 Then
                            Dim InvalidCharsAsString As New String(Path.GetInvalidFileNameChars().Where(Function(x) Not Char.IsControl(x)).ToArray())

                            Return String.Format(My.Resources.PrintFormLabelsMayNotContainFollowingCharacters, InvalidCharsAsString)
                        ElseIf pfCollection.Any(Function(pf) pf.TypeLabel.Length > 30) Then
                            Return String.Format(My.Resources.PrintFormLabelsMaxLength, 30)
                        End If
                    End If
                End If

        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName() As String
        Get
            Return My.Resources.AssessmentTestViewBase_FriendlyEntityName
        End Get
    End Property
End Class
