Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Cito.Tester.Common

Public MustInherit Class TestSetViewBase
    Inherits TestPackageComponentViewBase


    Private _components As TestPackageComponentViewBaseCollection



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(componentModel As TestSet)
        MyBase.New(componentModel)
    End Sub



    Protected Overrides ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Dim validateChildList As New List(Of IDataErrorInfo)
            validateChildList.Add(Me.Components)
            Return New ReadOnlyCollection(Of IDataErrorInfo)(validateChildList)
        End Get
    End Property


    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation




    Public ReadOnly Property Components As TestPackageComponentViewBaseCollection
        Get
            Return Me._components
        End Get
    End Property

    Public ReadOnly Property TestSetModel As TestSet
        Get
            Return DirectCast(Me.TestPackageComponentModel, TestSet)
        End Get
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property



    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)



    Public Overridable Shadows Sub AddDynamicPropertiesFromModel(testsetModel As TestSet)
        MyBase.AddDynamicPropertiesFromModel(DirectCast(testsetModel, TestPackageComponent))
        _components = New TestPackageComponentViewBaseCollection(testsetModel.Components, Me)
    End Sub



    Public Sub GetDependencyResourcesForThisTestset(ByRef allResources As ResourceEntryCollection, includeChildren As Boolean)
        Me.GetDependencyResourcesInThisNode(allResources)

        For Each componentView As TestPackageComponentViewBase In Me.Components
            Dim referenceView As TestReferenceViewBase = DirectCast(componentView, TestReferenceViewBase)
            referenceView.GetDependencyResourcesForThisTest(allResources)
        Next
    End Sub


End Class
