Imports System.Runtime.Serialization
Imports System.Xml
Namespace HelperClasses
    Public Class XmlElementSerializationSurrogate
        Implements ISerializationSurrogate


        Public Sub GetObjectData(ByVal obj As Object, ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializationSurrogate.GetObjectData
            Dim element As XmlNode = CType(obj, XmlElement)
            info.AddValue("data", element.OuterXml)
        End Sub

        Public Function SetObjectData(ByVal obj As Object, ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext, ByVal selector As System.Runtime.Serialization.ISurrogateSelector) As Object Implements System.Runtime.Serialization.ISerializationSurrogate.SetObjectData
            Dim data As String = info.GetString("data")
            Dim doc As New XmlDocument()
            doc.LoadXml(data)
            obj = doc.DocumentElement
            Return CType(doc.DocumentElement, XmlElement)
        End Function


    End Class
End Namespace