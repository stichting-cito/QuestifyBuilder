Imports System
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.FactoryClasses
    Public Class EntityFieldFactory
        Private Sub New()
        End Sub

        Public Shared Function Create(fieldIndex As System.Enum) As IEntityField2
            Return New EntityField2(FieldInfoProviderSingleton.GetInstance().GetFieldInfo(fieldIndex))
        End Function

        Public Shared Function Create(objectName As String, fieldName As String) As IEntityField2
            Return New EntityField2(FieldInfoProviderSingleton.GetInstance().GetFieldInfo(objectName, fieldName))
        End Function


    End Class
End Namespace
