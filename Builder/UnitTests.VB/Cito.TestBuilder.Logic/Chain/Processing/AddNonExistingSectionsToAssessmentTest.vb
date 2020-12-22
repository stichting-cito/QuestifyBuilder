
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Processing
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class AddNonExistingSectionsToAssessmentTest

    <TestMethod(), TestCategory("Logic")>
    Public Sub NoOperationNeeded_NothingShouldHaveHappend()
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                    Function(tp) tp.MakeTestPart("tp1",
                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)

        Dim status As ChainHandlerResult
        status = handler.ProcessRequest(req)

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status)
        Assert.AreEqual(1, assessment.GetAllSectionsInTest().Count)

    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub CreateSingleSection_SingleSectionShouldHaveBeenMadeUnderTargetSection()
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                    Function(tp) tp.MakeTestPart("tp1",
                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))


        req.OverridenTarget.Add(ResourceRefFactory.Make("1001"), New TestSection2() With {.Title = "new", .Identifier = "new"})

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)
        Dim status As ChainHandlerResult

        status = handler.ProcessRequest(req)

        Assert.AreEqual(2, assessment.GetAllSectionsInTest().Count)
        Assert.AreEqual(1, targetSection.GetAllSectionsInSection().Count)
        Assert.AreEqual("new", targetSection.GetAllSectionsInSection()(0).Identifier)

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub NoOperationNeededRetargetToExistingSection_NothingShouldHaveHappend()
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                    Function(tp) tp.MakeTestPart("tp1",
                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget),
                                                         Function(s) s.MakeSection("Re-TargetSection")))


        req.OverridenTarget.Add(ResourceRefFactory.Make("1001"), New TestSection2() With {.Identifier = "Re-TargetSection"})

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)
        Dim status As ChainHandlerResult

        status = handler.ProcessRequest(req)

        Assert.AreEqual(2, assessment.GetAllSectionsInTest().Count)
        Assert.AreEqual(0, targetSection.GetAllSectionsInSection().Count)

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status)
    End Sub


End Class
