
Imports System.Xml

<TestClass()>
Public Class TableWalker_Tests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableProblem1_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem1(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableProblem2_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem2(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableSimple_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithColspan_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithColspan(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan3x6_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan3x6(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan3x7_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan3x7(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan4x4_WalkTest()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFormDocument_isbalanced()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2_2x2Tables(1, ns)

        Dim result = t.TableIsBalanced()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFormDocument_hasSingleTable_andThus2Rows()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2_2x2Tables(1, ns)

        Dim result = t.GetRowCount

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_FirstTableHas3Columns()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(1, ns)

        Dim result = t.GetColumnCount()

        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_FirstTableHas1Row()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(1, ns)

        Dim result = t.GetColumnCount()

        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_SeccondTableHas1Columns()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(2, ns)

        Dim result = t.GetColumnCount()

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_SeccondTableHas2Rows()
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(2, ns)

        Dim result = t.GetRowCount()

        Assert.AreEqual(2, result)
    End Sub

End Class
