﻿
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.My.Resources

<TestClass()>
Public Class AspectEditorBehaviorTests

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub BusinessRulesAspectReferences_Test()
        'Arrange
        Dim _itemResource As New ItemResourceEntity()

        'Act
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, New Aspect() With {.Description = inline.ToString()})

        'Assert
        Assert.IsTrue(_behavior.CanInsertImages)
        Assert.IsTrue(_behavior.CanInsertFormula)

        Assert.IsFalse(_behavior.CanInsertMovies)
        Assert.IsFalse(_behavior.CanInsertAudio)
        Assert.IsFalse(_behavior.CanInsertControls)
        Assert.IsFalse(_behavior.CanCreateReferences)
        Assert.IsFalse(_behavior.CanInsertReferences)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline_HtmlConverterTest()
        'Arrange
        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, New Aspect() With {.Description = inline.ToString()})
        
        'Act
        Dim html = GetHtml(_behavior)

        'Assert
        Assert.IsTrue(UnitTestHelper.SetFixedIdsToCompare(html).Contains("<img isinlineelement=""true"" id=""fixedid"" width=""400"" height=""400"" alt=""TransparentPix.png"" src=""resource://package:8417/TransparentPix.png"" />"))
    End Sub

    Private Function GetHtml(ByRef behavior As AspectEditorBehavior) As String
        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        Return html
    End Function

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineCount_HtmlConverterTest()
        'Arrange
        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, New Aspect() With {.Description = inline.ToString()})
       
        'Act
        GetHtml(_behavior)
       
        'Assert
        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest()
        'Arrange
        Dim AspectParam As New Aspect() With {.Description = inline.ToString()}
        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, AspectParam)
        Dim html = GetHtml(_behavior) 'Convert the inline to editor
       
        'Act
        _behavior.SetHtml(html)
       
        'Assert
        Assert.IsFalse(AspectParam.Description.Contains("<cito:InlineElement"))
        Assert.IsTrue(AspectParam.Description.Contains("<img"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineBack2Parameter_HtmlConverterTest_KeyOfDictionaryDoesNotMatter()
        'Arrange
        Dim AspectParam As New Aspect() With {.Description = inline.ToString()}
        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, AspectParam)
        Dim html = GetHtml(_behavior) 'Convert the inline to editor
        Dim inlineKey = _behavior.InlineElements.Keys.First()
       
        'Act                                                 
        Dim switcher = _behavior.InlineElements(inlineKey)
        _behavior.InlineElements.Remove(inlineKey)
        _behavior.InlineElements.Add("I_Expect_ThatThisIdHasNoImpactWhatSoEver", switcher)
        'The html could have been altered due to moving of elements
        _behavior.SetHtml(html.Replace("e476357a-0e97-499d-aa51-510e91717cba", "I_Expect_ThatThisIdHasNoImpactWhatSoEver"))
      
        'Assert
        Assert.IsFalse(AspectParam.Description.Contains("<cito:InlineElement"))
        Assert.IsTrue(AspectParam.Description.Contains("<img"))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub CompareRepetitiveConvertion_HtmlConverterTest()
        'Arrange
        Dim aspectParam As New Aspect() With {.Description = inline.ToString()}
        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New AspectEditorBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, aspectParam)
       
        'Act
        Dim html = GetHtml(_behavior) 'Convert the inline to editor
        _behavior.SetHtml(html)
        Dim html2 = GetHtml(_behavior) 'Convert the inline to editor
       
        'Assert
        Dim xHtml1 As XDocument = XDocument.Parse(html)
        Dim xHtml2 As XDocument = XDocument.Parse(html2)
        Dim same As Boolean = XDocument.DeepEquals(xHtml1, xHtml2) 'Compare xml!
        Assert.IsTrue(same)
        Assert.AreEqual(1, _behavior.InlineElements.Count())
    End Sub

    Private inline As XElement = <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml"><img src="resource://package/TransparentPix.png" id="e476357a-0e97-499d-aa51-510e91717cba" alt="" width="400" height="400"/> and txt.</p>

End Class
