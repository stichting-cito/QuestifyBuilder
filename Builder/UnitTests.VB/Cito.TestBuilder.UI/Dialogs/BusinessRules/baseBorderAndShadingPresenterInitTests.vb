
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public MustInherit Class baseBorderAndShadingPresenterInitTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsNoneBasedOnTableStyle()
        Dim dto = GetTableStyleForNone()

        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)

        Assert.AreEqual("none", presenter.CurrentTableStyleStrategy)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsBoxBasedOnTableStyle()
        Dim dto = GetTableStyleForBox()

        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)

        If dto.HasInnser Then
            Assert.AreEqual("box", presenter.CurrentTableStyleStrategy)
        Else
            Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
        End If
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsAllBasedOnTableStyle()
        Dim dto = GetTableStyleForAll()

        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)

        Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsGridBasedOnTableStyle()
        Dim dto = GetTableStyleForGrid()

        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)

        If dto.HasInnser Then
            Assert.AreEqual("grid", presenter.CurrentTableStyleStrategy)
        Else
            Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
        End If
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsCustomBasedOnTableStyle()
        Dim dto = GetTableStyleForCustom()

        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)

        Assert.AreEqual("custom", presenter.CurrentTableStyleStrategy)
    End Sub

    Private Function GetTableStyleForNone() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Hidden, 1)
        dto.Inner(LineStyle.Hidden, 2)
        Return dto
    End Function

    Private Function GetTableStyleForBox() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Solid, 1)
        dto.Inner(LineStyle.Hidden, 2)
        Return dto
    End Function

    Private Function GetTableStyleForAll() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Solid, 2)
        dto.Inner(LineStyle.Solid, 2)
        Return dto
    End Function

    Private Function GetTableStyleForGrid() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Solid, 2)
        dto.Inner(LineStyle.Solid, 1)
        Return dto
    End Function

    Private Function GetTableStyleForCustom() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Solid, 2)
        dto.Inner(LineStyle.Solid, 1)
        dto.TopHorizontal = LineStyle.Dotted

        Return dto
    End Function

    Friend MustOverride Function CreateStyle() As TableStyleDto

End Class
