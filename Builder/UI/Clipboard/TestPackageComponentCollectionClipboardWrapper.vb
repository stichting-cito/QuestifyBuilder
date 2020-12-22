Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses

<Serializable()> _
Public Class TestPackageComponentCollectionClipboardWrapper
    Inherits ComponentCollectionClipboardWrapperBase



    Protected Overrides Sub TraverseComponents(ByRef Level As Integer, testComponents As System.Collections.IEnumerable, callBack As TraverseComponentsCallback, method As ClipboardHelper.TraversalMethod)
        For Each testPackageComponent As TestPackageNode In testComponents
            callBack.Invoke(testPackageComponent, Level)
        Next
    End Sub





    Public Overrides Sub OrphanTestComponents()
        Dim orphanedTestComponents As List(Of TestNodeBase)

        orphanedTestComponents = BinarySerializationHelper.DeepClone(Of List(Of TestNodeBase))(Me.Components)

        For Each tcb As TestNodeBase In orphanedTestComponents
            Dim tc As TestPackageComponent = TryCast(tcb, TestPackageComponent)
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