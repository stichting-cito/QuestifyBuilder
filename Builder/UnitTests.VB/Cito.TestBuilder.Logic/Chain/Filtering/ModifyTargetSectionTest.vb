
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Filtering
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ModifyTargetSectionTest
    Inherits ChainTest

    <TestMethod(), TestCategory("Logic")>
    Public Sub SetDifferentTargetForRequestedItems()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim section1 As TestSection2
        Dim section2 As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("s1", Sub(s1) section1 = s1),
                                                                         Function(s) s.MakeSection("s2", Sub(s2) section2 = s2)))

        Dim status As ChainHandlerResult
        Dim handler As New ModifyTargetSection(ResourceRefFactory.MakeMultiple("1001", "1002"), section2)

        'Act
        status = handler.ProcessRequest(req)

        'Assert
        Assert.IsTrue(req.OverridenTarget.ContainsKey(req.Items(0))) 'Item 1001 present?
        Assert.IsTrue(req.OverridenTarget.ContainsKey(req.Items(1))) 'Item 1002 present?

        Assert.AreEqual(section2.Title, req.OverridenTarget(req.Items(0)).Title) 'Section Set to "Section2"
        Assert.AreEqual(section2.Title, req.OverridenTarget(req.Items(1)).Title)

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'Chain handler ok?
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub SetDifferentTargetForRequestedItemsTwice()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim section1 As TestSection2
        Dim section2 As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("s1", Sub(s1) section1 = s1),
                                                                         Function(s) s.MakeSection("s2", Sub(s2) section2 = s2)))

        Dim status As ChainHandlerResult
        Dim handler As New ModifyTargetSection(ResourceRefFactory.MakeMultiple("1001", "1002"), section2)
        Dim handler2 As New ModifyTargetSection(ResourceRefFactory.MakeMultiple("1001", "1002"), section1)

        'Act
        status = handler.ProcessRequest(req) 'first move to section2
        status = handler2.ProcessRequest(req) 'first move to section1

        'Assert
        Assert.IsTrue(req.OverridenTarget.ContainsKey(req.Items(0))) 'Item 1001 present?
        Assert.IsTrue(req.OverridenTarget.ContainsKey(req.Items(1))) 'Item 1002 present?

        Assert.AreEqual(section1.Title, req.OverridenTarget(req.Items(0)).Title) 'Section Set to "Section2"
        Assert.AreEqual(section1.Title, req.OverridenTarget(req.Items(1)).Title)

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'Chain handler ok?
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub SetDifferentTargetForItemsNotInRequest()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        Dim section1 As TestSection2
        Dim section2 As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("s1", Sub(s1) section1 = s1),
                                                                         Function(s) s.MakeSection("s2", Sub(s2) section2 = s2)))

        Dim status As ChainHandlerResult
        Dim handler As New ModifyTargetSection(ResourceRefFactory.MakeMultiple("1003", "1004"), "s2", section2)

        'Act
        status = handler.ProcessRequest(req) 'first move to section2


        'Assert
        Assert.IsFalse(req.OverridenTarget.ContainsKey(req.Items(0))) 'Item 1001 present?
        Assert.IsFalse(req.OverridenTarget.ContainsKey(req.Items(1))) 'Item 1001 present?


        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'Chain handler ok?
    End Sub

End Class
