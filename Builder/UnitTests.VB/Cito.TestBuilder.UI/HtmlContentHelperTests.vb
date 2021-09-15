
Imports Questify.Builder.Logic

<TestClass()>
Public Class HtmlContentHelperTests

    'What is a ContextIdentifier?

    'Assume Bank (a) and Bank (b).
    'Noth have a picture called bird.jpg, but the images are not the same!
    'When an item is shown with bird.jpg embedded in the html,
    'the Asynchronous Application Protocol (APP) will receive an 
    'image url like: 'resource://package/bird.jpg'. To make a difference
    'we add a number so the APP can decide what bank (A or B) to use.

    'Url's will be rewritten to : 'resource://package:1/bird.jpg' and
    'resource://package:2/bird.jpg'

    'Note the :1 and :2! The ContextIdentifier needs to be removed, it should
    'not be stored in the DB.
    
    <TestMethod(), TestCategory("UILogic")>
    Public Sub RemoveContextIdentifier()
        'Arrange
        Dim _cntHlp As New HtmlContentHelper
        Dim data = <p><img id="fa0fd20d-f2c1-47ae-8c55-04b0866a675b" src="resource://package:1/bird.gif" alt="" isinlineelement="true"/></p>
       
        'Act
        Dim res As String
        res = _cntHlp.RemoveResourceElementContextNumber(data.ToString())
       
        'Assert
        Assert.IsTrue(res.Contains("resource://package/bird.gif"))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub AddContextIdentifier()
        'Arrange
        Dim _cntHlp As New HtmlContentHelper
        Dim data = <p><img id="fa0fd20d-f2c1-47ae-8c55-04b0866a675b" src="resource://package/bird.gif" alt="" isinlineelement="true"/></p>
       
        'Act
        Dim res As String
        res = _cntHlp.GiveResourceElementsContextNumber(data.ToString, 1)
       
        'Assert
        Assert.IsTrue(res.Contains("resource://package:1/bird.gif"))
    End Sub

End Class
