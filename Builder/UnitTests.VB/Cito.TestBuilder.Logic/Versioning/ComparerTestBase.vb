Imports Cito.Tester.Common
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass()>
Public MustInherit Class ComparerTestBase

    Protected _resourceManager As ResourceManagerBase = Nothing

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
        ItemTestData.AddItemTemplatesAndControlTemplates()

        _resourceManager = FakeDal.GetFakeResourceManager()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

End Class
