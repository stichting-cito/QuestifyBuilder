Imports System.Runtime.Serialization

Namespace HelperClasses

    Public Class BinarySerializationHelper

        Private Sub New()
        End Sub

        Public Shared Function BinaryDeserializeFromStream(Of T)(ByVal s As IO.Stream) As T
            Dim surrogateSelector As New Runtime.Serialization.SurrogateSelector()
            Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter

            surrogateSelector.AddSurrogate(GetType(System.Xml.XmlElement), New StreamingContext(StreamingContextStates.All), New XmlElementSerializationSurrogate())
            surrogateSelector.AddSurrogate(GetType(System.Xml.XmlNode), New StreamingContext(StreamingContextStates.All), New XmlElementSerializationSurrogate())
            formatter.SurrogateSelector = surrogateSelector

            Return CType(formatter.Deserialize(s), T)
        End Function

        Public Shared Sub BinarySerializeToStream(Of T)(ByVal s As IO.Stream, ByVal obj As T)
            Dim surrogateSelector As New Runtime.Serialization.SurrogateSelector()
            Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter

            surrogateSelector.AddSurrogate(GetType(System.Xml.XmlElement), New StreamingContext(StreamingContextStates.All), New XmlElementSerializationSurrogate())
            surrogateSelector.AddSurrogate(GetType(System.Xml.XmlNode), New StreamingContext(StreamingContextStates.All), New XmlElementSerializationSurrogate())
            formatter.SurrogateSelector = surrogateSelector

            formatter.Serialize(s, obj)
        End Sub


        Public Shared Function DeepClone(Of T)(ByVal obj As T) As T
            Using ms As New IO.MemoryStream
                BinarySerializeToStream(Of T)(ms, obj)
                ms.Position = 0
                Return BinaryDeserializeFromStream(Of T)(ms)
            End Using
        End Function

    End Class

End Namespace