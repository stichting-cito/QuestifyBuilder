Imports Cito.Tester.Common
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.Faketory
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

<TestClass()>
Public MustInherit Class ChainTest

    Public Property FakeServices As IFakeServices
    Private _isDSSet As Boolean = False

    <TestInitialize()>
    Public Sub Init()
        FakeServices = FakeFactory.FakeServices
        FakeServices.SetupFakeServices()
        _isDSSet = False
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeServices.CleanFakeServices()
    End Sub


    Protected Sub SetAvailableDataSource(ByVal dsName As String, ByVal ParamArray items As String())
        If (_isDSSet) Then
            Throw New Exception("Sorry you can only do this once every test")
        End If
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroup(dsName, items))
        _isDSSet = True
    End Sub


    Protected Sub SetAvailableDataSources(ByVal dict As Dictionary(Of String, IEnumerable(Of String)))
        If (_isDSSet) Then
            Throw New Exception("Sorry you can only do this once every test")
        End If
        A.CallTo(Function() FakeServices.FakeResourceService.GetDataSourcesForBank(A(Of Integer).Ignored,
                                                                      A(Of Nullable(Of Boolean)).Ignored,
                                                                      A(Of String()).Ignored)
                                                                  ).ReturnsLazily(Function() FakeFactory.Datasources.GetInclusionGroups(dict))
        _isDSSet = True
    End Sub

    Protected Sub SetDefaultRedirects()
        A.CallTo(Function() FakeServices.FakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).
                                                           ReturnsLazily(Function(a) FakeFactory.ItemEntities.GetItemResourceEntities(a.Arguments.Get(Of IList(Of String))(0)))
        A.CallTo(Function() FakeServices.FakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(a) a.GetArgument(Of ResourceEntity)(0).ResourceData)
    End Sub

End Class
