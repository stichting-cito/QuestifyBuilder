
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

<TestClass()>
Public Class xHtmlEditBehaviorTests
    Inherits baseItemTest

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub BusinessRulesXhtmlParamBehavior_Test()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        FakeDal.Add.ItemTemplate("choice.inline.dc", Sub(i) ItemTestData.SetResourceIdAndXmlAsBinData(i, ItemTestData.ilt_choice_inline_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.Base.Inline", Sub(c) ItemTestData.SetResourceIdAndXmlAsBinData(c, ItemTestData.ct_base_inline))

        Dim resManager = FakeDal.GetFakeResourceManager()
        Dim itemResource = MyBase.CreateItem("itm_inlinechoice", ItemTestData.item_choice_inline_dc, "choice.inline.dc")
        Dim prm = New XHtmlParameter() With {.Value = inline.ToString()}
        Dim behavior = New XHtmlParameterBehavior(itemResource, resManager, 8417, prm)

        Assert.IsTrue(behavior.CanInsertImages)
        Assert.IsTrue(behavior.CanInsertMovies)
        Assert.IsTrue(behavior.CanInsertAudio)
        Assert.IsTrue(behavior.CanInsertFormula)
        Assert.IsTrue(behavior.CanInsertReferences)

        Assert.IsFalse(behavior.CanInsertControls)
        Assert.IsFalse(behavior.CanCreateReferences)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline_HtmlConverterTest()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, New XHtmlParameter() With {.Value = inline.ToString()})

        Dim html = _behavior.GetHtml()

        Assert.IsTrue(html.Contains("<img isinlineelement=""true"" id=""e476357a-0e97-499d-aa51-510e91717cba"" width=""400"" height=""400"" alt=""TransparentPix.png"" src=""resource://package:8417/TransparentPix.png"" />"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineCount_HtmlConverterTest()
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, New XHtmlParameter() With {.Value = inline.ToString()})

        Dim html = _behavior.GetHtml()

        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest()
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)
        Dim html = _behavior.GetHtml()

        _behavior.SetHtml(html)

        Dim x = XDocument.Parse(xhtmlparam.Value)
        Dim ie = x.Descendants().FirstOrDefault(Function(d) d.Name.LocalName.Equals("InlineElement", StringComparison.InvariantCultureIgnoreCase))
        Assert.IsNotNull(ie)
        Assert.IsTrue(ie.Attributes().Any(Function(a) a.Name.ToString().Equals("id") AndAlso a.Value.Equals("e476357a-0e97-499d-aa51-510e91717cba", StringComparison.InvariantCultureIgnoreCase)))
        Assert.IsTrue(ie.Attributes().Any(Function(a) a.Name.ToString().Equals("layoutTemplateSourceName") AndAlso a.Value.Equals("InlineImageLayoutTemplate", StringComparison.InvariantCultureIgnoreCase)))
        Assert.IsFalse(xhtmlparam.Value.Contains("<img"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub CompareRepetitiveConvertion_HtmlConverterTest()
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline.ToString()}
        FakeDal.AddInline()
        FakeDal.AddTransparentPix()
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim html = _behavior.GetHtml()
        _behavior.SetHtml(html)
        Dim html2 = _behavior.GetHtml()

        Dim xHtml1 As XDocument = XDocument.Parse(html)
        Dim xHtml2 As XDocument = XDocument.Parse(html2)
        Dim same As Boolean = XDocument.DeepEquals(xHtml1, xHtml2)
        Assert.IsTrue(same)
        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub CannotSetTextToSpeechOptions()
        FakeDal.CanSaveResources()
        Dim xhtmlParam As New XHtmlParameter() With {.Value = String.Empty}
        Dim itemResource As New ItemResourceEntity With {.ResourceId = Guid.NewGuid()}
        Dim behavior As New XHtmlParameterBehavior(itemResource, FakeDal.GetFakeResourceManager(), 123, xhtmlParam)

        behavior.SetHtml(TestItemData.ToString())

        Assert.AreEqual(False, behavior.CanSetTextToSpeechOptions)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")> <WorkItem(9593)>
    <Description("Publiceren naar Facet met formule geeft Foutmeldingen: resource ....")>
    Public Sub AddResourceToItem_ItemHasDependencies()
        Dim itm As ItemResourceEntity = Nothing
        FakeDal.Add.Image("smile.png", Sub(img) img.ResourceId = Guid.NewGuid())
        FakeDal.Add.Item("someItem", Sub(i)
                                         i.ResourceId = Guid.NewGuid()
                                         itm = i
                                     End Sub)

        Dim _behavior As New XHtmlParameterBehavior(itm, FakeDal.GetFakeResourceManager(), 23, New XHtmlParameter)

        Dim result = _behavior.addDependency("smile.png", False)
        Dim result2 = _behavior.addDependency("smile.png", False)

        Assert.IsTrue(result)
        Assert.IsFalse(result2)
        Assert.IsTrue(itm.DependentResourceCollection.Count = 1)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub StylesheetsOnItemResourceEntity_AreNotSeen()
        FakeDal.CanSaveResources()
        Dim _behavior As XHtmlParameterBehavior = ConstructBehaviour_WithStylesheetOnItemResource()
        Dim dict As Dictionary(Of String, String)
        Dim ResultHeaderStyle As String

        dict = _behavior.GetStyle()
        ResultHeaderStyle = _behavior.HeaderStyleElementContent

        Assert.IsFalse(dict.ContainsKey("ItemStyle.css"))
        Assert.AreNotEqual(headerstyle, ResultHeaderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub StyleSheetsOnItemLayout_AreUsed()
        FakeDal.CanSaveResources()
        Dim _behavior As XHtmlParameterBehavior = ConstructBehaviour_WithSylesheetOnItemLayoutTemplate()
        Dim dict As Dictionary(Of String, String)
        Dim ResultHeaderStyle As String

        dict = _behavior.GetStyle()
        ResultHeaderStyle = _behavior.HeaderStyleElementContent

        Assert.IsTrue(dict.ContainsKey("ItemStyle.css"))
        Assert.IsTrue(dict.ContainsKey("EditItemStyle.css"))
        Assert.AreEqual(headerstyle, ResultHeaderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub StyleSheetsOnItemLayoutThatIsSystemType_AreUsed()
        Dim _itemResource As New ItemResourceEntity
        With FakeDal.Add.ItemTemplate("template1", Sub(i) i.ItemType = [Enum].GetName(GetType(ItemTypeEnum), ItemTypeEnum.System))
            .AddResource.StyleSheet("ItemStyle.css", style)
            .AddResource.StyleSheet("EditItemStyle.css", headerstyle)
            .IsUsedBy.Item("item", Sub(i) _itemResource = i)
        End With
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(),
                                                1235, New XHtmlParameter() With {.Value = String.Empty})
        Dim dict As Dictionary(Of String, String)
        Dim ResultHeaderStyle As String

        dict = _behavior.GetStyle()
        ResultHeaderStyle = _behavior.HeaderStyleElementContent

        Assert.IsTrue(dict.ContainsKey("ItemStyle.css"))
        Assert.IsTrue(dict.ContainsKey("EditItemStyle.css"))
        Assert.AreEqual(headerstyle, ResultHeaderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub StyleSheetsOnItemLayoutThatIsInlineType_IsIgnored()
        Dim _itemResource As New ItemResourceEntity
        With FakeDal.Add.ItemTemplate("template1", Sub(i) i.ItemType = [Enum].GetName(GetType(ItemTypeEnum), ItemTypeEnum.Inline))
            .AddResource.StyleSheet("ItemStyle.css", style)
            .AddResource.StyleSheet("EditItemStyle.css", headerstyle)
            .IsUsedBy.Item("item", Sub(i) _itemResource = i)
        End With
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(),
                                                1235, New XHtmlParameter() With {.Value = String.Empty})
        Dim dict As Dictionary(Of String, String)
        Dim ResultHeaderStyle As String

        dict = _behavior.GetStyle()
        ResultHeaderStyle = _behavior.HeaderStyleElementContent

        Assert.IsFalse(dict.ContainsKey("ItemStyle.css"))
        Assert.IsFalse(dict.ContainsKey("EditItemStyle.css"))
        Assert.AreEqual(String.Empty, ResultHeaderStyle)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub ResourcesInEditStyleSheetsAreLoaded()
        Dim item As ItemResourceEntity = Nothing
        Dim headerStyleWithResource = StyleWithResource.FirstNode.ToString

        With FakeDal.Add.ItemTemplate("template1", Sub(i) i.ItemType = [Enum].GetName(GetType(ItemTypeEnum), ItemTypeEnum.Choice))
            .AddResource.StyleSheet("Generic.css", "bla")
            .AddResource.StyleSheet("EditGeneric.css", headerStyleWithResource)
            .IsUsedBy.Item("item", Sub(i) item = i)
        End With

        Dim _behavior As New XHtmlParameterBehavior(item, FakeDal.GetFakeResourceManager(),
                                        1235, New XHtmlParameter() With {.Value = String.Empty})
        _behavior.GetStyle()

        Assert.IsTrue(_behavior.HeaderStyleElementContent.Contains(String.Format("resource://package:{0}", _behavior.ContextIdentifier.ToString)))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior"), TestCategory("QBContentModel"), WorkItem(9608)>
    Public Sub AssertThatAddingResourceToItemWillHaveDependencyCountOf_1()
        Dim itm As ItemResourceEntity
        Dim addResource As IAddResources = FakeDal.Add.Item("someItem", Sub(i) itm = i).AddResource
        Dim cntBefore As Integer = itm.DependentResourceCollection.Count

        addResource.Image("someImg.jpeg")

        Assert.AreNotEqual(cntBefore, itm.DependentResourceCollection.Count)
        Assert.AreEqual(1, itm.DependentResourceCollection.Count)
    End Sub

    Private Function ConstructBehaviour_WithStylesheetOnItemResource() As XHtmlParameterBehavior
        Dim _itemResource As New ItemResourceEntity()
        With FakeDal.Add.Item("item", Sub(i) _itemResource = i).AddResource
            .StyleSheet("ItemStyle.css", style)
            .StyleSheet("EditItemStyle.css", headerstyle)
        End With
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 1235, New XHtmlParameter() With {.Value = String.Empty})
        Return _behavior
    End Function

    Private Function ConstructBehaviour_WithSylesheetOnItemLayoutTemplate() As XHtmlParameterBehavior
        Dim _itemResource As New ItemResourceEntity
        With FakeDal.Add.ItemTemplate("template1")
            .AddResource.StyleSheet("ItemStyle.css", style)
            .AddResource.StyleSheet("EditItemStyle.css", headerstyle)
            .IsUsedBy.Item("item", Sub(i) _itemResource = i)
        End With

        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(),
                                                1235, New XHtmlParameter() With {.Value = String.Empty})
        Return _behavior
    End Function

    Private Function GetTestHtml(path As String) As XElement
        Dim ret As New XElement(MathData)
        Dim body As XElement = DirectCast(ret.FirstNode, XElement)
        Dim p As XElement = DirectCast(body.FirstNode, XElement)
        DirectCast(p.FirstNode, XElement).FirstAttribute.Value = New Uri(path).ToString()
        Return ret
    End Function

    Private style As String = "html,body { padding: 0; margin: 0; height: 100%; width: 100%; font-family: Verdana; font-size: 13px; cursor: default; }"
    Private headerstyle As String = "a { color : #ffffff; }"

    Private inline As XElement = <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">
                                     <cito:InlineElement xmlns:cito="http://www.cito.nl/citotester" id="e476357a-0e97-499d-aa51-510e91717cba" layoutTemplateSourceName="InlineImageLayoutTemplate">
                                         <cito:parameters>
                                             <cito:parameterSet id="entireItem">
                                                 <cito:resourceparameter name="source" height="400" width="400">TransparentPix.png</cito:resourceparameter>
                                                 <cito:booleanparameter name="showPopup">False</cito:booleanparameter>
                                                 <cito:resourceparameter name="largeImage"/>
                                             </cito:parameterSet>
                                         </cito:parameters>
                                     </cito:InlineElement>txt</p>

    Private MathData As XElement = <html xmlns="http://www.w3.org/1999/xhtml"><body>
                                       <p id="c1-id-9">
                                           <img src="" id="508543e1-5a9a-4ea2-8bbf-6825b5c90341" ismathmlimage="true" class="mml" alt=""/>
                                       </p>
                                       </body></html>

    Private TestItemData As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                           <body>
                                               <p id="c1-id-1">Eros bibendum nibh nobis impedit quae sapiente vulputate, tristique nec. Delectus sunt, dolorem suscipit, mauris quas molestie purus, quisquam venenatis egestas feugiat vitae arcu nostra.</p>
                                           </body>
                                       </html>

    Private StyleWithResource As XElement = <style>
                @font-face
                {
                    font-family: 'RobotoRegular';
                    src: url('resource://package/Roboto-Regular-webfont.eot');
                    src: url('resource://package/Roboto-Regular-webfont.eot?#iefix') format('embedded-opentype'), url('resource://package/Roboto-Regular-webfont.ttf') format('truetype');
                    font-weight: normal;
                    font-style: normal;
                }

                html, body, p
                {
                    margin: 0;
                    padding: 0;
                    border: 0;
                    font-size: 12px;
                    font-family: RobotoRegular;
                    vertical-align: baseline;
                    line-height: 17px;
                }
                </style>
End Class
