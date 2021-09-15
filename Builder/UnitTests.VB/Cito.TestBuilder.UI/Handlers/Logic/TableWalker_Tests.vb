
Imports System.Xml

<TestClass()>
Public Class TableWalker_Tests
    Inherits TableBaseTests

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableProblem1_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem1(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
      
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableProblem2_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableProblem2(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
      
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableSimple_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableSimple(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
     
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithColspan_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithColspan(ns)
       
        'Act
        Dim result = t.TableIsBalanced()
      
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
    
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan3x6_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan3x6(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
     
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan3x7_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan3x7(ns)
     
        'Act
        Dim result = t.TableIsBalanced()
    
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableWithRowSpan4x4_WalkTest()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetTableWithRowSpan4x4(ns)
      
        'Act
        Dim result = t.TableIsBalanced()
     
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFormDocument_isbalanced()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2_2x2Tables(1, ns)
       
        'Act
        Dim result = t.TableIsBalanced()
      
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub GetTableFormDocument_hasSingleTable_andThus2Rows()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2_2x2Tables(1, ns)
    
        'Act
        Dim result = t.GetRowCount
      
        'Assert
        Assert.AreEqual(2, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_FirstTableHas3Columns()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(1, ns)
      
        'Act
        Dim result = t.GetColumnCount()
       
        'Assert
        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_FirstTableHas1Row()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(1, ns)
      
        'Act
        Dim result = t.GetColumnCount()
       
        'Assert
        Assert.AreEqual(3, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_SeccondTableHas1Columns()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(2, ns)
      
        'Act
        Dim result = t.GetColumnCount()
       
        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")> <WorkItem(11013)>
    Public Sub GetDocumentWith2Tables_3x1_1x2_SeccondTableHas2Rows()
        'Arrange
        Dim ns As XmlNamespaceManager = Nothing
        Dim t = MyBase.GetDocumentWith2Tables_3x1_1x2(2, ns)
      
        'Act
        Dim result = t.GetRowCount()
       
        'Assert
        Assert.AreEqual(2, result)
    End Sub

End Class
