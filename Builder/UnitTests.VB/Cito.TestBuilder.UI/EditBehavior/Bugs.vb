
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass()>
Public Class Bugs

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        FakeDal.CanSaveResources()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior"), WorkItem(9787), Description("HtmlEditor - spaties tussen woorden die in een span staan verdwijnen")>
    Public Sub SpaceBetweenSpan_Span_Disapeared_ShouldNot()
        Dim _param As New XHtmlParameter()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), Nothing, _param)
        Dim repro As String = "<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1"">" +
                            "<body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-8"">" +
                            "<p id=""c1-id-9""><span id=""c1-id-10"" style=""text-decoration:underline"">woord</span> <span id=""c1-id-11"" style=""text-decoration:underline"">woord</span></p>" +
                            "</body></html>"

        _behavior.SetHtml(repro)

        Assert.IsFalse(_param.Value.Contains("</span><span"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior"), WorkItem(9787), Description("HtmlEditor - spaties tussen woorden die in een span staan verdwijnen")>
    Public Sub SpaceInParameterShouldBePerservedToEditor()
        Dim repro As String = "<p id=""c1-id-9""><span id=""c1-id-10"" style=""text-decoration:underline"">woord</span> <span id=""c1-id-11"" style=""text-decoration:underline"">woord</span></p>"
        Dim _param As New XHtmlParameter() With {.Value = repro}
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), Nothing, _param)

        Dim result = _behavior.GetHtml()

        Assert.IsFalse(repro.Contains("</span><span"))
        Assert.IsFalse(result.Contains("</span><span"))
    End Sub

End Class
