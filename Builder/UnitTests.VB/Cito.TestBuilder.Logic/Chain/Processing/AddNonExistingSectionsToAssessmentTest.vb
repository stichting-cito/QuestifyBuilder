
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Processing
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class AddNonExistingSectionsToAssessmentTest

    <TestMethod(), TestCategory("Logic")>
    Public Sub NoOperationNeeded_NothingShouldHaveHappend()
        'Arrange
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        'Very basic Assessment 
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                            Function(tp) tp.MakeTestPart("tp1",
                                                                 Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)
        
        'Act
        Dim status As ChainHandlerResult
        status = handler.ProcessRequest(req)
        
        'Assert
        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'All went ok (nothing should have happened).
        Assert.AreEqual(1, assessment.GetAllSectionsInTest().Count) 'Since nothing happened, the number of sections should be 1

    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub CreateSingleSection_SingleSectionShouldHaveBeenMadeUnderTargetSection()
        'Arrange
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        'Very basic Assessment 
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                            Function(tp) tp.MakeTestPart("tp1",
                                                                 Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))


        req.OverridenTarget.Add(ResourceRefFactory.Make("1001"), New TestSection2() With {.Title = "new", .Identifier = "new"}) 'Retarget an item.

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)
        Dim status As ChainHandlerResult

        'Act
        status = handler.ProcessRequest(req)

        'Assert
        Assert.AreEqual(2, assessment.GetAllSectionsInTest().Count) 'A single sections should have been created/
        Assert.AreEqual(1, targetSection.GetAllSectionsInSection().Count) 'The sections should have been placed in the target section.
        Assert.AreEqual("new", targetSection.GetAllSectionsInSection()(0).Identifier) 'Name should be the same.

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'Chain was handled?
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub NoOperationNeededRetargetToExistingSection_NothingShouldHaveHappend()
        'Arrange
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")
        'Construct assessment
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                            Function(tp) tp.MakeTestPart("tp1",
                                                                 Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget),
                                                                 Function(s) s.MakeSection("Re-TargetSection")))


        req.OverridenTarget.Add(ResourceRefFactory.Make("1001"), New TestSection2() With {.Identifier = "Re-TargetSection"}) 'Retarget an item.

        Dim handler As New AddNonExistingSectionsToAssessment(assessment, targetSection)
        Dim status As ChainHandlerResult

        'Act
        status = handler.ProcessRequest(req)

        'Assert
        Assert.AreEqual(2, assessment.GetAllSectionsInTest().Count) 'There are 2 sections in original assessment, none were created.
        Assert.AreEqual(0, targetSection.GetAllSectionsInSection().Count) 'Double check that nothing was moved.

        Assert.AreEqual(ChainHandlerResult.RequestHandled, status) 'Chain was handled?
    End Sub


End Class
