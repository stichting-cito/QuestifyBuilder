
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class PreferDatabindingValidationTest
    Inherits ChainTest

    <TestMethod(), TestCategory("Logic"), ExpectedException(GetType(SuggestDatasourceBindingException))>
    Sub AddWholeDSToEmptySection_ExpectsSuggestingException()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002", "1003")

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim targetSection As TestSection2 = Nothing
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget)
                                                                                                                        targetSection = sTarget
                                                                                                                    End Sub)))

        Dim handler As New PreferDatabindingValidation(dbMan, targetSection)

        handler.ProcessRequest(req)

    End Sub


    <TestMethod(), TestCategory("Logic"), ExpectedException(GetType(SuggestDatasourceBindingException))>
    Sub AddWholeDSToNonEmptySection_ExpectsSuggestingException()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002", "1003")

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim targetSection As TestSection2 = Nothing
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget)
                                                                                                                        targetSection = sTarget
                                                                                                                    End Sub, Function(itm) itm.MakeItem("2000"))))

        Dim handler As New PreferDatabindingValidation(dbMan, targetSection)

        handler.ProcessRequest(req)

    End Sub

    <TestMethod(), TestCategory("Logic")>
    Sub AddWholeDSToNonEmptySection_ExpectsSuggestingExceptionToNewSection()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "1002", "1003")

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim targetSection As TestSection2 = Nothing
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(sTarget)
                                                                                                                        targetSection = sTarget
                                                                                                                    End Sub, Function(itm) itm.MakeItem("2000"))))

        Dim handler As New PreferDatabindingValidation(dbMan, targetSection)

        Try
            handler.ProcessRequest(req)
        Catch ex As SuggestDatasourceBindingException
            Assert.IsTrue(ex.IsSuggestingNestedSection)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Sub AddACoupleOfItemsIncludingAgroup_ExpectsSuggestingExceptionToNewSection()
        Dim req As TestConstructionRequest = TestConstructionFactory.Add("1001", "2001", "2002", "1002", "1003")

        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                              A(Of Nullable(Of Boolean)).Ignored,
                                                              A(Of String()).Ignored)
                                                          ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup("ds1", New String() {"1003", "1002", "1001"}))

        Dim bankEntity As EntityClasses.BankEntity = A.Fake(Of EntityClasses.BankEntity)()
        Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(bankEntity.Id))

        Dim targetSection As TestSection2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment("Test1",
                                                    Function(tp) tp.MakeTestPart("tp1",
                                                                         Function(s) s.MakeSection("TargetSection", Sub(target) targetSection = target)))

        Dim handler As New PreferDatabindingValidation(dbMan, targetSection)

        Try

            handler.ProcessRequest(req)
        Catch ex As SuggestDatasourceBindingException
            Assert.IsTrue(ex.IsSuggestingNestedSection)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

End Class
