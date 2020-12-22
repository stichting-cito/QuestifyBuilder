
Imports Cito.Tester.Common

Public MustInherit Class QTITestPartBase
    Inherits GeneralTestPart





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
            Return Nothing
        End Get
    End Property



    Public Property TimeLimits As TimeLimits
        Get
            Return Me.TestPartModel.GetPropertyValue(Of TimeLimits)("timeLimits")
        End Get
        Set
            Me.TestPartModel.SetPropertyValue("timeLimits", value)
        End Set
    End Property

    Public Property Qti21NavigationMode As Qti21NavigationMode
        Get
            Return Me.TestPartModel.GetPropertyValue(Of Qti21NavigationMode)("qti21NavigationMode")
        End Get
        Set
            Me.TestPartModel.SetPropertyValue("qti21NavigationMode", value)
            Me.Validate("Qti21NavigationMode")
        End Set
    End Property

    Public Property SubmissionMode As SubmissionMode
        Get
            Return Me.TestPartModel.GetPropertyValue(Of SubmissionMode)("submissionMode")
        End Get
        Set
            Me.TestPartModel.SetPropertyValue("submissionMode", value)
            Me.Validate("SubmissionMode")
        End Set
    End Property



    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub



    Public Overrides Sub AddDynamicPropertiesFromModel(testPartModel As TestPart2)
        MyBase.AddDynamicPropertiesFromModel(testPartModel)
    End Sub



    Public Overrides Sub ValidateAllProperties()
    End Sub


End Class