
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Filtering
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ModifyItemsInRequestHandlerTest

    <TestMethod(), TestCategory("Logic")>
    Public Sub AddTwoItemsToAddRequest_ShouldHaveFourItemsTest()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim handler As New ModifyItemsInRequestHandler(ModifyItemsInRequestHandler.RequestTypeEnum.Add, _
                                                 ResourceRefFactory.MakeMultiple("1003", "1004"))

        handler.ProcessRequest(req)

        Assert.AreEqual(4, req.Items.Count)
    End Sub

    <TestCategory("Logic")> <TestMethod()>
    Public Sub AddTwoItemsToAddRequestWithOneOverlap_ShouldHaveThreeItemsTest()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim handler As New ModifyItemsInRequestHandler(ModifyItemsInRequestHandler.RequestTypeEnum.Add, _
                                                 ResourceRefFactory.MakeMultiple("1002", "1003"))

        handler.ProcessRequest(req)

        Assert.AreEqual(3, req.Items.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RemoveTwoItemsFromAddRequest_ShouldHaveZeroItemsTest()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim handler As New ModifyItemsInRequestHandler(ModifyItemsInRequestHandler.RequestTypeEnum.Remove,
                                                 ResourceRefFactory.MakeMultiple("1001", "1002"))

        handler.ProcessRequest(req)

        Assert.AreEqual(0, req.Items.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RemoveTwoItemsFromAddRequestWithOneOverlap_ShouldHaveOneItemsTest()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim handler As New ModifyItemsInRequestHandler(ModifyItemsInRequestHandler.RequestTypeEnum.Remove, _
                                                 ResourceRefFactory.MakeMultiple("1002", "1003"))

        handler.ProcessRequest(req)

        Assert.AreEqual(1, req.Items.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RemoveTwoItemsFromRemoveRequestWithOneOverlap_ShouldHaveOneItemsTest()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim handler As New ModifyItemsInRequestHandler(ModifyItemsInRequestHandler.RequestTypeEnum.Remove, _
                                                 ResourceRefFactory.MakeMultiple("1002", "1003"))

        handler.ProcessRequest(req)

        Assert.AreEqual(1, req.Items.Count)
    End Sub

End Class


