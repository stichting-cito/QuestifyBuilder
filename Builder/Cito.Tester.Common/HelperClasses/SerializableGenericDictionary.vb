Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Public Class SerializableGenericDictionary(Of TKey, TValue)
    Inherits Dictionary(Of TKey, TValue)
    Implements IXmlSerializable


    Protected Sub New(info As SerializationInfo, content As StreamingContext)
        MyBase.New(info, content)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



    Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function


    Public Shared Function ParseFrom(dict As Dictionary(Of TKey, TValue)) As SerializableGenericDictionary(Of TKey, TValue)
        Dim returnValue As New SerializableGenericDictionary(Of TKey, TValue)

        For Each pair As KeyValuePair(Of TKey, TValue) In dict
            returnValue.Add(pair.Key, pair.Value)
        Next

        Return returnValue
    End Function

    Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
        Dim keySerializer As New XmlSerializer(GetType(TKey))
        Dim valueSerializer As New XmlSerializer(GetType(TValue))
        Dim wasEmpty As Boolean = reader.IsEmptyElement

        reader.Read()

        If wasEmpty Then
            Return
        End If

        While reader.NodeType <> XmlNodeType.EndElement
            reader.ReadStartElement("item")
            reader.ReadStartElement("key")
            Dim key As TKey = DirectCast(keySerializer.Deserialize(reader), TKey)
            reader.ReadEndElement()
            reader.ReadStartElement("value")
            Dim value As TValue = DirectCast(valueSerializer.Deserialize(reader), TValue)
            reader.ReadEndElement()
            Me.Add(key, value)
            reader.ReadEndElement()
            reader.MoveToContent()
        End While
        reader.ReadEndElement()
    End Sub

    Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
        Dim keySerializer As New XmlSerializer(GetType(TKey))
        Dim valueSerializer As New XmlSerializer(GetType(TValue))

        For Each key As TKey In Me.Keys
            writer.WriteStartElement("item")
            writer.WriteStartElement("key")
            keySerializer.Serialize(writer, key)
            writer.WriteEndElement()
            writer.WriteStartElement("value")
            Dim value As TValue = Me(key)
            valueSerializer.Serialize(writer, value)
            writer.WriteEndElement()
            writer.WriteEndElement()
        Next
    End Sub


End Class