
Option Infer On
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Cito.Tester.ContentModel
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class TestConstrDeleteTests
    Inherits ChainTest

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub DeleteSingleItemFromAssesment_ItemsIsDeleted()
        Dim toDelete As New List(Of AssessmentTestNode)
        Dim facade As New TestConstructionFacade()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()

        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1) targetSection = s1,
                                                                                        Function(si) si.MakeItem("1001", Sub(i) toDelete.Add(i))))
        SetDefaultRedirects()

        TestConstructionOp.DeleteTestComponents(toDelete, assessment, assessmentTestResourceEntity, facade)

        Assert.AreEqual(0, targetSection.Components.Count)
    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub DeleteSingleItemFromAssesmentPartOfDataSource_ItemsIsDeleted()
        Dim ToDelete As New List(Of AssessmentTestNode)
        Dim facade As New TestConstructionFacade()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1) targetSection = s1,
                                                                                Function(si) si.MakeItem("1001", Sub(i) ToDelete.Add(i)),
                                                                                Function(si) si.MakeItem("1002"),
                                                                                Function(si) si.MakeItem("1003")
                                                                                    ))
        SetAvailableDataSource("ds1", "1001", "1002", "1003")

        SetDefaultRedirects()

        TestConstructionOp.DeleteTestComponents(ToDelete, assessment, assessmentTestResourceEntity, facade)

        Assert.AreEqual(2, targetSection.Components.Count)
    End Sub

End Class
