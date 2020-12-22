Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Partial Public Class TreeStructureCustomBankPropertyEntity

        Public Overrides ReadOnly Property ResourceType() As String
            Get
                Return "Tree Structure"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{Me.Name} - {Me.Title}"
        End Function

    End Class

End Namespace