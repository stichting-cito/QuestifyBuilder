Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Imports Cito.Tester.Common.Logging


Public Class AspV3StyleParser


    Private _foundReferences As List(Of String)
    Private _functions As String
    Private _template As String




    Public Sub New(template As String, functions As String)
        _template = template
        _functions = functions
        _foundReferences = New List(Of String)
    End Sub




    Public ReadOnly Property FoundReferences As ReadOnlyCollection(Of String)
        Get
            Return New ReadOnlyCollection(Of String)(_foundReferences)
        End Get
    End Property





    Public Function GetParsedResult() As String
        Log.TraceInformation(TraceCategory.RenderItem, My.Resources.Trace_AspV3StyleParser_GetParsedResult_ConstructCode)
        Dim modifiedcode As String = GetCode()

        Dim finalcode As String =
          "        Imports System" & Environment.NewLine &
          "        Imports System.Linq" & Environment.NewLine &
          "        Imports System.Xml.Linq" & Environment.NewLine &
          "        Imports System.Collections.Generic" & Environment.NewLine &
          "        Imports Cito.Tester" & Environment.NewLine &
          "        Imports Cito.Tester.ContentModel" & Environment.NewLine & Environment.NewLine &
          "        Public Class Parser" & Environment.NewLine &
          "           Public Shared Function Render(parameters As ParameterCollection, enableDebugger as Boolean) As String" & Environment.NewLine &
          "			    Using mStream As New System.IO.MemoryStream()" & Environment.NewLine &
          "			    Using writer As  New System.IO.StreamWriter(mStream,System.Text.Encoding.UTF8)" & Environment.NewLine &
          "               If enableDebugger Then" & Environment.NewLine &
          "                   System.Diagnostics.Debugger.Break()" & Environment.NewLine &
          "               End If" & Environment.NewLine &
          "#:::#RenderMethod#:::#" & Environment.NewLine &
          "               Using sr As New System.IO.StreamReader(mStream)" & Environment.NewLine &
          "			        writer.Flush()" & Environment.NewLine &
          "			        mStream.Position = 0" & Environment.NewLine &
          "			        return sr.ReadToEnd()" & Environment.NewLine &
          "               End Using" & Environment.NewLine &
          "             End Using" & Environment.NewLine &
          "             End Using" & Environment.NewLine &
          "           End Function" & Environment.NewLine &
          "			" & _functions &
          "       End Class"

        finalcode = Regex.Replace(finalcode, "#:::#RenderMethod#:::#", modifiedcode)

        Return finalcode
    End Function


    Private Shared Sub ConvertContentToCode(code As String, writer As StreamWriter)
        Dim lines() As String = code.Split(Chr(10))
        For i As Integer = 0 To lines.Length - 1
            lines(i) = lines(i).Replace(Chr(13), String.Empty)
            If i = lines.Length - 1 Then
                writer.Write(Chr(9) & Chr(9) & Chr(9) & Chr(9) & "writer.Write(""" & lines(i) & """)" & Environment.NewLine)
            Else
                writer.Write(Chr(9) & Chr(9) & Chr(9) & Chr(9) & "writer.Write(""" & lines(i) & """ & Environment.NewLine)" & Environment.NewLine)
            End If
        Next
    End Sub



    Friend Function GetCode() As String
        Dim finalCode As String = ParseScript()

        finalCode = Regex.Replace(finalCode, "<%=.*?%>", New MatchEvaluator(AddressOf RefineCodeShorthand), RegexOptions.Singleline)
        finalCode = Regex.Replace(finalCode, "<%\x23.*?%>", New MatchEvaluator(AddressOf RefineParameterShorthand), RegexOptions.Singleline)
        finalCode = Regex.Replace(finalCode, "<%%>", String.Empty, RegexOptions.Singleline)
        finalCode = Regex.Replace(finalCode, "<%[^=|@].*?%>", New MatchEvaluator(AddressOf RefineCodeTag), RegexOptions.Singleline)

        finalCode = Regex.Replace(finalCode, "(?i)<%@\s*Assembly\s*(?<Reference>.*?)\s*%>", New MatchEvaluator(AddressOf RefineAssemblyReference), RegexOptions.Singleline)

        Return finalCode
    End Function



    Private Function ParseScript() As String
        Dim lcCode As String = _template

        If lcCode Is Nothing Then
            Return ""
        End If
        Using mStream As MemoryStream = New MemoryStream
            Using writer As StreamWriter = New StreamWriter(mStream, Encoding.UTF8)
                Dim lnLast As Integer = 0
                Dim lnAt2 As Integer = 0
                Dim lnAt As Integer = lcCode.IndexOf("<%", 0)
                If lnAt < 1 Then
                    ConvertContentToCode(lcCode.Replace("""", """"""), writer)
                End If
                While lnAt > -1
                    If lnAt > -1 Then
                        ConvertContentToCode(lcCode.Substring(lnLast, lnAt - lnLast).Replace("""", """"""), writer)
                    End If
                    lnAt2 = lcCode.IndexOf("%>", lnAt)
                    If lnAt2 < 0 Then
                    End If
                    writer.Write(lcCode.Substring(lnAt, lnAt2 - lnAt + 2))
                    lnLast = lnAt2 + 2
                    lnAt = lcCode.IndexOf("<%", lnLast)
                    If lnAt < 0 Then
                        ConvertContentToCode(lcCode.Substring(lnLast, lcCode.Length - lnLast).Replace("""", """"""), writer)
                    End If
                End While
                writer.Flush()

                mStream.Position = 0
                Dim sr As StreamReader = New StreamReader(mStream)
                Dim returndata As String = sr.ReadToEnd
                sr.Close()
                writer.Close()
                Return returndata

            End Using
        End Using
    End Function



    Private Function RefineAssemblyReference(m As Match) As String
        Dim reference As String = m.Groups("Reference").Captures(0).Value

        _foundReferences.Add(reference)

        Return String.Empty
    End Function



    Private Function RefineCodeShorthand(m As Match) As String
        Dim x As String = m.ToString
        Dim y As Regex = New Regex("<%=(?'parameter'[^%]+)\W{0,}%>")

        Dim found As Match = y.Match(x)
        Return String.Concat(String.Format("{0}{0}{0}{0}writer.Write(", Chr(9)),
            found.Groups("parameter").Value, ")", Environment.NewLine)
    End Function



    Private Function RefineCodeTag(m As Match) As String
        Dim x As String = m.ToString
        x = x.Substring(2, x.Length - 4)
        x = Chr(9) & Chr(9) & Chr(9) & Chr(9) & x & Environment.NewLine
        Return x
    End Function



    Private Function RefineParameterShorthand(m As Match) As String
        Dim x As String = m.ToString
        Dim y As Regex = New Regex("<%\x23(?'parameter'[^%\W]+)\W{0,}%>")

        Dim found As Match = y.Match(x)
        Return String.Concat(String.Format("{0}{0}{0}{0}writer.Write(parameters.GetParameterByName(""", Chr(9)),
            found.Groups("parameter").Value, """).Value.ToString())", Environment.NewLine)
    End Function


End Class