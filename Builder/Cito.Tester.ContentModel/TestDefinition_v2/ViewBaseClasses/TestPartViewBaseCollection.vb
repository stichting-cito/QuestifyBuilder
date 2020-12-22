Public Class TestPartViewBaseCollection
    Inherits ValidatingEntityCollectionBase(Of TestPartViewBase)

    Private _testPartModelCollection As TestPartCollection2

    Public Sub New(testPartModelCollection As TestPartCollection2)
        _testPartModelCollection = testPartModelCollection
    End Sub

    Public Shadows Sub Add(componentToAdd As TestPartViewBase)
        MyBase.Add(componentToAdd)

        If Not _testPartModelCollection.Contains(componentToAdd.TestPartModel) Then
            _testPartModelCollection.Add(componentToAdd.TestPartModel)
        End If
    End Sub

    Public Shadows Sub Remove(componentToRemove As TestPartViewBase)
        MyBase.Remove(componentToRemove)

        If _testPartModelCollection.Contains(componentToRemove.TestPartModel) Then
            _testPartModelCollection.Remove(componentToRemove.TestPartModel)
        End If
    End Sub

    Public Shadows Sub AddRange(collection As IEnumerable(Of TestPartViewBase))
        For Each part As TestPartViewBase In collection
            Me.Add(part)
        Next
    End Sub

    Public Shadows Sub RemoveAt(index As Integer)
        Dim comp As TestPartViewBase = Me(index)

        If comp IsNot Nothing Then
            Me.Remove(comp)
        End If
    End Sub

End Class
