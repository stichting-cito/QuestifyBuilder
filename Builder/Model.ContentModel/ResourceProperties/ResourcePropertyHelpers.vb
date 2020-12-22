Imports System.Reflection
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports Questify.Builder.Model.ContentModel.ResourceProperties

Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Public NotInheritable Class ResourcePropertyHelpers

        Public Shared Function GetPropertyValuesFromClassProperty(ByVal resourceEntity As ResourceEntity, ByVal propertyDefinition As ResourceProperties.ResourcePropertyDefinition) As IList(Of Object)
            Dim returnValue As New List(Of Object)
            Dim propertyInfo As PropertyInfo = resourceEntity.GetType().GetProperty(propertyDefinition.Name)

            If (propertyInfo IsNot Nothing) Then
                returnValue.Add(propertyInfo.GetValue(resourceEntity, Nothing))
            End If

            Return returnValue
        End Function

        Public Shared Function GetPropertyValuesFromClassPropertyAsString(ByVal resourceEntity As ResourceEntity, ByVal propertyDefinition As ResourceProperties.ResourcePropertyDefinition) As IList(Of Object)
            Dim returnValue As List(Of Object) = New List(Of Object)
            For Each propValue As Object In GetPropertyValuesFromClassProperty(resourceEntity, propertyDefinition)
                If propValue Is Nothing Then
                    returnValue.Add("")
                Else
                    returnValue.Add(propValue.ToString())
                End If
            Next
            Return returnValue
        End Function

        Public Shared Function GetStateValues(states As HelperClasses.EntityCollection) As IList(Of ResourcePropertyListValueDefinition)
            Dim returnValue As New List(Of ResourcePropertyListValueDefinition)

            For Each state As StateEntity In states
                returnValue.Add(New ResourcePropertyListValueDefinition(GetDeterministicGuid(state.Name), state.StateId.ToString(), state.Title))
            Next

            Return returnValue
        End Function

        Public Shared Function GetPropertyValuesFromCustomProperty(ByVal resourceEntity As ResourceEntity, ByVal propertyDefinition As ResourceProperties.ResourcePropertyDefinition) As IList(Of Object)
            Dim returnValue As New List(Of Object)
            Dim customPropertyValue As EntityClasses.CustomBankPropertyValueEntity = resourceEntity.CustomBankPropertyValueCollection.GetFirstMatch(HelperClasses.CustomBankPropertyValueFields.CustomBankPropertyId = propertyDefinition.Key)

            If customPropertyValue IsNot Nothing Then
                If TypeOf customPropertyValue Is FreeValueCustomBankPropertyValueEntity Then
                    Dim value As EntityClasses.FreeValueCustomBankPropertyValueEntity = DirectCast(customPropertyValue, EntityClasses.FreeValueCustomBankPropertyValueEntity)
                    returnValue.Add(value.Value)
                ElseIf TypeOf customPropertyValue Is ListCustomBankPropertyValueEntity Then
                    Dim values As EntityClasses.ListCustomBankPropertyValueEntity = DirectCast(customPropertyValue, EntityClasses.ListCustomBankPropertyValueEntity)

                    For Each value As ListValueCustomBankPropertyEntity In values.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
                        returnValue.Add(value.Name)
                    Next
                ElseIf TypeOf customPropertyValue Is TreeStructureCustomBankPropertyValueEntity Then
                    Dim values As EntityClasses.TreeStructureCustomBankPropertyValueEntity = DirectCast(customPropertyValue, EntityClasses.TreeStructureCustomBankPropertyValueEntity)
                    For Each value As TreeStructurePartCustomBankPropertyEntity In values.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart
                        returnValue.Add(value.Name)
                    Next
                End If
            End If

            Return returnValue
        End Function

        Public Shared Function ConvertEnumToResourcePropertyListValueDefinitionCollection(ByVal type As Type) As IList(Of ResourcePropertyListValueDefinition)
            Dim result As IList(Of ResourcePropertyListValueDefinition) = New List(Of ResourcePropertyListValueDefinition)()

            For Each enumValue As String In [Enum].GetNames(type)
                result.Add(New ResourcePropertyListValueDefinition(GetDeterministicGuid(enumValue), enumValue, enumValue))
            Next

            Return result
        End Function

        Private Shared Function GetDeterministicGuid(ByVal input As String) As Guid
            Return New Guid(New MD5CryptoServiceProvider().ComputeHash(Text.Encoding.Default.GetBytes(input)))
        End Function

        Public Shared Function CreateBooleanSingleListValueCollection() As IList(Of ResourcePropertyListValueDefinition)
            Dim result As IList(Of ResourcePropertyListValueDefinition) = New List(Of ResourcePropertyListValueDefinition)()

            result.Add(New ResourcePropertyListValueDefinition(GetDeterministicGuid(True.ToString()), True.ToString(), True.ToString()))
            result.Add(New ResourcePropertyListValueDefinition(GetDeterministicGuid(False.ToString()), False.ToString(), False.ToString()))

            Return result
        End Function


    End Class
End Namespace
