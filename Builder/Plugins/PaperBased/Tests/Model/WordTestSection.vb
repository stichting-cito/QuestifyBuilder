
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class WordTestSection
    Inherits TestSectionViewBase


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestSection2)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model, PaperBasedTestPlugin.PLUGIN_NAME)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator() As IEntityValidation
        Get
            Return New WordTestSectionValidator()
        End Get
    End Property



    Public Property IsSampleSection() As Boolean
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of Boolean)("isSampleSection")
        End Get
        Set(value As Boolean)
            Me.TestSectionModel.SetPropertyValue("isSampleSection", value)
            Me.Validate("IsSampleSection")
        End Set
    End Property




    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)

        If Not String.IsNullOrEmpty(Me.ItemDataSource) Then
            allResources.AddResourceEntry(Me.ItemDataSource)
        End If
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testSection As TestSection2, assessmentTestViewType As String)
        MyBase.AddDynamicPropertiesFromModel(testSection, assessmentTestViewType)

        Me.TestSectionModel.AddDynamicPropertyIfNotExists("isSampleSection", GetType(Boolean), Nothing, False)
    End Sub




    Public Overrides Function CreateNewItemReference() As ItemReferenceViewBase
        Dim itemRefModel As New ItemReference2()

        Dim returnValue As New WordItemReference(itemRefModel)

        Return returnValue
    End Function


    Public Overrides Function CreateNewTestSection() As TestSectionViewBase
        Dim testSectionModel As New TestSection2()

        Dim returnValue As New WordTestSection(testSectionModel)

        Return returnValue
    End Function

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
        Me.Validate("IsSampleSection")
    End Sub


End Class