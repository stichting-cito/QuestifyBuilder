
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
        'Arrange
        Dim toDelete As New List(Of AssessmentTestNode)
        Dim facade As New TestConstructionFacade()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()

        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1) targetSection = s1,
                                                                                        Function(si) si.MakeItem("1001", Sub(i) toDelete.Add(i))))
        SetDefaultRedirects() 'Internal plumbing

        'Act
        TestConstructionOp.DeleteTestComponents(toDelete, assessment, assessmentTestResourceEntity, facade)

        'Assert
        Assert.AreEqual(0, targetSection.Components.Count) 'Section is empty.
    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub DeleteSingleItemFromAssesmentPartOfDataSource_ItemsIsDeleted()
        'Arrange
        Dim ToDelete As New List(Of AssessmentTestNode)
        Dim facade As New TestConstructionFacade()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Some fake assessment
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1) targetSection = s1,
                                                                                        Function(si) si.MakeItem("1001", Sub(i) ToDelete.Add(i)),
                                                                                        Function(si) si.MakeItem("1002"),
                                                                                        Function(si) si.MakeItem("1003")
                                                                                            ))
        'ToDelete = 1001, being part of the datasource.
        SetAvailableDataSource("ds1", "1001", "1002", "1003")

        SetDefaultRedirects() 'Internal plumbing

        'Act
        TestConstructionOp.DeleteTestComponents(ToDelete, assessment, assessmentTestResourceEntity, facade)

        'Assert
        Assert.AreEqual(2, targetSection.Components.Count) ' It is allowed to delete item from Assessment while being part of an group.
    End Sub

End Class
