
Imports Cito.Tester.Common


<Serializable> _
Public Class TestComponentCollection2
    Inherits List(Of TestComponent2)


    <NonSerialized> _
    Private _testComponentAddedHandlers As List(Of EventHandler(Of TestComponentAddedEventArgs))

    Private _parent As AssessmentTestNode




    Public Sub New(parent As AssessmentTestNode)
        MyBase.New()
        _parent = parent
    End Sub

    Public Sub New(collection As IEnumerable(Of TestComponent2))
        MyBase.New(collection)
    End Sub




    Protected Sub OnTestComponentAdded(e As TestComponentAddedEventArgs)
        RaiseEvent TestComponentAdded(Me, e)
    End Sub



    Public Overloads Sub Add(item As TestComponent2)
        ReflectionHelper.CheckIsNotNothing(item, "Test component")
        If MyBase.Count = 0 AndAlso TypeOf item Is ItemReference2 Then
            DirectCast(item, ItemReference2).FirstItemInSection = True
        End If
        MyBase.Add(item)
        item.Parent = _parent

        If TypeOf item Is TestSection2 Then
            SetSectionToParentOfItemReferences(DirectCast(item, TestSection2))
        End If

        OnTestComponentAdded(New TestComponentAddedEventArgs(item))
    End Sub

    Private Sub SetSectionToParentOfItemReferences(section As TestSection2)
        For Each testComponent As TestComponent2 In section.Components
            If TypeOf testComponent Is TestSection2 Then
                SetSectionToParentOfItemReferences(DirectCast(testComponent, TestSection2))
            ElseIf TypeOf testComponent Is ItemReference2 Then
                Dim itemRef As ItemReference2 = DirectCast(testComponent, ItemReference2)
                If itemRef.Parent Is Nothing Then
                    itemRef.Parent = section
                End If
            End If
        Next
    End Sub

    Public Overloads Function Contains(identifier As String) As Boolean
        For Each child As TestComponent2 In Me
            If String.Compare(identifier, child.Identifier) = 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overloads Sub Insert(index As Integer, item As TestComponent2)
        ReflectionHelper.CheckIsNotNothing(item, "Test component")


        MyBase.Insert(index, item)
        item.Parent = _parent
    End Sub



    Public Custom Event TestComponentAdded As EventHandler(Of TestComponentAddedEventArgs)
        AddHandler(value As EventHandler(Of TestComponentAddedEventArgs))
            If _testComponentAddedHandlers Is Nothing Then
                _testComponentAddedHandlers = New List(Of EventHandler(Of TestComponentAddedEventArgs))
            End If

            _testComponentAddedHandlers.Add(value)
        End AddHandler

        RemoveHandler(value As EventHandler(Of TestComponentAddedEventArgs))
            If _testComponentAddedHandlers Is Nothing Then
                _testComponentAddedHandlers = New List(Of EventHandler(Of TestComponentAddedEventArgs))
            End If

            _testComponentAddedHandlers.Remove(value)
        End RemoveHandler

        RaiseEvent(sender As Object, e As TestComponentAddedEventArgs)
            If _testComponentAddedHandlers IsNot Nothing Then
                For Each handler As EventHandler(Of TestComponentAddedEventArgs) In _testComponentAddedHandlers
                    handler.Invoke(sender, e)
                Next
            End If
        End RaiseEvent
    End Event


End Class