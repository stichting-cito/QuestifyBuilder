Imports System.Reflection
Imports Cito.Tester.Common

Public MustInherit Class EntityValidationBase(Of TContentModelEntity As ValidatingEntityBase)
    Implements IEntityValidation


    Protected Sub New()
    End Sub



    Public MustOverride ReadOnly Property FriendlyEntityName As String Implements IEntityValidation.FriendlyEntityName




    Protected MustOverride Function ValidateEntityFieldValue(entity As TContentModelEntity, fieldName As String, value As Object) As String




    Public Overridable Function ValidateEntityFieldValue(entity As ValidatingEntityBase, fieldName As String) As String Implements IEntityValidation.ValidateEntityFieldValue
        Dim castedEntity As TContentModelEntity = DirectCast(entity, TContentModelEntity)
        Dim fieldProperty As PropertyInfo = GetType(TContentModelEntity).GetProperty(fieldName)

        If fieldProperty IsNot Nothing Then
            Return ValidateEntityFieldValue(castedEntity, fieldName, fieldProperty.GetValue(castedEntity, Nothing))
        Else
            Throw New ContentModelException(
                $"Field with name '{fieldName}' could not be found on object '{castedEntity.GetType.FullName}' while validating.")
        End If
    End Function


End Class