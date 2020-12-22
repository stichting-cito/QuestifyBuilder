
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
Public Class TestConstructionTest
    Inherits ChainTest

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddTwoItemsToEmptyTest_ExpectsTwoItemsAdded()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(itemsToAdd.Count, targetSection.Components.Count)
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).
    MustHaveHappened(Repeated.Exactly.Twice)
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasource_ResolutionRetryFix_ExpectsWholeDatasourceAddedSectionWithBinding()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        Dim nrOfResolveCalls As Integer = 0
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      nrOfResolveCalls = nrOfResolveCalls + 1
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(3, targetSection.Components.Count)
        Assert.AreEqual("ds1", targetSection.ItemDataSource)
        Assert.AreEqual(2, nrOfResolveCalls)
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasourceToSectionWithOtherItem_ResolutionRetryFix_ExpectsWholeDatasourceAdded()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2,
                                                                                        Function(itm) itm.MakeItem("2000")))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))
        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        Dim nrOfResolveCalls As Integer = 0
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      nrOfResolveCalls = nrOfResolveCalls + 1
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(1, targetSection.GetAllItemReferencesInSection(False).Count)
        Assert.AreEqual(4, targetSection.GetAllItemReferencesInSection(True).Count)
        Assert.IsTrue(String.IsNullOrEmpty(targetSection.ItemDataSource))
        Assert.AreEqual("ds1", targetSection.GetAllSectionsInSection(0).ItemDataSource)
        Assert.IsTrue(targetSection.GetAllSectionsInSection(0).Title.Contains("ds1"))

        Assert.AreEqual(2, nrOfResolveCalls)
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasource_ResolutionRetryIgnore_ExpectSingleItemAdded()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(1, targetSection.Components.Count)
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddAllItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryFix_ExpectItemsAddedToOtherSection()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002", "1003"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1"
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(0, targetSection.GetAllItemReferencesInSection(False).Count)
        Assert.AreEqual(3, otherSection.GetAllItemReferencesInSection(False).Count)

    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddAllItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryIgnore_ExpectItemsAddedToWrongSection()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002", "1003"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1"
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)
        Assert.AreEqual(3, targetSection.GetAllItemReferencesInSection(False).Count)
        Assert.AreEqual(0, otherSection.GetAllItemReferencesInSection(False).Count)
    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddSomeItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryFix_ExpectAllItemsFromDSAddedToSectionS1()
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1"
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        Dim validationsSolved As Integer = 0
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      validationsSolved = validationsSolved + 1
                                                  End Sub

        TestConstructionOp.AddItemsToTest(assessment,
                                assessmentTestResourceEntity,
                                resourceManager,
                                itemsToAdd,
                                targetSection,
                                0,
                                facade)

        Assert.AreEqual(2, validationsSolved)
        Assert.AreEqual(0, targetSection.GetAllItemReferencesInSection(False).Count)
        Assert.AreEqual(3, otherSection.GetAllItemReferencesInSection(False).Count)
    End Sub

End Class