
Imports Cito.Tester.Common


Public Class GeneralTestPart
    Inherits TestPartViewBase


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestPart2, assessmentTest As AssessmentTest2)
        MyBase.New(model, assessmentTest)
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New GeneralTestPartValidator()
        End Get
    End Property





    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testPartModel As TestPart2)
        MyBase.AddDynamicPropertiesFromModel(testPartModel)
    End Sub




    Public Overrides Function CreateNewTestSection() As TestSectionViewBase
        Dim newTestSectionModel As New TestSection2()

        Dim testSectionView As New GeneralTestSection(newTestSectionModel)

        Return testSectionView
    End Function

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
    End Sub


End Class