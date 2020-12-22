Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Wordprocessing
Imports NotesFor.HtmlToOpenXml

Public Class TableCellBorderExtractor
    Public Shared Function Extract(tableCellElement As XElement) As TableCellBorders
        Dim styleAttribute = tableCellElement.Attribute("style")
        If styleAttribute Is Nothing Then
            Return Nothing
        End If

        Dim borderStyles = styleAttribute.Value.Split(";"c).Where(Function(s) s.StartsWith("border-", StringComparison.InvariantCultureIgnoreCase))
        If Not borderStyles.Any Then
            Return Nothing
        End If

        Dim tcB As New TableCellBorders()

        For Each styleSetting In borderStyles
            Dim name As String = styleSetting.Split(":"c)(0).Trim()
            Dim value As String = styleSetting.Split(":"c)(1).Trim()

            Dim tableCellBorderStyle As BorderType = GetBorderType(value, name)
            tcB.Append(tableCellBorderStyle)
        Next

        Return tcB
    End Function

    Public Shared Function GetBorderType(styleSetting As String, name As String) As BorderType
        Dim borderStyle As BorderStyle = GetBorderStyle(styleSetting)
        Dim borderType As BorderType = GetWordProcessingBorderType(name)
        If borderType Is Nothing Then
            Return Nothing
        End If

        ApplyBorderStyle(borderType, borderStyle)

        Return borderType
    End Function

    Public Shared Function GetBorderStyle(styleSetting As String) As BorderStyle
        Dim result As New BorderStyle()

        If styleSetting Is Nothing Then
            Return result
        End If

        For Each part In styleSetting.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

            Dim borderValue As BorderValues = TryParseBorderValue(part)
            If (borderValue <> BorderValues.Nil) Then
                result.BorderValue = borderValue
                Continue For
            End If

            Dim u As Unit = Unit.Parse(part)
            If u.IsValid Then
                result.BorderWidth = u
                Continue For
            End If

            Dim borderColor As String = TryParseBorderColor(part)
            If Not String.IsNullOrEmpty(borderColor) Then
                result.BorderColor = borderColor
            End If
        Next

        Return result
    End Function

    Public Shared Function TryParseBorderColor(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return Nothing
        End If

        If Regex.IsMatch(input, "#[0-9A-F]{6}") Then
            Return input.Substring(1)
        ElseIf input.Equals("black", StringComparison.InvariantCultureIgnoreCase) Then
            Return "000000"
        End If
        Return Nothing
    End Function

    Public Shared Function TryParseBorderValue(input As String) As BorderValues
        If String.IsNullOrEmpty(input) Then
            Return BorderValues.Nil
        End If

        Dim borderValue As BorderValues
        If BorderValues.TryParse(input, True, borderValue) Then
            Return borderValue
        Else
            Select Case input.ToLower()
                Case "solid"
                    Return BorderValues.Single
                Case "hidden"
                    Return BorderValues.None
            End Select
        End If

        Return BorderValues.Nil
    End Function

    Public Shared Sub ApplyBorderStyle(borderType As BorderType, borderStyle As BorderStyle)
        If borderType Is Nothing OrElse borderStyle Is Nothing Then
            Return
        End If

        If borderStyle.BorderValue <> BorderValues.Nil Then
            borderType.Val = borderStyle.BorderValue
        Else
            borderType.Val = BorderValues.Single
        End If

        If Not String.IsNullOrEmpty(borderStyle.BorderColor) Then
            borderType.Color = New StringValue() With {.Value = borderStyle.BorderColor}
        Else
            borderType.Color = New StringValue With {.Value = "000000"}
        End If

        If Not borderStyle.BorderWidth.Equals(Unit.Empty) Then
            borderType.Size = CType(borderStyle.BorderWidth.Value * 4, UInt32)
        End If
    End Sub

    Public Shared Function GetWordProcessingBorderType(name As String) As BorderType
        If String.IsNullOrEmpty(name) Then
            Return Nothing
        End If

        Dim borderType As BorderType = Nothing
        Select Case name.ToLower()
            Case "border-left"
                borderType = New LeftBorder()

            Case "border-right"
                borderType = New RightBorder()

            Case "border-top"
                borderType = New TopBorder()

            Case "border-bottom"
                borderType = New BottomBorder()

            Case Else
                Return Nothing
        End Select

        Return borderType
    End Function
End Class