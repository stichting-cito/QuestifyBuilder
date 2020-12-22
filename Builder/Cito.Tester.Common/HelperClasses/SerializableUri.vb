Imports System.Diagnostics.CodeAnalysis
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

<Serializable()> _
Public NotInheritable Class SerializableUri
    Implements IXmlSerializable


    Private _uri As Uri



    Public Property Uri() As Uri
        Get
            Return _uri
        End Get
        Set(value As Uri)
            _uri = value
        End Set
    End Property



    Public Sub New()
    End Sub

    Public Sub New(uri As Uri)
        If uri Is Nothing Then
            Throw New ArgumentNullException("uri", My.Resources.Error_Serializable_Constructor)
        End If
        _uri = uri
    End Sub

    Public Sub New(uri As String)
        Me.New(New Uri(uri))
    End Sub



    Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function

    <SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), _
 SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
        ReflectionHelper.CheckIsNotNothing(reader, "XMLreader serialization")

        Try
            _uri = New Uri(reader.ReadString)
            reader.ReadEndElement()
        Catch e As Exception
            _uri = Nothing
        End Try
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
        ReflectionHelper.CheckIsNotNothing(writer, "XMLwriter for object conversion")

        If _uri IsNot Nothing Then
            writer.WriteString(_uri.AbsoluteUri)
        End If
    End Sub

    Public Overloads Overrides Function ToString() As String
        If _uri IsNot Nothing Then
            Return _uri.AbsoluteUri
        Else
            Return String.Empty
        End If
    End Function


End Class
