
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class DecimalValueTest


    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    <ExpectedException(GetType(NotSupportedException))>
    Public Sub MatchValue_WithStringValue_ExpectsNotSupportedException()
        Dim dv As New DecimalValue
        Dim sv As New StringValue

        dv.IsMatch(sv)

    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MatchValue_WithSameValue_ExpectsMatch()
        Dim dv1 As New DecimalValue(New Decimal(1.2))
        Dim dv2 As New DecimalValue(New Decimal(1.2))

        Dim match As Boolean = dv1.IsMatch(dv2)

        Assert.IsTrue(match)
        Assert.AreEqual(dv1.Value, dv2.Value)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MatchValue_WithOtherValue_ExpectsMatch()
        Dim dv1 As New DecimalValue(New Decimal(1.2))
        Dim dv2 As New DecimalValue(New Decimal(888))

        Dim match As Boolean = dv1.IsMatch(dv2)

        Assert.IsFalse(match)
        Assert.AreNotEqual(dv1.Value, dv2.Value)
    End Sub

End Class
