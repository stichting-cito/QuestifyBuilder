Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing
Imports System.IO

Public NotInheritable Class StreamConverters


    Private Sub New()
    End Sub



    <SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="usingType"), _
 SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="resourceName"), _
 SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Shared Function ConvertStreamToString(resourceName As String, stream As StreamResource, usingType As Type) As Object
        ReflectionHelper.CheckIsNotNothing(stream, "resource stream")

        Dim reader As New StreamReader(stream.ResourceObject)
        Dim readedString As String = String.Empty
        Try
            readedString = reader.ReadToEnd()
        Catch ex As Exception
            Throw New TesterException(String.Format(My.Resources.Error_StreamConverters_ConvertStreamToString_ErrorOccurred, ex.Message), ex)
        Finally
            reader.Close()
            reader = Nothing
        End Try
        Return readedString
    End Function


    <SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="usingType"), _
 SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="resourceName"), _
 SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Shared Function ConvertStreamToImage(resourceName As String, stream As StreamResource, usingType As Type) As Object
        ReflectionHelper.CheckIsNotNothing(stream, "resource stream")
        Return Image.FromStream(stream.ResourceObject)
    End Function


    <SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="resourceName"), _
 SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Shared Function ConvertStreamToTypedInstance(resourceName As String, stream As StreamResource, usingType As Type) As Object
        ReflectionHelper.CheckIsNotNothing(stream, "resource stream")
        Return SerializeHelper.XmlDeserializeFromStream(stream.ResourceObject, usingType)
    End Function


    <SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="usingType"), _
 SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="resourceName"), _
 SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Shared Function ConvertStreamToByteArray(resourceName As String, stream As StreamResource, usingType As Type) As Object
        ReflectionHelper.CheckIsNotNothing(stream, "resource stream")

        Dim data(CType(stream.Length - 1, Integer)) As Byte
        Dim read As Integer

        Using br As New BinaryReader(stream.ResourceObject)
            Try
                read = br.Read(data, 0, CType(stream.Length, Integer))
            Catch ex As Exception
                Throw New TesterException(String.Format(My.Resources.Error_StreamConverters_ConvertStreamToByteArray_ErrorOccurred, ex.Message), ex)
            Finally
                br.Close()
            End Try
        End Using

        If read <> stream.Length Then
            Array.Resize(Of Byte)(data, read)
        End If

        Return data
    End Function


    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), _
    SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="usingType")> _
    Public Shared Function ConvertStreamToAssembly(resourceName As String, stream As StreamResource, usingType As Type) As Object
        ReflectionHelper.CheckIsNotNothing(stream, "resource stream")
        Dim symbolFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{resourceName}.pdb")

        If File.Exists(symbolFilePath) Then
            Dim symbolStream As Stream
            symbolStream = File.OpenRead(symbolFilePath)
            Return PluginHelper.LoadAssembly(resourceName, stream.ResourceObject, stream.Length, symbolStream, symbolStream.Length)
        Else
            Return PluginHelper.LoadAssembly(resourceName, stream)
        End If
    End Function

End Class
