Public Class TestComponentViewBaseCollection
    Inherits ValidatingEntityCollectionBase(Of TestComponentViewBase)


    Private _parent As TestComponentBase
    Private _testComponentCollection As TestComponentCollection2



    Public Sub New(testComponentCollection As TestComponentCollection2, parent As TestComponentBase)
        _testComponentCollection = testComponentCollection
        _parent = parent

        AddHandler testComponentCollection.TestComponentAdded, AddressOf TestComponentCollection_TestComponentAdded
    End Sub



    Private Sub TestComponentCollection_TestComponentAdded(sender As Object, e As TestComponentAddedEventArgs)





    End Sub



    Public Shadows Sub Add(componentToAdd As TestComponentViewBase)
        MyBase.Add(componentToAdd)

        If Not _testComponentCollection.Contains(componentToAdd.TestComponentModel) Then
            _testComponentCollection.Add(componentToAdd.TestComponentModel)
        End If

        componentToAdd.Parent = _parent
    End Sub

    Public Shadows Sub AddRange(collection As IEnumerable(Of TestComponentViewBase))
        For Each part As TestComponentViewBase In collection
            Me.Add(part)
        Next
    End Sub


    Public Function GetTestComponentByModel(model As TestComponent2) As TestComponentViewBase

        For Each c As TestComponentViewBase In Me
            If c.TestComponentModel.Equals(model) Then
                return c
            End If
        Next

        Return Nothing
    End Function

    Public Shadows Sub Remove(componentToRemove As TestComponentViewBase)
        MyBase.Remove(componentToRemove)

        If _testComponentCollection.Contains(componentToRemove.TestComponentModel) Then
            _testComponentCollection.Remove(componentToRemove.TestComponentModel)
        End If
    End Sub

    Public Shadows Sub RemoveAt(index As Integer)
        Dim comp As TestComponentViewBase = Me(index)

        If comp IsNot Nothing Then
            Me.Remove(comp)
        End If
    End Sub


End Class
