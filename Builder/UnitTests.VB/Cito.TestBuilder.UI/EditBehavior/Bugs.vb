
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass()>
Public Class Bugs

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
        FakeDal.AddInline() 'Make Inine possible
        FakeDal.AddTransparentPix() 'Just a test image
        FakeDal.CanSaveResources()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior"), WorkItem(9787), Description("HtmlEditor - spaties tussen woorden die in een span staan verdwijnen")>
    Public Sub SpaceBetweenSpan_Span_Disapeared_ShouldNot()
        'Arrange
        Dim _param As New XHtmlParameter()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), Nothing, _param)
        'Normally the repro would be a Xelement.ToString, but the space between the spans is not persisted by vb.net editor.
        Dim repro As String = "<html xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-1"">" +
                                    "<body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"" id=""c1-id-8"">" +
                                    "<p id=""c1-id-9""><span id=""c1-id-10"" style=""text-decoration:underline"">woord</span> <span id=""c1-id-11"" style=""text-decoration:underline"">woord</span></p>" +
                                    "</body></html>"
        
        'Act
        _behavior.SetHtml(repro) 'Value will be set to _param
       
        'Assert
        Assert.IsFalse(_param.Value.Contains("</span><span")) 'Marker that is has gone wrong.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior"), WorkItem(9787), Description("HtmlEditor - spaties tussen woorden die in een span staan verdwijnen")>
    Public Sub SpaceInParameterShouldBePerservedToEditor()
        'Arrange
        '                                                                                                Space is here\/
        Dim repro As String = "<p id=""c1-id-9""><span id=""c1-id-10"" style=""text-decoration:underline"">woord</span> <span id=""c1-id-11"" style=""text-decoration:underline"">woord</span></p>"
        Dim _param As New XHtmlParameter() With {.Value = repro}
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), Nothing, _param)
        
        'Act
        Dim result = _behavior.GetHtml()
        
        'Assert
        Assert.IsFalse(repro.Contains("</span><span")) 'Let's verify our input as well.
        Assert.IsFalse(result.Contains("</span><span")) 'Marker that is has gone wrong.
    End Sub

End Class
