Imports Cito.Tester.Common

Public Class GeneralTestSection
    Inherits TestSectionViewBase

    Private Const NAME As String = "GenericTest"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestSection2)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model, NAME)
        ValidateAllProperties()
    End Sub

    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New GeneralTestSectionValidator()
        End Get
    End Property

    Public Property ModuleHref As String
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of String)("moduleHref")
        End Get
        Set(value As String)
            Me.TestSectionModel.SetPropertyValue("moduleHref", value)
            Me.Validate("ModuleHref")
        End Set
    End Property

    Public Property DriverHref As String
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of String)("driverHref")
        End Get
        Set(value As String)
            Me.TestSectionModel.SetPropertyValue("driverHref", value)
            Me.Validate("DriverHref")
        End Set
    End Property

    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
        If Not String.IsNullOrEmpty(Me.ItemDataSource) Then
            allResources.AddResourceEntry(Me.ItemDataSource)
        End If

        If Not String.IsNullOrEmpty(Me.ModuleHref) Then
            allResources.AddResourceEntry(Me.ModuleHref)
        End If

        If Not String.IsNullOrEmpty(Me.DriverHref) Then
            allResources.AddResourceEntry(Me.DriverHref)
        End If
    End Sub

    Public Overrides Sub AddDynamicPropertiesFromModel(testSection As TestSection2, assessmentTestViewType As String)
        MyBase.AddDynamicPropertiesFromModel(testSection, assessmentTestViewType)
        Me.TestSectionModel.AddDynamicPropertyIfNotExists("driverHref", GetType(String), assessmentTestViewType, String.Empty)
        Me.TestSectionModel.AddDynamicPropertyIfNotExists("moduleHref", GetType(String), assessmentTestViewType, String.Empty)
    End Sub

    Public Overrides Function CreateNewItemReference() As ItemReferenceViewBase
        Dim itemRefModel As New ItemReference2()

        Return New GeneralItemReference(itemRefModel)
    End Function

    Public Overrides Function CreateNewTestSection() As TestSectionViewBase
        Dim testSectionModel As New TestSection2()

        Return New GeneralTestSection(testSectionModel)
    End Function

    Public Overrides Sub ValidateAllProperties()
        Validate("Title")
        Validate("Identifier")
        Me.Validate("DriverHref")
        Me.Validate("ModuleHref")
    End Sub
End Class