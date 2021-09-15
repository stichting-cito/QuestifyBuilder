﻿
Imports Cito.Tester.ContentModel
Imports System.Drawing

<TestClass()>
Public Class ShapeListTest

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub testAddRemoveOfShapesWithDifferentInstances()
        'Arrange
        Dim lst As New ShapeList()
        'Same shape new instance!
        Dim shape1 As New CircleShape() With {.AnchorPoint = New Point(1, 2), .Radius = 11}
        Dim shape2 As New CircleShape() With {.AnchorPoint = New Point(1, 2), .Radius = 11}
       
        'Act
        lst.Add(shape1)
        lst.Remove(shape2)
       
        'Assert
        Assert.AreEqual(0, lst.Count)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub testAddRemoveOfShapesWithSameInstance()
        'Arrange
        Dim lst As New ShapeList()
        'Same shape new instance!
        Dim shape1 As New CircleShape() With {.AnchorPoint = New Point(1, 2), .Radius = 11}
       
        'Act
        lst.Add(shape1)
        lst.Remove(shape1)
      
        'Assert
        Assert.AreEqual(0, lst.Count)
    End Sub

End Class
