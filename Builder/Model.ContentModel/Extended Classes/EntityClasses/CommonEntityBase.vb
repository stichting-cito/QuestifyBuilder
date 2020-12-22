Imports System.Collections.Generic
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

#If Not CF Then
#End If
Imports System.Linq

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class CommonEntityBase

        Public Function HasChangesInTopology() As Boolean
            Dim ogu As ObjectGraphUtils = New ObjectGraphUtils
            Dim enumerator As IEnumerable(Of IEntity2) = ogu.ProduceTopologyOrderedList(Me)

            For Each entity As IEntity2 In enumerator.ToList()
                If (entity.IsDirty OrElse entity.IsNew) Then
                    Return True
                End If

                If entity.GetType() Is GetType(ItemResourceEntity) Then
                    Dim item As ItemResourceEntity = CType(entity, ItemResourceEntity)
                    If item.CustomBankPropertyValueCollection IsNot Nothing _
                            AndAlso item.CustomBankPropertyValueCollection.RemovedEntitiesTracker IsNot Nothing _
                            AndAlso item.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Count > 0 Then

                        Return True
                    Else
                        For Each valueEntity As ListCustomBankPropertyValueEntity In item.CustomBankPropertyValueCollection.OfType(Of ListCustomBankPropertyValueEntity)()
                            If valueEntity.ListCustomBankPropertySelectedValueCollection IsNot Nothing AndAlso
                               valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker IsNot Nothing AndAlso
                                valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.Count > 0 Then
                                Return True
                            End If
                        Next
                    End If
                End If
            Next

            Return False
        End Function

        Public Function OnlyChangesInWorkflowMetaData() As Boolean
            Dim ogu As ObjectGraphUtils = New ObjectGraphUtils
            Dim enumerator As IEnumerable(Of IEntity2) = ogu.ProduceTopologyOrderedList(Me)

            For Each entity As IEntity2 In enumerator.ToList()
                If (entity.IsDirty Or entity.IsNew) Then
                    If TypeOf entity Is EntityClasses.ResourceEntity Then
                        For Each fld As IEntityField2 In entity.Fields
                            If Not fld.Name.Equals(ResourceFields.StateId.Name) AndAlso fld.IsChanged Then
                                Return False
                            End If
                        Next
                    Else
                        Return False
                    End If
                End If
            Next

            Return True
        End Function

        Public Function GetUpdatesInTestStructureParts() As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim ogu As ObjectGraphUtils = New ObjectGraphUtils
            Dim enumerator As IEnumerable(Of IEntity2) = ogu.ProduceTopologyOrderedList(Me)
            Dim result As New List(Of TreeStructurePartCustomBankPropertyEntity)

            For Each entity As IEntity2 In enumerator.OfType(Of TreeStructurePartCustomBankPropertyEntity)
                If (entity.IsDirty AndAlso Not entity.IsNew) Then
                    result.Add(CType(entity, TreeStructurePartCustomBankPropertyEntity))
                End If
            Next

            Return result
        End Function

        Public Overloads Property ObjectId As Guid
            Get
                Return MyBase.ObjectID
            End Get
            Set
                MyBase.ObjectID = Value
            End Set
        End Property
    End Class

End Namespace
