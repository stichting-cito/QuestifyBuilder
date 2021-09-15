
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class TableHelperTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFromNode()
        'Arrange
        '+-----+-----+-----+-----+
        '|  1  |  2  |  3  |  4  |
        '+-----|-----|-----+-----+
        '|  5  |  6  |  7  |  8  |
        '+-----|-----|-----+-----+
        '|  9  |  10 |  11 |  12 |
        '+-----+-----+-----+-----+
        Dim doc As New XmlDocument() : doc.LoadXml(TestData.tstData.ToString())
        Dim ns As New XmlNamespaceManager(doc.NameTable)
        ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim node = doc.SelectSingleNode("//def:td[2]", ns) 'A node.
     
        'Act
        Dim result = Table.getTableFromNode(node)
     
        'Assert
        Assert.IsNotNull(result)
    End Sub

End Class
