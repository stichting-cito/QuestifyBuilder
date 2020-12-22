Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Partial Public Class ConceptStructureCustomBankPropertyEntity

        Public Overrides ReadOnly Property ResourceType() As String
            Get
                Return "Concept Structure"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{Me.Name} - {Me.Title}"
        End Function
    End Class

End Namespace