﻿
Imports System.Windows.Forms
Imports System.IO
Imports Cito.Tester.ContentModel
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Questify.Builder.Logic
Imports Questify.Builder.Plugins.Html.ComponentOne
Imports Questify.Builder.Unittests.Framework.FakeAppTemplate
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers

<TestClass()>
Public Class ImgFilePasteTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property
    Shared _editor As XHtmlEditor

    <TestInitialize()> Public Sub MyTestInitialize()
        _editor = New XHtmlEditor()
        FakeDal.Init()
        FakeDal.CanSaveResources()
        FakeDal.AddInline()
    End Sub

    <TestCleanup()> Public Sub MyTestCleanup()
        FakeDal.Deinit()
        _editor.Dispose()
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EXPENSIVE")>
    <DeploymentItem("Resources\TestImage1.png")>
    <Description("Sanity check that clipboard is working as expected")>
    Public Sub SelfTestHtmlDataFormat()
        Dim testImage As String = "TestImage1.png"
        Dim imgLocation As String = Path.Combine(Environment.CurrentDirectory, testImage)

        SetHtmlData(My.Resources.PasteData.ImageFromWordTemplate.Replace("!!REPLACEME!!", imgLocation))

        Dim result = Clipboard.GetData("Html Format")
        Assert.IsNotNull(result, "The clipboard data was not in the 'Html Format'")
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EXPENSIVE")> <WorkItem(9831)>
    <DeploymentItem("Resources\TestImage1.png")>
    Public Sub PasteImageFromWord_EventTest()
        Dim htmlHandler As New HtmlHandlerBase(_editor, Nothing, FakeDal.GetFakeResourceManager(), Nothing)
        Dim testImage As String = "TestImage1.png"
        Dim imgLocation As String = Path.Combine(Environment.CurrentDirectory, testImage)

        SetHtmlData(My.Resources.PasteData.ImageFromWordTemplate.Replace("!!REPLACEME!!", imgLocation))
        Dim InlineName, ResourceAddedName As String : InlineName = String.Empty : ResourceAddedName = String.Empty
        AddHandler htmlHandler.InlineElementAdded, Sub(sender As Object, e As InlineElementEventArgs)
                                                       InlineName = DirectCast(e.InlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter).Value
                                                   End Sub
        AddHandler htmlHandler.ResourceAdded, Sub(sender As Object, e As ResourceNameEventArgs)
                                                  ResourceAddedName = e.ResourceName
                                              End Sub

        htmlHandler.PerformPasteOperation(False)

        Assert.AreNotEqual(String.Empty, InlineName)
        Assert.AreNotEqual(String.Empty, ResourceAddedName)
        Assert.AreEqual(InlineName, ResourceAddedName)
    End Sub

    Sub SetHtmlData(html As String)
        Clipboard.SetText(html, TextDataFormat.Html)
    End Sub

End Class
