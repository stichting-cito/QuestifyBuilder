Imports System.Runtime.CompilerServices
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports System.IO

Module ExtensionMethod
    <Extension()>
    Function ToXDocument(aStream As Stream) As XDocument
        Dim sr As StreamReader = New StreamReader(aStream)
        Dim xhtmlDoc As New XHtmlDocument
        xhtmlDoc.LoadXml(sr.ReadToEnd)
        Return XDocument.Parse(xhtmlDoc.OuterXml)
    End Function
End Module
