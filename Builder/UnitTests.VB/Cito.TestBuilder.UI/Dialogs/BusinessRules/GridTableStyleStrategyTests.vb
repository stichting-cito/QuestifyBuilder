
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public Class GridTableStyleStrategyTests
    Inherits baseTableStyleTest

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleIsApplied()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New GridTableStyleStrategy(presenter)
        presenter.CurrentLineWidth = 10
        presenter.CurrentLineStyle = LineStyle.Dotted
        
        'Act
        styleHanler.CalculateNewStyle()
        
        'Assert
        Assert.IsTrue(BoxEquals(presenter.Style, LineStyle.Dotted, 10))
        Assert.IsTrue(InnerEquals(presenter.Style, LineStyle.Solid, 1))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub TogglesAreSetAfterInit()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New GridTableStyleStrategy(presenter)
       
        'Act
        styleHanler.InitiateState()
        
        'Assert
        Assert.IsTrue(styleHanler.GetBoxStatus(True)) 'Checks if set to true.
        Assert.IsTrue(styleHanler.GetInnerStatus(True)) 'Checks if set to true.
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCanHandleStateAfterInit()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New GridTableStyleStrategy(presenter)
      
        'Act
        styleHanler.InitiateState()
      
        'Assert
        Assert.IsTrue(styleHanler.CanHandleState())
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub StyleHandlerCannotHandleIfNotAllChecksAreOn()
        'Arrange
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), TableStyleDto.ColAndRow())
        Dim styleHanler As New GridTableStyleStrategy(presenter)
     
        'Act
        styleHanler.InitiateState()
        presenter.TopChecked = False
      
        'Assert
        Assert.IsFalse(styleHanler.CanHandleState())
    End Sub

End Class
