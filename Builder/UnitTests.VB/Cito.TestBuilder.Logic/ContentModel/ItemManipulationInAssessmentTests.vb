
Imports FakeItEasy
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.Faketory
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

<TestClass>
Public Class ItemManipulationInAssessmentTests

    <TestInitialize()>
    Public Sub Init()
        FakeFactory.FakeServices.SetupFakeServices()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeFactory.FakeServices.CleanFakeServices()
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Find_ExistingItemInAssessmentTest_ShouldFind()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
        
        'Act
        Dim result = worker.ContainsItemCode("1001")
        
        'Assert
        Assert.IsTrue(result, "This item should be found")
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Find_Non_ExistingItemInAssessmentTest_ShouldNotFind()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
       
        'Act
        Dim result = worker.ContainsItemCode("I DO NOT EXIST")
        
        'Assert
        Assert.IsFalse(result, "Found a NON EXISTING ITEM?!")
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Rename_ExistingItemInAssessmentTest_ShouldFind()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
        
        'Act
        Dim result = worker.Rename("1001", "XYZ")
        
        'Assert
        Assert.IsTrue(result, "This item should be found")
    End Sub

    <TestMethod(), TestCategory("Logic")>
    <Description("This is behavior not enforced by the ItemManioulator. It Could be in the future.")>
    Public Sub Rename_ExistingItem_UsingAnExistingOtherName_InAssessmentTest_ShouldFind()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
        
        'Act
        Dim result1 = worker.ContainsItemCode("1002")
        Dim result2 = worker.Rename("1001", "1002")
        
        'Assert
        Assert.IsTrue(result1, "The item 1002 was not found")
        Assert.IsTrue(result2, "It appears as if renaming 1001 to 1002 was blocked?")
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Rename_Non_ExistingItemInAssessmentTest_ShouldNotFind()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
        
        'Act
        Dim result = worker.Rename("I DO NOT EXIST", "new Code")
        
        'Assert
        Assert.IsFalse(result, "Found a NON EXISTING ITEM?!")
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub Rename_ExistingItemInAssessmentTest_AssessmentIsSaved()
        'Arrange
        Dim assessment = GetAssessment_1001_tm_1004()
        Dim Assmt_res As New AssessmentTestResourceEntity
        Assmt_res.SetAssessmentToAssessmentResource(assessment)
        Dim worker As New ItemManipulationInAssessment(Assmt_res)
        
        'Act
        worker.Rename("1001", "XYZ")
        
        'Assert
        A.CallTo(Function() FakeFactory.FakeServices.FakeResourceService.UpdateAssessmentTestResource(A(Of AssessmentTestResourceEntity).Ignored)).
            MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    ''' <summary>
    ''' Gets the default assessment.
    ''' </summary>
    Function GetAssessment_1001_tm_1004() As AssessmentTest2
        Dim ret As AssessmentTest2

        ret = FakeFactory.AssesmentTest.MakeAssessment(Function(s As ITestSectionMaker) s.MakeSection("S1", Function(i As ISectionOrItemMaker) i.MakeItem("1001"),
                                                                                                            Function(i As ISectionOrItemMaker) i.MakeItem("1002"),
                                                                                                            Function(i As ISectionOrItemMaker) i.MakeItem("1003"),
                                                                                                            Function(i As ISectionOrItemMaker) i.MakeItem("1004")
                                                                                                                )
                                                                                                            )

        Return ret
    End Function

End Class
