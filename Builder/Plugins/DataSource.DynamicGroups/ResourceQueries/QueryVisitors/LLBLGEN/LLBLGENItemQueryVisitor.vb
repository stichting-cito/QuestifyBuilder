Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Imports Questify.Builder.Model.ContentModel.ResourceProperties
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Cito.Tester.ContentModel
Imports System.Collections.ObjectModel

Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic

Public Class LLBLGENItemQueryVisitor
    Inherits ItemQueryVisitor


    Private _AddOnce_FreeValueRelation As Boolean = False
    Private _AddOnce_SelectedListValueRelation As Boolean = False
    Private _AddOnce_ItemLayoutTemplateRelation As Boolean
    Private _bucket As RelationPredicateBucket
    Private _predicateExpressionStack As New Stack(Of IPredicateExpression)
    Private _bankId As Integer



    Public Sub New(query As ItemQuery)
        MyBase.New(query)

        Me.AddVisitorForType(GetType(AndFilterPredicate), AddressOf AndVisitHandler)
        Me.AddVisitorForType(GetType(OrFilterPredicate), AddressOf OrVisitHandler)
        Me.AddVisitorForType(GetType(NotFilterPredicate), AddressOf NotVisitHandler)
        Me.AddVisitorForType(GetType(ItemInTestFilterPredicate), AddressOf ItemInTestFilterPredicateVisitHandler)
        Me.AddVisitorForType(GetType(ResourcePropertyFilterPredicate), AddressOf ResourcePropertyFilterPredicateHandler)
    End Sub



    Public Function Execute(bankId As Integer) As EntityCollection
        Dim resserv As IResourceService = ResourceFactory.Instance
        _bankId = bankId
        initBucket()
        Me.Visit()
        If _predicateExpressionStack.Count > 0 Then
            _bucket.PredicateExpression.Add(_predicateExpressionStack.Pop)
        End If
        Dim items As EntityCollection = resserv.GetItemsForBank(bankId, _bucket)
        Return items
    End Function

    Private Sub AddOnce_FreeValueRelation(bucket As RelationPredicateBucket)
        If Not _AddOnce_FreeValueRelation Then
            bucket.Relations.Add(CreateFreeValueRelation)
            _AddOnce_FreeValueRelation = True
        End If
    End Sub

    Private Sub AddOnce_SelectedListValueRelation(bucket As RelationPredicateBucket)
        If Not _AddOnce_SelectedListValueRelation Then
            bucket.Relations.Add(CreateSelectedListValueRelation)
            _AddOnce_SelectedListValueRelation = True
        End If
    End Sub

    Private Sub AndVisitHandler(filter As AndFilterPredicate)
        Dim oneExpression As IPredicateExpression
        Dim otherExpression As IPredicateExpression
        Dim andExpression As IPredicateExpression = New PredicateExpression

        Visit(filter.One)
        Visit(filter.Other)

        oneExpression = _predicateExpressionStack.Pop
        otherExpression = _predicateExpressionStack.Pop

        andExpression.Add(oneExpression)
        andExpression.AddWithAnd(otherExpression)

        _predicateExpressionStack.Push(andExpression)
    End Sub

    Private Function ConvertFilterOperator(FilterOp As ResourcePropertyFilterPredicate.FilterOperatorEnum) As ComparisonOperator
        Dim op As ResourcePropertyFilterPredicate.FilterOperatorEnum

        Select Case FilterOp
            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.EQUAL
                op = ComparisonOperator.Equal

            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.GREATER_THAN
                op = ComparisonOperator.GreaterThan

            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.GREATER_THAN_OR_EQUAL
                op = ComparisonOperator.GreaterEqual

            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.LESS_THAN
                op = ComparisonOperator.LesserThan

            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.LESS_THAN_OR_EQUAL
                op = ComparisonOperator.LessEqual

            Case ResourcePropertyFilterPredicate.FilterOperatorEnum.NOT_EQUAL
                op = ComparisonOperator.NotEqual
        End Select

        Return op
    End Function

    Private Sub AddOnceItemLayoutTemplateRelation(bucket As RelationPredicateBucket)

        If Not _AddOnce_ItemLayoutTemplateRelation Then
            bucket.Relations.Add(ResourceEntity.Relations.DependentResourceEntityUsingResourceId, "ResourceToDepResource")
            bucket.Relations.Add(DependentResourceEntity.Relations.ResourceEntityUsingDependentResourceId, "ResourceToDepResource", "DepResourceToItemLayoutResource", JoinHint.Inner)
            bucket.Relations.Add(ResourceEntity.Relations.GetSubTypeRelation("ItemLayoutTemplateResourceEntity"), "DepResourceToItemLayoutResource", "ItemLayoutResource", JoinHint.Inner)

            _AddOnce_ItemLayoutTemplateRelation = True
        End If

    End Sub

    Private Function CreateFreeValueRelation() As IEntityRelation
        Dim relation As IEntityRelation = New EntityRelation(RelationType.OneToMany, True)
        relation.AddEntityFieldPair(ResourceFields.ResourceId, FreeValueCustomBankPropertyValueFields.ResourceId)
        relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
        relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FreeValueCustomBankPropertyValueEntity", False)

        Return relation
    End Function

    Private Function CreateSelectedListValueRelation() As IEntityRelation
        Dim relation As IEntityRelation = New EntityRelation(RelationType.OneToMany, True)
        relation.AddEntityFieldPair(ResourceFields.ResourceId, ListCustomBankPropertySelectedValueFields.ResourceId)
        relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", True)
        relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ListCustomBankPropertySelectedValueEntity", False)

        Return relation
    End Function

    Private Function GetResourceProperty(bankId As Integer, Key As Guid) As ResourcePropertyDefinition
        Dim propertyDef As ResourcePropertyDefinition = Nothing
        Dim bank = BankFactory.Instance.GetBank(bankId)
        For Each def As ResourcePropertyDefinition In BankFactory.Instance.GetResourcePropertyDefinitions(bank)
            If def.Key.ToString = Key.ToString Then
                propertyDef = def
            End If
        Next
        Return propertyDef
    End Function

    Private Function GetAssessmentTest(key As Guid) As AssessmentTest2
        Dim testResource As AssessmentTestResourceEntity = ResourceFactory.Instance.GetAssessmentTest(New AssessmentTestResourceEntity(key))

        If testResource Is Nothing Then
            Throw New Exception(String.Format("ItemInTest Filter Error: referenced Test with resource id {0} does nog exist", key))
        End If

        Dim assessmentTest As AssessmentTest2 = GetTestFromResource(testResource)

        Return assessmentTest
    End Function

    Private Function GetTestFromResource(ByVal testEntity As AssessmentTestResourceEntity) As AssessmentTest2
        Dim testDefinition As AssessmentTest2 = Nothing
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testEntity)
        Dim result As ReturnedAssessmentTestModelInfo

        result = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(data.BinData, True)

        testDefinition = result.AssessmentTestv2
        testEntity.ResourceData = data

        Return testDefinition
    End Function

    Private Sub initBucket()
        _bucket = New RelationPredicateBucket()
        _AddOnce_FreeValueRelation = False
        _AddOnce_SelectedListValueRelation = False
        _AddOnce_ItemLayoutTemplateRelation = False
    End Sub

    Private Sub ItemInTestFilterPredicateVisitHandler(filter As ItemInTestFilterPredicate)
        Dim test As AssessmentTest2 = GetAssessmentTest(filter.AssessmentTest)

        Dim ItemMetaDataFilter As IPredicateExpression
        Dim fieldIndex As ItemResourceFieldIndex = ItemResourceFieldIndex.Name
        EntityFieldFactory.Create(fieldIndex)

        Dim ItemRefs As ReadOnlyCollection(Of ItemReference2) = test.GetAllItemReferencesInTest
        Dim ItemRefArray As New ArrayList

        For Each ref As ItemReference2 In ItemRefs
            ItemRefArray.Add(ref.SourceName)
        Next

        ItemMetaDataFilter = New PredicateExpression(
            New FieldCompareRangePredicate(ItemResourceFields.Name, Nothing, ItemRefArray.ToArray))
        Me._predicateExpressionStack.Push(ItemMetaDataFilter)
    End Sub
    Private Sub NotVisitHandler(filter As NotFilterPredicate)
        Dim wrappedExpression As IPredicateExpression

        Visit(filter.Wrapped)

        wrappedExpression = _predicateExpressionStack.Pop
        wrappedExpression.Negate = Not wrappedExpression.Negate

        _predicateExpressionStack.Push(wrappedExpression)
    End Sub

    Sub OrVisitHandler(filter As OrFilterPredicate)
        Dim oneExpression As IPredicateExpression
        Dim otherExpression As IPredicateExpression
        Dim orExpression As IPredicateExpression = New PredicateExpression

        Visit(filter.One)
        Visit(filter.Other)

        oneExpression = _predicateExpressionStack.Pop
        otherExpression = _predicateExpressionStack.Pop

        orExpression.Add(oneExpression)
        orExpression.AddWithOr(otherExpression)

        _predicateExpressionStack.Push(orExpression)
    End Sub

    Private Sub ResourcePropertyFilterPredicateHandler(filter As ResourcePropertyFilterPredicate)
        Dim propertyDef As ResourcePropertyDefinition = GetResourceProperty(_bankId, filter.PropertyKey)
        Dim propertyType As ResourcePropertyDefinition.PropertyTypeEnum = propertyDef.PropertyType
        Dim propertyValue As ResourcePropertyDefinition.PropertyValueTypeEnum = propertyDef.PropertyListValue
        Dim op As ComparisonOperator = ConvertFilterOperator(filter.Op)

        Select Case propertyType
            Case ResourcePropertyDefinition.PropertyTypeEnum.Static

                Select Case propertyDef.Name
                    Case "BankName"
                        Dim field As EntityField2 = EntityFieldFactory.Create(BankFieldIndex.Name)
                        Dim relation As IEntityRelation = New EntityRelation(RelationType.OneToMany, True)
                        relation.AddEntityFieldPair(ResourceFields.BankId, BankFields.Id)

                        _bucket.Relations.Add(relation)
                        Dim bankFilter = New PredicateExpression(New FieldCompareValuePredicate(field, Nothing, op, filter.Value))
                        _predicateExpressionStack.Push(bankFilter)

                    Case "ItemLayoutTemplateUsedName"
                        AddOnceItemLayoutTemplateRelation(_bucket)

                        Dim fldPred = New FieldCompareValuePredicate(ResourceFields.Name, Nothing, op, filter.Value, "DepResourceToItemLayoutResource")
                        Dim templateNameFilter = New PredicateExpression(fldPred)
                        _predicateExpressionStack.Push(templateNameFilter)

                    Case "ItemTypeFromItemLayoutTemplate"

                        AddOnceItemLayoutTemplateRelation(_bucket)

                        Dim fldPred = New FieldCompareValuePredicate(ItemLayoutTemplateResourceFields.ItemType, Nothing, op, filter.Value, "ItemLayoutResource")
                        Dim templateTypeFilter = New PredicateExpression(fldPred)
                        _predicateExpressionStack.Push(templateTypeFilter)

                    Case [Enum].GetName(GetType(ItemResourceFieldIndex), ItemResourceFieldIndex.CreatedBy)
                        Dim userFilter As PredicateExpression
                        userFilter = SearchOnName(ResourceFields.CreatedBy, filter.Value, op)
                        _predicateExpressionStack.Push(userFilter)

                    Case [Enum].GetName(GetType(ItemResourceFieldIndex), ItemResourceFieldIndex.ModifiedBy)
                        Dim userFilter As PredicateExpression
                        userFilter = SearchOnName(ResourceFields.ModifiedBy, filter.Value, op)
                        _predicateExpressionStack.Push(userFilter)

                    Case Else

                        Dim ItemMetaDataFilter As IPredicateExpression
                        Dim fieldIndex As ItemResourceFieldIndex = [Enum].Parse(GetType(ItemResourceFieldIndex), propertyDef.Name)
                        Dim field As EntityField2 = EntityFieldFactory.Create(fieldIndex)

                        ItemMetaDataFilter = New PredicateExpression(New FieldCompareValuePredicate(field, Nothing, op, filter.Value))
                        _predicateExpressionStack.Push(ItemMetaDataFilter)

                End Select
            Case ResourcePropertyDefinition.PropertyTypeEnum.Dynamic
                Dim customPropertyid As Guid = filter.PropertyKey
                Dim propertyPredicate As IPredicate
                Dim valuePredicate As IPredicate

                Select Case propertyValue
                    Case ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText
                        Dim valueAsString As String = filter.Value.ToString

                        propertyPredicate = New FieldCompareValuePredicate(FreeValueCustomBankPropertyValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, customPropertyid)
                        valuePredicate = New FieldCompareValuePredicate(FreeValueCustomBankPropertyValueFields.Value, Nothing, op, valueAsString)

                        AddOnce_FreeValueRelation(_bucket)

                    Case ResourcePropertyDefinition.PropertyValueTypeEnum.MultiListValue, ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue
                        Dim selectedListValue As Guid = filter.SelectedListValueKey

                        propertyPredicate = New FieldCompareValuePredicate(ListCustomBankPropertySelectedValueFields.CustomBankPropertyId, Nothing, ComparisonOperator.Equal, customPropertyid)
                        valuePredicate = New FieldCompareValuePredicate(ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId, Nothing, op, selectedListValue)

                        AddOnce_SelectedListValueRelation(_bucket)
                    Case Else
                        Throw New NotSupportedException(propertyValue.ToString)
                End Select

                _predicateExpressionStack.Push(New PredicateExpression(propertyPredicate, PredicateExpressionOperator.And, valuePredicate))
        End Select
    End Sub

    Friend Function SearchOnName(ByVal searchOn As EntityField2, ByVal name As String, ByVal op As ComparisonOperator) As PredicateExpression
        Dim field As EntityField2 = EntityFieldFactory.Create(UserFieldIndex.FullName)
        Dim relation As IEntityRelation = New EntityRelation(RelationType.OneToMany, True)
        relation.AddEntityFieldPair(ResourceFields.CreatedBy, UserFields.Id)
        _bucket.Relations.Add(relation)
        Return New PredicateExpression(New FieldCompareValuePredicate(field, Nothing, op, name))
    End Function


End Class