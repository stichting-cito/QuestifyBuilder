Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Cito.Tester.Common


Public MustInherit Class TestSectionViewBase
    Inherits TestComponentViewBase

    Private _components As TestComponentViewBaseCollection
    Private _logicVariables As SerializableGenericDictionary(Of String, String)

    Public Sub New()
        MyBase.New()

        Me._logicVariables = New SerializableGenericDictionary(Of String, String)
    End Sub

    Public Sub New(componentModel As TestSection2)
        MyBase.New(componentModel)

        Me._logicVariables = New SerializableGenericDictionary(Of String, String)
    End Sub

    Protected Overrides ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Dim validateChildList As New List(Of IDataErrorInfo)
            validateChildList.Add(Me.Components)
            Return New ReadOnlyCollection(Of IDataErrorInfo)(validateChildList)
        End Get
    End Property

    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation

    Public ReadOnly Property Components As TestComponentViewBaseCollection
        Get
            Return Me._components
        End Get
    End Property

    Public Property ItemDataSource As String
        Get
            Return Me.TestSectionModel.ItemDataSource
        End Get
        Set
            Me.TestSectionModel.ItemDataSource = value
            Me.Validate("ItemDataSource")
        End Set
    End Property

    Public ReadOnly Property LogicVariables As SerializableGenericDictionary(Of String, String)
        Get
            Return _logicVariables
        End Get
    End Property

    Public Property PickedComponents As Integer
        Get
            Return Me.TestSectionModel.PickedComponents
        End Get
        Set
            Me.TestSectionModel.PickedComponents = value
        End Set
    End Property

    Public Property SectionType As enumSectionType
        Get
            Return Me.TestSectionModel.SectionType
        End Get
        Set
            Me.TestSectionModel.SectionType = value
            Me.Validate("SectionType")
        End Set
    End Property

    Public ReadOnly Property TestSectionModel As TestSection2
        Get
            Return DirectCast(Me.TestComponentModel, TestSection2)
        End Get
    End Property

    Public Property ItemWeightForVariantTests As Double
        Get
            Return Me.TestSectionModel.ItemWeightForVariantTests
        End Get
        Set
            Me.TestSectionModel.ItemWeightForVariantTests = value
        End Set
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property

    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)

    Public Overridable Shadows Sub AddDynamicPropertiesFromModel(testSectionModel As TestSection2, assessmentViewType As String)
        MyBase.AddDynamicPropertiesFromModel(DirectCast(testSectionModel, TestComponent2))
        _components = New TestComponentViewBaseCollection(testSectionModel.Components, Me)
    End Sub

    Public Sub GetDependencyResourcesForThisTestSection(ByRef allResources As ResourceEntryCollection, includeChildren As Boolean)
        Me.GetDependencyResourcesInThisNode(allResources)

        For Each componentView As TestComponentViewBase In Me.Components
            If TypeOf componentView Is TestSectionViewBase AndAlso includeChildren Then
                Dim sectionView As TestSectionViewBase = DirectCast(componentView, TestSectionViewBase)
                sectionView.GetDependencyResourcesForThisTestSection(allResources, True)
            End If

            If TypeOf componentView Is ItemReferenceViewBase Then
                Dim referenceView As ItemReferenceViewBase = DirectCast(componentView, ItemReferenceViewBase)
                referenceView.GetDependencyResourcesForThisItem(allResources)
            End If
        Next
    End Sub

    Public Function ContainsPickedComponents() As Boolean
        Me.TestSectionModel.ContainsPickedComponents()
    End Function

    Public MustOverride Function CreateNewTestSection() As TestSectionViewBase

    Public MustOverride Function CreateNewItemReference() As ItemReferenceViewBase
End Class
