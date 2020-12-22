
Imports Cito.Tester.Common

Public Class GeneralTestPackage
    Inherits TestPackageViewBase


    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub


    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("Title")
    End Sub


    Public Function CreateNewTestSet() As GeneralTestSet
        Dim newTestSetModel As New TestSet()

        Dim testSetView As New GeneralTestSet(newTestSetModel)

        Return testSetView
    End Function


    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New GeneralTestPackageValidator()
        End Get
    End Property



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestPackage)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testModel As TestPackage)
        MyBase.AddDynamicPropertiesFromModel(testModel)
    End Sub


End Class
