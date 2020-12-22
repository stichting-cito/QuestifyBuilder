
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Xml
Imports System.Xml.Linq

Namespace Conversion

    Public Module ExtensionMethods

        <Extension()>
        Private Function Save(document As XDocument, fileName As String, encodingType As Integer, withBOM As Boolean) As Boolean
            Dim success As Boolean = True
            Try
                Dim writerSettings As XmlWriterSettings = New XmlWriterSettings()
                writerSettings.Indent = True
                If encodingType = 0 Then
                    writerSettings.Encoding = New UTF8Encoding(withBOM)
                    Using writer As XmlWriter = XmlWriter.Create(fileName, writerSettings)
                        document.Save(writer)
                    End Using
                Else
                    writerSettings.Encoding = New UnicodeEncoding(False, withBOM)
                    Using writer As XmlWriter = XmlWriter.Create(fileName, writerSettings)
                        document.Save(writer)
                    End Using
                End If
            Catch ex As Exception
                success = False
            End Try
            Return success
        End Function

        <Extension()>
        Public Sub RemoveWithLineFeeds(element As XElement)
            If Not element.ElementsAfterSelf().Any() Then
                Dim newLineTextNode As XText = element.NodesAfterSelf().OfType(Of XText)().FirstOrDefault()
                If newLineTextNode IsNot Nothing Then
                    Dim value As String = newLineTextNode.Value
                    If value.Length > 1 Then
                        newLineTextNode.AddAfterSelf(New XText(value.Substring(value.IndexOf(ControlChars.Lf) + 1)))
                    End If
                    newLineTextNode.Remove()
                End If
            End If
            If Not element.ElementsBeforeSelf().Any() Then
                Dim newLineTextNode As XText = element.NodesBeforeSelf().OfType(Of XText)().FirstOrDefault()
                If newLineTextNode IsNot Nothing Then
                    Dim value As String = newLineTextNode.Value
                    If value.Length > 1 Then
                        newLineTextNode.AddAfterSelf(New XText(value.Substring(value.IndexOf(ControlChars.Lf) + 1)))
                    End If
                    newLineTextNode.Remove()
                End If
            End If
            element.Remove()
        End Sub

        <Extension()>
        Public Sub RemoveReadOnlyAttributeFromFiles(directory As DirectoryInfo, recursive As Boolean)
            Dim readOnlyFiles As IEnumerable(Of FileInfo) = From f In directory.GetFiles()
                                                            Where (f.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly

            For Each file As FileInfo In readOnlyFiles
                file.Attributes = FileAttributes.Normal
            Next

            If recursive Then
                For Each subDirectory As DirectoryInfo In directory.GetDirectories()
                    subDirectory.RemoveReadOnlyAttributeFromFiles(True)
                Next
            End If
        End Sub

    End Module
End NameSpace