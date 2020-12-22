
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class WordAssessmentTest
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



    Protected Overrides ReadOnly Property Validator() As IEntityValidation
        Get
            Return New WordAssessmentTestValidator()
        End Get
    End Property



    Public Property PrintForm() As PrintFormCollection
        Get
            Return Me.TestModel.GetPropertyValue(Of Cito.Tester.ContentModel.PrintFormCollection)("PrintForm")
        End Get
        Set(value As PrintFormCollection)
            Me.TestModel.SetPropertyValue("PrintForm", value)
            Me.Validate("PrintForm")
        End Set
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testModel As AssessmentTest2)
        MyBase.AddDynamicPropertiesFromModel(testModel)

        Me.TestModel.AddDynamicPropertyIfNotExists("PrintForm", GetType(PrintFormCollection), PaperBasedTestPlugin.PLUGIN_NAME, New PrintFormCollection)
    End Sub




    Public Overrides Function CreateNewTestPart() As TestPartViewBase
        Dim newTestPartModel As New TestPart2()

        Dim testPartView As New WordTestPart(newTestPartModel, _testModel)

        Return testPartView
    End Function

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
        Me.Validate("PrintForm")
    End Sub


End Class