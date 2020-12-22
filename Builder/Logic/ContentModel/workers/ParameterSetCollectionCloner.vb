Imports Cito.Tester.ContentModel

Namespace ContentModel

    Friend Class ParameterSetCollectionCloner
        Inherits ParameterCollectionClonerBase(Of ParameterSetCollection)

        Public Sub New(parameter As ParameterSetCollection)
            MyBase.New(parameter)
        End Sub

        Protected Overrides Sub DoPostCloneAction(collection As ParameterSetCollection)
            collection.ShouldSort = Parameter.ShouldSort
        End Sub
    End Class

End Namespace

