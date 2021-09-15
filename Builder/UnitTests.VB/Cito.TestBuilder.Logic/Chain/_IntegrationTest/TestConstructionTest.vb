
Option Infer On

Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.UnitTests.Framework.Faketory

''' <summary>
''' This 'testclass' is responsible for testing the WHOLE TestConstruction Chain. Thus considered an integration test.
''' During development some of these test may fail, they are written to the *current* chain and it's handlers. Please keep in good shape.
''' Please add more tests when necessary.
''' </summary>
<TestClass()>
Public Class TestConstructionTest
    Inherits ChainTest

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddTwoItemsToEmptyTest_ExpectsTwoItemsAdded()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(itemsToAdd.Count, targetSection.Components.Count) 'In an empty Section all items have been added.
        'We added 2 items, so two items have been tried to add to the resources.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).
            MustHaveHappened(Repeated.Exactly.Twice) 'Check that we tried to add resources.
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasource_ResolutionRetryFix_ExpectsWholeDatasourceAddedSectionWithBinding()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'How to resolve problem? FIX IT!
        Dim nrOfResolveCalls As Integer = 0
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      nrOfResolveCalls = nrOfResolveCalls + 1
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(3, targetSection.Components.Count) 'In an empty Section all items have been added.
        Assert.AreEqual("ds1", targetSection.ItemDataSource) 'Datasource has been set.
        Assert.AreEqual(2, nrOfResolveCalls) 'Expected number of resolveActions.
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasourceToSectionWithOtherItem_ResolutionRetryFix_ExpectsWholeDatasourceAdded()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2,
                                                                                        Function(itm) itm.MakeItem("2000")))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))
        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'How to resolve problem? FIX IT!
        Dim nrOfResolveCalls As Integer = 0
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      nrOfResolveCalls = nrOfResolveCalls + 1
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(1, targetSection.GetAllItemReferencesInSection(False).Count) 'This is item '2000'
        Assert.AreEqual(4, targetSection.GetAllItemReferencesInSection(True).Count) 'This is item '2000' + 1001 ,1002, 1003
        Assert.IsTrue(String.IsNullOrEmpty(targetSection.ItemDataSource)) 'Datasource has been not been set.
        Assert.AreEqual("ds1", targetSection.GetAllSectionsInSection(0).ItemDataSource)
        Assert.IsTrue(targetSection.GetAllSectionsInSection(0).Title.Contains("ds1"))

        Assert.AreEqual(2, nrOfResolveCalls) 'Expected number of resolveActions.
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddItemFromDatasource_ResolutionRetryIgnore_ExpectSingleItemAdded()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1"),
                                                              Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'How to resolve problem? Ignore It!
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(1, targetSection.Components.Count) 'In an empty Section single item have been added.
    End Sub

    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddAllItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryFix_ExpectItemsAddedToOtherSection()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002", "1003"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1" 'Set Datasource!!!!
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'How to resolve problem? FIX IT!
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      'Add All Items
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(0, targetSection.GetAllItemReferencesInSection(False).Count) 'All items are placed in "otherSection"
        Assert.AreEqual(3, otherSection.GetAllItemReferencesInSection(False).Count) 'all items should be here.

    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddAllItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryIgnore_ExpectItemsAddedToWrongSection()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001", "1002", "1003"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1" 'Set Datasource!!!!
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2)) 'This is the WRONG section

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        'How to resolve problem? Ignore it!
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      'This will cause the target of the items to be add to be set to Original Target section... the WRONG section.
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryIgnore
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert
        Assert.AreEqual(3, targetSection.GetAllItemReferencesInSection(False).Count) 'All items are placed in "otherSection"
        Assert.AreEqual(0, otherSection.GetAllItemReferencesInSection(False).Count) 'In an empty Section single item have been added.
    End Sub


    <TestMethod()> <TestCategory("Logic")> <TestCategory("Integration")>
    Public Sub AddSomeItemsFromDatasourceToSectionOtherSectionWithBindingExists_ResolutionRetryFix_ExpectAllItemsFromDSAddedToSectionS1()
        'Arrange
        Dim itemsToAdd As New List(Of ResourceRef)(ResourceRefFactory.MakeMultiple("1001"))
        Dim facade As New TestConstructionFacade()
        Dim resourceManager = FakeResourceManager.Make()
        Dim assessment As AssessmentTest2
        Dim targetSection As TestSection2
        Dim otherSection As TestSection2 = Nothing
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1", Sub(s1)
                                                                                                  s1.ItemDataSource = "ds1" 'Set Datasource!!!!
                                                                                                  otherSection = s1
                                                                                              End Sub),
                                                                                        Function(s) s.MakeSection("s2", Sub(s2) targetSection = s2))

        Dim assessmentTestResourceEntity As AssessmentTestResourceEntity = A.Fake(Of AssessmentTestResourceEntity)()
        'Ignore adding Resources, just say it's there.
        A.CallTo(Function() assessmentTestResourceEntity.ContainsDependentResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function() True)

        'Intercept call to return ItemResourceEntity that are reconstructed resourceRefs
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                                   ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        'Returns Serialized Data.
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)

        'Returns a inclusiongroup
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True }
        A.CallTo(Function() resourceManager.GetResource(A(Of String).Ignored, A(Of ResourceProcessingFunction).Ignored, request)).ReturnsLazily(Function() New BinaryResource(""))

        Dim validationsSolved As Integer = 0 'count how often the 'ResolveValidationError' event has been raised.
        'How to resolve problem? FIX IT!
        AddHandler facade.ResolveValidationError, Sub(sender As Object, e As TestConstructionValidationEventArgs)
                                                      'This will cause the target of the items to be add to be set to Original Target section... the WRONG section.
                                                      e.Resolution = TestConstructionValidationEventArgs.resolutionEnum.RetryFix
                                                      validationsSolved = validationsSolved + 1
                                                  End Sub

        'Act
        TestConstructionOp.AddItemsToTest(assessment,
                                        assessmentTestResourceEntity,
                                        resourceManager,
                                        itemsToAdd,
                                        targetSection,
                                        0,
                                        facade)
        'Assert

        Assert.AreEqual(2, validationsSolved) 'How often we needed to resolve an error. (1) Yes, add all items from datasource; (2) Yes, redirect to section with binding.
        Assert.AreEqual(0, targetSection.GetAllItemReferencesInSection(False).Count) 'All items are placed in "otherSection"
        Assert.AreEqual(3, otherSection.GetAllItemReferencesInSection(False).Count) 'All items from datasource.
    End Sub

End Class