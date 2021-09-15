
Imports Questify.Builder.Logic.Chain
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel
Imports Cito.Tester.ContentModel
Imports System.Diagnostics
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ItemInDatasourceTargetValidationTest
    Inherits ChainTest

    <TestMethod(), TestCategory("Logic"),
    ExpectedException(GetType(ItemDatasourceUsedElsewhereException))>
    Public Sub AddPartOfInclusionGroupToTestSectionWithoutBindingWithSectionWithBinding_ExpectsException()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        'Redirect call
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).That.Matches(Function(arg) arg(0) = "inclusion"))
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        'Fake DataBaseResourceManager
        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        'Assessment
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("s1",
                                                                                            Sub(s1) s1.ItemDataSource = "ds1"),
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))

        Dim handler As New ItemInDatasourceTargetValidation(dbMan, targetSection, assessment)

        Debug.WriteLine(handler.DumpAssesmentTree(assessment))

        'Act
        handler.ProcessRequest(req)
        
        'Assert
        'Expecting an exception.
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub AddPartOfInclusionGroupToTestSectionWithoutBinding_ExpectsHandeledRequest()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")


        'Redirect call
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).That.Matches(Function(arg) arg(0) = "inclusion"))
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        'Fake DataBaseResourceManager
        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        'Assessment
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))

        Dim handler As New ItemInDatasourceTargetValidation(dbMan, targetSection, assessment)

        Debug.WriteLine(handler.DumpAssesmentTree(assessment))

        'Act
        Dim result As ChainHandlerResult
        handler.ProcessRequest(req)

        'Assert
        Assert.AreEqual(ChainHandlerResult.RequestHandled, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub AddPartOfInclusionGroupToTestSectionWithBinding_ExpectsHandeledRequest()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")

        'Redirect call
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).That.Matches(Function(arg) arg(0) = "inclusion"))
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        'Fake DataBaseResourceManager
        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        'Assessment
        Dim targetSection As TestSection2 = Nothing
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget)
                                                                                                                        targetSection = sTarget
                                                                                                                        sTarget.ItemDataSource = "ds1"
                                                                                                                    End Sub)))

        Dim handler As New ItemInDatasourceTargetValidation(dbMan, targetSection, assessment)

        Debug.WriteLine(handler.DumpAssesmentTree(assessment))

        'Act
        Dim result As ChainHandlerResult
        handler.ProcessRequest(req)

        'Assert
        Assert.AreEqual(ChainHandlerResult.RequestHandled, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub AddPartOfInclusionGroupToTestSectionWithoutBindingWithSectionWithBinding_TargetIsOverriden_ExpectsHandled()
        'Arrange
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002")


        'Redirect call
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).That.Matches(Function(arg) arg(0) = "inclusion"))
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        'Fake DataBaseResourceManager
        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        'Assessment
        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("s1",
                                                                                            Sub(s1) s1.ItemDataSource = "ds1"),
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget) targetSection = sTarget)))

        'Target has been overridden to explicit use "targetSection" instead of preferred "s1" section.
        req.OverridenTarget.Add(ResourceRefFactory.Make("1001"), targetSection)
        req.OverridenTarget.Add(ResourceRefFactory.Make("1002"), targetSection)

        Dim handler As New ItemInDatasourceTargetValidation(dbMan, targetSection, assessment)

        Debug.WriteLine(handler.DumpAssesmentTree(assessment))

        'Act
        handler.ProcessRequest(req)
        
        'Assert
        'Expecting an exception.
    End Sub

End Class
