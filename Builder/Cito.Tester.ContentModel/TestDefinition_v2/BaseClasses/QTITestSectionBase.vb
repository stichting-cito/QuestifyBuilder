
Imports Cito.Tester.Common

Public MustInherit Class QTITestSectionBase
    Inherits GeneralTestSection


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(model As TestSection2, assessmentTestViewType As String)
        MyBase.New(model)
        AddDynamicPropertiesFromModel(model, assessmentTestViewType)
        ValidateAllProperties()
    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return Nothing
        End Get
    End Property



    Public Property Visible As Boolean
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of Boolean)("visible")
        End Get
        Set
            Me.TestSectionModel.SetPropertyValue("visible", value)
            Me.Validate("Visible")
        End Set
    End Property

    Public Property KeepTogether As Boolean
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of Boolean)("keepTogether")
        End Get
        Set
            Me.TestSectionModel.SetPropertyValue("keepTogether", value)
            Me.Validate("KeepTogether")
        End Set
    End Property

    Public Property SectionPart As SectionPart
        Get
            Return Me.TestSectionModel.GetPropertyValue(Of SectionPart)("sectionPart")
        End Get
        Set
            Me.TestSectionModel.SetPropertyValue("sectionPart", value)
        End Set
    End Property



    Public Overrides Sub AddDynamicPropertiesFromModel(testSection As TestSection2, assessmentTestViewType As String)
        MyBase.AddDynamicPropertiesFromModel(testSection, assessmentTestViewType)

        Me.TestSectionModel.AddDynamicPropertyIfNotExists("visible", GetType(Boolean), assessmentTestViewType, True)
        Me.TestSectionModel.AddDynamicPropertyIfNotExists("keepTogether", GetType(Boolean), assessmentTestViewType, False)
        Me.TestSectionModel.AddDynamicPropertyIfNotExists("sectionPart", GetType(SectionPart), assessmentTestViewType, Nothing)
    End Sub



    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Title")
        Me.Validate("Identifier")
    End Sub

End Class