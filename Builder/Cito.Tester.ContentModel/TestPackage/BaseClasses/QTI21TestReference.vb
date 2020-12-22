
Imports Cito.Tester.Common

<Serializable> _
Public Class QTI21TestReference
    Inherits TestReferenceViewBase


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




    Public Sub New(model As TestReference)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Sub AddDynamicPropertiesFromModel(testReferenceModel As TestReference)
        MyBase.AddDynamicPropertiesFromModel(testReferenceModel)
    End Sub

End Class
