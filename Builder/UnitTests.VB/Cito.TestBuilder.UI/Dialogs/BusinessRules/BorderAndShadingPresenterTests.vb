
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public Class BorderAndShadingPresenterTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertTransitionToDefaultFromNone()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), GetTableStyleForNone())
        presenter.CurrentLineStyle = LineStyle.Double

        presenter.LeftChecked = True

        Assert.AreEqual("custom", presenter.CurrentTableStyleStrategy)
        Assert.IsFalse(presenter.Style.LeftVertical.Value = LineStyle.Hidden)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertTransitionToDefaultFromAll()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), GetTableStyleForAll())
        presenter.CurrentLineStyle = LineStyle.Double

        presenter.LeftChecked = False

        Assert.AreEqual("custom", presenter.CurrentTableStyleStrategy)
        Assert.IsTrue(presenter.Style.LeftVertical.Value = LineStyle.Hidden)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertTransitionToDefaultFromGrid()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), GetTableStyleForGrid())
        presenter.CurrentLineStyle = LineStyle.Double

        presenter.LeftChecked = True

        Assert.AreEqual("grid", presenter.CurrentTableStyleStrategy)
        Assert.IsFalse(presenter.Style.LeftVertical.Value = LineStyle.Hidden)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertTransitionToDefaultFromBox()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), GetTableStyleForBox())
        presenter.CurrentLineStyle = LineStyle.Double

        presenter.MidHorizontalChecked = True

        Assert.AreEqual("custom", presenter.CurrentTableStyleStrategy)
        Assert.IsFalse(presenter.Style.LeftVertical.Value = LineStyle.Hidden)
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

    Private Function CreateStyle() As TableStyleDto
        Return TableStyleDto.ColAndRow()
    End Function

End Class
