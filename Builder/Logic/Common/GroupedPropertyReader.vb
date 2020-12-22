Imports System.Text.RegularExpressions

Namespace Common

    Public Class GroupedPropertyReader

        Private ReadOnly _str As String

        Public Sub New(ByVal str As String)
            If (str Is Nothing) Then Throw New ArgumentNullException("str")
            _str = str
        End Sub

        Public Function GetAsList() As List(Of Dictionary(Of String, String))
            Dim ret As New List(Of Dictionary(Of String, String))(Parse())

            Return ret
        End Function

        Public Function GetAsDictionary(ByVal key As String) As Dictionary(Of String, Dictionary(Of String, String))
            Dim ret As New Dictionary(Of String, Dictionary(Of String, String))

            For Each d In GetAsList()
                Debug.Assert(d.ContainsKey(key), "A entry exists that does not have this key")
                ret.Add(d(key), d)
            Next

            Return ret
        End Function

        Friend Iterator Function Parse() As IEnumerable(Of Dictionary(Of String, String))

            Dim pattern = New Regex("(?:\()" + "(?<Payload>[^\)]+)" + "(?:\))")
            Dim innerPattern = New Regex("(?<key>.+?)=(?<val>[^;]+);?")

            For Each m As Match In pattern.Matches(_str)
                Dim ret As New Dictionary(Of String, String)(StringComparer.InvariantCultureIgnoreCase)
                For Each m2 As Match In innerPattern.Matches(m.Groups("Payload").Value)
                    ret.Add(m2.Groups("key").Value, m2.Groups("val").Value)
                Next
                Yield ret
            Next

        End Function

    End Class

End Namespace
