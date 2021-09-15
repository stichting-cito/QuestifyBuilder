
Imports System.IO
Imports System.Linq
Imports DocumentFormat.OpenXml.Packaging
Imports FluentAssertions
Imports NotesFor.HtmlToOpenXml

<TestClass>
Public Class HtmlToOpenXmlTests
    <TestMethod(), TestCategory("HtmlToOpenXml")>
    Public Sub CanConvertParagraphs()
        'Arrange
        Dim mem = new MemoryStream()
        mem.Write(My.Resources.Test_Doc, 0, My.Resources.Test_Doc.Length)
        Dim wordDoc As WordprocessingDocument = WordprocessingDocument.Open(mem, true)
        Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
        
        Dim htmlConverter As HtmlConverter = new HtmlConverter(mainPart)
        Dim input As String = "<p>Hey</p><p>Ho</p>"
        Dim expected1 As String = "<w:r xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""><w:t xml:space=""preserve"">Hey</w:t></w:r>"
        Dim expected2 As String = "<w:r xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""><w:t xml:space=""preserve"">Ho</w:t></w:r>"
        
        'Act
        Dim pars = htmlConverter.Parse(input)
        Dim result1 = pars.First().InnerXml
        Dim result2 = pars.Last().InnerXml
        Console.WriteLine("Document Body Innards:")
        Console.Write(wordDoc.MainDocumentPart.Document.Body.InnerXml) 'Note that .ParseHtml appends the parsed bits to document, but .Parse does not...
        
        'Assert
        result1.Should().Be(expected1)
        result2.Should().Be(expected2)
        
    End Sub
End Class
