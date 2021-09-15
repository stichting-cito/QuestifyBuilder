
Imports System.Xml
Imports System.Runtime.CompilerServices
Imports System.Xml.Linq

Module Extensions

    ''' <summary>
    ''' Converts the XElement to an XmlNode
    ''' </summary>
    ''' <param name="element">The element.</param>
    <Extension()>
    Public Function ToXmlNode(element As XElement) As XmlNode
        Dim root As New XDocument(New XElement("Root", element))
        Using xmlReader As XmlReader = root.CreateReader()
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlReader)
            Return xmlDoc.SelectSingleNode("/Root/*")
        End Using
    End Function

    ''' <summary>
    ''' Converts the XElement to an XmlNode
    ''' </summary>
    ''' <param name="element">The element.</param>
    <Extension()>
    Public Function ToXmlElement(element As XElement) As XmlElement
        Dim ret As XmlNode = element.ToXmlNode()
        If TypeOf ret Is XmlElement Then
            Return DirectCast(ret, XmlElement)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Convert an array of XElements to a XML node list.
    ''' </summary>
    ''' <param name="elements">The elements.</param>
    Public Function ToXmlNodeList(ByVal ParamArray elements() As XElement) As XmlNodeList
        Dim root As New XDocument(New XElement("Root", elements))
        Using xmlReader As XmlReader = root.CreateReader()
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlReader)
            Return xmlDoc.SelectNodes("//Root/*")
        End Using
    End Function

End Module
