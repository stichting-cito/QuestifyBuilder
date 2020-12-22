
Imports Cito.Tester.Common

Public MustInherit Class TestReferenceViewBase
    Inherits TestPackageComponentViewBase



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(componentModel As TestReference)
        MyBase.New(componentModel)
    End Sub



    Protected ReadOnly Property TestReferenceModel As TestReference
        Get
            Return DirectCast(Me.TestPackageComponentModel, TestReference)
        End Get
    End Property


    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation




    Public Property SourceName As String
        Get
            Return Me.TestReferenceModel.SourceName
        End Get
        Set
            Me.TestReferenceModel.SourceName = value
            Me.Validate("SourceName")
        End Set
    End Property



    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)



    Public Overridable Shadows Sub AddDynamicPropertiesFromModel(testReference As TestReference)
        MyBase.AddDynamicPropertiesFromModel(DirectCast(testReference, TestReference))

    End Sub



    Public Sub GetDependencyResourcesForThisTest(ByRef allResources As ResourceEntryCollection)
        Me.GetDependencyResourcesInThisNode(allResources)
    End Sub


End Class