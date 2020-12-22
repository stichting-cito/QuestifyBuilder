Imports System.Text
Imports System.Text.RegularExpressions

Public Class ExtendedNamedDecimalMap

    Private Shared part As String = "\s*\w+\s*:\s*(-)?\d+(\.\d+)?\s*"
    Private Shared parts As String = part & "(," & part & ")*"
    Private Shared mapParts As String = "^(\{" & parts & "\})"
    Private Shared extension As String = "\w+:\w*\d*"
    Private Shared extensionParts As String = "(\[" & extension & "\])*$"
    Private Shared validateRegex As Regex = New Regex(mapParts & "*" & extensionParts)

    Public NamedDecimalMap As NamedDecimalMap

    Private _extensions As Dictionary(Of String, String)

    Public Property Extensions As Dictionary(Of String, String)
        Get
            Return _extensions
        End Get
        Set
            _extensions = value
        End Set
    End Property

    Public Shared Function FromString(formattedString As String) As ExtendedNamedDecimalMap
        Const DECIMALMAP_END_BRACE As String = "}"
        Const COMBINATION_BRACES As String = DECIMALMAP_END_BRACE & "["
        Dim ret As New ExtendedNamedDecimalMap()
        Dim correctFormat = validateRegex.IsMatch(formattedString)
        If Not correctFormat Then Throw New ArgumentException()

        If formattedString.Contains(DECIMALMAP_END_BRACE) Then
            Dim decimalMapStringPart = formattedString.Substring(0, formattedString.LastIndexOf(DECIMALMAP_END_BRACE) + 1)
            ret.NamedDecimalMap = NamedDecimalMap.FromString(decimalMapStringPart)
        End If
        If formattedString.Contains(COMBINATION_BRACES) Then
            Dim extensionMapStringPart = formattedString.Substring(formattedString.LastIndexOf(COMBINATION_BRACES) + 1)
            ret._extensions = ExtractExtensions(extensionMapStringPart)
        End If

        Return ret
    End Function

    Private Shared Function ExtractExtensions(formattedString As String) As Dictionary(Of String, String)
        Const part As String = "(?'name'\w+):(?'value'\w*\d*)"
        Dim regex = New Regex(part)
        Dim matches = regex.Matches(formattedString)

        Dim extensions = New Dictionary(Of String, String)
        For Each match As Match In matches
            Dim name = match.Groups("name").Value
            Dim value = match.Groups("value").Value

            If (Not extensions.ContainsKey(name)) Then
                extensions.Add(name, value)
            End If
        Next
        Return extensions
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder()
        sb.Append(NamedDecimalMap.ToString())

        For Each kvp In _extensions
            sb.Append($"[{kvp.Key}:{kvp.Value}]")
        Next
        Return sb.ToString()
    End Function

    Public Sub New()
        _extensions = New Dictionary(Of String, String)
        NamedDecimalMap = New NamedDecimalMap
    End Sub

End Class
