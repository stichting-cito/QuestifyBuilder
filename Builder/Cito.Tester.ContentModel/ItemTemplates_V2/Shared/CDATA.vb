Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Public Class CDATA
    Implements IXmlSerializable


    Private _text As String



    Public Sub New()
    End Sub

    Public Sub New(text As String)
        Me._text = text
    End Sub



    Public ReadOnly Property Text As String
        Get
            Return _text
        End Get
    End Property



    Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function


    Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
        Me._text = reader.ReadElementString
    End Sub

    Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
        writer.WriteCData(Me._text)
    End Sub


End Class