Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml.Linq

Public NotInheritable Class SerializeHelper


    Private Sub New()
    End Sub




    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"),
SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")>
    Public Shared Function GetMD5Hash(serializable As Object) As Byte()
        ReflectionHelper.CheckIsNotNothing(serializable, "Serializable object")

        Using memStream As New MemoryStream()
            Dim serializer As New XmlSerializer(serializable.GetType())
            serializer.Serialize(memStream, serializable)

            memStream.Seek(0, SeekOrigin.Begin)

            Using reader As New StreamReader(memStream)
                Dim xmlAsString As String = XElement.Parse(reader.ReadToEnd()).ToString(SaveOptions.DisableFormatting)

                Using provider As New MD5CryptoServiceProvider
                    Return provider.ComputeHash(Encoding.UTF8.GetBytes(xmlAsString))
                End Using
            End Using
        End Using
    End Function

    Private Shared Sub DebugGetHash(serializable As Object)
        Using stream As New MemoryStream()
            Dim formatter As New XmlSerializer(serializable.GetType())
            formatter.Serialize(stream, serializable)

            stream.Seek(0, SeekOrigin.Begin)

            Using reader As New StreamReader(stream)
                Dim dbgFrm As New Form
                Dim txtbox As New TextBox
                dbgFrm.Controls.Add(txtbox)
                txtbox.Multiline = True
                txtbox.Dock = DockStyle.Fill
                txtbox.Text = $"Ticks = {DateTime.Now.Ticks.ToString}"
                txtbox.Text &= vbCrLf
                txtbox.Text &= vbCrLf
                txtbox.Text &= reader.ReadToEnd
                dbgFrm.Show()

                reader.Close()
                reader.Dispose()
            End Using
        End Using
    End Sub

    Public Shared Function XmlDeserializeFromByteArray(data As Byte(), type As Type, preserveWhiteSpace As Boolean) As Object
        Using s As New MemoryStream(data)
            Dim result As Object

            Dim serializer As New XmlSerializer(type)

            If preserveWhiteSpace Then
                Using reader As XmlReader = XmlReader.Create(s)
                    result = serializer.Deserialize(reader, String.Empty)
                End Using
            Else
                result = serializer.Deserialize(s)
            End If

            Return result
        End Using
    End Function


    Public Shared Function XmlDeserializeFromByteArray(data As Byte(), type As Type) As Object
        Return XmlDeserializeFromByteArray(data, type, False)
    End Function


    Public Shared Function XmlDeserializeFromFile(path As String, type As Type) As Object
        Using stream As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
            Return XmlDeserializeFromStream(stream, type)
        End Using
    End Function

    Public Shared Function XmlDeserializeFromReader(textReader As TextReader, type As Type) As Object
        Dim serializer As New XmlSerializer(type)
        Return serializer.Deserialize(textReader)
    End Function

    Public Shared Function XmlDeserializeFromStream(stream As Stream, type As Type) As Object
        Dim serializer As New XmlSerializer(type)
        Try
            Return serializer.Deserialize(XmlReader.Create(stream, New XmlReaderSettings() With {.IgnoreWhitespace = False}))
        Catch ex As Exception
            Dim str = GetAciiStringWithControlCharsReplaced(stream, 5)
            If str <> "<?xml" Then Throw New XmlException("Expected XML, but data starts with '" + str + "'.", ex)
            Throw
        End Try
    End Function

    Public Shared Function XmlDeserializeFromStream(Of T)(stream As Stream) As T
        Return DirectCast(XmlDeserializeFromStream(stream, GetType(T)), T)
    End Function

    Public Shared Function XmlDeserializeFromString(xmlContent As String, type As Type) As Object
        Using stringreader As New StringReader(xmlContent)
            Return XmlDeserializeFromReader(stringreader, type)
        End Using
    End Function

    Public Shared Function XmlDeserializeFromString(Of T)(xmlContent As String) As T
        Return DirectCast(XmlDeserializeFromString(xmlContent, GetType(T)), T)
    End Function

    Public Shared Function XmlSerializeToByteArray(obj As Object) As Byte()
        Using s As New MemoryStream()
            XmlSerializeToStream(s, obj)
            Return s.ToArray()
        End Using
    End Function


    Public Shared Sub XmlSerializeToFile(path As String, obj As Object)
        Using stream As New FileStream(path, FileMode.Create)
            XmlSerializeToStream(stream, obj)
        End Using
    End Sub


    Public Shared Sub XmlSerializeToStream(stream As Stream, obj As Object)
        If obj IsNot Nothing Then
            Dim settings As New XmlWriterSettings() With {.Indent = False, .Encoding = new UTF8Encoding(false)}
            Using writer As XmlWriter = XmlWriter.Create(stream, settings)
                Dim serializer As New XmlSerializer(obj.GetType())
                serializer.Serialize(writer, obj)
            End Using
        Else
            Throw New TesterException(My.Resources.Error_SerializeHelper_XmlSerializeTo_ObjectIsNothing)
        End If
    End Sub


    Public Shared Function XmlSerializeToString(obj As Object) As String
        Return XmlSerializeToString(obj, False)
    End Function

    Public Shared Function XmlSerializeToString(obj As Object, stripNameSpaces As Boolean) As String
        Return XmlSerializeToString(obj, stripNameSpaces, Nothing, False, Encoding.UTF8)
    End Function


    Public Shared Function XmlSerializeToString(obj As Object, stripNameSpaces As Boolean, omitXmlDeclaration As Boolean) As String
        Return XmlSerializeToString(obj, stripNameSpaces, Nothing, omitXmlDeclaration, Encoding.UTF8)
    End Function

    Public Shared Function XmlSerializeToString(obj As Object, request As SerializationRequestDTO) As String
        Dim xmlString As String = String.Empty
        If request.OmitXmlDeclaration Then
            xmlString = XmlSerializeToStringWithOptions(obj, request)
        Else
            Using stringwriter As New StringWriter
                XmlSerializeToWriter(stringwriter, obj, request)
                xmlString = stringwriter.ToString()
            End Using
        End If
        Return xmlString
    End Function

    Public Shared Function XmlSerializeToString(obj As Object, stripNameSpaces As Boolean, nameSpaces As XmlSerializerNamespaces, omitXmlDeclaration As Boolean, encoding As Encoding) As String
        Dim request = New SerializationRequestDTO With {
            .StripNameSpaces = stripNameSpaces,
            .NameSpaces = nameSpaces,
            .OmitXmlDeclaration = omitXmlDeclaration,
            .Encoding = encoding,
            .Indent = False
        }

        Return XmlSerializeToString(obj, request)
    End Function

    Public Shared Function XmlSerializeToString(obj As Object, stripNameSpaces As Boolean, omitXmlDeclaration As Boolean, encoding As Encoding) As String
        Return XmlSerializeToString(obj, stripNameSpaces, Nothing, omitXmlDeclaration, encoding)
    End Function


    Public Shared Sub XmlSerializeToWriter(writer As TextWriter, obj As Object)
        XmlSerializeToWriter(writer, obj, False, Nothing)
    End Sub

    Public Shared Sub XmlSerializeToWriter(writer As TextWriter, obj As Object, request As SerializationRequestDTO)

        If obj IsNot Nothing Then
            Try
                Dim settings = New XmlWriterSettings() With {.Encoding = request.Encoding, .Indent = request.Indent}

                Using xmlWriter As XmlWriter = XmlWriter.Create(writer, settings)
                    Dim serializer As XmlSerializer
                    If Not String.IsNullOrEmpty(request.OverrideDefaultNameSpace) OrElse Not String.IsNullOrEmpty(request.OverrideClassName) Then
                        Dim attrOverrides As New XmlAttributeOverrides()
                        Dim xAttrs As New XmlAttributes()
                        Dim xRoot As New XmlRootAttribute()
                        xRoot.Namespace = request.OverrideDefaultNameSpace
                        xRoot.ElementName = request.OverrideClassName
                        xAttrs.XmlRoot = xRoot

                        attrOverrides.Add(obj.GetType, request.OverrideClassName, xAttrs)
                        serializer = New XmlSerializer(obj.GetType(), attrOverrides)
                    Else
                        serializer = New XmlSerializer(obj.GetType())
                    End If
                    If request.StripNameSpaces Then
                        If request.NameSpaces Is Nothing Then
                            request.NameSpaces = New XmlSerializerNamespaces()
                        End If
                        request.NameSpaces.Add("", "")
                        serializer.Serialize(xmlWriter, obj, request.NameSpaces)
                    Else
                        serializer.Serialize(xmlWriter, obj, request.NameSpaces)
                    End If
                End Using
            Catch ex As Exception
                Throw
            End Try
        Else
            Throw New TesterException(My.Resources.Error_SerializeHelper_XmlSerializeTo_ObjectIsNothing)
        End If
    End Sub

    Public Shared Sub XmlSerializeToWriter(writer As TextWriter, obj As Object, stripNameSpaces As Boolean, nameSpaces As XmlSerializerNamespaces)
        Dim request As New SerializationRequestDTO With
        {
                .Indent = False,
                .StripNameSpaces = stripNameSpaces,
                .NameSpaces = nameSpaces
        }

        XmlSerializeToWriter(writer, obj, request)
    End Sub

    Private Shared Function XmlSerializeToStringWithOptions(obj As Object, request As SerializationRequestDTO) As String
        Dim returnValue As String = String.Empty
        If obj IsNot Nothing Then
            Try
                Using ms As New MemoryStream()
                    Dim settings As New XmlWriterSettings()
                    settings.Indent = request.Indent
                    settings.OmitXmlDeclaration = request.OmitXmlDeclaration
                    settings.Encoding = request.Encoding

                    Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                        Dim serializer As XmlSerializer

                        If Not String.IsNullOrEmpty(request.OverrideDefaultNameSpace) OrElse Not String.IsNullOrEmpty(request.OverrideClassName) Then
                            Dim attrOverrides As New XmlAttributeOverrides()
                            Dim xAttrs As New XmlAttributes()
                            Dim xRoot As New XmlRootAttribute()
                            xRoot.Namespace = request.OverrideDefaultNameSpace
                            xRoot.ElementName = request.OverrideClassName
                            xAttrs.XmlRoot = xRoot
                            attrOverrides.Add(obj.GetType, xAttrs)
                            serializer = New XmlSerializer(obj.GetType(), attrOverrides)
                        Else
                            serializer = New XmlSerializer(obj.GetType())
                        End If

                        If request.StripNameSpaces Then
                            If request.NameSpaces Is Nothing Then
                                request.NameSpaces = New XmlSerializerNamespaces()
                            End If
                            request.NameSpaces.Add("", "")
                        End If

                        serializer.Serialize(writer, obj, request.NameSpaces)
                        returnValue = request.Encoding.GetString(ms.ToArray)
                    End Using
                End Using
                Return returnValue
            Catch ex As Exception
                Throw
            End Try
        Else
            Throw New TesterException(My.Resources.Error_SerializeHelper_XmlSerializeTo_ObjectIsNothing)
        End If
    End Function


    Public Shared Function XmlSerializableClone(Of T)(obj As T) As T
        Using objectStream As New MemoryStream()
            XmlSerializeToStream(objectStream, obj)

            objectStream.Seek(0, SeekOrigin.Begin)

            Return DirectCast(XmlDeserializeFromStream(objectStream, obj.GetType()), T)
        End Using
    End Function


    Public Shared Function GetAciiStringWithControlCharsReplaced(stream As Stream, length As Integer) As String
        stream.Seek(0, SeekOrigin.Begin)
        Dim buf(length) As Byte
        Dim size = stream.Read(buf, 0, length)
        Return ReplaceControlChars(Encoding.ASCII.GetString(buf, 0, size))
    End Function


    Public Shared Function ReplaceControlChars(str As String) As String
        Return Regex.Replace(str, "\p{Cc}", Function(a) $"[{Asc(a.Value(0)):X2}]")
    End Function


End Class