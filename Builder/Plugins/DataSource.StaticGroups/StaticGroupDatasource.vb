Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.PlugIns.DataSource.StaticGroups.Entities

Public Class StaticGroupDatasource
    Inherits ItemDataSource

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
            return DirectCast(Me.Config(), StaticGroupDataSourceConfig).GroupDefinition.OfType(Of Entities.ItemReference).Count + DirectCast(Me.Config(), StaticGroupDataSourceConfig).GroupDefinition.OfType(Of ItemGroup).Sum(function(ig) ig.Items.Count)
        End Get
    End Property

    Public Overrides Function [Get](resourceManager As ResourceManagerBase) As IEnumerable(Of ResourceRef)
        Dim returnValue As New List(Of ResourceRef)

        Dim settings = DirectCast(Me.Config(), StaticGroupDataSourceConfig)
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
        For Each groupEntry As StaticGroupEntry In settings.GroupDefinition
            If TypeOf groupEntry Is Entities.ItemReference Then
                Dim newResourceReference As New ResourceRef
                newResourceReference.Identifier = groupEntry.ResourceIdentifier

                returnValue.Add(newResourceReference)
            ElseIf TypeOf groupEntry Is GroupReference Then
                Dim resource As BinaryResource = resourceManager.GetTypedResource(groupEntry.ResourceIdentifier, GetType(DataSourceSettings), request)
                Debug.Assert(resource.ResourceObject IsNot Nothing)

                Dim dataSourceSettings = DirectCast(resource.ResourceObject, DataSourceSettings)

                Dim initialSetDataSource = CType(dataSourceSettings.CreateGetDataSource(), ItemDataSource)
                Dim results As IEnumerable(Of ResourceRef) = initialSetDataSource.Get(resourceManager)
                For Each item As ResourceRef In results
                    If Not item.Properties.ContainsKey("NestedGroup") Then
                        item.Properties.Add("NestedGroup", groupEntry.ResourceIdentifier)
                    End If
                Next
                returnValue.AddRange(results)
            ElseIf TypeOf groupEntry Is ItemGroup Then
            Else
                Throw New NotSupportedException(String.Format("Unknown static group entry type: {0}", groupEntry.GetType().FullName))
            End If
        Next

        Return returnValue
    End Function

    Public Overrides Function RenameItem(currentItemCode As String, newItemCode As String) As Boolean
        Dim itemsUpdated As Boolean = False
        Dim config = DirectCast(Me.Config(), StaticGroupDataSourceConfig)

        itemsUpdated = RenameItemInItemRefs(config.GroupDefinition.OfType(Of ItemReference), currentItemCode, newItemCode)

        For Each ig In config.GroupDefinition.OfType(Of ItemGroup)
            itemsUpdated = RenameItemInItemRefs(ig.Items, currentItemCode, newItemCode)
        Next

        Return itemsUpdated
    End Function

    Private Function RenameItemInItemRefs(itemRefs As IEnumerable(Of ItemReference), currentItemCode As String, newItemCode As String) As Boolean
        Dim itemsUpdated As Boolean = False

        For Each ir In itemRefs
            If ir.ResourceIdentifier.Equals(currentItemCode) Then
                ir.ResourceIdentifier = newItemCode

                If Not String.IsNullOrEmpty(ir.Title) AndAlso ir.Title.Equals(currentItemCode) Then
                    ir.Title = newItemCode
                End If
                itemsUpdated = True
            End If
        Next

        Return itemsUpdated
    End Function

End Class