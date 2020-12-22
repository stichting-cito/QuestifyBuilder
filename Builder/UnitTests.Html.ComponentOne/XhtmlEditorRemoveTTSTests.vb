
Imports System.Xml
Imports System.Xml.Linq
Imports FluentAssertions
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Questify.Builder.Plugins.Html.ComponentOne

<TestClass()>
Public Class XhtmlEditorRemoveTTSTests

    <TestMethod()>
    Public Sub RemoveAlternativeTTSClassShownTextSelectedTest()
        Dim inputHtml As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                           <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                               <p>Hier wordt iets <span class="TTSAlternative"><span class="TTSAlias">Alternatief </span>moois </span>verklankt</p>
                                           </body>
                                       </html>)

        Dim expected = XDocument.Parse("<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1""><head id=""c1-id-2""><title id=""c1-id-3"">Document Title</title><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" id=""c1-id-4"" /></head><body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-5""><p id=""c1-id-6"">Hier wordt iets moois verklankt</p></body></html>")

        Dim editor = New XHtmlEditor()
        Dim doc = New XmlDocument()
        doc.LoadXml(inputHtml.ToString())
        editor.Document = doc
        editor.Select(30, 0)

        RemoveTTSAndCompare(editor, expected)
        editor.Dispose()
    End Sub

    <TestMethod()>
    Public Sub RemoveAlternativeTTSClassAlternativeTextSelectedTest()
        Dim inputHtml As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                           <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                               <p>Hier wordt iets <span class="TTSAlternative"><span class="TTSAlias">Alternatief </span>moois </span>verklankt</p>
                                           </body>
                                       </html>)

        Dim expected = XDocument.Parse("<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1""><head id=""c1-id-2""><title id=""c1-id-3"">Document Title</title><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" id=""c1-id-4"" /></head><body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-5""><p id=""c1-id-6"">Hier wordt iets moois verklankt</p></body></html>")

        Dim editor = New XHtmlEditor()
        Dim doc = New XmlDocument()
        doc.LoadXml(inputHtml.ToString())
        editor.Document = doc
        editor.Select(25, 0)

        RemoveTTSAndCompare(editor, expected)
        editor.Dispose()
    End Sub


    <TestMethod()>
    Public Sub RemovePauseTTSClassTest()
        Dim inputHtml As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                           <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                               <p>En hier staat een <span class="TTSPause PauseDuration_200">Kort</span> pauze in</p>
                                           </body>
                                       </html>)

        Dim expected = XDocument.Parse("<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1""><head id=""c1-id-2""><title id=""c1-id-3"">Document Title</title><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" id=""c1-id-4"" /></head><body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-5""><p id=""c1-id-6"">En hier staat een pauze in</p></body></html>")

        Dim editor = New XHtmlEditor()
        Dim doc = New XmlDocument()
        doc.LoadXml(inputHtml.ToString())
        editor.Document = doc
        editor.Select(19, 0)

        RemoveTTSAndCompare(editor, expected)
        editor.Dispose()
    End Sub

    <TestMethod()>
    Public Sub RemoveMuteTTSClassTest()
        Dim inputHtml As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                           <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                               <p>Dit hoeft <span class="TTSMute">niet</span> uitgesproken</p>
                                           </body>
                                       </html>)

        Dim expected = XDocument.Parse("<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1""><head id=""c1-id-2""><title id=""c1-id-3"">Document Title</title><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" id=""c1-id-4"" /></head><body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-5""><p id=""c1-id-6"">Dit hoeft niet uitgesproken</p></body></html>")

        Dim editor = New XHtmlEditor()
        Dim doc = New XmlDocument()
        doc.LoadXml(inputHtml.ToString())
        editor.Document = doc
        editor.Select(10, 0)

        RemoveTTSAndCompare(editor, expected)
        editor.Dispose()
    End Sub

    <TestMethod()>
    Public Sub RemoveTTSLanguageTest()
        Dim inputHtml As New XDocument(<html xmlns="http://www.w3.org/1999/xhtml">
                                           <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;">
                                               <p>Dit is <span class="LangTTSFrans">en francais</span> gesproken</p>
                                           </body>
                                       </html>)

        Dim expected = XDocument.Parse("<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1""><head id=""c1-id-2""><title id=""c1-id-3"">Document Title</title><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" id=""c1-id-4"" /></head><body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-5""><p id=""c1-id-6"">Dit is en francais gesproken</p></body></html>")

        Dim editor = New XHtmlEditor()
        Dim doc = New XmlDocument()
        doc.LoadXml(inputHtml.ToString())
        editor.Document = doc
        editor.Select(10, 0)

        RemoveTTSAndCompare(editor, expected)
        editor.Dispose()
    End Sub

    Private Sub RemoveTTSAndCompare(editor As XHtmlEditor, expected As XDocument)
        editor.RemoveTextToSpeech()
        Dim result = XDocument.Parse(editor.Document().DocumentElement.OuterXml)

        result.Should().BeEquivalentTo(expected)
    End Sub
End Class