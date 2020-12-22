
Imports FakeItEasy
Imports System.Xml.Linq
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class HtmlConverter_DealWithMovedInlineControlTests

    <TestMethod(), TestCategory("UILogic")>
    Public Sub NothingToDo_NoAlterations()
        Dim converter = New HtmlConverter_DealWithMovedInlineControl(A.Fake(Of IHtmlEditorBehaviour))

        Dim result = converter.ConvertHtml(singleInline.ToString())

        Dim doc1 = XDocument.Load(singleInline.CreateReader())
        Dim doc2 = XDocument.Parse(result)
        Assert.IsTrue(UnitTestHelper.AreSame(doc1, doc2))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub NothingToDo_InlineIdsMatchesHtmlInlineIds()
        Dim converter = New HtmlConverter_DealWithMovedInlineControl(GetFakeWithInlineElements("Ice32c0ba-73db-456d-b3d3-c92265282cf7"))

        Dim result = converter.ConvertHtml(singleInline.ToString())

        Dim doc1 = XDocument.Load(singleInline.CreateReader())
        Dim doc2 = XDocument.Parse(result)
        Assert.IsTrue(UnitTestHelper.AreSame(doc1, doc2))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub NothingToDo_2htmlInlineIds_1matchInInlineId()
        Dim converter = New HtmlConverter_DealWithMovedInlineControl(GetFakeWithInlineElements("Ice32c0ba-73db-456d-b3d3-c92265282cf7"))

        Dim result = converter.ConvertHtml(doubleInline.ToString())

        Dim doc1 = XDocument.Load(doubleInline.CreateReader())
        Dim doc2 = XDocument.Parse(result)
        Assert.IsTrue(UnitTestHelper.AreSame(doc1, doc2))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub NeedsToDoSomething_2htmlInlineIds_1matchInInlineId_andHasOtherIdThatIsNotPresent_HtmlIdIsRenamed()
        Dim converter = New HtmlConverter_DealWithMovedInlineControl(GetFakeWithInlineElements("Ice32c0ba-73db-456d-b3d3-c92265282cf7", "IsGone"))

        Dim result = converter.ConvertHtml(doubleInline.ToString())

        Dim doc1 = XDocument.Load(doubleInline.CreateReader())
        Dim doc2 = XDocument.Parse(result)
        Assert.IsTrue(UnitTestHelper.AreSame(doc1, doc2))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub RenameOfInlineIdAffectsIdsOfBehavior()
        Dim fakeHtmlEditorBehavior = GetFakeWithInlineElements("Ice32c0ba-73db-456d-b3d3-c92265282cf7", "IsGone")
        Dim converter = New HtmlConverter_DealWithMovedInlineControl(fakeHtmlEditorBehavior)

        Dim result = converter.ConvertHtml(doubleInline.ToString())

        Assert.IsTrue(fakeHtmlEditorBehavior.InlineElements.ContainsKey("Ice32c0ba-73db-456d-b3d3-c92265282cf7"))
        Assert.IsFalse(fakeHtmlEditorBehavior.InlineElements.ContainsKey("IsGone"))
        Assert.IsTrue(fakeHtmlEditorBehavior.InlineElements.ContainsKey("c1-id-09"))
        Assert.AreEqual("IsGone", fakeHtmlEditorBehavior.InlineElements("c1-id-09").Item1.Identifier, "The actual id should NOT have changed")
    End Sub


    ReadOnly singleInline As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                            <body style="padding: 2px; margin : 0px; box-sizing: border-box; height: auto; width: 100%;" id="c1-id-7" xmlns="http://www.w3.org/1999/xhtml">
                                                <p id="c1-id-8">
                                                    <img id="Ice32c0ba-73db-456d-b3d3-c92265282cf7" style="VERTICAL-ALIGN: middle" alt="" src="" isinlinecontrol="true" isinlineelement="true"/>
                                                </p>
                                            </body>
                                        </html>

    ReadOnly doubleInline As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                            <body style="padding: 2px; margin : 0px; box-sizing: border-box; height: auto; width: 100%;" id="c1-id-7" xmlns="http://www.w3.org/1999/xhtml">
                                                <p id="c1-id-8">
                                                    <img id="Ice32c0ba-73db-456d-b3d3-c92265282cf7" style="VERTICAL-ALIGN: middle" alt="" src="" isinlinecontrol="true" isinlineelement="true"/>
                                                    <img id="c1-id-09" style="VERTICAL-ALIGN: middle" alt="" src="" isinlinecontrol="true" isinlineelement="true"/>
                                                </p>
                                            </body>
                                        </html>


    Private Function GetFakeWithInlineElements(ParamArray ids As String()) As IHtmlEditorBehaviour
        Dim ret = A.Fake(Of IHtmlEditorBehaviour)()
        Dim newDict = ids.ToDictionary(Function(id) id, Function(id) New Tuple(Of InlineElement, Boolean)(New InlineElement() With {.Identifier = id}, False))
        A.CallTo(Function() ret.InlineElements).ReturnsLazily(Function(args)
                                                                  Return newDict
                                                              End Function)
        Return ret
    End Function

End Class
