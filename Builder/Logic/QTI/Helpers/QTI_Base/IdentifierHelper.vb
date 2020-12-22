Imports System.Text.RegularExpressions

Namespace QTI.Helpers.QTI_Base

    Public Class IdentifierHelper

        Public Shared Function GetStrippedId(id As String) As String
            Dim result As String = id
            If String.IsNullOrEmpty(result) Then Return result

            Dim idRegEx As New Regex(".*?(?<idExtension>\[.*\].*?).*?", RegexOptions.IgnoreCase)
            For Each match As Match In idRegEx.Matches(id)
                Dim partToStrip As String = match.Groups("idExtension").Value
                If Not String.IsNullOrEmpty(partToStrip) Then result = result.Replace(partToStrip, String.Empty)
            Next

            Dim inputIdRegEx As New Regex(".*?(?<inputId>Input[A-Z])", RegexOptions.IgnoreCase)
            For Each match As Match In inputIdRegEx.Matches(result)
                Dim inputIdPart As String = match.Groups("inputId").Value
                If Not String.IsNullOrEmpty(inputIdPart) Then result = inputIdPart
            Next

            Return result
        End Function

    End Class

End Namespace