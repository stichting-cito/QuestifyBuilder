Imports Cito.Tester.ContentModel

Namespace ContentModel

    Friend Class ParameterCollectionCloner
        Inherits ParameterCollectionClonerBase(Of ParameterCollection)


        Public Sub New(parameter As ParameterCollection)
            MyBase.New(parameter)
        End Sub


        Protected Overrides Sub DoPostCloneAction(collection As ParameterCollection)
        End Sub

    End Class

End Namespace

