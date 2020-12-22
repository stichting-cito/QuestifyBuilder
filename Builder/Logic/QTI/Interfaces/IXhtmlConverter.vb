Imports System.Xml

Namespace QTI.Interfaces

    Public Interface IXhtmlConverter

        Function ConvertXhtmlToQti(xHtml As String, checkRoot As Boolean) As String

        Sub Initialise(uniqueId As String)

        Function ConvertStylesToCss(xmlDocument As XmlDocument, ByRef css As String) As Boolean

    End Interface
End NameSpace