
Imports Cito.Tester.Common

Public Class GeneralAssessmentTest
    Inherits AssessmentTestViewBase


    Private _testModel As AssessmentTest2



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As AssessmentTest2)
        Me.New()
        AddDynamicPropertiesFromModel(model)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New GeneralAssessmentTestValidator()
        End Get
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testModel As AssessmentTest2)
        MyBase.AddDynamicPropertiesFromModel(testModel)
    End Sub




    Public Overrides Function CreateNewTestPart() As TestPartViewBase
        Dim newTestPartModel As New TestPart2()

        Dim testPartView As New GeneralTestPart(newTestPartModel, _testModel)

        Return testPartView
    End Function

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
    End Sub


End Class