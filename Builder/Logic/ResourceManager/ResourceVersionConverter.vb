Namespace ResourceManager
    Public Class ResourceVersionConverter

        Private Const MajorVersionSeed As Integer = 10000


        Public Shared Function ConvertVersion(currentValue As String) As Integer
            If String.IsNullOrEmpty(currentValue) OrElse currentValue = "0" Then
                Return 1
            End If

            If Not currentValue.Contains(".") Then
                Return Integer.Parse(currentValue)
            End If

            Try
                Dim versionArray As String() = currentValue.Split(New Char() {"."c})
                Dim major As Integer = Integer.Parse(versionArray(0))
                Dim minor As Integer = Integer.Parse(versionArray(1))

                Return (major * MajorVersionSeed) + minor
            Catch ex As Exception
                Return 1
            End Try

        End Function


        Public Shared Function ConvertBackVersion(currentValue As Integer) As String

            If currentValue < MajorVersionSeed Then
                Return $"{0}.{IIf(currentValue = 0, 1, currentValue)}"
            Else
                Dim major As Integer = Convert.ToInt16(Math.Round(currentValue / MajorVersionSeed))
                Dim minor As Integer = currentValue - major * MajorVersionSeed
                Return $"{major}.{minor}"
            End If

        End Function


        Public Shared Function FromString(version As String) As Version
            If String.IsNullOrEmpty(version) OrElse version = "0" Then
                Return New Version(0, 1)
            End If

            If Not version.Contains(".") Then
                Return New Version(0, Integer.Parse(version))
            End If

            Dim versionArray As String() = version.Split(New Char() {"."c})
            Dim major As Integer = Integer.Parse(versionArray(0))
            Dim minor As Integer = Integer.Parse(versionArray(1))

            return new Version(major, minor)
        End Function
    End Class
End NameSpace