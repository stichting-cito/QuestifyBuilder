Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Friend Class NumericIdGenerator
        Implements ICollectionParameterNaming
        Private ReadOnly _collectionParameter As CollectionParameter
        Public Sub New(collectionParameter As CollectionParameter)
            _collectionParameter = collectionParameter
        End Sub

        Public Function GetId() As String Implements ICollectionParameterNaming.GetId
            Return _collectionParameter.Value.Count.ToString()
        End Function
    End Class
End Namespace
