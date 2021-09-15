
Imports FakeItEasy
Imports System.Drawing
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableStyleDeterminatorTests
    Inherits baseFakedTable

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleForSingleCell()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpForEachCellSolidBorder(fakeTabel)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0))
     
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert
        Assert.IsFalse(result.HasMidVertical)
        Assert.IsFalse(result.HasMidHorizontal)
        Assert.IsTrue(result.BoxIsSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Once)
    End Sub


    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_2Cols1Row()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpForEachCellSolidBorder(fakeTabel)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 1))
     
        'Act
        Dim result = determinator.DetermineStyle()
      
        'Assert
        Assert.IsTrue(result.HasMidVertical)
        Assert.IsFalse(result.HasMidHorizontal)
        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsTrue(result.BoxAndInnerSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(2))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_1Cols2Row()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpForEachCellSolidBorder(fakeTabel)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 1, 2))
     
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert
        Assert.IsFalse(result.HasMidVertical)
        Assert.IsTrue(result.HasMidHorizontal)
        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsTrue(result.BoxAndInnerSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(2))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_2Cols2Row()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpForEachCellSolidBorder(fakeTabel)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 2))
      
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert
        Assert.IsTrue(result.HasMidVertical)
        Assert.IsTrue(result.HasMidHorizontal)
        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsTrue(result.BoxAndInnerSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(4))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_1Cols2Row_MidHorizontal_ShouldHaveUndeterminedStyle()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpEachSideIsOverridable(fakeTabel, Sub(style, x, y)
                                                  If (y = 0) Then style.BorderBottom = BlackDashed1px()
                                              End Sub)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 1, 2))
      
        'Act
        Dim result = determinator.DetermineStyle()
      
        'Assert
        Assert.IsFalse(result.HasMidVertical)
        Assert.IsTrue(result.HasMidHorizontal)
        Assert.IsNull(result.MidHorizontal) 'Multiple styles detected this nothing!
        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsFalse(result.BoxAndInnerSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(2))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_2Cols1Row_MidVertical_ShouldHaveUndeterminedStyle()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpEachSideIsOverridable(fakeTabel, Sub(style, x, y)
                                                  If (x = 0) Then style.BorderRight = BlackDashed1px()
                                              End Sub)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 1))
      
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert
        Assert.IsTrue(result.HasMidVertical)
        Assert.IsNull(result.MidHorizontal) 'Multiple styles detected this nothing!
        Assert.IsFalse(result.HasMidHorizontal)
        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsFalse(result.BoxAndInnerSameStyle())
        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(2))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_2Cols2Row_MidVertical_ShouldHaveUndeterminedStyle()
        'Arrange
        Dim fakeTabel = GetFakeTable()
        SetUpEachSideIsOverridable(fakeTabel, Sub(style, x, y)
                                                  If (x = 0 AndAlso y = 0) Then style.BorderRight = BlackDashed1px()
                                                  If (x = 1 AndAlso y = 1) Then style.BorderLeft = BlackDashed1px()
                                              End Sub)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 2))
      
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert
        Assert.IsTrue(result.HasMidVertical)
        Assert.IsTrue(result.HasMidHorizontal)

        Assert.IsNull(result.MidVertical)
        Assert.IsNotNull(result.MidHorizontal)

        Assert.IsTrue(result.BoxIsSameStyle())
        Assert.IsFalse(result.BoxAndInnerSameStyle())

        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(4))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_GetColorWhenTableHasMoreThanOneColor()
        'Arrange
        Dim fakeTabel = GetFakeTable() : Dim tel As Integer = 1
        SetUpEachSideIsOverridable(fakeTabel, Sub(style, x, y)
                                                  tel += 1
                                                  style.Background_color = If(tel Mod 2 = 0, Color.Red, Color.Blue)
                                              End Sub)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 2))
     
        'Act
        Dim result = determinator.DetermineStyle()
     
        'Assert    
        Assert.IsNull(result.BackColor)

        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(4))
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetStyleFor_GetColorWhenTableSingleColor()
        'Arrange
        Dim fakeTabel = GetFakeTable() : Dim tel As Integer = 1
        SetUpEachSideIsOverridable(fakeTabel, Sub(style, x, y)
                                                  tel += 1
                                                  style.Background_color = If(tel Mod 2 = 0, Color.Red, Color.Red)
                                              End Sub)
        Dim determinator As New TableStyleDeterminator(fakeTabel, New TableBounds(0, 0, 2, 2))
    
        'Act
        Dim result = determinator.DetermineStyle()
    
        'Assert    
        Assert.IsNotNull(result.BackColor)
        Assert.AreEqual(Color.Red.R, result.BackColor.Value.R)
        Assert.AreEqual(Color.Red.G, result.BackColor.Value.G)
        Assert.AreEqual(Color.Red.B, result.BackColor.Value.B)

        A.CallTo(Function() fakeTabel.GetCellByCoords(A(Of Integer).Ignored, A(Of Integer).Ignored)).MustHaveHappened(Repeated.Exactly.Times(4))
    End Sub

End Class
