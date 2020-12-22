
Imports Questify.Builder.Logic

<TestClass()>
Public Class HtmlContentHelperTests





    <TestMethod(), TestCategory("UILogic")>
    Public Sub RemoveContextIdentifier()
        Dim _cntHlp As New HtmlContentHelper
        Dim data = <p><img id="fa0fd20d-f2c1-47ae-8c55-04b0866a675b" src="resource://package:1/bird.gif" alt="" isinlineelement="true"/></p>

        Dim res As String
        res = _cntHlp.RemoveResourceElementContextNumber(data.ToString())

        Assert.IsTrue(res.Contains("resource://package/bird.gif"))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub AddContextIdentifier()
        Dim _cntHlp As New HtmlContentHelper
        Dim data = <p><img id="fa0fd20d-f2c1-47ae-8c55-04b0866a675b" src="resource://package/bird.gif" alt="" isinlineelement="true"/></p>

        Dim res As String
        res = _cntHlp.GiveResourceElementsContextNumber(data.ToString, 1)

        Assert.IsTrue(res.Contains("resource://package:1/bird.gif"))
    End Sub

End Class
