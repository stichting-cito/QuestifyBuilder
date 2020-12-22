Imports System.Windows.Forms
Imports Enums
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces

Public Class PropertyFilterEditor


    Private _bankId As Integer
    Private _bankService As IBankService



    Public Overrides Property Filter() As FilterPredicate
        Get
            Return MyBase.Filter
        End Get
        Set(value As FilterPredicate)
            MyBase.Filter = value
            ResourcePropertyFilterPredicateBindingSource.DataSource = Me.Filter
        End Set
    End Property



    Private Sub PropertyFilterEditor_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        OpComboBox.DataSource = System.Enum.GetValues(GetType(ResourcePropertyFilterPredicate.FilterOperatorEnum))
    End Sub

    Public Overrides Property ResourceManager As DataBaseResourceManager
        Get
            Return MyBase.ResourceManager
        End Get
        Set(value As DataBaseResourceManager)
            MyBase.ResourceManager = value
            _bankId = Me.ResourceManager.BankId
            _bankService = BankFactory.Instance

            SetResourcePropertyDefinitions()
        End Set
    End Property

    Private Sub FilterOperatorsBySelectedFilterType(filterType As Type)
        Dim filteredOperatorList As New List(Of ResourcePropertyFilterPredicate.FilterOperatorEnum)()
        Dim allOperators As Array = System.Enum.GetValues(GetType(ResourcePropertyFilterPredicate.FilterOperatorEnum))
        For i As Integer = 0 To allOperators.Length - 1
            Dim op As ResourcePropertyFilterPredicate.FilterOperatorEnum = allOperators(i)
            Select Case filterType.Name
                Case GetType(String).Name, GetType(Boolean).Name
                    If op = ResourcePropertyFilterPredicate.FilterOperatorEnum.EQUAL OrElse op = ResourcePropertyFilterPredicate.FilterOperatorEnum.NOT_EQUAL Then
                        filteredOperatorList.Add(op)
                    End If
                Case Else
                    filteredOperatorList.Add(op)
            End Select
        Next

        OpComboBox.DataSource = filteredOperatorList
    End Sub

    Private Sub SetResourcePropertyDefinitions()
        Dim bank = _bankService.GetBank(_bankId)
        Dim resourcePropertyDefCollection As ResourceProperties.ResourcePropertyDefinitionCollection = _bankService.GetResourcePropertyDefinitions(bank)

        resourcePropertyDefCollection.RemoveAll(AddressOf IsNotItemResource)

        ResourcePropertyDefinitionBindingSource.DataSource = resourcePropertyDefCollection
        ResourcePropertyListValueDefinitionBindingSource.DataSource = ResourcePropertyDefinitionBindingSource
        ResourcePropertyListValueDefinitionBindingSource.DataMember = "ListValues"
    End Sub

    Private Shared Function IsNotItemResource(propDef As ResourceProperties.ResourcePropertyDefinition) As Boolean
        If propDef.ApplicableToMask = (propDef.ApplicableToMask Or ResourceTypeEnum.ItemResource) Then
            Return False
        End If
        Return True
    End Function

    Private Sub ResourcePropertyDefinitionComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ResourcePropertyDefinitionComboBox.SelectedIndexChanged
        Dim selectedProperty As ResourceProperties.ResourcePropertyDefinition = DirectCast(sender, ComboBox).SelectedItem
        Dim propertyFilter As ResourcePropertyFilterPredicate = CType(Me.Filter, ResourcePropertyFilterPredicate)

        OpComboBox.Enabled = False

        For Each ctrl As Control In ResourcePropertyValuePanel.Controls
            ctrl.Visible = False
        Next

        If selectedProperty IsNot Nothing Then
            propertyFilter.PropertyName = selectedProperty.Title
            ResourcePropertyDefinitionComboBox.Enabled = False

            Select Case selectedProperty.PropertyType
                Case ResourceProperties.ResourcePropertyDefinition.PropertyTypeEnum.Static, ResourceProperties.ResourcePropertyDefinition.PropertyTypeEnum.Dynamic
                    ResourcePropertyDefinitionComboBox.Enabled = True
                    FilterOperatorsBySelectedFilterType(selectedProperty.ReturnType)
                    OpComboBox.Enabled = True
                Case Else
                    Trace.Assert(String.Format("unexpected listPropertyType: {0}", selectedProperty.PropertyListValue.ToString))
            End Select

            Select Case selectedProperty.PropertyListValue
                Case ResourceProperties.ResourcePropertyDefinition.PropertyValueTypeEnum.FreeText
                    ResourcePropertyFreeValueTextBox.Clear()
                    ResourcePropertyFreeValueTextBox.Visible = True

                Case ResourceProperties.ResourcePropertyDefinition.PropertyValueTypeEnum.MultiListValue, ResourceProperties.ResourcePropertyDefinition.PropertyValueTypeEnum.SingleListValue
                    ResourcePropertyListValueDefinitionComboBox.Items.Clear()
                    For Each item As ResourceProperties.ResourcePropertyListValueDefinition In selectedProperty.ListValues
                        With ResourcePropertyListValueDefinitionComboBox
                            .Items.Add(item)
                            If item.Key.ToString.Equals(propertyFilter.SelectedListValueKey.ToString) Then
                                ResourcePropertyListValueDefinitionComboBox.SelectedItem = item

                                SetFilterValueForList(selectedProperty, propertyFilter, item)
                            End If

                        End With
                    Next
                    ResourcePropertyListValueDefinitionComboBox.Visible = True

                Case Else
                    Trace.Assert(String.Format("unexpected value type {0} for list property {1}", selectedProperty.PropertyListValue.ToString, selectedProperty.PropertyType.ToString))
            End Select
        Else
        End If
    End Sub

    Private Sub ResourcePropertyListValueDefinitionComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ResourcePropertyListValueDefinitionComboBox.SelectedIndexChanged
        Dim selectedDefProperty As ResourceProperties.ResourcePropertyDefinition = ResourcePropertyDefinitionComboBox.SelectedItem
        Dim selectedProperty As ResourceProperties.ResourcePropertyListValueDefinition = DirectCast(sender, ComboBox).SelectedItem
        Dim propertyFilter As ResourcePropertyFilterPredicate = CType(Me.Filter, ResourcePropertyFilterPredicate)

        If propertyFilter IsNot Nothing Then
            If selectedProperty IsNot Nothing Then
                propertyFilter.SelectedListValueName = selectedProperty.Title
                propertyFilter.SelectedListValueKey = selectedProperty.Key

                SetFilterValueForList(selectedDefProperty, propertyFilter, selectedProperty)
            Else
                propertyFilter.SelectedListValueName = String.Empty
                propertyFilter.SelectedListValueKey = Guid.Empty
            End If
        End If
    End Sub

    Private Sub SetFilterValueForList(filterdef As ResourceProperties.ResourcePropertyDefinition, ByRef pred As ResourcePropertyFilterPredicate, selectedProperty As ResourceProperties.ResourcePropertyListValueDefinition)
        If filterdef.FilterOnListNameColumn Then
            pred.Value = selectedProperty.Name
            pred.DisplayValue = selectedProperty.Title
        Else
            pred.Value = selectedProperty.Title
            pred.DisplayValue = selectedProperty.Title
        End If
    End Sub


End Class
