Imports System.Text
Imports System.Reflection
Imports Cito.Tester.Common

Public Class MhtFile
    Private _mhtBuilder As StringBuilder
    Private Const MimeBoundaryTag As String = "----=_NextPart_000_00"

    Public Sub New()
        _MhtBuilder = New StringBuilder(String.Empty)
    End Sub

    Public Sub AppendMhtBinaryFile(file As BinaryResource, resourcePath As String)
        Dim contentType As String = FileHelper.GetMimeFromByteArray(resourcePath, DirectCast(file.ResourceObject, Byte()))

        AppendMhtBoundary()
        AppendMhtLine("Content-Type: " & contentType)
        AppendMhtLine("Content-Transfer-Encoding: base64")
        AppendMhtLine("Content-Location: " & resourcePath)
        AppendMhtLine()

        Const chunkSize As Integer = 57
        Dim len As Integer = DirectCast(file.ResourceObject, Byte()).Length
        If len <= chunkSize Then
            AppendMhtLine(Convert.ToBase64String(DirectCast(file.ResourceObject, Byte()), 0, len))
        Else
            Dim i As Integer = 0
            Do While i + chunkSize < len
                AppendMhtLine(Convert.ToBase64String(DirectCast(file.ResourceObject, Byte()), i, chunkSize))
                i += chunkSize
            Loop
            If i <> len Then
                AppendMhtLine(Convert.ToBase64String(DirectCast(file.ResourceObject, Byte()), i, len - i))
            End If
        End If
    End Sub

    Public Sub AppendMhtTextFile(xmlHtmlString As String)
        AppendMhtBoundary()
        AppendMhtLine("Content-Type: text/html; charset=""ANSI;")
        AppendMhtLine("Content-Transfer-Encoding: quoted-printable")
        AppendMhtLine()
        AppendMhtLine(QuotedPrintableEncode(xmlHtmlString, Encoding.Default))
    End Sub

    Public Function FinalizeMht() As String
        Dim s As String = _MhtBuilder.ToString
        _MhtBuilder = Nothing
        Return s
    End Function

    Public Sub AppendMhtHeader()
        _MhtBuilder = New StringBuilder

        AppendMhtLine("From: <Saved by " & Environment.UserName & " on " & Environment.MachineName & ">")
        AppendMhtLine("Subject: Mht File for Docx.")
        AppendMhtLine("Date: " & DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz"))
        AppendMhtLine("MIME-Version: 1.0")
        AppendMhtLine("Content-Type: multipart/related;")
        AppendMhtLine(Convert.ToChar(9) & "type=""text/html"";")
        AppendMhtLine(Convert.ToChar(9) & "boundary=""" & MimeBoundaryTag & """")
        AppendMhtLine("X-MimeOLE: Produced by " & Me.GetType.ToString & " " & Assembly.GetExecutingAssembly.GetName.Version.ToString())
        AppendMhtLine()
        AppendMhtLine("This is a multi-part message in MIME format.")
    End Sub
    Public Sub AppendMhtBoundary(endOfBoundary As Boolean)
        If endOfBoundary = False Then
            AppendMhtLine("--" & MimeBoundaryTag)
        Else
            AppendMhtLine("--" & MimeBoundaryTag & "--")
        End If

    End Sub



    Private Sub AppendMhtLine(Optional ByVal s As String = "")
        _MhtBuilder.Append(s)
        _MhtBuilder.Append(Environment.NewLine)
    End Sub

    Private Sub AppendMhtBoundary()
        AppendMhtBoundary(False)
    End Sub



    Private Function QuotedPrintableEncode(s As String, e As Encoding) As String
        Dim ascii As Integer
        Dim lastSpace As Integer = 0
        Dim lineLength As Integer = 0
        Dim lineBreaks As Integer = 0
        Dim sb As New StringBuilder

        If s Is Nothing OrElse s.Length = 0 Then
            Return ""
        End If

        For Each c As Char In s

            ascii = Convert.ToInt32(c)

            If ascii = 61 OrElse ascii > 126 Then
                If ascii <= 255 Then
                    sb.Append("=")
                    sb.Append(Convert.ToString(ascii, 16).ToUpper)
                    lineLength += 3
                Else
                    For Each b As Byte In e.GetBytes(c)
                        sb.Append("=")
                        sb.Append(Convert.ToString(b, 16).ToUpper)
                        lineLength += 3
                    Next
                End If
            Else
                sb.Append(c)
                lineLength += 1
                If ascii = 32 Then lastSpace = sb.Length
            End If

            If lineLength >= 73 Then
                If lastSpace = 0 Then
                    sb.Insert(sb.Length, "=" & Environment.NewLine)
                    lineLength = 0
                Else
                    sb.Insert(lastSpace, "=" & Environment.NewLine)
                    lineLength = sb.Length - lastSpace - 1
                End If
                lineBreaks += 1
                lastSpace = 0
            End If

        Next

        If lineBreaks > 0 Then
            If sb.Chars(sb.Length - 1) = " " Then
                sb.Remove(sb.Length - 1, 1)
                sb.Append("=20")
            End If
        End If

        Return sb.ToString
    End Function


End Class

