Imports System.Xml

Public Interface ITextRange
    ReadOnly Property Node As XmlNode
    Property Text As String
    ReadOnly Property [End] As XmlNode
    ReadOnly Property Start As XmlNode
    Property XmlText As String


    Sub SetXmlElement(element As XmlElement)
    Sub RemoveStyle(propertyName As String, propertyValue As String)
    Sub RemoveTag(tagName As String)
    Sub ApplyStyle(propertyName As String, propertyValue As String)
    Sub RemoveClass(className As String)
    Sub ApplyClass(className As String)
    Sub ApplyClass(className As String, type As StyleType)
    Sub ApplyTag(tagName As String)
    Sub ClearFormatting()
    Sub MoveTo(node As XmlNode)
    Sub Move(offset As Integer, length As Integer)
    Sub [Select]()
    Sub Normalize()
    Sub Trim()

    Function IsTagApplied(tagName As String) As Boolean
    Function IsClassApplied(className As String) As Boolean
    Function IsStyleApplied(propertyName As String) As Boolean
    Function GetStyleValue(propertyName As String) As String

End Interface
