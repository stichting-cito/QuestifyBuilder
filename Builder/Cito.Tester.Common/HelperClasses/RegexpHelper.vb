Imports System.Text.RegularExpressions

Public NotInheritable Class RegexpHelper
    Public Shared ReadOnly WIDTH As String = "width"
    Public Shared ReadOnly HEIGHT As String = "height"
    Public Shared ReadOnly RESOURCENAME As String = "resourceName"
    Public Shared ReadOnly TEXT As String = "text"
    Public Shared ReadOnly PERCTRANSPARENCY As String = "perctransparancy"
    Public Shared ReadOnly EXTENSION As String = "extension"
    Public Shared ReadOnly BASE64 As String = ";base64,"
    Private Shared ReadOnly PlaceHolderRegExpString As String = $"resource://package(:\d+)?/placeholderimage_(?<{WIDTH}>\d+)_(?<{HEIGHT}>\d+)_(?<{RESOURCENAME}>.*)"
    Private Shared ReadOnly GapMatchImageRegExpString As String = $"resource://package(:\d+)?/gapmatchimage_(?<{WIDTH}>\d+)_(?<{HEIGHT}>\d+)_(?<{PERCTRANSPARENCY}>\d+)_(?<{TEXT}>.*(?x))\.(?<4>\w+)"
    Private Shared ReadOnly GapMatchMathMlImageRegExpString As String = $"resource://package(:\d+)?/hsmathml_(?<{WIDTH}>\d+)_(?<{HEIGHT}>\d+)_(?<{PERCTRANSPARENCY}>\d+)_(?<{RESOURCENAME}>.*)"
    Private Shared ReadOnly NotSupportedPlaceholderRegExpString As String = $"resource://package(:\d+)?/notsupportedimage_(?<{WIDTH}>\d+)_(?<{HEIGHT}>\d+)_(?<{TEXT}>.*(?x))\.(?<3>\w+)"
    Private Shared ReadOnly Base64ImageRegExpString As String = $"resource://package(\d+)?(:?)\/data:image\/(\w+){BASE64}"
    Private Shared placeHolderRegEx As New Regex(PlaceHolderRegExpString)

    Private Sub New()
    End Sub

    Public Shared Function TryMatchPlaceHolder(input As String, ByRef result As Match) As Boolean
        result = placeHolderRegEx.Match(input)
        Return result.Success
    End Function

    Public Shared Function TryMatchBase64ImageSource(input As String, ByRef result As Match) As Boolean
        Dim regex As New Regex(Base64ImageRegExpString)
        result = regex.Match(input)

        Return result.Success
    End Function

    Public Shared Function TryMatchGapMatchImage(input As String, ByRef result As Match) As Boolean
        Dim regex As New Regex(GapMatchImageRegExpString)
        result = regex.Match(input)

        Return result.Success
    End Function

    Public Shared Function TryMatchGapMatchMathMLImage(input As String, ByRef result As Match) As Boolean
        Dim regex As New Regex(GapMatchMathMlImageRegExpString)
        result = regex.Match(input)

        Return result.Success
    End Function

    Public Shared Function TryMatchNotSupportedPlaceholder(input As String, ByRef result As Match) As Boolean
        Dim regex As New Regex(NotSupportedPlaceholderRegExpString)
        result = regex.Match(input)

        Return result.Success
    End Function
End Class
