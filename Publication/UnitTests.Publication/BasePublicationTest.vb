

Imports Questify.Builder.UnitTests.Framework.Faketory
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

<TestClass()>
Public MustInherit Class BasePublicationTest

    Public Property FakeServices As IFakeServices

    <TestInitialize()>
    Public Sub Init()
        FakeServices = FakeFactory.FakeServices
        FakeServices.SetupFakeServices()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeServices.CleanFakeServices()
    End Sub

End Class
