Imports Questify.Builder.Model.ContentModel.Interfaces
Namespace Versioning.Retrieval
    Public MustInherit Class MetaDataRetrieverBase(Of T)

        Private ReadOnly _propertyEntity As IPropertyEntity

        Public Sub New(ByVal propertyEntity As IPropertyEntity)
            _propertyEntity = propertyEntity
        End Sub

        Public MustOverride Function CreateMetaData() As T

        Protected ReadOnly Property PropertyEntity As IPropertyEntity
            Get
                Return _propertyEntity
            End Get
        End Property

    End Class
End Namespace