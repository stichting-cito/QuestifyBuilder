
Imports Questify.Builder.Logic.CustomProperties.Interfaces
Imports Questify.Builder.Logic.CustomProperties.ValueConverters
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Text
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Logic.Service.Model.Entities.Custom

Namespace CustomProperties.Helpers

    Public Class CustomPropertyHelper

        Public Function GetCustomBankConverter(customBankPropery As CustomBankPropertyEntity) As ICustomPropertyValueConverter
            Dim customBankPropertyType = GetCustomBankPropertyType(customBankPropery)
            If customBankPropertyType IsNot Nothing Then
                Return GetCustomBankConverter(CType(customBankPropertyType, CustomPropertyType))
            End If
            Return Nothing
        End Function

        Public Function GetCustomBankConverter(customPropertyType__1 As CustomPropertyType) As ICustomPropertyValueConverter
            Dim converter As ICustomPropertyValueConverter = Nothing
            Select Case customPropertyType__1
                Case CustomPropertyType.Free
                    converter = New FreeCustomPropertyValueConverter
                    Exit Select
                Case CustomPropertyType.ListSingle, CustomPropertyType.ListMultiple
                    converter = New ListCustomPropertyValueConverter
                    Exit Select
                Case CustomPropertyType.Concept
                    converter = New ConceptStructureCustomPropertyValueConverter
                    Exit Select
                Case CustomPropertyType.Tree
                    converter = New TreeStructureCustomPropertyValueConverter
                    Exit Select
                Case CustomPropertyType.FreeRichText
                    converter = New RichTextCustomPropertyValueConverter
                    Exit Select
            End Select
            Return converter
        End Function

        Public Shared Function GetCustomBankPropertyType(customBankPropery As CustomBankPropertyEntity) As System.Nullable(Of CustomPropertyType)
            Select Case customBankPropery.[GetType]().Name
                Case "FreeValueCustomBankPropertyEntity"
                    Return CustomPropertyType.Free
                Case "RichTextValueCustomBankPropertyEntity"
                    Return CustomPropertyType.FreeRichText
                Case "ListCustomBankPropertyEntity"
                    If DirectCast(customBankPropery, ListCustomBankPropertyEntity).MultipleSelect Then
                        Return CustomPropertyType.ListMultiple
                    End If
                    If True Then
                        Return CustomPropertyType.ListSingle
                    End If
                Case "ConceptStructureCustomBankPropertyEntity"
                    Return CustomPropertyType.Concept
                Case "TreeStructureCustomBankPropertyEntity"
                    Return CustomPropertyType.Tree
            End Select
            Return Nothing
        End Function

        Public Shared Function GetValueFromSelectedValueCollection(selectedValues As IEnumerable(Of Guid), values As IList(Of CustomBankPropertyDto)) As String
            Dim customBankPropertyValue As IEnumerable(Of CustomPropertyValueDto) = New List(Of CustomPropertyValueDto)()
            customBankPropertyValue = values.Where(Function(cv) cv.Values IsNot Nothing).Aggregate(customBankPropertyValue, Function(current, c) current.Union(c.Values))
            Return GetValueFromSelectedValueCollection(selectedValues, customBankPropertyValue.ToList)
        End Function

        Public Shared Function GetValueFromSelectedValueCollection(selectedValues As IEnumerable(Of Guid), values As IList(Of CustomPropertyValueDto)) As String
            Dim returnValue = String.Empty
            If selectedValues Is Nothing OrElse values Is Nothing Then
                Return returnValue
            End If
            Dim displaytext = New StringBuilder()
            For Each value In selectedValues
                Dim selectValue = value
                If displaytext.Length > 0 Then
                    displaytext.Append(";")
                End If
                Dim customPropertyDtoValue = values.FirstOrDefault(Function(v) v.CustomPropertyValueId = selectValue)
                If customPropertyDtoValue IsNot Nothing Then
                    displaytext.Append(customPropertyDtoValue.DisplayValue)
                End If
            Next
            Return displaytext.ToString()
        End Function

        Friend Shared Function GetTreeValueFromCustomBankPropertyValue(treeValue As TreeStructureCustomBankPropertyValueEntity) As String
            Dim treeStructureCustomBankProperty = BankFactory.Instance.PopulateTreeCustomBankPropertyHierarchy(treeValue.CustomBankPropertyId)
            Dim treeStructureCustomBankPropertyValueEntity = treeStructureCustomBankProperty.TreeStructureCustomBankPropertyValueCollection.FirstOrDefault(Function(x) x.CustomBankPropertyId = treeValue.CustomBankPropertyId AndAlso x.ResourceId = treeValue.ResourceId)
            If treeStructureCustomBankPropertyValueEntity IsNot Nothing Then
                Return [String].Join(";", CreateTreeStructureDisplayText(treeStructureCustomBankPropertyValueEntity.TreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart))
            End If
            Return String.Empty
        End Function

        Friend Shared Function GetTreeValueFromCustomBankPropertySelectedParts(treeValue As TreeStructureCustomBankPropertyValueEntity) As String
            If treeValue.TreeStructureCustomBankPropertySelectedPartCollection IsNot Nothing AndAlso treeValue.TreeStructureCustomBankPropertySelectedPartCollection.Count > 0 Then
                Dim selectedParts = BankFactory.Instance.GetTreeStructurePartCustomBankProperties(treeValue.TreeStructureCustomBankPropertySelectedPartCollection.Select(Function(sp) sp.TreeStructurePartId).ToList(), True)
                Dim listOfSelectedParts As New EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)
                selectedParts.ToList.ForEach(Sub(cv)
                                                 listOfSelectedParts.Add(CType(cv, TreeStructurePartCustomBankPropertyEntity))
                                             End Sub)
                Return [String].Join(";", CreateTreeStructureDisplayText(listOfSelectedParts))
            End If
            Return String.Empty
        End Function

        Private Shared Function CreateTreeStructureDisplayText(treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)) As String
            Dim result As New List(Of String)
            Dim roots = GetRoots(treeStructurePartCustomBankPropertyEntities)
            Dim parentsWithSelectedChilds = New List(Of TreeStructurePartCustomBankPropertyEntity)()
            For Each root In roots
                Dim flatTree = CreateFlatTree(root, treeStructurePartCustomBankPropertyEntities, parentsWithSelectedChilds)
                result.AddRange(flatTree.Except(parentsWithSelectedChilds).Select(Function(i) i.Name))
            Next
            Return String.Join(";", result)
        End Function

        Private Shared Function CreateFlatTree(rootTreeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity, treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity), ByRef parentsWithSelectedChilds As List(Of TreeStructurePartCustomBankPropertyEntity)) As IEnumerable(Of TreeStructurePartCustomBankPropertyEntity)
            Dim tree = New List(Of TreeStructurePartCustomBankPropertyEntity)()

            For Each child As ChildTreeStructurePartCustomBankPropertyEntity In rootTreeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.OrderBy(Function(i) i.VisualOrder)
                Dim childAsPart = treeStructurePartCustomBankPropertyEntities.FirstOrDefault(Function(i) i.TreeStructurePartCustomBankPropertyId = child.ChildTreeStructurePartCustomBankPropertyId)
                If childAsPart IsNot Nothing Then
                    tree.Add(childAsPart)

                    Dim parent = GetParent(childAsPart, treeStructurePartCustomBankPropertyEntities)

                    If Not parentsWithSelectedChilds.Contains(parent) Then
                        parentsWithSelectedChilds.Add(parent)
                    End If

                    tree.AddRange(CreateFlatTree(childAsPart, treeStructurePartCustomBankPropertyEntities, parentsWithSelectedChilds))
                End If
            Next

            If Not tree.Any() Then
                tree.Add(rootTreeStructurePartCustomBankPropertyEntity)
            End If
            Return tree
        End Function

        Private Shared Function GetRoots(treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)) As List(Of TreeStructurePartCustomBankPropertyEntity)
            Dim roots As New List(Of TreeStructurePartCustomBankPropertyEntity)

            For Each treeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntities
                Dim parent As TreeStructurePartCustomBankPropertyEntity = Nothing
                parent = GetParent(treeStructurePartCustomBankPropertyEntity, treeStructurePartCustomBankPropertyEntities)

                If parent Is Nothing Then
                    If Not roots.Contains(treeStructurePartCustomBankPropertyEntity) Then
                        roots.Add(treeStructurePartCustomBankPropertyEntity)
                    End If
                Else
                    Dim parentOfParent = GetParent(parent, treeStructurePartCustomBankPropertyEntities)

                    If parentOfParent Is Nothing Then
                        If Not roots.Contains(parent) Then
                            roots.Add(parent)
                        End If
                    End If
                End If
            Next

            Return roots
        End Function

        Private Shared Function GetParent(treeStructurePartCustomBankPropertyEntityToGetParentFrom As TreeStructurePartCustomBankPropertyEntity, treeStructurePartCustomBankPropertyEntities As EntityCollection(Of TreeStructurePartCustomBankPropertyEntity)) As TreeStructurePartCustomBankPropertyEntity
            For Each treeStructurePartCustomBankPropertyEntity In treeStructurePartCustomBankPropertyEntities
                If Not Object.ReferenceEquals(treeStructurePartCustomBankPropertyEntityToGetParentFrom, treeStructurePartCustomBankPropertyEntity) Then
                    Dim parent = treeStructurePartCustomBankPropertyEntity.ChildTreeStructurePartCustomBankPropertyCollection.FirstOrDefault(Function(i) i.ChildTreeStructurePartCustomBankPropertyId = treeStructurePartCustomBankPropertyEntityToGetParentFrom.TreeStructurePartCustomBankPropertyId)

                    If parent IsNot Nothing Then
                        Return treeStructurePartCustomBankPropertyEntities.First(Function(i) i.TreeStructurePartCustomBankPropertyId = parent.TreeStructurePartCustomBankPropertyId)
                    End If
                End If
            Next

            Return Nothing
        End Function

    End Class
End Namespace