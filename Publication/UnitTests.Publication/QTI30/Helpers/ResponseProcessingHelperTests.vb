
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI30.Helpers
    <TestClass>
    Public Class ResponseProcessingHelperTests
        <TestMethod>
        Public Sub MergeResponseProcessing_NoValues_ReturnsNothing()
            Dim valueFirst As ResponseProcessingType = Nothing
            Dim valueSecond As ResponseProcessingType = Nothing

            Dim result = ResponseProcessingHelper.MergeResponseProcessing(valueFirst, valueSecond)

            Assert.IsNull(result)
        End Sub

        <TestMethod>
        Public Sub MergeResponseProcessing_OnlyFirstValue_ReturnsFirstValue()
            Dim valueFirst = New ResponseProcessingType()
            valueFirst.Items = {New ResponseConditionType()}
            Dim valueSecond As ResponseProcessingType = Nothing

            Dim result = ResponseProcessingHelper.MergeResponseProcessing(valueFirst, valueSecond)

            Assert.IsNotNull(result)
            Assert.AreEqual(valueFirst, result)
        End Sub

        <TestMethod>
        Public Sub MergeResponseProcessing_OnlySecondValue_ReturnsSecondValue()
            Dim valueFirst As ResponseProcessingType = Nothing
            Dim valueSecond = New ResponseProcessingType()
            valueSecond.Items = {New ResponseConditionType()}

            Dim result = ResponseProcessingHelper.MergeResponseProcessing(valueFirst, valueSecond)

            Assert.IsNotNull(result)
            Assert.AreEqual(valueSecond, result)
        End Sub

        <TestMethod>
        Public Sub MergeResponseProcessing_BothValues_ReturnsMerged()
            Dim valueFirst = New ResponseProcessingType()
            valueFirst.Items = {New ResponseConditionType()}
            Dim valueSecond = New ResponseProcessingType()
            valueSecond.Items = {New ResponseConditionType()}

            Dim result = ResponseProcessingHelper.MergeResponseProcessing(valueFirst, valueSecond)

            Assert.IsNotNull(result)
            Assert.AreEqual(2, result.Items.Count)
        End Sub
    End Class
End Namespace