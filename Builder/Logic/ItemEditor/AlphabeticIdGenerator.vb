Imports Cito.Tester.ContentModel
Namespace ItemEditor
    Friend Class AlphabeticIdGenerator
        Implements ICollectionParameterNaming
        Private ReadOnly _collectionParameter As CollectionParameter
        Public Sub New(collectionParameter As CollectionParameter)
            _collectionParameter = collectionParameter
        End Sub

        Public Function GetId() As String Implements ICollectionParameterNaming.GetId
            Return String.Empty
        End Function

    End Class
End Namespace
