Imports Questify.Builder.Logic.Annotations
Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Public NotInheritable Class CollectionParameterNamingFactory
        Private Sub New()
        End Sub
        Private Shared ReadOnly Creator As Dictionary(Of String, Func(Of String, CollectionParameter, ICollectionParameterNaming))

        Shared Sub New()
            Creator = New Dictionary(Of String, Func(Of String, CollectionParameter, ICollectionParameterNaming))() From {
                {"Alphabetic", Function(id, param) New AlphabeticIdGenerator(param)},
                {"Numeric", Function(id, param) New NumericIdGenerator(param)}
            }
        End Sub

        <CanBeNull> _
        Public Shared Function Create(subSetIdentifierStrategyName As String, parameter As CollectionParameter) As ICollectionParameterNaming
            Dim factory As Func(Of String, CollectionParameter, ICollectionParameterNaming)
            If Creator.TryGetValue(subSetIdentifierStrategyName, factory) Then
                Return factory(subSetIdentifierStrategyName, parameter)
            End If

            Debug.Assert(False)
            Return Nothing
        End Function

    End Class
End Namespace
