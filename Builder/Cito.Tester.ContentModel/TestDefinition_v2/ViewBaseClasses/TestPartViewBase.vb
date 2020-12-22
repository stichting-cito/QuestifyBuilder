Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Cito.Tester.Common


Public MustInherit Class TestPartViewBase
    Inherits TestComponentBase


    Private _sections As TestSectionViewBaseCollection
    Private _assessmentTest As AssessmentTest2



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(testPartModel As TestPart2, assessmentTest As AssessmentTest2)
        MyBase.New(testPartModel)

        _assessmentTest = assessmentTest

        AddDynamicPropertiesFromModel(testPartModel)
    End Sub


    <XmlIgnore>
    Public Property Parent As AssessmentTest2
        Get
            Return _assessmentTest
        End Get
        Set
            _assessmentTest = value
        End Set
    End Property

    Public ReadOnly Property MaxScore As Double
        Get
            Return Me.TestPartModel.MaxScore
        End Get
    End Property

    Public ReadOnly Property Sections As TestSectionViewBaseCollection
        Get
            Return _sections
        End Get
    End Property

    Public ReadOnly Property TestPartModel As TestPart2
        Get
            Return DirectCast(Me.NodeModel, TestPart2)
        End Get
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property



    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)

    Protected Overrides ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Dim validateChildList As New List(Of IDataErrorInfo)
            validateChildList.Add(Me.Sections)
            Return New ReadOnlyCollection(Of IDataErrorInfo)(validateChildList)
        End Get
    End Property

    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation



    Public Overridable Sub AddDynamicPropertiesFromModel(testPartModel As TestPart2)
        Me.NodeModel = testPartModel
        _sections = New TestSectionViewBaseCollection(testPartModel.Sections, Me)

        Me.TestPartModel.AddDynamicPropertyIfNotExists("settingsCollection", GetType(SettingsCollection2), Nothing, New SettingsCollection2())
    End Sub





    Public MustOverride Function CreateNewTestSection() As TestSectionViewBase

    Public Sub GetDependencyResourcesForThisTestPart(ByRef allResources As ResourceEntryCollection, includeChildren As Boolean)
        Me.GetDependencyResourcesInThisNode(allResources)

        If includeChildren Then
            For Each testSectionView As TestSectionViewBase In Me.Sections
                testSectionView.GetDependencyResourcesForThisTestSection(allResources, True)
            Next
        End If
    End Sub


End Class