Imports Questify.Builder.Model.ContentModel.Interfaces
Imports System.Collections.Generic
Namespace Versioning.Retrieval
    Public Class PropertyEntityRetriever
        Inherits MetaDataRetrieverBase(Of List(Of PropertyEntityMetaData))

        Private _userName As String

        Public Sub New(ByVal propertyEntity As IPropertyEntity, ByVal userName As String)
            MyBase.New(propertyEntity)
            _userName = userName
        End Sub

        Public Overrides Function CreateMetaData() As List(Of PropertyEntityMetaData)
            Dim result As New List(Of PropertyEntityMetaData)()
            result.Add(New PropertyEntityMetaData(Guid.NewGuid(), "Code", PropertyEntity.Name, Nothing))
            result.Add(New PropertyEntityMetaData(Guid.NewGuid(), "Title", PropertyEntity.Title, Nothing))
            result.Add(New PropertyEntityMetaData(Guid.NewGuid(), "Description", PropertyEntity.Description, Nothing))
            result.Add(New PropertyEntityMetaData(Guid.NewGuid(), "StateName", PropertyEntity.StateName, Nothing))
            result.Add(New PropertyEntityMetaData(Guid.NewGuid(), "ModifiedBy", _userName, Nothing))

            Return result
        End Function
    End Class
End Namespace