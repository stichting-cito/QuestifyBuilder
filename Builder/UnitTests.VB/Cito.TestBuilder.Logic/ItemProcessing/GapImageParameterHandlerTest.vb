
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemProcessing

<TestClass()> Public Class GapImageParameterHandlerTest

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub MergeResultHoldsAllPropertyValues()
        Dim currParam As New GapImageParameter() With {.Name = "gip", .MatchMax = 4, .ContentType = GapImageParameter.GapImageParameterContentType.Text, .Width = 20, .Height = 25}
        Dim newParam As New GapImageParameter()
        Dim warnErr As New WarningsAndErrors
        Dim pMerger As New GapImageParameterHandler()

        pMerger.Merge(newParam, currParam, warnErr)

        Assert.AreEqual(currParam.ContentType, newParam.ContentType)
        Assert.AreEqual(currParam.EnteredText, newParam.EnteredText)
        Assert.AreEqual(currParam.Width, newParam.Width)
        Assert.AreEqual(currParam.Height, newParam.Height)
    End Sub

End Class

