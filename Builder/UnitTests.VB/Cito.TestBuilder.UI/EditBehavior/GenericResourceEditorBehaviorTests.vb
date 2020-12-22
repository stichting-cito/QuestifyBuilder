
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.My.Resources

<TestClass>
Public Class GenericResourceEditorBehaviorTests

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
        Dim resource As GenericResourceEntity = ConstructResource()

        Dim behavior As New GenericResourceEditorBehaviour(resource, FakeDal.GetFakeResourceManager(), 8417, False)

        Assert.IsTrue(behavior.CanInsertImages)
        Assert.IsTrue(behavior.CanInsertFormula)
        Assert.IsTrue(behavior.CanCreateReferences)

        Assert.IsFalse(behavior.CanInsertReferences)
        Assert.IsFalse(behavior.CanInsertMovies)
        Assert.IsFalse(behavior.CanInsertAudio)
        Assert.IsFalse(behavior.CanInsertControls)

    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline_HtmlConverterTest()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim resource As GenericResourceEntity = ConstructResource()
        Dim behavior As New GenericResourceEditorBehaviour(resource, FakeDal.GetFakeResourceManager(), 9991, False)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        Assert.IsTrue(html.Contains("alt=""TransparentPix.png"" src=""resource://package:9991/TransparentPix.png"""))
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineCount_HtmlConverterTest()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim resource As GenericResourceEntity = ConstructResource()
        Dim behavior As New GenericResourceEditorBehaviour(resource, FakeDal.GetFakeResourceManager(), 8417, False)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        Assert.AreEqual(1, behavior.InlineElements.Count())
        Assert.AreEqual(1, behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())
        Assert.AreEqual(0, behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count())
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim resource As GenericResourceEntity = ConstructResource()
        Dim behavior As New GenericResourceEditorBehaviour(resource, FakeDal.GetFakeResourceManager(), 8417, False)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix))
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        behavior.SetHtml(html)

        Dim tmp As String = New System.Text.UTF8Encoding().GetString(resource.ResourceData.BinData)
        Assert.IsTrue(tmp.Contains("<img"))
        Assert.IsFalse(tmp.Contains("<cito:InlineElement"))
        Assert.IsFalse(tmp.Contains("isinlineelement"))
        Assert.AreEqual(1, behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())
        Assert.AreEqual(0, behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count())
    End Sub

    <TestMethod, TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub RetrieveStylesheet_FromResource()
        Dim resource As GenericResourceEntity
        With FakeDal.Add.Text("SomeFakeStory.xml", _data, Sub(g) resource = g)
            .AddResource.StyleSheet("Style1.css", _style)
            .AddResource.StyleSheet("EditStyle1.css", _headerstyle)
        End With
        Dim behavior As New GenericResourceEditorBehaviour(resource, FakeDal.GetFakeResourceManager(), 8417, False)

        Dim stylesheetDict = behavior.GetStyleFromResource()
        Dim hdrStyle = behavior.HeaderStyleElementContent

        Assert.IsTrue(stylesheetDict.ContainsKey("Style1.css"))
        Assert.AreEqual(_style, stylesheetDict("Style1.css"))
        Assert.AreEqual(_headerstyle, hdrStyle)
    End Sub

    Function ConstructResource() As GenericResourceEntity
        Dim ret As New GenericResourceEntity
        ret = MakeTextResource("txt", _data)
        Return ret
    End Function

    Public Function MakeTextResource(name As String, data As XElement) As GenericResourceEntity
        Dim id As Guid = Guid.NewGuid()
        Dim ret As New GenericResourceEntity(id)
        ret.Name = name
        ret.MediaType = "text/html"
        ret.Description = name
        ret.ResourceData = New ResourceDataEntity(Guid.NewGuid) With {
            .BinData = New System.Text.UTF8Encoding().GetBytes(data.ToString())}
        Return ret
    End Function

    Private ReadOnly _data As XElement = <p xmlns="http://www.w3.org/1999/xhtml">
                                             <img src="resource://package/TransparentPix.png" id="3cc1b750-bee3-4622-8700-b080b8da5ad7" alt=""/>
                                             <b>Met de deur in huis vallen</b><br xmlns="http://www.w3.org/1999/xhtml"/>
                                             <br xmlns="http://www.w3.org/1999/xhtml"/>‘Met de deur in huis vallen’ is bij ons een bekende uitdrukking.<br xmlns="http://www.w3.org/1999/xhtml"/>Wij gebruiken deze uitdrukking als we bij iemand binnenkomen en<br xmlns="http://www.w3.org/1999/xhtml"/>meteen duidelijk maken wat we willen.<br xmlns="http://www.w3.org/1999/xhtml"/>Eskimo’s zullen deze uitdrukking niet gauw gebruiken.<br xmlns="http://www.w3.org/1999/xhtml"/>Ook niet in hun eigen taal.
                                    <br xmlns="http://www.w3.org/1999/xhtml"/>Ze zouden niet op het idee komen.<br xmlns="http://www.w3.org/1999/xhtml"/>
				                    Ze <span id="refff882ff7-8841-4ce1-ba7e-1ca24c1f56e9" contenteditable="false" xmlns="http://www.w3.org/1999/xhtml" xmlns:cito="http://www.cito.nl/citotester" atomicselection="true" cito:value="1" cito:type="reference" cito:reftype="Element" cito:description="Element 1"><u>            1            </u></span>.
			                        <br xmlns="http://www.w3.org/1999/xhtml"/>Eskimo’s graven een lange, lage tunnel in de grond.<br xmlns="http://www.w3.org/1999/xhtml"/>Die staat in verbinding met een luik in de vloer van hun huis.<br xmlns="http://www.w3.org/1999/xhtml"/>Buiten gaan ze de tunnel in.<br xmlns="http://www.w3.org/1999/xhtml"/>Als ze er aan de andere kant uitkomen, staan ze midden in <br xmlns="http://www.w3.org/1999/xhtml"/>het woongedeelte van hun huis.<br xmlns="http://www.w3.org/1999/xhtml"/>Je kunt de tunnel van het eskimohuis vergelijken met de<br xmlns="http://www.w3.org/1999/xhtml"/>gang of hal in onze huizen.<br xmlns="http://www.w3.org/1999/xhtml"/>
                                         </p>

    Private _style As String = "html,body { padding: 0; margin: 0; height: 100%; width: 100%; font-family: Verdana; font-size: 13px; cursor: default; }"
    Private _headerstyle As String = "a { color : #ffffff; }"

End Class



