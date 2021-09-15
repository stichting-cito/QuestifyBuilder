
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports FluentAssertions
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate.FakeDal
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Framework.My.Resources

<TestClass()>
Public Class InlineChoiceOptionBehaviorTests

    <TestInitialize()>
    Public Sub Init()
        FailOnAssert.Disable = True 'For this test I rely on getting data from fakeDal that is not there.
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FailOnAssert.Disable = False
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub BehaviorForAddingInlineElemetns_OnlyImage()
        'Arrange
        Dim xhtmlparam As New XHtmlParameter() With {.Value = _data.ToString()}
        Dim itemResource As New ItemResourceEntity()

        'Act
        Dim behavior As New InlineChoiceOptionBehavior(itemResource, GetFakeResourceManager(), Nothing, xhtmlparam)

        'Assert
        Assert.IsTrue(behavior.CanInsertImages)
        Assert.IsTrue(behavior.CanInsertFormula)

        Assert.IsFalse(behavior.CanInsertMovies)
        Assert.IsFalse(behavior.CanInsertAudio)

        Assert.IsFalse(behavior.CanInsertControls)
        Assert.IsFalse(behavior.CanCreateReferences)
        Assert.IsFalse(behavior.CanInsertReferences)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub dataWithImage_ShouldHaveInlineElement()
        
        'Arrange
        FakeDal.AddTransparentPix() 'Just a test image
        Dim xhtmlparam As New XHtmlParameter() With {.Value = _data.ToString()}
        Dim itemResource As New ItemResourceEntity()
        Dim behavior As New InlineChoiceOptionBehavior(itemResource, GetFakeResourceManager(), Nothing, xhtmlparam)

        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource(UnitTestHelper.ImageToByte2(FakeStaticResources.transparentPix)) 'Dummy resource
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        Dim html = behavior.GetHtml()
        RemoveHandler TestSessionContext.ResourceNeeded, evnt

        'Assert
        behavior.InlineElements.Keys.Count.Should().Be(1, "1 inline element was expected")
    End Sub

#Region "Data"
    ReadOnly _data As XElement = <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"> <img id="Ic59482e6-6068-45f3-be96-786cac3b9c1b" width="150" height="38" alt="TransparentPix.png" src="resource://package/TransparentPix.png"/><br id="c1-id-12"/><em id="c1-id-13">Amazon</em></p>
#End Region

End Class
