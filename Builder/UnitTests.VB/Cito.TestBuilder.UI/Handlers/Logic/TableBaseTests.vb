
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Public Class TableBaseTests

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----|-----+-----+
    '|  5  |  6  |  7  |  8  |
    '+-----|-----|-----+-----+
    '|  9  |  10 |  11 |  12 |
    '+-----+-----+-----+-----+
    Friend Function GetTableSimple(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|     |  2  |  3  |  4  |
    '+  1  +-----+-----+-----+
    '|     |  5  |     |  7  |
    '+-----|-----+  6  +-----+
    '|  8  |  9  |     |  10 |
    '+-----+-----+-----+-----+
    Friend Function GetTableWithRowSpan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan.ToString(), ns)
    End Function

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+  4  |-----+  9  +
    '|     |  7  |     |
    '+     |-----+     +
    '|     |  8  |     |
    '+-----|-----+-----+
    '| 10  |  11 |  12 |
    '+-----+-----+-----+
    Friend Function GetTableWithRowSpan3x6(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x6.ToString(), ns)
    End Function

    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|     |  5  |     |
    '+     |-----+     +
    '|     |  6  |     |
    '+     |-----+     +
    '|  4  |  7  | 10  |
    '+     |-----+     +
    '|     |  8  |     |
    '+     |-----+     +
    '|     |  9  |     |
    '+-----|-----+-----+
    '| 11  |  12 |  13 |
    '+-----+-----+-----+
    Friend Function GetTableWithRowSpan3x7(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataRowspan3x7.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|  1  |  2  |  3  |  4  |
    '+-----|-----+-----+-----+
    '|  5  |           |  7  |
    '+-----|     6     |-----+
    '|  8  |           |  9  |
    '+-----+-----+-----+-----+
    '|  10 |  11 |  12 |  13 |
    '+-----+-----+-----+-----+
    Friend Function GetTableWithRowSpan4x4(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstData4x4.ToString(), ns)
    End Function

    '+-----+-----+-----+-----+
    '|  1  |     2     |  3  |
    '+-----|-----+-----+-----+
    '|  4  |  5  |  6  |  7  |
    '+-----|-----|-----+-----+
    '|  8  |  9  |  10 |  11 |
    '+-----+-----+-----+-----+
    Friend Function GetTableWithColspan(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tstDataColspan.ToString(), ns)
    End Function


    '+----+----+----+----+
    '|  1 |    |    |    |
    '+----+    | [1]|    |
    '|r[3]|    |    |    |
    '+----+  2 +----+  3 |
    '|r[2]|    |    |    |
    '+----+    |r[4]|    |
    '|r[1]|    |    |    |
    '+-------------------+
    '|  4 |    5    |  6 |
    '+-------------------+
    '|  7 |    8    |  9 |
    '+-------------------+
    Friend Function GetTableProblem1(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.problem1.ToString(), ns)
    End Function


    '+-----+-----+-----+
    '|  1  |  2  |  3  |
    '+-----+-----+-----+
    '|           |  5  |
    '+     4     +-----|
    '|           |  6  |
    '+-----+-----+-----+
    Friend Function GetTableProblem2(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.problem2.ToString(), ns)
    End Function

    'Single row with interesting tags.
    '<td/>
    '<td>text</td>
    '<td><p>1</p></td>
    '<td><p>2</p><p>3</p></td>
    '<td><p>4</p>out of p tag<p>5</p></td>
    Friend Function GetTableForCellMerging(ByRef ns As XmlNamespaceManager) As Table
        Return GetTable(TestData.tableMergeData.ToString(), ns)
    End Function

    ' +-----+-----+-----+
    ' |  1  |  2  |  3  |
    ' +-----+-----+-----+
    '
    '+-----
    '|  a |
    '+-----
    '|  b |
    '+-----
    Friend Function GetDocumentWith2Tables_3x1_1x2(ByVal tableNumber As Integer, ByRef ns As XmlNamespaceManager) As Table
        Return GetTable2(TestData.DocumentWith2Tables_3x1_1x2.ToString(), tableNumber, ns)
    End Function

    '+-----
    '|  1 |
    '+-----
    '|  2 |
    '+-----
    '
    '+-----
    '|  a |
    '+-----
    '|  b |
    '+-----
    Friend Function GetDocumentWith2_2x2Tables(ByVal tableNumber As Integer, ByRef ns As XmlNamespaceManager) As Table
        Return GetTable2(TestData.DocumentWith2_2x2Tables.ToString(), tableNumber, ns)
    End Function

    Private Function GetTable(xml As String, ByRef ns As XmlNamespaceManager) As Table
        Dim doc As New XmlDocument() : doc.LoadXml(xml)
        ns = New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim result = Table.GetTableFromNode(doc.SelectSingleNode("//def:td[2]", ns))
        Return result
    End Function

    Private Function GetTable2(xml As String, tableNumber As Integer, ByRef ns As XmlNamespaceManager) As Table
        Dim doc As New XmlDocument() : doc.LoadXml(xml)
        ns = New XmlNamespaceManager(doc.NameTable) : ns.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        Dim query = String.Format("//def:table[{0}]//def:td[1]", tableNumber)
        Dim result = Table.GetTableFromNode(doc.SelectSingleNode(query, ns))
        Return result
    End Function

End Class
