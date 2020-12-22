
Imports Cito.Tester.Common


<Serializable> _
Public Class TestSectionCollection2
    Inherits List(Of TestSection2)


    Private _parent As TestPart2




    Public Sub New(parent As TestPart2)
        _parent = parent
    End Sub



    Public Overloads Sub Add(section As TestSection2)
        ReflectionHelper.CheckIsNotNothing(section, "Test section")
        MyBase.Add(section)
        section.Parent = _parent

        SetSectionToParentOfItemReferences(section)
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


End Class