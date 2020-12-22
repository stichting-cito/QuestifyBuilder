Namespace ItemEditor
    Public Interface IHasGroups
        ReadOnly Property Groups() As IEnumerable(Of IGroup)

        Function AddGroup() As IGroup
    End Interface
End Namespace
