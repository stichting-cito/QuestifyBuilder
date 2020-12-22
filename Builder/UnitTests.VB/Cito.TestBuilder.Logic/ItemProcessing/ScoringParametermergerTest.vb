
Imports Questify.Builder.Logic.ItemProcessing
Imports Cito.Tester.ContentModel

<TestClass>
Public Class ScoringParametermergerTest

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub default_Merge_Test()
        Dim spMerger = New ScoringParameterHandler
        Dim warnErr As New WarningsAndErrors
        Dim newPrm As New MultiChoiceScoringParameter With {.BluePrint = New ParameterCollection(), .Value = New ParameterSetCollection()}
        Dim currPrm As New MultiChoiceScoringParameter With {.BluePrint = New ParameterCollection(), .Value = New ParameterSetCollection()}

        currPrm.BluePrint.InnerParameters.Add(New XHtmlParameter())
        Dim prmColl As New ParameterCollection() With {.Id = "1"} : prmColl.InnerParameters.Add(New XHtmlParameter())
        currPrm.Value.Add(prmColl)
        prmColl = New ParameterCollection() With {.Id = "2"} : prmColl.InnerParameters.Add(New XHtmlParameter())
        currPrm.Value.Add(prmColl)

        spMerger.Merge(newPrm, currPrm, warnErr)

        Assert.AreEqual(2, newPrm.Value.Count)
    End Sub

End Class
