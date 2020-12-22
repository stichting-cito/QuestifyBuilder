
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass>
Public Class BaseHtmlSplitterTests

    <TestInitialize()> _
    Public Sub MyTestInitialize()
        FailOnAssert.Disable = True
    End Sub

    <TestCleanup()> _
    Public Sub MyTestCleanup()
        FailOnAssert.Disable = False
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Public Sub ConstructorTest_NormalExample()
        Dim doc As New XmlDocument
        doc.LoadXml(<body><p>test</p></body>.ToString())
        Dim htmlSplicer As New BaseHtmlSplitterTestClass(doc.SelectSingleNode("/body/p/text()"))


    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    Public Sub ConstructorTest_NormalExample2()
        Dim doc As New XmlDocument
        doc.LoadXml(<body><p>test</p><p>test2</p></body>.ToString())
        Dim htmlSplicer As New BaseHtmlSplitterTestClass(doc.SelectSingleNode("/body/p/text()"))


    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Inline")>
    <Description("Fails because node is not an XmlText Node")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub ConstructorTest_FailExample()
        Dim doc As New XmlDocument
        doc.LoadXml(<body><div>test</div><div>test2</div></body>.ToString())
        Dim htmlSplicer As New BaseHtmlSplitterTestClass(doc.SelectSingleNode("/body/div"))


        Assert.Fail()
    End Sub

    Friend Class BaseHtmlSplitterTestClass
        Inherits BaseHtmlSplitter

        Public Sub New(selectedNode As XmlNode)
            MyBase.New(selectedNode, 0, selectedNode, 0)
        End Sub

        Public Overrides Function Split() As IEnumerable(Of XmlNode)
            Return Nothing
        End Function
    End Class

End Class
