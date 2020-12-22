Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public MustInherit Class ItemReferenceViewBase
    Inherits TestComponentViewBase



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(componentModel As ItemReference2)
        MyBase.New(componentModel)
    End Sub



    Protected ReadOnly Property ItemReferenceModel As ItemReference2
        Get
            Return DirectCast(Me.TestComponentModel, ItemReference2)
        End Get
    End Property

    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation



    Public Property Active As Boolean
        Get
            Return Me.ItemReferenceModel.Active
        End Get
        Set
            Me.ItemReferenceModel.Active = value
            Me.Validate("Active")
        End Set
    End Property

    Public Property FirstItemInSection As Boolean
        Get
            Return Me.ItemReferenceModel.FirstItemInSection
        End Get
        Set
            Me.ItemReferenceModel.FirstItemInSection = value
        End Set
    End Property

    Public Property ItemFunctionalType As ItemFunctionalType
        Get
            Return Me.ItemReferenceModel.ItemFunctionalType
        End Get
        Set
            Me.ItemReferenceModel.ItemFunctionalType = value
            Me.Validate("ItemFunctionalType")
        End Set
    End Property

    Public ReadOnly Property ItemScoreTranslationTable As ItemScoreTranslationTable
        Get
            Return Me.ItemReferenceModel.ItemScoreTranslationTable
        End Get
    End Property

    Public Property Weight As Double
        Get
            Return Me.ItemReferenceModel.Weight
        End Get
        Set
            Me.ItemReferenceModel.Weight = value
        End Set
    End Property

    Public Property LastItemInTestPart As Boolean
        Get
            Return Me.ItemReferenceModel.LastItemInTestPart
        End Get
        Set
            Me.ItemReferenceModel.LastItemInTestPart = value
        End Set
    End Property

    Public Property SourceName As String
        Get
            Return Me.ItemReferenceModel.SourceName
        End Get
        Set
            Me.ItemReferenceModel.SourceName = value
            Me.Validate("SourceName")
        End Set
    End Property

    Public Property IsAnchorItem As Boolean
        Get
            Return Me.ItemReferenceModel.IsAnchorItem
        End Get
        Set
            Me.ItemReferenceModel.IsAnchorItem = value
        End Set
    End Property



    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)


    Public Overridable Shadows Sub AddDynamicPropertiesFromModel(itemRef As ItemReference2)
        MyBase.AddDynamicPropertiesFromModel(itemRef)
    End Sub


    Public Sub GetDependencyResourcesForThisItem(ByRef allResources As ResourceEntryCollection)
        Me.GetDependencyResourcesInThisNode(allResources)
    End Sub


End Class