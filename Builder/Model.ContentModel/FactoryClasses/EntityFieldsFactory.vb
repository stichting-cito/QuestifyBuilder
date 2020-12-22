Imports System
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Collections.Generic
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.FactoryClasses
    Public Class EntityFieldsFactory
        Private Shared ReadOnly _entityTypeNamesCache As New Dictionary(Of Questify.Builder.Model.ContentModel.EntityType, String)()

        Shared Sub New()
            For Each value As Questify.Builder.Model.ContentModel.EntityType In System.Enum.GetValues(GetType(Questify.Builder.Model.ContentModel.EntityType))
                _entityTypeNamesCache.Add(value, System.Enum.GetName(GetType(Questify.Builder.Model.ContentModel.EntityType), value))
            Next
        End Sub

        Private Sub New()
        End Sub

        Public Shared Function CreateEntityFieldsObject(relatedEntityType As Questify.Builder.Model.ContentModel.EntityType) As IEntityFields2
            Return FieldInfoProviderSingleton.GetInstance().GetEntityFields(InheritanceInfoProviderSingleton.GetInstance(), _entityTypeNamesCache(relatedEntityType))
        End Function

        Friend Shared Function CreateFields(entityName As String) As IEntityFieldCore()
            Return FieldInfoProviderSingleton.GetInstance().GetEntityFieldsArray(entityName)
        End Function





    End Class
End Namespace