Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Runtime.Serialization

Public Class BinaryCloner

    Public Shared Function DeepClone(Of T)(objectToClone As T) As T
        Using memStream As New MemoryStream()
            Dim binFormatter As New BinaryFormatter()

            binFormatter.Context = New StreamingContext(StreamingContextStates.Clone)
            binFormatter.Serialize(memStream, objectToClone)
            memStream.Position = 0
            Return DirectCast(binFormatter.Deserialize(memStream), T)
        End Using
    End Function

End Class
