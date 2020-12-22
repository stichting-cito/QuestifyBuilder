Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Text
Imports System.Text.RegularExpressions

Public NotInheritable Class FileHelper
    Public Const BlockSize As Integer = 8192



    Public Shared ReadOnly Property MimeTypes As Dictionary(Of String, String)
        Get
            Return MimeTypeRetriever.MimeTypes
        End Get
    End Property

    Public Shared Function HasMatchingFileExtensionAndMimeType(fqfn As String) As Boolean
        Return HasMatchingFileExtensionAndMimeType(fqfn, Nothing)
    End Function

    Public Shared Function HasMatchingFileExtensionAndMimeType(name As String, rawBytes As Byte()) As Boolean
        Dim mimeType As String = String.Empty
        Dim extension As String = Path.GetExtension(name).Trim("."c).ToLower
        If Not MimeTypeRetriever.GetMimeTypeByExtension(extension, mimeType) Then
            mimeType = MimeTypeRetriever.GetMimeTypeFromFile(name, rawBytes)
        End If
        Dim allowedExtensions = MimeTypeRetriever.GetExtensionsForMimeType(mimeType)

        Return Not String.IsNullOrEmpty(extension) AndAlso allowedExtensions.Any(Function(e) e.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
    End Function

    Public Shared Function GetFileExtensionByMimeType(mimeType As String) As String
        Return MimeTypeRetriever.GetFileExtensionByMimeType(mimeType)
    End Function

    Public Shared Function GetMimeFromFile(file As String) As String
        Return MimeTypeRetriever.GetMimeTypeFromFile(file)
    End Function

    Public Shared Function GetMimeFromByteArray(file As String, byteArray() As Byte) As String
        Return MimeTypeRetriever.GetMimeTypeFromFile(file, byteArray)
    End Function
    Public Shared Function GetSizeFromByteArray(bytes As Byte()) As Size
        If bytes IsNot Nothing AndAlso bytes.Length > 0 Then
            Try
                Using ms As New MemoryStream(bytes)
                    Using img As Image = Image.FromStream(ms)
                        Return img.Size
                    End Using
                End Using
            Catch Ex As Exception
            End Try

            Try
                Dim fileContents As String = ASCIIEncoding.ASCII.GetString(bytes)
                Dim svg As New XmlDocument()
                Dim namespaceManager As New XmlNamespaceManager(svg.NameTable)
                namespaceManager.AddNamespace("def", "http://www.w3.org/2000/svg")
                svg.LoadXml(fileContents)

                If svg.DocumentElement.Name = "svg" Then
                    Return New Size(CInt(svg.SelectSingleNode("//def:svg/@width", namespaceManager).Value.TrimEnd("px".ToCharArray())), CInt(svg.SelectSingleNode("//def:svg/@height", namespaceManager).Value.TrimEnd("px".ToCharArray())))
                End If
            Catch ex As Exception
            End Try

            Throw New ArgumentException("Unsupported image format.")
        End If
    End Function

    Public Shared Function MakeFileFromByteArray(filename As String, file As Byte()) As Boolean
        If (filename Is Nothing) OrElse (file Is Nothing) Then
            Return False
        Else
            Try
                Using fs As New FileStream(filename, FileMode.Create)
                    fs.Write(file, 0, file.Length)
                    fs.Flush()
                    Return True
                End Using
            Catch ex As ResourceException
                Return False
            End Try
        End If
    End Function

    Public Shared Function MakeByteArrayFromFile(filename As String) As Byte()
        Using fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim fileSize As Integer = CInt(fs.Length)
            Dim data(fileSize - 1) As Byte
            fs.Read(data, 0, fileSize)

            Return data
        End Using
    End Function

    Public Shared Function GetProgramFilesx86() As String
        If 8 = IntPtr.Size OrElse (Not [String].IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) Then
            Return Environment.GetEnvironmentVariable("ProgramFiles(x86)")
        End If

        Return Environment.GetEnvironmentVariable("ProgramFiles")
    End Function


    Public Shared Function IsImage(filename As String) As Boolean
        Return IsImage(filename, Nothing)
    End Function

    Public Shared Function IsImage(filename As String, binaryData As Byte()) As Boolean
        If File.Exists(filename) Then
            Return CheckFileIsImageByContent(filename)
        Else
            Return CheckFileIsImageByName(filename, binaryData)
        End If
    End Function

    Public Shared Function IsAudio(filename As String) As Boolean
        Return IsAudio(filename, Nothing)
    End Function

    Public Shared Function IsAudio(filename As String, binaryData As Byte()) As Boolean
        Return CheckFileIsAudioByName(filename, binaryData)
    End Function

    Private Shared Function CheckFileIsImageByName(filename As String, Optional ByVal binaryData As Byte() = Nothing) As Boolean
        Dim mimeType As String = MimeTypeRetriever.GetMimeTypeFromFile(filename, binaryData)

        If mimeType.ToLower.Contains("image/") Then
            Return True
        End If

        Return MatchesImageExtension(Path.GetExtension(filename).ToLower())

    End Function

    Private Shared Function CheckFileIsImageByContent(filename As String) As Boolean

        Dim ImageMinimumBytes As Integer = 512
        Dim mimeType As String = GetMimeFromFile(filename)

        If Not mimeType.ToLower.Contains("image/") Then
            Return False
        End If

        If Not MatchesImageExtension(Path.GetExtension(filename).ToLower()) Then
            Return False
        End If

        Dim result As Boolean = True
        Dim fileBytes() = MakeByteArrayFromFile(filename)

        Try
            If fileBytes.Length < ImageMinimumBytes Then
                result = False
            Else
                Using fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
                    Dim buffer As Byte() = New Byte(511) {}
                    fs.Read(buffer, 0, 512)
                    Dim content As String = Encoding.UTF8.GetString(fileBytes)
                    If Regex.IsMatch(content, "<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant Or RegexOptions.Multiline) Then
                        result = False
                    End If
                    Array.Resize(buffer, 0)
                End Using
            End If
        Catch generatedExceptionName As Exception
            result = False
        Finally
            Array.Resize(fileBytes, 0)
        End Try

        Return result

    End Function

    Private Shared Function CheckFileIsAudioByName(filename As String, Optional ByVal binaryData As Byte() = Nothing) As Boolean

        Dim mimeType As String = MimeTypeRetriever.GetMimeTypeFromFile(filename, binaryData)

        If mimeType.ToLower.Contains("audio/") Then
            Return True
        End If

        Return MatchesAudioExtension(Path.GetExtension(filename).ToLower())

    End Function

    Private Shared Function MatchesImageExtension(extension As String) As Boolean
        Dim supportedExtensions as String() = New String() {
            ".jpg",
            ".png",
            ".gif",
            ".svg",
            ".bmp",
            ".jpeg"
        }

        Return supportedExtensions.Contains(extension.ToLower())
    End Function

    Private Shared Function MatchesAudioExtension(extension As String) As Boolean
        Dim supportedExtensions as String() = New String() {
            ".mp3",
            ".opus",
            ".oga",
            ".ogg"
        }

        Return supportedExtensions.Contains(extension.ToLower())
    End Function
End Class
