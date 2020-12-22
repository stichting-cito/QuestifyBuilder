Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Public Class NamedDecimalMap

    Private ReadOnly _values As New Dictionary(Of String, List(Of Decimal))

    Private Shared part As String = "\s*\w+\s*:\s*(-)?\d+(\.\d+)?\s*"
    Private Shared parts As String = part & "(," & part & ")*"
    Private Shared groupParts As String = "^(\{" & parts & "\})*$"
    Private Shared validateRegex As Regex = New Regex(groupParts)

    Public Shared Function FromString(formattedString As String) As NamedDecimalMap
        Const part As String = "(?'name'\w+):(?'value'-?\d+(\.\d+)?)"

        Dim correctFormat = validateRegex.IsMatch(formattedString)
        If (Not correctFormat) Then Throw New ArgumentException()

        Dim workingObject As Dictionary(Of String, List(Of Decimal)) = CreateWorkingObject(formattedString, part)

        Dim result = New NamedDecimalMap()

        For Each s As KeyValuePair(Of String, List(Of Decimal)) In workingObject
            result.SetValuesFor(s.Key, s.Value)
        Next

        If (Not result.IsValid()) Then Throw New ArgumentException()

        Return result
    End Function

    Private Shared Function CreateWorkingObject(formattedString As String, partString As String) As Dictionary(Of String, List(Of Decimal))

        Dim regex = New Regex(partString)
        Dim matches = regex.Matches(formattedString)

        Dim workingObject = New Dictionary(Of String, List(Of Decimal))
        For Each match As Match In matches

            Dim name = match.Groups("name").Value
            Dim value = match.Groups("value").Value

            If (Not workingObject.ContainsKey(name)) Then
                workingObject.Add(name, New List(Of Decimal)())
            End If

            workingObject(name).Add(Decimal.Parse(value, CultureInfo.InvariantCulture))
        Next
        Return workingObject
    End Function

    Public Sub SetValuesFor(name As String, values As IEnumerable(Of Decimal))
        If (Not _values.ContainsKey(name)) Then
            _values.Add(name, New List(Of Decimal)())
        End If

        _values(name) = New List(Of Decimal)(values)
    End Sub


    Public Function IsValid() As Boolean
        Return _values.All(Function(kvp) kvp.Value.Count = Length)
    End Function

    Public Function GetNames() As IEnumerable(Of String)
        Return _values.Keys
    End Function

    Public Function GetValuesFor(name As String) As IEnumerable(Of Decimal)
        Return _values(name)
    End Function

    Private ReadOnly Property Length As Integer
        Get
            If (_values.Count > 0) Then Return _values.First().Value.Count
            Return 0
        End Get
    End Property

    Public Overrides Function ToString() As String
        If (Not IsValid()) Then Throw New InvalidOperationException()

        Dim sb As New StringBuilder()

        For position = 0 To Length - 1
            sb.Append(ValueToString(position, ","))
        Next

        Return sb.ToString()
    End Function

    Public Function GetSolutions() As String()
        If (Not IsValid()) Then Throw New InvalidOperationException()

        Dim result(Length - 1) As String

        For position = 0 To Length - 1
            result(position) = ValueToString(position, ";")
        Next

        Return result
    End Function


    Private Function ValueToString(position As Integer, separator As String) As String
        Dim sb As New StringBuilder()

        sb.Append("{")
        Dim isFirst As Boolean = True
        For Each name As String In _values.Keys
            If (Not isFirst) Then sb.Append(separator)
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}", name, _values(name)(position))
            isFirst = False
        Next
        sb.Append("}")
        Return sb.ToString()

    End Function
End Class
