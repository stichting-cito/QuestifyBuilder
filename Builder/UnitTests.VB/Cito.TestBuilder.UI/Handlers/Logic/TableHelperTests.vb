
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableHelperTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFromNode()
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstData.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable)
        ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim node = doc.SelectSingleNode("//def:td[2]", ns)

        Dim result = Table.getTableFromNode(node)

        Assert.IsNotNull(result)
    End Sub

End Class
