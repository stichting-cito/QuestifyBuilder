
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public Class AllTableStyleStrategyTests
    Inherits baseTableStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleIsApplied()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New AllStyleStrategy(presenter)
        presenter.CurrentLineWidth = 10
        presenter.CurrentLineStyle = LineStyle.Dotted

        styleHanler.CalculateNewStyle()

        Assert.IsTrue(BoxEquals(presenter.Style, LineStyle.Dotted, 10))
        Assert.IsTrue(InnerEquals(presenter.Style, LineStyle.Dotted, 10))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TogglesAreSetAfterInit()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New AllStyleStrategy(presenter)

        styleHanler.InitiateState()

        Assert.IsTrue(styleHanler.GetBoxStatus(True))
        Assert.IsTrue(styleHanler.GetInnerStatus(True))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCanHandleStateAfterInit()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New AllStyleStrategy(presenter)

        styleHanler.InitiateState()

        Assert.IsTrue(styleHanler.CanHandleState())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCannotHandleIfNotAllChecksAreOn()
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New AllStyleStrategy(presenter)

        styleHanler.InitiateState()
        presenter.TopChecked = False

        Assert.IsFalse(styleHanler.CanHandleState())
    End Sub

End Class
