
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass()>
Public Class AspectReferenceEditBehaviorTests


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

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub BusinessRulesAspectReferences_Test()
        'Arrange

        'Act
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviour()

        'Assert
        Assert.IsTrue(_behavior.CanInsertImages)
        Assert.IsTrue(_behavior.CanInsertMovies)
        Assert.IsTrue(_behavior.CanInsertAudio)
        Assert.IsTrue(_behavior.CanInsertFormula)

        Assert.IsFalse(_behavior.CanInsertControls)
        Assert.IsFalse(_behavior.CanCreateReferences)
        Assert.IsFalse(_behavior.CanInsertReferences)
    End Sub



    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline_HtmlConverterTest()
        'Arrange
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviour()
        
        'Act
        Dim html = _behavior.GetHtml()
        
        'Assert
        Assert.IsTrue(html.Contains("<img isinlineelement=""true"" id=""e476357a-0e97-499d-aa51-510e91717cba"" width=""400"" height=""400"" alt=""TransparentPix.png"" src=""resource://package:8417/TransparentPix.png"" />"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineCount_HtmlConverterTest()
        'Arrange
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviour()
        
        'Act
        Dim html = _behavior.GetHtml()
        
        'Assert
        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest()
        'Arrange
        Dim AspectParam As New AspectReference() With {.Description = inline.ToString()}
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviour()
        Dim html = _behavior.GetHtml() 'Convert the inline to editor
        'Act
        _behavior.SetHtml(html)
        'Assert
        Assert.IsTrue(AspectParam.Description.Contains("<cito:InlineElement xmlns:cito=""http://www.cito.nl/citotester"" id=""e476357a-0e97-499d-aa51-510e91717cba"" layoutTemplateSourceName=""InlineImageLayoutTemplate"">"))
        Assert.IsFalse(AspectParam.Description.Contains("<img")) 'Does not contain old image
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub CompareRepetitiveConvertion_HtmlConverterTest()
        'Arrange
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviour()
        
        'Act
        Dim html = _behavior.GetHtml() 'Convert the inline to editor
        _behavior.SetHtml(html)
        Dim html2 = _behavior.GetHtml() 'Convert the inline to editor
       
        'Assert
        Dim xHtml1 As XDocument = XDocument.Parse(html)
        Dim xHtml2 As XDocument = XDocument.Parse(html2)
        Dim same As Boolean = XDocument.DeepEquals(xHtml1, xHtml2) 'Compare xml!!!
        Assert.IsTrue(same)
        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub GetStylesheetFromBehaviour()
        'Arrange
        Dim _behavior As AspectReferenceEditorBehavior = ConstructBehaviourForStylesheetTest()
        
        'Act
        Dim dict As Dictionary(Of String, String)
        dict = _behavior.GetStyle()
       
        'Assert
        Assert.IsTrue(dict.ContainsKey("AspectStyle.css")) 'This was added in ConstructBehaviour
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub NothingDescription_HtmlConverterTest()
        'Arrange
        Dim _aspectResource As AspectResourceEntity
        FakeDal.Add.Aspect("someAspect", Sub(a) _aspectResource = a)
        Dim _behavior As New AspectReferenceEditorBehavior(_aspectResource, FakeDal.GetFakeResourceManager(), 8417, New AspectReference() With {.Description = Nothing})
        
        'Act
        Dim html = _behavior.GetHtml()
        
        'Assert
        Assert.IsTrue(html.Contains("<body style=""fontFamilyPlaceholderKey:fontFamilyPlaceholderValue;padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;""></body>"))
    End Sub

    Function ConstructBehaviour() As AspectReferenceEditorBehavior
        Dim _aspectResource As New AspectResourceEntity()
        Dim itemLayoutTemplateResourceEntity As New ItemLayoutTemplateResourceEntity()
        Dim resource As New GenericResourceEntity With {.MediaType = "text/css", .Name = "AspectStyle.css", .ResourceData = New ResourceDataEntity() With
                                                                                                 {.BinData = System.Text.UTF8Encoding.UTF8.GetBytes(style.ToCharArray())}}
        itemLayoutTemplateResourceEntity.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResource = _aspectResource})
        _aspectResource.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResource = resource})

        FakeDal.FakeServices.FakeResourceService.UpdateItemLayoutTemplateResource(itemLayoutTemplateResourceEntity)

        Dim _behavior As New AspectReferenceEditorBehavior(itemLayoutTemplateResourceEntity, _aspectResource, FakeDal.GetFakeResourceManager(), 8417, New AspectReference() With {.Description = inline.ToString()})
        Return _behavior
    End Function

    Function ConstructBehaviourForStylesheetTest() As AspectReferenceEditorBehavior
        Dim _aspectResource As New AspectResourceEntity()
        Dim itemResourceEntity As New ItemResourceEntity()
        Dim itemLayoutTemplateResourceEntity As New ItemLayoutTemplateResourceEntity()
        Dim resource As New GenericResourceEntity With {.MediaType = "text/css", .Name = "AspectStyle.css", .ResourceData = New ResourceDataEntity() With
                                                                                                 {.BinData = System.Text.UTF8Encoding.UTF8.GetBytes(style.ToCharArray())}}
        itemLayoutTemplateResourceEntity.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResource = resource})
        itemResourceEntity.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResource = itemLayoutTemplateResourceEntity})
        _aspectResource.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResource = resource})

        FakeDal.FakeServices.FakeResourceService.UpdateItemLayoutTemplateResource(itemLayoutTemplateResourceEntity)
        FakeDal.FakeServices.FakeResourceService.UpdateItemResource(itemResourceEntity)

        Dim _behavior As New AspectReferenceEditorBehavior(itemResourceEntity, _aspectResource, FakeDal.GetFakeResourceManager(), 8417, New AspectReference() With {.Description = inline.ToString()})
        Return _behavior
    End Function

    Private inline As XElement = <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">
                                     <cito:InlineElement xmlns:cito="http://www.cito.nl/citotester" id="e476357a-0e97-499d-aa51-510e91717cba" layoutTemplateSourceName="InlineImageLayoutTemplate">
                                         <cito:parameters>
                                             <cito:parameterSet id="entireItem">
                                                 <cito:resourceparameter name="source" width="400" height="400">TransparentPix.png</cito:resourceparameter>
                                                 <cito:booleanparameter name="showPopup">False</cito:booleanparameter>
                                                 <cito:resourceparameter name="largeImage"/>
                                             </cito:parameterSet>
                                         </cito:parameters>
                                     </cito:InlineElement>txt</p>


    Private style As String = "html,body { padding: 0; margin: 0; height: 100%; width: 100%; font-family: Verdana; font-size: 13px; cursor: default; }"
End Class
