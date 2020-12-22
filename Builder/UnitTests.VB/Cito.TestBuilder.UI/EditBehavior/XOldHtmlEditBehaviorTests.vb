
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.My.Resources

<TestClass>
Public Class XOldHtmlEditBehaviorTests

    <TestInitialize>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub BusinessRulesXhtmlParamBehavior_Test()
        Dim itemResource As New ItemResourceEntity()

        Dim behavior As New XOldHtmlParameterBehavior(itemResource, GetFakeResourceManager(), 8417, New XHtmlParameter() With {.Value = _inline.ToString()})

        Assert.IsTrue(behavior.CanInsertImages)
        Assert.IsTrue(behavior.CanInsertFormula)
        Assert.IsTrue(behavior.CanInsertReferences)

        Assert.IsFalse(behavior.CanInsertMovies)
        Assert.IsFalse(behavior.CanInsertAudio)
        Assert.IsFalse(behavior.CanInsertControls)
        Assert.IsFalse(behavior.CanCreateReferences)
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline_HtmlConverterTest()
        AddInline()
        AddTransparentPix()
        Dim itemResource As New ItemResourceEntity()
        Dim behavior As New XOldHtmlParameterBehavior(itemResource, GetFakeResourceManager(), 9991, New XHtmlParameter() With {.Value = _inline.ToString()})

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        Assert.IsTrue(html.Contains("alt=""TransparentPix.png"" src=""resource://package:9991/TransparentPix.png"))
        Assert.IsTrue(html.Contains("isinlineelement"))
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineCount_HtmlConverterTest()
        AddInline()
        AddTransparentPix()
        Dim itemResource As New ItemResourceEntity()
        Dim behavior As New XOldHtmlParameterBehavior(itemResource, GetFakeResourceManager(), 8417, New XHtmlParameter() With {.Value = _inline.ToString()})

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        Assert.AreEqual(1, behavior.InlineElements.Count())
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest()
        Dim xhtmlparam As New XHtmlParameter() With {.Value = _inline.ToString()}
        AddInline()
        AddTransparentPix()
        Dim itemResource As New ItemResourceEntity()
        Dim behavior As New XOldHtmlParameterBehavior(itemResource, GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        behavior.SetHtml(html)

        Assert.IsTrue(xhtmlparam.Value.Contains("<img"))
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Param2Inline2Parameter2Inline_HtmlConverterTest()
        Dim xhtmlparam As New XHtmlParameter() With {.Value = _inline.ToString()}
        AddInline()
        AddTransparentPix()
        Dim itemResource As New ItemResourceEntity()
        Dim behavior As New XOldHtmlParameterBehavior(itemResource, GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        behavior.SetHtml(html)
        Dim html2 = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        html = UnitTestHelper.SetFixedIdsToCompare(html)
        html2 = UnitTestHelper.SetFixedIdsToCompare(html2)

        Assert.AreEqual(html, html2, "when converted both results should be the same")
        Assert.AreEqual(1, behavior.InlineElements.Count())
    End Sub


    Private ReadOnly _inline As XElement = <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">
                                               <img src="resource://package/TransparentPix.png" id="3cc1b750-bee3-4622-8700-b080b8da5ad7" alt=""/>
                                txt</p>

End Class