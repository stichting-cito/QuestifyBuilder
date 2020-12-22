Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Cloning
    Public Class ItemResourceCloner
        Inherits BaseResourceCloner

        Sub New(item As ResourceEntity)
            MyBase.New(item)
        End Sub

        Public Overrides Sub DoSpecificClone(originalResource As ResourceEntity)
            Dim item As ItemResourceEntity = DirectCast(Resource, ItemResourceEntity)
            Dim originalItem As ItemResourceEntity = DirectCast(originalResource, ItemResourceEntity)
            item.IsSystemItem = originalItem.IsSystemItem
            item.AlternativesCount = originalItem.AlternativesCount
            item.KeyValues = originalItem.KeyValues
            item.ResponseCount = originalItem.ResponseCount
            item.RawScore = originalItem.RawScore
            item.TesterSchemaVersion = originalItem.TesterSchemaVersion
            item.MaxScore = originalItem.MaxScore
            item.Iltname = originalItem.Iltname
            item.Iltversion = originalItem.Iltversion

        End Sub
    End Class
End Namespace
