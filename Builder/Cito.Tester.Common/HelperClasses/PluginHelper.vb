Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports System.IO

Public NotInheritable Class PluginHelper

    Private Shared ReadOnly PluginCollection As New Dictionary(Of String, Assembly)(StringComparer.OrdinalIgnoreCase)

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Shared Function LoadAssembly(cacheKey As String, resourceStream As StreamResource) As Assembly
        ReflectionHelper.CheckIsNotNothing(resourceStream, "Stream containing assembly")

        Return LoadAssembly(cacheKey, resourceStream.ResourceObject, resourceStream.Length, Nothing, 0)
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Shared Function LoadAssembly(cacheKey As String, assemblyStream As Stream, symbolStream As Stream) As Assembly
        ReflectionHelper.CheckIsNotNothing(assemblyStream, "Stream containing assembly")

        Dim symLength As Long = 0
        If symbolStream IsNot Nothing Then
            symLength = symbolStream.Length
        End If

        Return LoadAssembly(cacheKey, assemblyStream, assemblyStream.Length, symbolStream, symLength)
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Shared Function LoadAssembly(cacheKey As String, assemblyStream As Stream, assemblyLength As Long, symbolStream As Stream, symbolLength As Long) As Assembly
        ReflectionHelper.CheckIsNotNothing(assemblyStream, "Stream containing assembly")

        Dim pluginAssembly As Assembly

        If PluginCollection.ContainsKey(cacheKey) Then
            pluginAssembly = PluginCollection.Item(cacheKey)

            Dim len As Integer
            Const bufLen = 8192
            Dim bin(bufLen) As Byte

            Do
                len = assemblyStream.Read(bin, 0, bufLen)
            Loop While (len > 0)
        Else

            Dim bytes = CInt(assemblyLength)
            Dim fileData(bytes) As Byte
            assemblyStream.Read(fileData, 0, bytes)

            If symbolStream IsNot Nothing Then
                Dim symbolBytes As Integer = CInt(symbolLength)
                Dim symbolData(symbolBytes) As Byte
                symbolStream.Read(symbolData, 0, symbolBytes)

                pluginAssembly = AppDomain.CurrentDomain.Load(fileData, symbolData)
                symbolStream.Close()
            Else
                pluginAssembly = AppDomain.CurrentDomain.Load(fileData)
            End If

            assemblyStream.Close()
            PluginCollection.Add(cacheKey, pluginAssembly)
        End If

        assemblyStream.Close()
        Return pluginAssembly
    End Function

End Class
