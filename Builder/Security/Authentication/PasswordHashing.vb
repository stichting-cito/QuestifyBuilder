Imports System.Security.Cryptography

Public NotInheritable Class PasswordHashing
    Private Const SaltSize As Integer = 16
    Private Const HashSize As Integer = 20
    Public Const Prefix As String = "$LEB@n0n$"
    Private Const IterationCount As Integer = 1000

    Public Shared Function HashContainsExpectedPrefix(ByVal hashString As String) As Boolean
        Return hashString.Contains(Prefix)
    End Function

    Public Shared Function CreateHash(ByVal password As String) As String
        Dim salt As Byte() = New Byte(SaltSize) {}

        Dim saltGenerator = New RNGCryptoServiceProvider
        saltGenerator.GetBytes(salt)

        Dim hashTool = New Rfc2898DeriveBytes(password, salt, IterationCount)
        Dim hash As Byte() = hashTool.GetBytes(HashSize)

        Dim base64Salt As String = Convert.ToBase64String(salt)
        Dim base64Hash As String = Convert.ToBase64String(hash)

        Return String.Format("{0}{1}:{2}", Prefix, base64Salt, base64Hash)

    End Function


    Public Shared Function Verify(ByVal password As String, ByVal hashedPassword As String) As Boolean
        If Not HashContainsExpectedPrefix(hashedPassword) Then
            Throw New NotSupportedException("The hashtype is not supported")
        End If

        Dim hashString As String = hashedPassword.Replace(Prefix, "")

        Dim hashParts As String() = hashString.Split(":")
        Dim originalSalt As Byte() = Convert.FromBase64String(hashParts(0))
        Dim originalHash As Byte() = Convert.FromBase64String(hashParts(1))

        Dim hashTool = New Rfc2898DeriveBytes(password, originalSalt, IterationCount)
        Dim hash As Byte() = hashTool.GetBytes(originalHash.Length)
        If Not hash.Length = originalHash.Length Then
            Return False
        End If

        For i As Integer = 0 To Hash.Length - 1

            If originalHash(i) <> hash(i) Then
                Return False
            End If
        Next

        Return True
    End Function

End Class

