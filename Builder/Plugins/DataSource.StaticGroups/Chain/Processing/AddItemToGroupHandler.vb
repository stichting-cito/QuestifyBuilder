Imports System.Linq
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Plugins.DataSource.StaticGroups.Entities

Public Class AddItemToGroupHandler
    Inherits ChainHandlerBase(Of DataSourceConstructionRequest)
    Private ReadOnly _itemResourceEntity As IEnumerable(Of ItemResourceDto)
    Private ReadOnly _config As StaticGroupDataSourceConfig
    Private ReadOnly _selectedItemGroup As ItemGroup
    Private ReadOnly _onDependentResourceAddedAction As Action(Of ResourceNameEventArgs)



    Sub New(itemResourceEntity As IEnumerable(Of ItemResourceDto),
        config As StaticGroupDataSourceConfig,
        onDependentResourceAddedAction As Action(Of ResourceNameEventArgs),
        selectedItemGroup As ItemGroup)

        _itemResourceEntity = itemResourceEntity
        _config = config
        _onDependentResourceAddedAction = onDependentResourceAddedAction
        _selectedItemGroup = selectedItemGroup
    End Sub


    Public Overrides Function ProcessRequest(requestData As DataSourceConstructionRequest) As ChainHandlerResult
        For Each itm As ResourceRef In requestData.Items
            Dim newItemReference As ItemReference = GetNewItemReferenceById(itm.Identifier)
            If _selectedItemGroup IsNot Nothing Then
                _selectedItemGroup.Items.Add(newItemReference)
            Else
                _config.GroupDefinition.Add(newItemReference)
            End If
            Dim resourceIdentifier As String = newItemReference.ResourceIdentifier
            If itm.Properties.Any() Then
                Dim resourceIdFromProperties = itm.Properties.FirstOrDefault(Function(p) p.Key.Equals("resourceId", StringComparison.InvariantCultureIgnoreCase) AndAlso Not String.IsNullOrEmpty(p.Value))
                resourceIdentifier = resourceIdFromProperties.Value
            End If
            _onDependentResourceAddedAction(New ResourceNameEventArgs(resourceIdentifier))
        Next
        Return ChainHandlerResult.RequestHandled
    End Function

    Private Function GetNewItemReferenceById(id As String) As ItemReference
        Dim ret As ItemReference = Nothing

        For Each e In _itemResourceEntity
            If (e.name = id) Then
                ret = New ItemReference(e.name) With {.Title = e.title}
            End If
        Next

        Debug.Assert(Not ret Is Nothing, "Collection of returned items is different form selected collection")

        Return ret
    End Function

End Class
