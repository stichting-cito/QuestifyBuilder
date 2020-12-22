Imports System.IO
Imports System.Text.RegularExpressions

Public Class InvalidCharactersHelper
    Private Shared invalidFileNameChars As String = Path.GetInvalidFileNameChars() + "*"
    Private Shared invalidPathChars As String = Path.GetInvalidPathChars()
    Private Shared invalidCodeChars As String = Path.GetInvalidPathChars() + Path.GetInvalidFileNameChars() + "<>""#%{}|\/^~[]';?:@=&^$+()!,`*"

    Public Shared Function IsValidFileName(fileName As String) As Boolean
        Return fileName.Equals(ReplaceInvalidFileNameChars(fileName))
    End Function

    Public Shared Function ReplaceInvalidFileNameChars(fileName As String) As String
        For Each str As Char In invalidFileNameChars
            fileName = fileName.Replace(str, "_")
        Next
        Return fileName
    End Function

    Public Shared Function IsValidPath(path As String) As Boolean
        Return path.Equals(ReplaceInvalidPathChars(path))
    End Function

    Public Shared Function ReplaceInvalidPathChars(path As String) As String
        For Each str As Char In invalidPathChars
            path = path.Replace(str, "_")
        Next
        Return path
    End Function


    Public Shared Function ReplaceInvalidFileNameAndPathChars(value As String) As String
        Return Regex.Replace(value, invalidFileNameChars + invalidPathChars, "_").Trim()
    End Function



    Public Shared Function IsValidCode(code As String) As Boolean
        Return code.Equals(ReplaceInvalidCodeChars(code))
    End Function

    Public Shared Function ReplaceInvalidCodeChars(code As String) As String
        Return ReplaceInvalidCodeChars(code, False, True)
    End Function

    Public Shared Function ReplaceInvalidCodeChars(code As String, replacePeriod As Boolean) As String
        Return ReplaceInvalidCodeChars(code, False, replacePeriod)
    End Function



    Public Shared Function ReplaceInvalidCodeChars(code As String, skipExtensionCheck As Boolean, replacePeriod As Boolean) As String
        Dim ext As String = If(skipExtensionCheck, String.Empty, Path.GetExtension(code))
        If Not String.IsNullOrEmpty(ext) Then
            code = code.Substring(0, code.LastIndexOf(ext))
        End If
        For Each str As Char In invalidCodeChars
            ext = ext.Replace(str, "_")
            code = code.Replace(str, "_")
        Next
        ext = ext.Replace("é", "e").Replace(".", "").Trim()
        code = code.Replace("é", "e").Trim()
        If replacePeriod Then code = code.Replace(".", "_").Trim()
        Dim result As String
        If (String.IsNullOrEmpty(code)) Then
            result = ext
        Else
            result = If(String.IsNullOrEmpty(ext), code, code & "." & ext)
        End If
        Return result
    End Function



    Public Shared Function IsEscapedXml(unescapedValue As String) As Boolean
        Return unescapedValue.Equals(EscapeXmlLiterals(unescapedValue))
    End Function

    Public Shared Function EscapeXmlLiterals(unescapedValue As String) As String
        unescapedValue = unescapedValue.Replace("&amp;", "temporaryStringToReplaceAmpersandCharacter")
        unescapedValue = unescapedValue.Replace("&quot;", "temporaryStringToReplaceDopubleQuoteCharacter")
        unescapedValue = unescapedValue.Replace("&lt;", "temporaryStringToReplaceLessThanCharacter")
        unescapedValue = unescapedValue.Replace("&gt;", "temporaryStringToReplaceGreaterThanCharacter")
        unescapedValue = unescapedValue.Replace("&", "&amp;")
        unescapedValue = unescapedValue.Replace("""", "&quot;")
        unescapedValue = unescapedValue.Replace("<", "&lt;")
        unescapedValue = unescapedValue.Replace(">", "&gt;")
        unescapedValue = unescapedValue.Replace("temporaryStringToReplaceAmpersandCharacter", "&amp;")
        unescapedValue = unescapedValue.Replace("temporaryStringToReplaceDopubleQuoteCharacter", "&quot;")
        unescapedValue = unescapedValue.Replace("temporaryStringToReplaceLessThanCharacter", "&lt;")
        unescapedValue = unescapedValue.Replace("temporaryStringToReplaceGreaterThanCharacter", "&gt;")
        Return unescapedValue
    End Function

    Public Shared Function UnescapeXmlLiterals(escapedValue As String) As String
        escapedValue = escapedValue.Replace("&amp;", "&")
        escapedValue = escapedValue.Replace("&quot;", """")
        escapedValue = escapedValue.Replace("&lt;", "<")
        escapedValue = escapedValue.Replace("&gt;", ">")
        escapedValue = escapedValue.Replace("&apos;", "'")
        Return escapedValue
    End Function


    Public Shared Function FixFilesWithInvalidCharctersInFileName(fileName As String, temporaryFilePath As String) As String
        Dim fixedFileName As String = fileName

        If Not File.Exists(Path.Combine(temporaryFilePath, fileName)) Then
            fileName = ReplaceInvalidCodeChars(fileName)
        End If

        If File.Exists(Path.Combine(temporaryFilePath, fileName)) Then
            fixedFileName = ReplaceInvalidCodeChars(fileName)

            If (Not fileName = fixedFileName) Then
                My.Computer.FileSystem.RenameFile(Path.Combine(temporaryFilePath, fileName), fixedFileName)
            End If
            Return fixedFileName
        End If
        Throw New Exception("Bestand """ & fileName & """ niet gevonden.")
    End Function
End Class
