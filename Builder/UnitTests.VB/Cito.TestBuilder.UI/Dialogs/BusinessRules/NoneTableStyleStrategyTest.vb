
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public Class NoneTableStyleStrategyTest
    Inherits baseTableStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleIsApplied()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        presenter.Style.Inner(LineStyle.Double, 1)
        Dim styleHanler As New NoneTableStyleStrategy(presenter)
        presenter.CurrentLineWidth = 10
        presenter.CurrentLineStyle = LineStyle.Dotted
        
        'Act
        styleHanler.CalculateNewStyle()
       
        'Assert
        Assert.IsTrue(BoxEquals(presenter.Style, LineStyle.Hidden, 0))
        Assert.IsTrue(InnerEquals(presenter.Style, LineStyle.Hidden, 0))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TogglesAreSetAfterInit()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New NoneTableStyleStrategy(presenter)
     
        'Act
        styleHanler.InitiateState()
     
        'Assert
        Assert.IsTrue(styleHanler.GetBoxStatus(False)) 'Checks if set to false.
        Assert.IsTrue(styleHanler.GetInnerStatus(False)) 'Checks if set to false.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCanHandleStateAfterInit()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New NoneTableStyleStrategy(presenter)
       
        'Act
        styleHanler.InitiateState()
      
        'Assert
        Assert.IsTrue(styleHanler.CanHandleState())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCannotHandleIfNotAllChecksAreOn()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New NoneTableStyleStrategy(presenter)
       
        'Act
        styleHanler.InitiateState()
        presenter.TopChecked = True
      
        'Assert
        Assert.IsFalse(styleHanler.CanHandleState())
    End Sub

End Class
