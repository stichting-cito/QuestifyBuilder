Namespace Extended_Classes
    Public NotInheritable Class VersionHelper

        Public Shared Function TryParseVersion(ByVal version As String, ByRef majorVersion As Integer, ByRef minorVersion As Integer) As Boolean
            Dim splittedVersion As String() = version.Split(".".ToCharArray())

            If splittedVersion.Length = 1 Then
                Return False
            ElseIf splittedVersion.Length = 2 Then
                If Not Integer.TryParse(splittedVersion(0), majorVersion) Then Return False
                If Not Integer.TryParse(splittedVersion(1), minorVersion) Then Return False
            Else
                Return False
            End If

            Return True
        End Function

        Public Shared Function CreateVersionString(ByVal majorVersion As Integer, ByVal minorVersion As Integer) As String
            Return String.Concat(majorVersion, ".", minorVersion)
        End Function

    End Class
End NameSpace