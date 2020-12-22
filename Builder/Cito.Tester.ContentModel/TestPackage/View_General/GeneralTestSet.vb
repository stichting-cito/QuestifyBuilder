
Imports Cito.Tester.Common

Public Class GeneralTestSet
    Inherits TestSetViewBase


    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub


    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("Title")
    End Sub

    Public Overrides Sub AddDynamicPropertiesFromModel(model As TestSet)
        MyBase.AddDynamicPropertiesFromModel(model)
    End Sub


    Public Function CreateNewTestReference() As GeneralTestReference
        Dim testRefModel As New TestReference()

        Dim returnValue As New GeneralTestReference(testRefModel)

        Return returnValue
    End Function


    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New GeneralTestSetValidator()
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestSet)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub

End Class
