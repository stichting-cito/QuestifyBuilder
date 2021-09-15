
Imports FakeItEasy
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic

<TestClass()>
Public MustInherit Class baseBorderAndShadingPresenterInitTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsNoneBasedOnTableStyle()
        'Arrange
        Dim dto = GetTableStyleForNone()
        
        'Act
        Dim presenter As New  BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)
        
        'Assert
        Assert.AreEqual("none", presenter.CurrentTableStyleStrategy)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsBoxBasedOnTableStyle()
        'Arrange
        Dim dto = GetTableStyleForBox()
        
        'Act
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)
        
        'Assert
        If dto.HasInnser Then 'This test is ran with multiple configurations of the DTO object
            Assert.AreEqual("box", presenter.CurrentTableStyleStrategy)
        Else
            Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
        End If
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsAllBasedOnTableStyle()
        'Arrange
        Dim dto = GetTableStyleForAll()
        
        'Act
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)
        
        'Assert
        Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsGridBasedOnTableStyle()
        'Arrange
        Dim dto = GetTableStyleForGrid()
        
        'Act
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)
        
        'Assert
        If dto.HasInnser Then 'This test is ran with multiple configurations of the DTO object
            Assert.AreEqual("grid", presenter.CurrentTableStyleStrategy)
        Else
            Assert.AreEqual("all", presenter.CurrentTableStyleStrategy)
        End If
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub AssertThatStrategyIsCustomBasedOnTableStyle()
        'Arrange
        Dim dto = GetTableStyleForCustom()
        
        'Act
        Dim presenter As New BorderAndShadingPresenter(A.Fake(Of IBordersAndShadingView), dto)
        
        'Assert
        Assert.AreEqual("custom", presenter.CurrentTableStyleStrategy)
    End Sub

    Private Function GetTableStyleForNone() As TableStyleDto
        Dim dto = CreateStyle()
        dto.Box(LineStyle.Hidden, 1) 'Width of line is of no concern.
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
        dto.TopHorizontal = LineStyle.Dotted 'Clearly set other style

        Return dto
    End Function

    Friend MustOverride Function CreateStyle() As TableStyleDto

End Class
