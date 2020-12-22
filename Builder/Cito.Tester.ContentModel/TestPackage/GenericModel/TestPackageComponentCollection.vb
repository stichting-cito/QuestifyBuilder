
Imports Cito.Tester.Common


<Serializable> _
Public Class TestPackageComponentCollection
    Inherits List(Of TestPackageComponent)


    <NonSerialized> _
    Private _testpackageComponentAddedHandlers As List(Of EventHandler(Of TestPackageComponentAddedEventArgs))

    Private _parent As TestPackageNode




    Public Sub New(parent As TestPackageNode)
        MyBase.New()
        _parent = parent
    End Sub

    Public Sub New(collection As IEnumerable(Of TestPackageComponent))
        MyBase.New(collection)
    End Sub




    Protected Sub OnTestPackageComponentAdded(e As TestPackageComponentAddedEventArgs)
        RaiseEvent TestPackageComponentAdded(Me, e)
    End Sub




    Public Overloads Sub Add(test As TestPackageComponent)
        ReflectionHelper.CheckIsNotNothing(test, "TestPackage component")
        MyBase.Add(test)
        test.Parent = _parent

        OnTestPackageComponentAdded(New TestPackageComponentAddedEventArgs(test))
    End Sub


    Public Overloads Function Contains(identifier As String) As Boolean
        For Each child As TestPackageComponent In Me
            If String.Compare(identifier, child.Identifier) = 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Sub Insert(index As Integer, test As TestPackageComponent)
        ReflectionHelper.CheckIsNotNothing(test, "Test component")

        MyBase.Insert(index, test)
        test.Parent = _parent
    End Sub



    Public Custom Event TestPackageComponentAdded As EventHandler(Of TestPackageComponentAddedEventArgs)
        AddHandler(value As EventHandler(Of TestPackageComponentAddedEventArgs))
            If _testpackageComponentAddedHandlers Is Nothing Then
                _testpackageComponentAddedHandlers = New List(Of EventHandler(Of TestPackageComponentAddedEventArgs))
            End If

            _testpackageComponentAddedHandlers.Add(value)
        End AddHandler

        RemoveHandler(value As EventHandler(Of TestPackageComponentAddedEventArgs))
            If _testpackageComponentAddedHandlers Is Nothing Then
                _testpackageComponentAddedHandlers = New List(Of EventHandler(Of TestPackageComponentAddedEventArgs))
            End If

            _testpackageComponentAddedHandlers.Remove(value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As TestPackageComponentAddedEventArgs)
            If _testpackageComponentAddedHandlers IsNot Nothing Then
                For Each handler As EventHandler(Of TestPackageComponentAddedEventArgs) In _testpackageComponentAddedHandlers
                    handler.Invoke(sender, e)
                Next
            End If
        End RaiseEvent
    End Event


End Class