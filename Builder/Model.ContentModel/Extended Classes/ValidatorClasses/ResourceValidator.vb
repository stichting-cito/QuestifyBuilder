Imports Questify.Builder.Model.ContentModel.DatabaseSpecific
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Text.RegularExpressions
Imports Extended_Classes.HelperClasses

Namespace Questify.Builder.Model.ContentModel.ValidatorClasses

    Partial Public Class ResourceValidator

        Private Shared ReadOnly _illegalCharactersNameField As String = "[<>""#%\{}\|\\/\^~\[\]';\?:@=&\^\$\+\(\)!,`\*]"

        Public Shared Function ReplaceIllegalCharacters(ByVal stringToBeReplaced As String) As String
            Return Regex.Replace(stringToBeReplaced, _illegalCharactersNameField, "_", RegexOptions.None)
        End Function

        Public Overrides Function ValidateFieldValue(ByVal involvedEntity As IEntityCore, ByVal fieldIndex As Integer, ByVal value As Object) As Boolean
            Dim toReturn As Boolean = True
            Select Case CType(fieldIndex, ResourceFieldIndex)
                Case ResourceFieldIndex.Name
                    ValidateNameField(involvedEntity, value)
                Case Else
                    toReturn = True
            End Select
            Return toReturn
        End Function

        Public Shared Function ValidateNameField(ByVal involvedEntity As IEntityCore, ByVal value As Object) As Boolean
            Dim toReturn As Boolean = True
            If Not CType(value, String).Length > 0 Then
                toReturn = False
                Dim errorString As String = My.Resources.CodeIsRequired
                involvedEntity.SetEntityFieldError("Name", errorString, False)
                involvedEntity.SetEntityError(errorString)
            ElseIf CType(value, String).Trim().Length = 0 Then
                toReturn = False
                Dim errorString As String = My.Resources.CodeHasOnlySpaces
                involvedEntity.SetEntityFieldError("Name", errorString, False)
                involvedEntity.SetEntityError(errorString)
            ElseIf CType(value, String).Replace(" ", "").Contains("..") Then
                toReturn = False
                Dim errorString As String = My.Resources.CodeHasMultiplePeriodsInARow
                involvedEntity.SetEntityFieldError("Name", errorString, False)
                involvedEntity.SetEntityError(errorString)
            Else
                Dim illegalChars As String = _illegalCharactersNameField
                If Regex.IsMatch(CType(value, String), _illegalCharactersNameField, RegexOptions.None) Then
                    toReturn = False
                    Dim errorString As String = String.Format(My.Resources.CodeHasIllegalChars, illegalChars)
                    involvedEntity.SetEntityFieldError("Name", errorString, False)
                    involvedEntity.SetEntityError(errorString)
                End If
            End If
            Return toReturn
        End Function

        Public Overrides Sub ValidateEntityBeforeDelete(ByVal involvedEntity As SD.LLBLGen.Pro.ORMSupportClasses.IEntityCore)
            Dim adapter As New DataAccessAdapter

            Dim involvedResourceEntity As EntityClasses.ResourceEntity = DirectCast(involvedEntity, EntityClasses.ResourceEntity)

            Dim dependentResources As New HelperClasses.EntityCollection(New FactoryClasses.DependentResourceEntityFactory)

            Dim filter As IRelationPredicateBucket = New RelationPredicateBucket()
            filter.PredicateExpression.Add(HelperClasses.DependentResourceFields.DependentResourceId = involvedResourceEntity.ResourceId)

            Dim testPrefetchPath As IPrefetchPath2 = New PrefetchPath2(CType(EntityType.DependentResourceEntity, Integer))
            Dim dependent As IPrefetchPathElement2 = EntityClasses.DependentResourceEntity.PrefetchPathResource
            testPrefetchPath.Add(dependent)

            adapter.FetchEntityCollection(dependentResources, filter, testPrefetchPath)

            If dependentResources.Count > 0 Then
                Dim sb As New Text.StringBuilder()
                sb.AppendFormat(My.Resources.CannotDelete, GetResourceEntityFriendlyName(involvedResourceEntity), involvedResourceEntity.Name, Environment.NewLine)
                For Each r As EntityClasses.DependentResourceEntity In dependentResources
                    sb.AppendFormat("   - {0}{1}", r.Resource.Name, Environment.NewLine)
                Next
                Throw New ORMEntityValidationException(sb.ToString(), involvedEntity)
            End If
        End Sub

        Public Overrides Sub ValidateEntityBeforeSave(ByVal involvedEntity As SD.LLBLGen.Pro.ORMSupportClasses.IEntityCore)
            Dim involvedResourceEntity As EntityClasses.ResourceEntity = DirectCast(involvedEntity, EntityClasses.ResourceEntity)

            If involvedResourceEntity.IsNew OrElse involvedEntity.GetFieldByName(HelperClasses.ItemResourceFields.Name.Name).IsChanged Then
                Using adapter As New DatabaseSpecific.DataAccessAdapter()

                    Dim bankTreeIds() As Integer = BankstructureHelper.GetBankBrancheIds(adapter, involvedResourceEntity.BankId)

                    Dim filter As IRelationPredicateBucket = New RelationPredicateBucket()
                    With filter
                        .PredicateExpression.Add(HelperClasses.ResourceFields.Name = involvedResourceEntity.Name.ToLower())
                        .PredicateExpression.AddWithAnd(New FieldCompareRangePredicate(HelperClasses.ResourceFields.BankId, Nothing, bankTreeIds))
                    End With

                    adapter.StartTransaction(IsolationLevel.ReadUncommitted, Guid.NewGuid.ToString().Substring(0, 32))

                    Dim results As New HelperClasses.EntityCollection(Of EntityClasses.ResourceEntity)
                    Try
                        adapter.FetchEntityCollection(results, filter, 1)
                        adapter.Commit()
                    Catch ex As Exception
                        Throw ex
                    End Try

                    If results.Count > 0 Then
                        Throw New ORMEntityValidationException(My.Resources.AResourceWithTheSameCodeAlreadyExists, involvedResourceEntity)
                    End If
                End Using
            End If
        End Sub

        Public Overrides Sub ValidateEntity(involvedEntity As SD.LLBLGen.Pro.ORMSupportClasses.IEntityCore)

            Dim sb As New Text.StringBuilder

            For Each field As IEntityField2 In involvedEntity.Fields
                Dim possibleErr As String = DirectCast(involvedEntity, ComponentModel.IDataErrorInfo)(field.Name)
                If (Not String.IsNullOrEmpty(possibleErr)) Then
                    sb.Append(possibleErr + ";")
                End If
            Next

            involvedEntity.SetEntityError(sb.ToString())

            MyBase.ValidateEntity(involvedEntity)
        End Sub


        Private Function GetResourceEntityFriendlyName(ByVal entity As EntityClasses.ResourceEntity) As String
            Select Case entity.GetType().FullName
                Case GetType(EntityClasses.AssessmentTestResourceEntity).FullName
                    Return "test"

                Case GetType(EntityClasses.ItemResourceEntity).FullName
                    Return "item"

                Case GetType(EntityClasses.ItemLayoutTemplateResourceEntity).FullName
                    Return "item layout template"

                Case GetType(EntityClasses.GenericResourceEntity).FullName
                    Return "media"

                Case GetType(EntityClasses.ControlTemplateResourceEntity).FullName
                    Return "control template"

                Case Else
                    Return "resource"

            End Select
        End Function



    End Class

End Namespace

