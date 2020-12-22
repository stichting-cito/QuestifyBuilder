Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq

Public Class baseParamValidator

    Public Function XmlDeserializeFromXelementWithDesignerSetting(Of T)(ByVal xelement As XElement) As T
        Using stringreader As New IO.StringReader(xelement.ToString())

            Dim xml_overrides As New XmlAttributeOverrides
            Dim xml_attrbts As New XmlAttributes()
            xml_attrbts.XmlIgnore = False
            xml_attrbts.XmlElements.Add(New XmlElementAttribute("designersetting"))
            xml_overrides.Add(GetType(ParameterBase), "DesignerSettings", xml_attrbts)
            Dim ser As New XmlSerializer(GetType(T), xml_overrides)


            Dim obj As Object = ser.Deserialize(stringreader)
            stringreader.Close()
            stringreader.Dispose()
            Return DirectCast(obj, T)
        End Using
    End Function


    Friend Shared Function XmlDeserializeFromReader(ByVal reader As IO.TextReader, ByVal type As System.Type) As Object
        Dim serializer As New XmlSerializer(type)
        Return serializer.Deserialize(reader)
    End Function
End Class
