
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework
Imports System.Xml.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.UnitTests.Framework.My.Resources

<TestClass()>
Public Class PopupContentBehaviorTests

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline2Editor_InlineImageTest()
        'Arrange
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline1.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New PopupContentBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
            e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
            result = e.ResourceName
        End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = _behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        'Assert
        Assert.IsTrue(html.Contains("isinlineelement"))
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())  'Old model inline element, indicating that inline images should be converted to img tag instead of cito inline control
        Assert.AreEqual(0, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count()) 'New inline elements, that should be converted to cito inline controls
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline2Param_InlineImageTest()
        'Arrange
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline1.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New PopupContentBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
            e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
            result = e.ResourceName
        End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = _behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        _behavior.SetHtml(html)

        'Assert
        Assert.IsTrue(xhtmlparam.Value.Contains("<img")) 'Does contain old image
        Assert.IsFalse(xhtmlparam.Value.Contains("<cito:InlineElement")) 'Does not contain cito inline element
        Assert.IsFalse(xhtmlparam.Value.Contains("isinlineelement"))
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())  'Old model inline element, indicating that inline images should be converted to img tag instead of cito inline control
        Assert.AreEqual(0, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count()) 'New inline elements, that should be converted to cito inline controls
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub InlineRepetitiveConversion_InlineImageTest()
        'Arrange
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline1.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New PopupContentBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
            e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
            result = e.ResourceName
        End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = _behavior.GetHtml()
        _behavior.SetHtml(html)

        Dim html2 = _behavior.GetHtml() 'Convert the inline to editor
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        'Assert
        Dim xHtml1 As XDocument = XDocument.Parse(html)
        UnitTestHelper.SetFixedIdsToCompare(xHtml1)
        Dim xHtml2 As XDocument = XDocument.Parse(html2)
        UnitTestHelper.SetFixedIdsToCompare(xHtml2)

        Dim same As Boolean = XDocument.DeepEquals(xHtml1, xHtml2) 'Compare xml!!!
        Assert.IsTrue(same)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline2Editor_InlineImageAndVideoTest()
        'Arrange
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline2.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New PopupContentBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = _behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        'Assert
        Assert.IsTrue(html.Contains("isinlineelement"))
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())  'Old model inline element, indicating that inline images should be converted to img tag instead of cito inline control
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count()) 'New inline elements, that should be converted to cito inline controls
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub Inline2Param_InlineImageAndVideoTest()
        'Arrange
        Dim requiredSetting = New DesignerSetting() With {.Key = "required", .Value = "False"}
        Dim xhtmlparam As New XHtmlParameter() With {.Value = inline2.ToString()}
        xhtmlparam.DesignerSettings.Add(requiredSetting)

        FakeDal.AddInline() 'Make Inline possible
        FakeDal.AddTransparentPix() 'Just a test image
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New PopupContentBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 8417, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
            e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
            result = e.ResourceName
        End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = _behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        _behavior.SetHtml(html)

        'Assert
        Assert.IsTrue(xhtmlparam.Value.Contains("<img")) 'Does contain old image
        Assert.IsFalse(xhtmlparam.Value.Contains("isinlineelement"))
        Assert.IsTrue(xhtmlparam.Value.Contains("<cito:InlineElement"))
        Assert.AreEqual(xhtmlparam.Value.IndexOf("<cito:InlineElement"), xhtmlparam.Value.LastIndexOf("<cito:InlineElement")) 'Does contain only one cito inline element, so IndexOf should be the same as LastIndexOf
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = True).Count())  'Old model inline element, indicating that inline images should be converted to img tag instead of cito inline control
        Assert.AreEqual(1, _behavior.InlineElements.Where(Function(ie) ie.Value.Item2 = False).Count()) 'New inline elements, that should be converted to cito inline controls
    End Sub

#Region "Data"

    Private inline1 As XElement =
        <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
            <img src="resource://package/TransparentPix.png" id="I79293efb-c31a-48d0-a1ea-d81f09f1a65c" alt="" width="57" height="19"/>
        </p>

    Private inline2 As XElement =
        <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
            <img src="resource://package/TransparentPix.png" id="I79293efb-c31a-48d0-a1ea-d81f09f1a65c" alt="" width="57" height="19"/>
            <cito:InlineElement xmlns:cito="http://www.cito.nl/citotester" id="Ic46ee1e9-2ba2-4486-99ec-37fe8bded144" layoutTemplateSourceName="InlineVideoLayoutTemplate">
                <cito:parameters>
                    <cito:parameterSet id="entireItem">
                        <cito:plaintextparameter name="controlId">Ic46ee1e9-2ba2-4486-99ec-37fe8bded144</cito:plaintextparameter>
                        <cito:resourceparameter name="sourceWebm">TestVideoCES.webm</cito:resourceparameter>
                        <cito:resourceparameter name="sourceMp4"/>
                        <cito:resourceparameter name="sourceMpg"/>
                        <cito:booleanparameter name="autoStart">False</cito:booleanparameter>
                        <cito:integerparameter name="width">320</cito:integerparameter>
                        <cito:integerparameter name="height">240</cito:integerparameter>
                        <cito:booleanparameter name="showToolbar">True</cito:booleanparameter>
                        <cito:booleanparameter name="showPlayButton">True</cito:booleanparameter>
                        <cito:booleanparameter name="showPauseButton">True</cito:booleanparameter>
                        <cito:booleanparameter name="showStopButton">True</cito:booleanparameter>
                        <cito:booleanparameter name="showTimeSlider">True</cito:booleanparameter>
                        <cito:integerparameter name="maxPlay">0</cito:integerparameter>
                        <cito:booleanparameter name="showElapsedTime">False</cito:booleanparameter>
                        <cito:booleanparameter name="showTotalTime">False</cito:booleanparameter>
                        <cito:booleanparameter name="showFastforwardButton">False</cito:booleanparameter>
                        <cito:booleanparameter name="showRewindButton">False</cito:booleanparameter>
                        <cito:plaintextparameter name="mediaPlayerDescription"/>
                    </cito:parameterSet>
                </cito:parameters>
            </cito:InlineElement>
        </p>

#End Region

End Class
