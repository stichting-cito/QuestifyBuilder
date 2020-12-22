Imports Cito.Tester.ContentModel

<Serializable()> _
Public MustInherit Class ComponentCollectionClipboardWrapperBase

    Private _components As New List(Of TestNodeBase)


    Protected MustOverride Sub TraverseComponents(ByRef Level As Integer, testComponents As System.Collections.IEnumerable, callBack As TraverseComponentsCallback, method As ClipboardHelper.TraversalMethod)

    Public ReadOnly Property Components() As List(Of TestNodeBase)
        Get
            Return _components
        End Get
    End Property




    Public MustOverride Sub OrphanTestComponents()

    Public MustOverride Sub TraverseComponents(ByVal callBack As TraverseComponentsCallback, ByVal method As ClipboardHelper.TraversalMethod)



    Delegate Sub TraverseComponentsCallback(ByVal component As TestNodeBase, ByRef level As Integer)


End Class
