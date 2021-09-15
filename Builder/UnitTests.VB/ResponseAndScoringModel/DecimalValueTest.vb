
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class DecimalValueTest


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <ExpectedException(GetType(NotSupportedException))>
    Public Sub MatchValue_WithStringValue_ExpectsNotSupportedException()
        'Arrange
        Dim dv As New DecimalValue
        Dim sv As New StringValue
       
        'Act
        dv.IsMatch(sv)
       
        'Assert
        'Expects Exception
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MatchValue_WithSameValue_ExpectsMatch()
        'Arrange
        Dim dv1 As New DecimalValue(New Decimal(1.2))
        Dim dv2 As New DecimalValue(New Decimal(1.2))
      
        'Act
        Dim match As Boolean = dv1.IsMatch(dv2)
       
        'Assert
        Assert.IsTrue(match)
        Assert.AreEqual(dv1.Value, dv2.Value)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MatchValue_WithOtherValue_ExpectsMatch()
        'Arrange
        Dim dv1 As New DecimalValue(New Decimal(1.2))
        Dim dv2 As New DecimalValue(New Decimal(888))
       
        'Act
        Dim match As Boolean = dv1.IsMatch(dv2)
      
        'Assert
        Assert.IsFalse(match)
        Assert.AreNotEqual(dv1.Value, dv2.Value)
    End Sub

End Class
