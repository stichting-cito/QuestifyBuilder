Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.PlugIns.DataSource.StaticGroups.Entities

Public Class SeedingGroupDatasource
    Inherits ItemDataSourceManyOutput
    Implements IDataSourceWithContext





    Public Sub New(settings As ItemDataSourceConfig)
        MyBase.New(settings)
    End Sub




    Public Overrides ReadOnly Property ShowPreviewControl As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property ItemCount As Integer
        Get
            Return DirectCast(Me.Config(), StaticGroupDataSourceConfig).GroupDefinition.OfType(Of Entities.ItemReference).Count + DirectCast(Me.Config(), StaticGroupDataSourceConfig).GroupDefinition.OfType(Of ItemGroup).Sum(Function(ig) ig.Items.Count)
        End Get
    End Property

    Public Overrides ReadOnly Property ClearSectionWhenProposing As Boolean
        Get
            Return False
        End Get
    End Property



    Public Overrides Function GetMany(resourceManager As ResourceManagerBase, numberOfRequests As Integer) As IList(Of IEnumerable(Of ResourceRef))
        Dim returnValue As New List(Of IEnumerable(Of ResourceRef))
        Dim groupDictionary As New Dictionary(Of Integer, Integer)
        Dim settings = DirectCast(Me.Config(), StaticGroupDataSourceConfig)
        Dim groups = settings.GroupDefinition.OfType(Of ItemGroup).ToList
        Dim groupIndex As Integer = 0
        For proposalIndex = 0 To Math.Min(numberOfRequests - 1, ItemCount)
            Dim itemIndex As Integer = GetNextItemFromGroup(groupDictionary, groupIndex)
            Dim itemGroup As ItemGroup = Nothing
            If groups.Count > groupIndex Then
                itemGroup = groups(groupIndex)
            End If
            Dim canPickFormCurrentGroup = itemGroup IsNot Nothing AndAlso itemGroup.Items.Count >= itemIndex
            While Not canPickFormCurrentGroup
                groupIndex = CType(IIf(groups.Count <= (1 + groupIndex), 0, groupIndex + 1), Integer)
                itemGroup = groups(groupIndex)
                itemIndex = GetNextItemFromGroup(groupDictionary, groupIndex)
                canPickFormCurrentGroup = itemGroup.Items.Count > itemIndex
            End While
            If Not groupDictionary.ContainsKey(groupIndex) Then groupDictionary.Add(groupIndex, 0)
            groupDictionary(groupIndex) = itemIndex
            Dim proposalResult As New List(Of ResourceRef)
            Dim firstItem As String = String.Empty
            For index = 0 To itemGroup.Items.Count - 1
                If itemIndex > itemGroup.Items.Count - 1 Then itemIndex = 0
                If index = 0 Then firstItem = itemGroup.Items.Item(itemIndex).ResourceIdentifier
                Dim resourceRef = New ResourceRef With {
                    .Identifier = itemGroup.Items.Item(itemIndex).ResourceIdentifier,
                    .Properties = New Cito.Tester.Common.SerializableGenericDictionary(Of String, String) From {
                            {"seeding", "true"}, {"equallyDivided", "true"},
                            {"seeding_group", itemGroup.Title}, {"seeding_group_first_item", firstItem}}}

                proposalResult.Add(resourceRef)
                itemIndex += 1
            Next
            returnValue.Add(proposalResult)
            groupIndex += 1
        Next
        Return returnValue
    End Function

    Public Overrides Function GetAllItemcodes(resourceManager As ResourceManagerBase) As IEnumerable(Of String)
        return DirectCast(Me.Config(), StaticGroupDataSourceConfig).GroupDefinition.OfType(Of ItemGroup).SelectMany(Function(ig) ig.Items).Select(function(i) i.ResourceIdentifier).Distinct
    End Function

    Private Function GetNextItemFromGroup(groupDictionary As Dictionary(Of Integer, Integer), groupIndex As Integer) As Integer
        Dim index As Integer = 0
        If groupDictionary.ContainsKey(groupIndex) Then
            index = groupDictionary(groupIndex) + 1
        End If
        Return index
    End Function

    Public Overrides Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of ResourceRef)
        Dim returnValue As New List(Of ResourceRef)
        If TestSectionContext IsNot Nothing Then
            For Each itm In TestSectionContext.GetAllItemReferencesInSection(True)
                Dim newResourceReference As New ResourceRef
                newResourceReference.Identifier = itm.Identifier
                returnValue.Add(newResourceReference)
            Next
        End If
        Return returnValue
    End Function


    Public Property TestSectionContext As TestSection2 Implements IDataSourceWithContext.TestSectionContext

    Public Property AssessmentTestContext As AssessmentTest2 Implements IDataSourceWithContext.AssessmentTestContext
End Class