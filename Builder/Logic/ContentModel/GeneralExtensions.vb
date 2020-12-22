Imports System.Collections.Concurrent
Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Module GeneralExtensions

    <Extension>
    Public Sub Clear(Of T)(blockingCollection As BlockingCollection(Of T))
        If blockingCollection Is Nothing Then
            Return
        End If

        While blockingCollection.Count > 0
            Dim item As T
            blockingCollection.TryTake(item)
        End While
    End Sub


    <Extension>
    Public Function IsCultureInvariantDecimal(input As String) As Boolean
        Return input.IsCultureInvariantDecimal(New Decimal)
    End Function


    <Extension>
    Public Function IsCultureInvariantDecimal(input As String, ByRef output As Decimal) As Boolean
        Return Decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, output)
    End Function

    <Extension>
    Public Function GetCoordinateX(input As String) As String
        Return input.GetCoordinateFromString("x"c)
    End Function

    <Extension>
    Public Function GetCoordinateY(input As String) As String
        Return input.GetCoordinateFromString("y"c)
    End Function

    <Extension>
    Private Function GetCoordinateFromString(input As String, axis As Char) As String
        Dim match = Regex.Match(input, $"{axis}:[\s\S]*?\)")
        If match.Value IsNot Nothing Then
            Return match.Value.Replace($"{axis}:", "").Replace(")", "")
        End If
        Return String.Empty
    End Function


End Module
