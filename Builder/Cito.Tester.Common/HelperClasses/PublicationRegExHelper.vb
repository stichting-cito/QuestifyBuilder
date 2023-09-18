Imports System.Text

Public NotInheritable Class PublicationRegExHelper

    Private Sub New()

    End Sub

    Public Shared Function CreateIntegerRegEx(numberOfDigits As Integer, isUnsignedEntry As Boolean) As String
        Dim builder As New StringBuilder()

        builder.Append("^")

        If Not isUnsignedEntry Then
            builder.Append("-?")
        End If

        builder.Append("([0-9]{1,")
        builder.Append(Convert.ToString(numberOfDigits))
        builder.Append("})?")
        builder.Append("$")

        Return builder.ToString()
    End Function

    Public Shared Function CreateDecimalRegEx(numberOfDigits As Integer, numberOfDecimals As Integer, isUnsignedEntry As Boolean) As String
        Return CreateDecimalRegEx(numberOfDigits, numberOfDecimals, isUnsignedEntry, "\,")
    End Function


    Public Shared Function CreateDecimalRegEx(numberOfDigits As Integer, numberOfDecimals As Integer, isUnsignedEntry As Boolean, acceptedSeperators As String) As String
        Dim builder As New StringBuilder()
        builder.Append("^")

        If Not isUnsignedEntry Then
            builder.Append("-?")
        End If

        builder.Append("([0-9]{1,")
        builder.Append(Convert.ToString(numberOfDigits))
        builder.Append("})?")

        If numberOfDecimals > 0 Then
            If String.IsNullOrEmpty(acceptedSeperators) Then
                Debug.Assert(True, "No separator defined!")
                acceptedSeperators = "\."
            End If
            builder.Append("(([")
            For Each acceptedSeparator In acceptedSeperators.Split("|"c)
                builder.Append(acceptedSeparator)
            Next
            builder.Append("])")
            builder.Append("([0-9]{0,")
            builder.Append(Convert.ToString(numberOfDecimals))
            builder.Append("}))?")
        End If
        builder.Append("$")
        Return builder.ToString()
    End Function

    Public Shared Function CreateCurrencyRegEx(numberOfDigits As Integer, numberOfDecimals As Integer, isUnsignedEntry As Boolean) As String
        Return CreateDecimalRegEx(numberOfDigits, numberOfDecimals, isUnsignedEntry)
    End Function

    Public Shared Function CreateCurrencyRegEx(numberOfDigits As Integer, numberOfDecimals As Integer, isUnsignedEntry As Boolean, acceptedSeperators As String) As String
        Return CreateDecimalRegEx(numberOfDigits, numberOfDecimals, isUnsignedEntry, acceptedSeperators)
    End Function

    Public Shared Function CreateDateRegEx(dateSubType As String) As String
        Dim builder As New StringBuilder()

        If dateSubType.Length > 0 Then
            Select Case dateSubType
                Case "dutch"
                    builder.Append("^(0?[1-9]|[12][0-9]|3[01])[-](0?[1-9]|1[012])[-](19|20)\d\d$")
                Case "american"
                    builder.Append("^(0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])[/](19|20)\d\d$")
                Case "scandinavian"
                    builder.Append("^(0?[1-9]|[12][0-9]|3[01])[.](0?[1-9]|1[012])[.](19|20)\d\d$")
            End Select
        Else
            builder.Append("^(0?[1-9]|[12][0-9]|3[01])[-](0?[1-9]|1[012])[-](19|20)\d\d$")
        End If

        Return builder.ToString()
    End Function

    Public Shared Function CreateTimeRegEx(timeSubType As String) As String
        Dim builder As New StringBuilder()
        Dim timePartResult As String = String.Empty
        If String.IsNullOrEmpty(timeSubType) Then timeSubType = "hhmm"

        builder.Append("^")

        For x As Integer = 0 To timeSubType.Length - 1 Step 2
            If x > 0 Then builder.Append(":")
            timePartResult = CreateTimePartRegEx(timeSubType.Substring(x, 2))
            builder.Append(timePartResult.Substring(1, timeSubType.Length - 2))
        Next

        builder.Append("$")

        Return builder.ToString()
    End Function

    Public Shared Function CreateTimePartRegEx(timePart As String) As String
        Dim builder As New StringBuilder()

        If timePart.Length > 0 Then
            Select Case timePart
                Case "hh"
                    builder.Append("^(([0-1]?[0-9])|([2][0-3]))$")
                Case Else
                    builder.Append("^([0-5]?[0-9])$")
            End Select
        End If

        Return builder.ToString()
    End Function

    Public Shared Function CreateDatePartRegEx(datePart As String) As String
        Dim builder As New StringBuilder()

        If datePart.Length > 0 Then
            Select Case datePart.ToLower
                Case "mm"
                    builder.Append("^(?![0])(([0-9])|([1][0-2]))$")
                Case "dd"
                    builder.Append("^(?![0])(([0-2]?[0-9])|([3][0-1]))$")
                Case "yyyy"
                    builder.Append("^([0-9]{1,4})$")
            End Select
        End If

        Return builder.ToString()
    End Function

    Public Shared Function CreateZipCodeRegEx() As String
        Return "^([0-9]{1,4})?([a-z|A-Z]{1,2})?$"
    End Function

    Public Shared Function CreateStringRegEx(numberOfCharacters As Integer, autoInputProcessing As String) As String
        Dim builder As New StringBuilder()

        If autoInputProcessing.Length > 0 Then
            Select Case autoInputProcessing.ToLower()
                Case "uppercase", "uppercaseauto"
                    builder.Append("^[^a-z]{0,")
                    builder.Append(Convert.ToString(numberOfCharacters))
                    builder.Append("}$")
                Case "lowercase", "lowercaseauto"
                    builder.Append("^[^A-Z]{0,")
                    builder.Append(Convert.ToString(numberOfCharacters))
                    builder.Append("}$")
                Case Else
                    builder.Append("^.{0,")
                    builder.Append(Convert.ToString(numberOfCharacters))
                    builder.Append("}$")
            End Select
        Else
            builder.Append("^.{0,")
            builder.Append(Convert.ToString(numberOfCharacters))
            builder.Append("}$")
        End If

        Return builder.ToString()
    End Function

    Public Shared Function CreateGeneralRegEx(numberOfDigits As Integer) As String
        Dim builder As New StringBuilder()

        builder.Append("^.{0,")
        builder.Append(Convert.ToString(numberOfDigits))
        builder.Append("}$")

        Return builder.ToString()
    End Function
End Class
