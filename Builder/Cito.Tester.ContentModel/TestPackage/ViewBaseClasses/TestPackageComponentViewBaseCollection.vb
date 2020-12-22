Public Class TestPackageComponentViewBaseCollection
    Inherits ValidatingEntityCollectionBase(Of TestPackageComponentViewBase)


    Private _parent As TestPackageComponentBase
    Private _testPackageComponentCollection As TestPackageComponentCollection



    Public Sub New(testpackageComponentCollection As TestPackageComponentCollection, parent As TestPackageComponentBase)
        _testPackageComponentCollection = testpackageComponentCollection
        _parent = parent

        AddHandler testpackageComponentCollection.TestPackageComponentAdded, AddressOf TestPackageComponentCollection_TestPackageComponentAdded
    End Sub



    Private Sub TestPackageComponentCollection_TestPackageComponentAdded(sender As Object, e As TestPackageComponentAddedEventArgs)



    End Sub



    Public Shadows Sub Add(componentToAdd As TestPackageComponentViewBase)
        MyBase.Add(componentToAdd)

        If Not _testPackageComponentCollection.Contains(componentToAdd.TestPackageComponentModel) Then
            _testPackageComponentCollection.Add(componentToAdd.TestPackageComponentModel)
        End If

        componentToAdd.Parent = _parent
    End Sub

    Public Shadows Sub AddRange(collection As IEnumerable(Of TestPackageComponentViewBase))
        For Each part As TestPackageComponentViewBase In collection
            Me.Add(part)
        Next
    End Sub


    Public Function GetTestPackageComponentByModel(model As TestPackageComponent) As TestPackageComponentViewBase

        For Each c As TestPackageComponentViewBase In Me
            If c.TestPackageComponentModel.Equals(model) Then
                return c
            End If
        Next

        Return Nothing
    End Function

    Public Shadows Sub Remove(componentToRemove As TestPackageComponentViewBase)
        MyBase.Remove(componentToRemove)

        If _testPackageComponentCollection.Contains(componentToRemove.TestPackageComponentModel) Then
            _testPackageComponentCollection.Remove(componentToRemove.TestPackageComponentModel)
        End If
    End Sub

    Public Shadows Sub RemoveAt(index As Integer)
        Dim comp As TestPackageComponentViewBase = Me(index)

        If comp IsNot Nothing Then
            Me.Remove(comp)
        End If
    End Sub


End Class
