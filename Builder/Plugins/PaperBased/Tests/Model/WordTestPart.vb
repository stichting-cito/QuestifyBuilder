Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common


Public Class WordTestPart
    Inherits TestPartViewBase


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestPart2, assessmentTest As AssessmentTest2)
        MyBase.New(model, assessmentTest)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator() As IEntityValidation
        Get
            Return New WordTestPartValidator()
        End Get
    End Property



    Public Property DisplaySectionHeaderPage() As Boolean
        Get
            Return Me.TestPartModel.GetPropertyValue(Of Boolean)("displaySectionHeaderPage")
        End Get
        Set(value As Boolean)
            Me.TestPartModel.SetPropertyValue("displaySectionHeaderPage", value)
            Me.Validate("DisplaySectionHeaderPage")
        End Set
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testPartModel As TestPart2)
        MyBase.AddDynamicPropertiesFromModel(testPartModel)

        Me.TestPartModel.AddDynamicPropertyIfNotExists("displaySectionHeaderPage", GetType(Boolean), Nothing, False)
    End Sub




    Public Overrides Function CreateNewTestSection() As TestSectionViewBase
        Dim newTestSectionModel As New TestSection2()

        Dim testSectionView As New WordTestSection(newTestSectionModel)

        Return testSectionView
    End Function

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
        Me.Validate("DisplaySectionHeaderPage")
    End Sub


End Class