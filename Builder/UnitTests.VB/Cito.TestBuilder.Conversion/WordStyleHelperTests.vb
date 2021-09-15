
Imports System.Linq
Imports System.Xml.Linq
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports FluentAssertions
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Plugins.PaperBased
Imports Questify.Builder.UnitTests.Framework

Namespace Cito.TestBuilder.Conversion

    <TestClass>
    Public Class WordStyleHelperTests
        <TestMethod()>
        Public Sub StyleHelperShouldExtractBorders()
            Dim html As XElement = <w:customXml xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" w:uri="http://www.cito.nl/citotester" w:element="xhtmlElementToOpenXml" xml:space="preserve">
                                       <table style="BORDER-COLLAPSE: collapse; " width="100%" id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                                           <colgroup id="c1-id-13">
                                               <col width="33" id="c1-id-14"/>
                                               <col width="33" id="c1-id-15"/>
                                               <col width="33" id="c1-id-16"/>
                                           </colgroup>
                                           <tbody id="c1-id-17">
                                               <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;" id="c1-id-18">
                                                   <td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;" width="33" id="c1-id-19">
                                                       <p id="c1-id-20"></p>
                                                   </td>
                                                   <td style="border-bottom:#C00000 4px Double;BORDER-LEFT: black 1px solid;BORDER-TOP: black 1px solid;BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px;PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;" width="33" id="c1-id-21">
                                                       <p id="c1-id-22"></p>
                                                   </td>
                                                   <td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;" width="33" id="c1-id-23">
                                                       <p id="c1-id-24"></p>
                                                   </td>
                                               </tr>
                                           </tbody>
                                       </table>
                                   </w:customXml>

            Dim expected As New TableCellBorders(
                New BottomBorder() With {.Val = BorderValues.Double, .Color = "C00000", .Size = New UInt32Value() With {.Value = 16}},
                New LeftBorder() With {.Val = BorderValues.Single, .Color = "000000", .Size = New UInt32Value() With {.Value = 4}},
                New TopBorder() With {.Val = BorderValues.Single, .Color = "000000", .Size = New UInt32Value() With {.Value = 4}},
                New RightBorder() With {.Val = BorderValues.Single, .Color = "000000", .Size = New UInt32Value() With {.Value = 4}}
                )

            Dim styleHelper = New WordStyleHelper()
            styleHelper.ExtractStyles(html.OuterXml())
            Dim list = styleHelper.StylesList

            Assert.IsTrue(list.ContainsKey("TCB_c1-id-19") AndAlso list.ContainsKey("TCB_c1-id-21") AndAlso list.ContainsKey("TCB_c1-id-23"))
            Assert.IsTrue(UnitTestHelper.AreSame(XDocument.Parse(expected.OuterXml), XDocument.Parse(list("TCB_c1-id-21").TableCellBorders.OuterXml)))
        End Sub

        <TestMethod>
        Public Sub HtmlStyleMissingHandlerStyleEventArgsNothing()
            Dim wsHelper As New WordStyleHelper()

            ' Should NOT throw an exception
            wsHelper.HtmlStyleMissing(Nothing, Nothing)
        End Sub

        <TestMethod>
        Public Sub HtmlStyleMissingHandlerStyleDefinitionsPartNothing()
            Dim wsHelper As New WordStyleHelper()
            Using monitor = wsHelper.Monitor()
                ' Should NOT throw an exception
                wsHelper.ApplyStyleToTableCellIfExists("Foo", Nothing)
                monitor.Should.NotRaise("StylesChanged")
            End Using
        End Sub

        <TestMethod>
        Public Sub HtmlStyleMissingHandlerStyleDefinitionsPart()
            Dim wsHelper As New WordStyleHelper()
            wsHelper.StylesList.Add("Foo", New TableCellProperties())

            Using monitor = wsHelper.Monitor()
                Using ms As New System.IO.MemoryStream()
                    Using wordDoc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, True)
                        Dim mainPart = wordDoc.AddMainDocumentPart()
                        Dim stylePart = mainPart.AddNewPart(Of StyleDefinitionsPart)

                        wsHelper.ApplyStyleToTableCellIfExists("Foo", stylePart)
                        stylePart.Styles.Descendants(Of StyleName).Any(Function(sn) sn.Val.Value.Equals("Foo")).Should().BeTrue()
                    End Using
                End Using

                monitor.Should.Raise("StylesChanged")
            End Using
        End Sub

    End Class
End Namespace