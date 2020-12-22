
Imports Cito.Tester.Common

<Serializable> _
Public MustInherit Class QTIItemReferenceBase
    Inherits GeneralItemReference


    Private _timeSpend As Integer



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As ItemReference2)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return Nothing
        End Get
    End Property




    Public Property SectionPart As SectionPart
        Get
            Return Me.ItemReferenceModel.GetPropertyValue(Of SectionPart)("sectionPart")
        End Get
        Set
            Me.ItemReferenceModel.SetPropertyValue("sectionPart", value)
        End Set
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
        If Not String.IsNullOrEmpty(Me.SourceName) Then
            allResources.AddResourceEntry(Me.SourceName)
        End If
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(itemReference As ItemReference2)
        MyBase.AddDynamicPropertiesFromModel(itemReference)
    End Sub



    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
    End Sub


End Class