
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class WordItemReference
    Inherits ItemReferenceViewBase


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As ItemReference2)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator() As IEntityValidation
        Get
            Return New WordItemReferenceValidator()
        End Get
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
        If Not String.IsNullOrEmpty(Me.SourceName) Then
            allResources.AddResourceEntry(Me.SourceName)
        End If
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testSection As ItemReference2)
        MyBase.AddDynamicPropertiesFromModel(testSection)
    End Sub


    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
        Me.Validate("SourceName")
        Me.Validate("ItemFunctionalType")
    End Sub

End Class