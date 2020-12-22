
Option Infer On
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ItemRelationshipValidationHandlerTest
    Inherits ChainTest

    <TestMethod(), TestCategory("Logic"), ExpectedException(GetType(ItemRelationshipException))>
    Public Sub AddPartOfInclusionGroupToAssesmentTest_ThrowsValidationException()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).That.Matches(Function(arg) arg(0) = "inclusion"))
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim handler As New ItemRelationshipValidationHandler(dbMan, "inclusion")

        handler.ProcessRequest(req)

    End Sub

    <TestCategory("Logic")> <TestMethod(), ExpectedException(GetType(ItemRelationshipException))>
    Public Sub AddSomeItemsFromExculsionGroup_ThrowsValidationException()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add(New String() {"1002"},
                                                                 New String() {"1001"})

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).That.Matches(Function(arg) arg(0) = "exclusion"))
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetExclusionGroup("ds1", New String() {"1001", "1002", "1003"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim handler As New ItemRelationshipValidationHandler(dbMan, "exclusion")

        handler.ProcessRequest(req)

    End Sub

End Class
