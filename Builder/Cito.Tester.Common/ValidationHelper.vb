Imports System.Diagnostics.CodeAnalysis
Imports System.Text.RegularExpressions
Imports System.Xml


Public NotInheritable Class ValidationHelper

    Private Sub New()
    End Sub

    Public Shared Function IsNotEmpty(validation As Object) As Boolean
        If validation IsNot Nothing Then
            Return Not String.IsNullOrEmpty(validation.ToString())
        Else
            Return False
        End If
    End Function

    Public Shared Function IsNumeric(validation As String) As Boolean
        Dim output As Double = 0
        Return Double.TryParse(validation, output)
    End Function

    Public Shared Function IsFollowingRegexRule(validation As String, regexExpression As String) As Boolean
        Dim regExEngine As New Regex(regexExpression)

        Dim m As Match = regExEngine.Match(validation)
        If m.Success AndAlso m.Captures.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function IsBetweenNumericValues(validation As Double, betweenFrom As Double, betweenTo As Double) As Boolean
        Return (validation >= betweenFrom AndAlso validation <= betweenTo)
    End Function

    Public Shared Function IsBetweenNumericValues(validation As Integer, betweenFrom As Integer, betweenTo As Integer) As Boolean
        Return (validation >= betweenFrom AndAlso validation <= betweenTo)
    End Function

    Public Shared Function IsBetweenNumericValues(validation As Single, betweenFrom As Single, betweenTo As Single) As Boolean
        Return (validation >= betweenFrom AndAlso validation <= betweenTo)
    End Function

    Public Shared Function IsBetweenNumericValues(validation As Decimal, betweenFrom As Decimal, betweenTo As Decimal) As Boolean
        Return (validation >= betweenFrom AndAlso validation <= betweenTo)
    End Function


    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Shared Function DoesntContainsCharacters(validation As String, illegalChars As String) As Boolean
        If Not String.IsNullOrEmpty(illegalChars) AndAlso Not String.IsNullOrEmpty(validation) Then
            Return Not (validation.IndexOfAny(illegalChars.ToCharArray()) > -1)
        Else
            Return False
        End If

    End Function


    Public Shared Function DoesntConsistIfSpacesOnly(value As String) As Boolean
        Return Not IsNotEmpty(value.Trim())
    End Function



    Public Shared Function IsValidTestCode(value As String) As String
        If Not IsNotEmpty(value) Then Return My.Resources.ValidationHelper_CodeRequiredField
        If Not DoesntContainsCharacters(DirectCast(value, String), "<>""#%{}|\/^~[]';?:@=&^$+()!,`*") Then Return My.Resources.ValidationHelper_CodeCannotContainIllegalCharacters
        Return String.Empty
    End Function


    Public Shared Function IsValidResourceCode(value As String) As String
        Dim result As String = String.Empty
        Dim message As String = String.Empty

        If Not IsNotEmpty(value) Then
            result = My.Resources.ValidationHelper_CodeRequiredField
        ElseIf DoesntConsistIfSpacesOnly(value) Then
            result = My.Resources.ValidationHelper_CodeNotOnlyWhiteSpaces
        ElseIf Not DoesntContainsCharacters(DirectCast(value, String), "<>""#%{}|\/^~[]';?:@=&^$+()!,`*") Then
            result = My.Resources.ValidationHelper_CodeCannotContainIllegalCharacters
        ElseIf value.Length > 255 Then
            result = My.Resources.ValidationHelper_CodeCannotContainMoreThen255Characters
        ElseIf value.Replace(" ", "").Contains("..") Then
            result = My.Resources.ValidationHelper_CodeCannotContainMultiplePeriodsInARow
        ElseIf Not IsValidNCName(value, message) Then
            result = String.Format(My.Resources.ValidationHelper_CodeIsNotAValidNcName + "{0}", message)
        End If

        Return result
    End Function

    Public Shared Function IsValidNCName(value As String, Optional ByRef message As String = "") As Boolean
        Try
            Dim formattedValue = value.Replace(" ", "_")
            formattedValue = $"prefix-{formattedValue}"
            Return Not String.IsNullOrEmpty(XmlConvert.VerifyNCName(formattedValue))
        Catch ex As XmlException
            If message IsNot Nothing Then
                message = $": {ex.Message}"
            End If
            Return False
        End Try
    End Function

    Public Shared Function IsValidYear(value As String) As Boolean
        Return value.Trim().Length = 4
    End Function

    Public Shared Function IsValidSchoolYear(value As String) As Boolean
        If value.Trim().Length = 9 Then
            Dim years As String() = value.Split("-".ToCharArray())

            Return CInt(years(0)) = CInt(years(1)) - 1
        End If

        Return False
    End Function

    Public Shared Function IsValidPassword(value As String) As Boolean
        Dim charCategories As New Dictionary(Of String, String)
        charCategories.Add("LowerCaseChars", "abcdefghijklmnopqrstuvwxyz")
        charCategories.Add("UpperCaseChars", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        charCategories.Add("Numeric", "0123456789")
        charCategories.Add("Symbols", ".-#@~!$^*()+={}[]")

        If value.Length < 8 OrElse value.Length > 40 Then
            Return False
        End If

        Dim usedCategories As New List(Of String)
        For Each passwordChar As Char In value.ToCharArray()
            Dim found As Boolean = False
            For Each category As KeyValuePair(Of String, String) In charCategories
                If category.Value.IndexOf(passwordChar) > -1 Then
                    found = True
                    If Not usedCategories.Contains(category.Key) Then
                        usedCategories.Add(category.Key)
                    End If
                    Exit For
                End If
            Next

            If Not found Then
                Return False
            End If
        Next

        If usedCategories.Count < 3 Then
            Return False
        End If

        Return True
    End Function

End Class
