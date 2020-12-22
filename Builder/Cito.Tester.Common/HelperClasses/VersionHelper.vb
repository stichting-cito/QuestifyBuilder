Imports System.Reflection

Public Class VersionHelper

    Public Shared Function GetInformationalVersion(assembly As Assembly) As String
        Dim attributes() As Object = assembly.GetCustomAttributes(GetType(AssemblyInformationalVersionAttribute), True)
        If attributes.Length = 0 Then
            Return String.Empty
        End If

        Return DirectCast(attributes(0), AssemblyInformationalVersionAttribute).InformationalVersion
    End Function

    Public Shared Function GetFileVersion(assembly As Assembly) As String
        Dim attributes() As Object = assembly.GetCustomAttributes(GetType(AssemblyFileVersionAttribute), True)
        If attributes.Length = 0 Then
            Return String.Empty
        End If

        Return DirectCast(attributes(0), AssemblyFileVersionAttribute).Version
    End Function

    Public Shared Function GetAssemblyVersion(assembly As Assembly) As String
        Return assembly.GetName().Version.ToString()
    End Function

    Public Shared Function GetCopyrightInfo(assembly As Assembly) As String
        Dim attributes() As Object = assembly.GetCustomAttributes(GetType(AssemblyCopyrightAttribute), True)
        If attributes.Length = 0 Then
            Return String.Empty
        End If

        Dim copyrightText As String = DirectCast(attributes(0), AssemblyCopyrightAttribute).Copyright
        Return copyrightText.Replace("(c)", "©")
    End Function


    Public Shared Function GetDescription(assembly As Assembly) As String
        Dim attributes() As Object = assembly.GetCustomAttributes(GetType(AssemblyDescriptionAttribute), True)
        If attributes.Length = 0 Then
            Return String.Empty
        End If

        Dim descText As String = DirectCast(attributes(0), AssemblyDescriptionAttribute).Description
        Return descText
    End Function

End Class
