

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Cito.Tester.Common

Public MustInherit Class AssessmentTestViewBase
    Inherits ValidatingEntityBase


    Private _testModel As AssessmentTest2
    Private _testParts As TestPartViewBaseCollection



    Public Sub New()
    End Sub

    Public Sub New(testModel As AssessmentTest2)
        Me.New()

        AddDynamicPropertiesFromModel(testModel)
    End Sub



    Protected Overrides ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Dim validateChildList As New List(Of IDataErrorInfo)
            validateChildList.Add(Me.TestParts)
            Return New ReadOnlyCollection(Of IDataErrorInfo)(validateChildList)
        End Get
    End Property

    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation



    Public ReadOnly Property TestModel As AssessmentTest2
        Get
            Return _testModel
        End Get
    End Property



    Public Property Identifier As String
        Get
            Return Me.TestModel.Identifier
        End Get
        Set
            Me.TestModel.Identifier = value
            Me.Validate("Identifier")
        End Set
    End Property

    Public Property LockedForEdit As Boolean
        Get
            Return Me.TestModel.LockedForEdit
        End Get
        Set
            Me.TestModel.LockedForEdit = value
        End Set
    End Property

    Public ReadOnly Property MaxTestScore As Double
        Get
            Return Me.TestModel.MaxScore
        End Get
    End Property

    Public ReadOnly Property SettingsCollection2 As SettingsCollection2
        Get
            Return Me.TestModel.GetPropertyValue(Of SettingsCollection2)("settingsCollection")
        End Get
    End Property


    Public ReadOnly Property TestParts As TestPartViewBaseCollection
        Get
            Return _testParts
        End Get
    End Property

    Public Property Title As String
        Get
            Return Me.TestModel.Title
        End Get
        Set
            Me.TestModel.Title = value
            Me.Validate("Title")
        End Set
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property




    Private Shared Function FindItemReferenceInSectionByName(section As TestSectionViewBase, nameToFind As String) As ItemReferenceViewBase
        Dim result As ItemReferenceViewBase = Nothing

        For Each comp As TestComponentViewBase In section.Components
            If TypeOf comp Is ItemReferenceViewBase Then
                Dim item As ItemReferenceViewBase = DirectCast(comp, ItemReferenceViewBase)
                If item.SourceName = nameToFind Then
                    return item
                End If
            Else
                Dim innerSection As TestSectionViewBase = DirectCast(comp, TestSectionViewBase)
                result = FindItemReferenceInSectionByName(innerSection, nameToFind)
                If result IsNot Nothing Then
                    Return result
                End If
            End If
        Next

        Return result
    End Function


    Private Shared Function FindItemReferenceInSectionByIdentifier(section As TestSectionViewBase, identifierToFind As String) As ItemReferenceViewBase
        Dim result As ItemReferenceViewBase = Nothing

        For Each comp As TestComponentViewBase In section.Components
            If TypeOf comp Is ItemReferenceViewBase Then
                Dim item As ItemReferenceViewBase = DirectCast(comp, ItemReferenceViewBase)
                If item.Identifier = identifierToFind Then
                    return item
                End If
            Else
                Dim innerSection As TestSectionViewBase = DirectCast(comp, TestSectionViewBase)
                result = FindItemReferenceInSectionByIdentifier(innerSection, identifierToFind)
                If result IsNot Nothing Then
                    Return result
                End If
            End If
        Next

        Return result
    End Function


    Private Shared Function FindSectionModelInSection(section As TestSectionViewBase, modelToFind As TestSection2) As TestSectionViewBase
        Dim result As TestSectionViewBase = Nothing

        For Each comp As TestComponentViewBase In section.Components
            If TypeOf comp Is TestSectionViewBase Then
                Dim innerSection As TestSectionViewBase = DirectCast(comp, TestSectionViewBase)
                If innerSection.TestComponentModel Is modelToFind Then
                    return innerSection
                Else
                    return FindSectionModelInSection(innerSection, modelToFind)
                End If

            End If
        Next

        Return result
    End Function



    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)



    Public Overridable Sub AddDynamicPropertiesFromModel(testModel As AssessmentTest2)
        _testModel = testModel
        _testParts = New TestPartViewBaseCollection(testModel.TestParts)

        Me.TestModel.AddDynamicPropertyIfNotExists("settingsCollection", GetType(SettingsCollection2), Nothing, New SettingsCollection2())
    End Sub





    Public MustOverride Function CreateNewTestPart() As TestPartViewBase


    Public Function GetAllResourcesForTest() As ResourceEntryCollection
        Dim testResources As New ResourceEntryCollection

        Me.GetDependencyResourcesInThisNode(testResources)

        For Each testPartView As TestPartViewBase In Me.TestParts
            testPartView.GetDependencyResourcesForThisTestPart(testResources, True)
        Next

        Return testResources
    End Function

    Public Function GetAllItemReferenceViewBasesForTest() As ReadOnlyCollection(Of ItemReferenceViewBase)

        Dim allItemsInTestCollection As New List(Of ItemReferenceViewBase)

        For Each part As TestPartViewBase In Me.TestParts
            For Each section As TestSectionViewBase In part.Sections
                For Each comp As TestComponentViewBase In section.Components
                    GetItemsForSection(comp, allItemsInTestCollection)
                Next
            Next
        Next

        Return New ReadOnlyCollection(Of ItemReferenceViewBase)(allItemsInTestCollection)
    End Function

    Public Function GetAllItemReferencesForTest() As ReadOnlyCollection(Of ItemReference2)
        If TestModel Is Nothing Then
            Return Nothing
        End If

        Return TestModel.GetAllItemReferencesInTest()
    End Function

    Private Sub GetItemsForSection(component As TestComponentViewBase, ByRef items As List(Of ItemReferenceViewBase))
        If TypeOf component Is ItemReferenceViewBase Then
            Dim itemRef As ItemReferenceViewBase = DirectCast(component, ItemReferenceViewBase)
            items.Add(itemRef)
        Else
            Dim innerSection As TestSectionViewBase = DirectCast(component, TestSectionViewBase)
            For Each comp As TestComponentViewBase In innerSection.Components
                GetItemsForSection(comp, items)
            Next
        End If
    End Sub


    Public Shadows Function GetTestPartByModel(testpartModel As TestPart2) As TestPartViewBase

        For Each part As TestPartViewBase In Me.TestParts
            If part.TestPartModel Is testpartModel Then
                return part
            End If
        Next

        Return Nothing
    End Function


    Public Function GetSectionByModel(sectionModel As TestSection2) As TestSectionViewBase

        For Each part As TestPartViewBase In Me.TestParts
            For Each section As TestSectionViewBase In part.Sections
                If section.TestComponentModel Is sectionModel Then
                    return section
                Else
                    return FindSectionModelInSection(section, sectionModel)
                End If
            Next
        Next

        Return Nothing
    End Function


    Public Function GetItemReferenceByModel(itemRefModel As ItemReference2) As ItemReferenceViewBase
        Return GetItemReferenceByIdentifier(itemRefModel.Identifier)
    End Function

    Public Function GetItemReferenceByName(name As String) As ItemReferenceViewBase
        Dim result As ItemReferenceViewBase = Nothing

        For Each part As TestPartViewBase In Me.TestParts
            For Each section As TestSectionViewBase In part.Sections
                result = FindItemReferenceInSectionByName(section, name)
                If result IsNot Nothing Then
                    return result
                End If
            Next
        Next

        Return Nothing
    End Function

    Public Function GetItemReferenceByIdentifier(identifier As String) As ItemReferenceViewBase
        Dim result As ItemReferenceViewBase = Nothing

        For Each part As TestPartViewBase In Me.TestParts
            For Each section As TestSectionViewBase In part.Sections
                result = FindItemReferenceInSectionByIdentifier(section, identifier)
                If result IsNot Nothing Then
                    Return result
                End If
            Next
        Next

        Return result
    End Function

    Public Function IsItemReferenceInSection(itemRef As ItemReferenceViewBase, sectionToFind As TestSectionViewBase, includeChilds As Boolean) As Boolean
        Dim section As TestSectionViewBase = DirectCast(itemRef.Parent, TestSectionViewBase)

        Do Until section Is Nothing
            If section Is sectionToFind Then
                return True
            End If

            If includeChilds Then
                If TypeOf section.Parent Is TestSectionViewBase Then
                    section = DirectCast(section.Parent, TestSectionViewBase)
                Else
                    section = Nothing
                End If
            Else
                section = Nothing
            End If
        Loop

        Return false
    End Function


    Public Function GetMD5HashOfModel() As Byte()
        Return SerializeHelper.GetMD5Hash(Me.TestModel)
    End Function


End Class