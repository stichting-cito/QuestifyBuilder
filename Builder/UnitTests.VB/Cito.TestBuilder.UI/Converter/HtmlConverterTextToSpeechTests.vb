
Imports System.Diagnostics
Imports System.IO
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class HtmlConverterTextToSpeechTests

    <TestMethod(), TestCategory("UILogic")>
    Public Sub EditorToParamTest()
        'Arrange
        Dim inputHtml As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                        <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                            <p><span class="TTSAlternative"><span class="TTSAlias">Bah</span>Misschien</span></p>
                                        </body>
                                    </html>
        Dim expectedOutput As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                        <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                            <p><span class="TTSAlternative"><span class="TTSAlias" data-alias="Bah"></span>Misschien</span></p>
                                        </body>
                                    </html>)

        Dim converter = New HtmlConverter_TextToSpeechToHtml()

        'Act
        Dim result = XDocument.Parse(converter.ConvertHtml(inputHtml.ToString()))

        'Assert
        Dim areEqual = UnitTestHelper.AreSame(result, expectedOutput)
        If Not areEqual Then CompareFiles(result, expectedOutput)
        Assert.IsTrue(areEqual)
    End Sub

    <TestMethod, TestCategory("UILogic")>
     Public Sub ParamToEditorTest()
        'Arrange
        Dim expectedOutput As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                        <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                            <p><span class="TTSAlternative"><span class="TTSAlias">Bah</span>Misschien</span></p>
                                        </body>
                                    </html>)
        Dim inputHtml As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                        <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                            <p><span class="TTSAlternative"><span class="TTSAlias" data-alias="Bah"></span>Misschien</span></p>
                                        </body>
                                    </html>

        Dim converter = New HtmlConverter_HtmlToTextToSpeech()

        'Act
        Dim result = XDocument.Parse(converter.ConvertHtml(inputHtml.ToString()))

        'Assert
        Dim areEqual = UnitTestHelper.AreSame(result, expectedOutput)
        If Not areEqual Then CompareFiles(result, expectedOutput)
        Assert.IsTrue(areEqual)
    End Sub

    Private Shared Sub CompareFiles(output As XDocument, expectedOutput As XDocument)
#If DEBUG Then
        Dim file1 As String = Path.Combine(Path.GetTempPath, "output.xml")
        Dim file2 As String = Path.Combine(Path.GetTempPath, "expected.xml")
        output.Save(file1)
        expectedOutput.Save(file2)
        Dim p = New Process()
        p.StartInfo.FileName = "devenv"
        p.StartInfo.Arguments = String.Format(" /diff ""{0}"" ""{1}""", file1, file2)
        p.Start()
#End If
    End Sub
End Class
