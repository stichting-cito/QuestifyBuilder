
Imports System.Xml.Linq
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Wordprocessing
Imports FluentAssertions
Imports Questify.Builder.Plugins.PaperBased

<TestClass>
Public Class WidthExtractorTests

    <TestMethod>
    Public Sub ExtractTableCellIsNothing()
        WidthExtractor.Extract(Nothing).Should.BeNull()
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellIsEmptyObject()
        WidthExtractor.Extract(New XElement(XName.Get("td", "http://www.w3.org/1999/xhtml"))).Should.BeNull()
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellWidthIsPercentage()
        Dim input As XElement = <td width="33%"/>
        Dim expectation As New TableCellWidth With {.Type = TableWidthUnitValues.Pct, .Width = New StringValue("1650")}
        WidthExtractor.Extract(input).Should.BeEquivalentTo(expectation)
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellWidthIsPoint()
        Dim input As XElement = <td width="33pt"/>
        Dim expectation As New TableCellWidth With {.Type = TableWidthUnitValues.Dxa, .Width = New StringValue("660")}
        WidthExtractor.Extract(input).Should.BeEquivalentTo(expectation)
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellWidthIsPixels()
        Dim input As XElement = <td width="300px"/>
        Dim expectation As New TableCellWidth With {.Type = TableWidthUnitValues.Dxa, .Width = New StringValue("300")}
        WidthExtractor.Extract(input).Should.BeEquivalentTo(expectation)
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellWidthNoTypeGiven()
        Dim input As XElement = <td width="33"/>
        Dim expectation As New TableCellWidth With {.Type = TableWidthUnitValues.Dxa, .Width = New StringValue("495")}
        WidthExtractor.Extract(input).Should.BeEquivalentTo(expectation)
    End Sub

    <TestMethod>
    Public Sub ExtractTableCellInvalidWidth()
        Dim input As XElement = <td width="abc"/>
        WidthExtractor.Extract(input).Should.BeNull()
    End Sub
End Class