
Imports Cito.Tester.Common

Public Class QTI21TestPackage
    Inherits TestPackageViewBase


    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub


    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("Title")
    End Sub







    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return Nothing
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestPackage)
        MyBase.New()
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub




    Public Overrides Sub AddDynamicPropertiesFromModel(testPackageModel As TestPackage)
        MyBase.AddDynamicPropertiesFromModel(testPackageModel)

    End Sub



End Class
