Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public MustInherit Class QTIAssessmentTestBase
    Inherits GeneralAssessmentTest

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
            Return Nothing
        End Get
    End Property

    Protected MustOverride ReadOnly Property AssessmentTestViewType As String



    <XmlAttribute("timeLimits")>
    Public Property TimeLimits As TimeLimits
        Get
            Return Me.TestModel.GetPropertyValue(Of TimeLimits)("timeLimits")
        End Get
        Set
            Me.TestModel.SetPropertyValue("timeLimits", value)
            Me.Validate("TimeLimits")
        End Set
    End Property



    Public Property ToolName As String
        Get
            Return Me.TestModel.GetPropertyValue(Of String)("toolName")
        End Get
        Set
            Me.TestModel.SetPropertyValue("toolName", value)
            Me.Validate("ToolName")
        End Set
    End Property

    Public Property ToolVersion As String
        Get
            Return Me.TestModel.GetPropertyValue(Of String)("toolVersion")
        End Get
        Set
            Me.TestModel.SetPropertyValue("toolVersion", value)
            Me.Validate("ToolVersion")
        End Set
    End Property





    Protected Overrides Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)
    End Sub


    Public Overrides Sub AddDynamicPropertiesFromModel(testModel As AssessmentTest2)
        MyBase.AddDynamicPropertiesFromModel(testModel)
        Me.TestModel.AddDynamicPropertyIfNotExists("toolName", GetType(String), AssessmentTestViewType, Application.ProductName)
        Me.TestModel.AddDynamicPropertyIfNotExists("toolVersion", GetType(String), AssessmentTestViewType,
                                                   $"{My.Application.Info.Version.Major}.{My.Application.Info.Version.Minor}")
        Me.TestModel.AddDynamicPropertyIfNotExists("timeLimits", GetType(TimeLimits), AssessmentTestViewType, Nothing)
    End Sub

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("Title")
        Me.Validate("TimeLimits")
        Me.Validate("ToolName")
        Me.Validate("ToolVersion")
    End Sub

End Class