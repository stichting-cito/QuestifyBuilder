Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization

Namespace HelperClasses

    Public Module XMLExtensions

        <Extension()>
        Public Function GetXElement(node As XmlNode) As XElement
            Dim xDoc As New XDocument()
            Using xmlWriter As XmlWriter = xDoc.CreateWriter()
                node.WriteTo(xmlWriter)
            End Using
            Return xDoc.Root
        End Function

        <Extension()>
        Public Function GetXmlNode(element As XElement) As XmlNode
            Using xmlReader As XmlReader = element.CreateReader()
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(xmlReader)
                Return xmlDoc
            End Using
        End Function

        <Extension()>
        Public Function GetXmlElement(element As XElement) As XmlElement
            Using xmlReader As XmlReader = element.CreateReader()
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(xmlReader)
                Return xmlDoc.DocumentElement
            End Using
        End Function

        <Extension()>
        Public Function GetXDocument(document As XmlDocument) As XDocument
            Dim xDoc As New XDocument()
            Using xmlWriter As XmlWriter = xDoc.CreateWriter()
                document.WriteTo(xmlWriter)
            End Using
            Dim decl As XmlDeclaration = document.ChildNodes.OfType(Of XmlDeclaration)().FirstOrDefault()
            If decl IsNot Nothing Then
                xDoc.Declaration = New XDeclaration(decl.Version, decl.Encoding, decl.Standalone)
            End If
            Return xDoc
        End Function

        <Extension()>
        Public Function GetXmlDocument(document As XDocument) As XmlDocument
            Using xmlReader As XmlReader = document.CreateReader()
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(xmlReader)
                If document.Declaration IsNot Nothing Then
                    Dim dec As XmlDeclaration = xmlDoc.CreateXmlDeclaration(document.Declaration.Version, document.Declaration.Encoding, document.Declaration.Standalone)
                    xmlDoc.InsertBefore(dec, xmlDoc.FirstChild)
                End If
                Return xmlDoc
            End Using
        End Function

        <Extension()>
        Public Function Deserialize(Of T)(input As XElement) As T
            Dim ret As T
            Dim s = New XmlSerializer(GetType(T))

            Using m As New StringReader(input.ToString())
                ret = DirectCast(s.Deserialize(m), T)
            End Using

            Return ret
        End Function

    End Module

End Namespace