﻿
Imports Cito.Tester.Common.Controls.Canvas
Imports Cito.Tester.Common.Controls.Canvas.Factory.DrawableShapeFactory

<TestClass()>
Public Class Add_Remove_Behavior

    <TestMethod()> <TestCategory("Controls")>
    Public Sub AddShape_AddEventFired()
        'Arrange
        Dim added, removed As Integer
        Dim canvas As New Canvas
        Dim c As ICanvas = DirectCast(canvas, ICanvas)
        AddHandler c.CollectionChanged, Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                                            Select Case e.Action

                                                Case NotifyCollectionChangedAction.Add
                                                    added += 1
                                                Case NotifyCollectionChangedAction.Remove
                                                    removed += 1
                                                Case Else
                                                    Throw New Exception() 'Not expected!
                                            End Select
                                        End Sub
       
        'Act
        c.AddItem(New DefaultShapeFactory().CreateShape(Of IRectangle)())
        
        'Assert
        Assert.AreEqual(1, added)
        Assert.AreEqual(0, removed)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub AddShapeAndRemove_AddPlusRemoveEventFired()
        'Arrange
        Dim added, removed As Integer
        Dim canvas As New Canvas
        Dim c As ICanvas = DirectCast(canvas, ICanvas)
        AddHandler c.CollectionChanged, Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                                            Select Case e.Action

                                                Case NotifyCollectionChangedAction.Add
                                                    added += 1
                                                Case NotifyCollectionChangedAction.Remove
                                                    removed += 1
                                                Case Else
                                                    Throw New Exception() 'Not expected!
                                            End Select
                                        End Sub
      
        'Act
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        c.AddItem(itm)
        c.RemoveItem(itm)
      
        'Assert
        Assert.AreEqual(1, added)
        Assert.AreEqual(1, removed)
    End Sub

    <TestMethod()> <TestCategory("Controls")>
    Public Sub RemoveNotAddedShape_EventsShouldNotFire()
        'Arrange
        Dim added, removed As Integer
        Dim canvas As New Canvas
        Dim c As ICanvas = DirectCast(canvas, ICanvas)
        AddHandler c.CollectionChanged, Sub(s As Object, e As NotifyCollectionChangedEventArgs)
                                            Select Case e.Action

                                                Case NotifyCollectionChangedAction.Add
                                                    added += 1
                                                Case NotifyCollectionChangedAction.Remove
                                                    removed += 1
                                                Case Else
                                                    Throw New Exception() 'Not expected!
                                            End Select
                                        End Sub
     
        'Act
        Dim itm As IDrawableItem = New DefaultShapeFactory().CreateShape(Of IRectangle)()
        c.RemoveItem(itm)
    
        'Assert
        Assert.AreEqual(0, added)
        Assert.AreEqual(0, removed)
    End Sub

End Class
