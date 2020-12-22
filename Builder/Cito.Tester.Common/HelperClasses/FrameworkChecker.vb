Imports Microsoft.Win32

Public Class FrameworkChecker

    Private Const V1_0 As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\.NETFramework\Policy\v1.0\3705"
    Private Const V1_1 As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v1.1.4322\Install"
    Private Const V2_0 As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v2.0.50727\Install"
    Private Const V3_0 As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup\InstallSuccess"
    Private Const V3_5 As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v3.5\Install"
    Private Const V4_0_Client_Profile As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v4\Client\Install"
    Private Const V4_0_Full_Profile As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v4\Full\Install"


    Public Shared Function IsVersionInstalled(version As String, Optional full As Boolean = False) As Boolean

        Dim ndpkey As String = Nothing
        Select Case version
            Case "v1.0"
                ndpkey = V1_0
            Case "v1.1"
                ndpkey = V1_1
            Case "v2.0"
                ndpkey = V2_0
            Case "v3.0"
                ndpkey = V3_0
            Case "v3.5"
                ndpkey = V3_5
            Case "v4.0"
                If full Then
                    ndpkey = V4_0_Full_Profile
                Else
                    ndpkey = V4_0_Client_Profile
                End If
        End Select

        If String.IsNullOrEmpty(ndpkey) Then
            Return False
        End If

        Dim indexOfValueName = ndpkey.LastIndexOf("\"c)
        Dim ndpRegValue = ndpkey.Substring(indexOfValueName + 1)
        Dim ndpRegKey = ndpkey.Remove(indexOfValueName)

        Try
            Return Registry.GetValue(ndpRegKey, ndpRegValue, "0").ToString() = "1"
        Catch e As Exception
            Return False
        End Try

    End Function
End Class
