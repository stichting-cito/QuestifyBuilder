Public Class TestSectionViewBaseCollection
    Inherits ValidatingEntityCollectionBase(Of TestSectionViewBase)

    Private _testSectionModelCollection As TestSectionCollection2
    Private _parent As TestPartViewBase

    Public Sub New(testSectionModelCollection As TestSectionCollection2, parent As TestPartViewBase)
        _testSectionModelCollection = testSectionModelCollection
        _parent = parent
    End Sub

    Public Shadows Sub Add(componentToAdd As TestSectionViewBase)
        MyBase.Add(componentToAdd)

        If Not _testSectionModelCollection.Contains(componentToAdd.TestSectionModel) Then
            _testSectionModelCollection.Add(componentToAdd.TestSectionModel)
        End If

        componentToAdd.Parent = _parent
    End Sub

    Public Shadows Sub Remove(componentToRemove As TestSectionViewBase)
        MyBase.Remove(componentToRemove)

        If _testSectionModelCollection.Contains(componentToRemove.TestSectionModel) Then
            _testSectionModelCollection.Remove(componentToRemove.TestSectionModel)
        End If
    End Sub

    Public Shadows Sub AddRange(collection As IEnumerable(Of TestSectionViewBase))
        For Each part As TestSectionViewBase In collection
            Me.Add(part)
        Next
    End Sub

    Public Shadows Sub RemoveAt(index As Integer)
        Dim comp As TestSectionViewBase = Me(index)

        If comp IsNot Nothing Then
            Me.Remove(comp)
        End If
    End Sub

End Class
