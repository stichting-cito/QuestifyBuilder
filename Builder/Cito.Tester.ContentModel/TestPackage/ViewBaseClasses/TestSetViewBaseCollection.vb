Public Class TestSetViewBaseCollection
    Inherits ValidatingEntityCollectionBase(Of TestSetViewBase)

    Private _testSetModelCollection As TestsetCollection


    Public Sub New(testSetModelCollection As TestsetCollection)
        _testSetModelCollection = testSetModelCollection
    End Sub


    Public Shadows Sub Add(componentToAdd As TestSetViewBase)
        MyBase.Add(componentToAdd)

        If Not _testSetModelCollection.Contains(componentToAdd.TestSetModel) Then
            _testSetModelCollection.Add(componentToAdd.TestSetModel)
        End If
    End Sub


    Public Shadows Sub Remove(componentToRemove As TestSetViewBase)
        MyBase.Remove(componentToRemove)

        If _testSetModelCollection.Contains(componentToRemove.TestSetModel) Then
            _testSetModelCollection.Remove(componentToRemove.TestSetModel)
        End If
    End Sub


    Public Shadows Sub AddRange(collection As IEnumerable(Of TestSetViewBase))
        For Each part As TestSetViewBase In collection
            Me.Add(part)
        Next
    End Sub

    Public Shadows Sub RemoveAt(index As Integer)
        Dim comp As TestSetViewBase = Me(index)

        If comp IsNot Nothing Then
            Me.Remove(comp)
        End If
    End Sub

End Class
