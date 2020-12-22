Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses

<Serializable()> _
Public Class TestComponentCollectionClipboardWrapper
    Inherits ComponentCollectionClipboardWrapperBase


    Protected Overrides Sub TraverseComponents(ByRef Level As Integer, testComponents As System.Collections.IEnumerable, callBack As TraverseComponentsCallback, method As ClipboardHelper.TraversalMethod)
        Select Case method

            Case ClipboardHelper.TraversalMethod.BreadthFirst
                For Each testComponent As AssessmentTestNode In testComponents
                    callBack.Invoke(testComponent, Level)
                Next

                For Each testComponent As AssessmentTestNode In testComponents
                    If TypeOf testComponent Is TestPart2 Then
                        Dim part As TestPart2 = DirectCast(testComponent, TestPart2)
                        TraverseComponents(Level + 1, part.Sections, callBack, method)

                    ElseIf TypeOf testComponent Is TestSection2 Then
                        Dim section As TestSection2 = DirectCast(testComponent, TestSection2)
                        TraverseComponents(Level + 1, section.Components, callBack, method)
                    End If
                Next

            Case ClipboardHelper.TraversalMethod.DepthFirst
                For Each testComponent As AssessmentTestNode In testComponents
                    callBack.Invoke(testComponent, Level)

                    If TypeOf testComponent Is TestPart2 Then
                        Dim part As TestPart2 = DirectCast(testComponent, TestPart2)
                        TraverseComponents(Level + 1, part.Sections, callBack, method)

                    ElseIf TypeOf testComponent Is TestSection2 Then
                        Dim section As TestSection2 = DirectCast(testComponent, TestSection2)
                        TraverseComponents(Level + 1, section.Components, callBack, method)
                    End If
                Next
        End Select
    End Sub


    Public Overrides Sub OrphanTestComponents()
        Dim orphanedTestComponents As List(Of TestNodeBase)

        orphanedTestComponents = BinarySerializationHelper.DeepClone(Of List(Of TestNodeBase))(Me.Components)

        For Each tcb As TestNodeBase In orphanedTestComponents
            Dim tc As TestComponent2 = TryCast(tcb, TestComponent2)
            If tc IsNot Nothing Then
                tc.Parent = Nothing
            End If
        Next

        Me.Components.Clear()
        Me.Components.AddRange(orphanedTestComponents)
    End Sub

    Public Overrides Sub TraverseComponents(ByVal callBack As TraverseComponentsCallback, ByVal method As ClipboardHelper.TraversalMethod)
        Dim level As Integer = 0
        TraverseComponents(level, Me.Components, callBack, method)
    End Sub


End Class