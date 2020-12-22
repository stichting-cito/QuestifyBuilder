
Option Infer On

Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class TestConstrAddBugFixesTests
    Inherits ChainTest

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    <WorkItem(7629)>
    Public Sub AddSecondGroupToSection_Ignore_ExpectsTwoGoupsAdded()
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function()
                                                                              Return FakeFactory.Datasources.GetInclusionGroups(New KeyValuePair(Of String, IEnumerable(Of String))() {
                                                                                                                                New KeyValuePair(Of String, IEnumerable(Of String))("ds1", New String() {"1001", "1002", "1003"}),
                                                                                                                                New KeyValuePair(Of String, IEnumerable(Of String))("ds2", New String() {"1004", "1005", "1006"})})
                                                                          End Function)
        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource("xxx"))

        Dim nrOfResolveCalls As Integer = 0
        Dim stage As Integer = 1
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      If (stage = 1) Then
                                                          e.Resolution = TestConstructionValidationEventArgs.ResolutionEnum.RetryIgnore
                                                      Else
                                                          e.Resolution = TestConstructionValidationEventArgs.ResolutionEnum.RetryIgnore
                                                      End If
                                                      nrOfResolveCalls = nrOfResolveCalls + 1
                                                  End Sub

        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002", "1003"))
        Dim itemsToAdd2 As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1004", "1005", "1006"))


        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)

        stage = 2
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd2,
                                        targetSection,
                                        0,
                                        facade)
        Assert.AreEqual(6, targetSection.Components.Count)
        Assert.IsTrue(String.IsNullOrEmpty(targetSection.ItemDataSource))
        Assert.AreEqual(2, nrOfResolveCalls)
    End Sub

End Class
