Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Cito.Tester.Common

Public MustInherit Class TestPackageViewBase
    Inherits ValidatingEntityBase


    Private _testPackageModel As TestPackage
    Private _testsets As TestSetViewBaseCollection




    Friend Sub New()
    End Sub

    Friend Sub New(testModel As TestPackage)
        Me.New()

        AddDynamicPropertiesFromModel(testModel)
    End Sub



    Protected Overrides ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Dim validateChildList As New List(Of IDataErrorInfo)
            validateChildList.Add(Me.TestSets)
            Return New ReadOnlyCollection(Of IDataErrorInfo)(validateChildList)
        End Get
    End Property

    Protected MustOverride Overrides ReadOnly Property Validator As IEntityValidation



    Public ReadOnly Property TestPackageModel As TestPackage
        Get
            Return _testPackageModel
        End Get
    End Property



    Public Property Identifier As String
        Get
            Return Me.TestPackageModel.Identifier
        End Get
        Set
            Me.TestPackageModel.Identifier = value
            Me.Validate("Identifier")
        End Set
    End Property


    Public ReadOnly Property SettingsCollection As SettingsCollection2
        Get
            Return Me.TestPackageModel.GetPropertyValue(Of SettingsCollection2)("settingsCollection")
        End Get
    End Property

    Public ReadOnly Property TestSets As TestSetViewBaseCollection
        Get
            Return _testsets
        End Get
    End Property

    Public Property Title As String
        Get
            Return Me.TestPackageModel.Title
        End Get
        Set
            Me.TestPackageModel.Title = value
            Me.Validate("Title")
        End Set
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property





    Private Shared Function FindTestReferenceInTestSetByName(testset As TestSetViewBase, nameToFind As String) As TestReferenceViewBase

        For Each comp As TestPackageComponentViewBase In testset.Components
            Dim item As TestReferenceViewBase = DirectCast(comp, TestReferenceViewBase)
            If item.SourceName = nameToFind Then
                Return item
            End If
        Next

        Return Nothing
    End Function


    Private Shared Function FindTestReferenceInTestSetByIdentifier(testSet As TestSetViewBase, identifierToFind As String) As TestReferenceViewBase

        For Each item As TestReferenceViewBase In testSet.Components
            If item.Identifier = identifierToFind Then
                return item
            End If
        Next
        Return Nothing
    End Function




    Protected MustOverride Sub GetDependencyResourcesInThisNode(ByRef allResources As ResourceEntryCollection)



    Public Overridable Sub AddDynamicPropertiesFromModel(testPackageModel As TestPackage)
        _testPackageModel = testPackageModel
        _testsets = New TestSetViewBaseCollection(testPackageModel.TestSets)

        Me.TestPackageModel.AddDynamicPropertyIfNotExists("settingsCollection", GetType(SettingsCollection2), Nothing, New SettingsCollection2())
    End Sub




    Public Function GetAllResourcesForTestPackage() As ResourceEntryCollection
        Dim testpackageResources As New ResourceEntryCollection

        Me.GetDependencyResourcesInThisNode(testpackageResources)

        For Each testSetView As TestSetViewBase In Me.TestSets
            testSetView.GetDependencyResourcesForThisTestset(testpackageResources, True)
        Next

        Return testpackageResources
    End Function



    Public Shadows Function GetTestSetByModel(testpartModel As TestPart2) As TestSetViewBase

        For Each testset As TestSetViewBase In Me.TestSets
            If testset.TestSetModel Is testpartModel Then
                return testset
            End If
        Next
        Return Nothing
    End Function



    Public Function GetTestReferenceByModel(testRefModel As TestReference) As TestReferenceViewBase
        Return GetTestReferenceByIdentifier(testRefModel.Identifier)
    End Function



    Public Function GetTestReferenceByName(name As String) As TestReferenceViewBase
        Dim result As TestReferenceViewBase = Nothing

        For Each part As TestSetViewBase In Me.TestSets
            result = FindTestReferenceInTestSetByName(part, name)
            If result IsNot Nothing Then
                Return result
            End If
        Next
        Return result
    End Function



    Public Function GetTestReferenceByIdentifier(identifier As String) As TestReferenceViewBase
        Dim result As TestReferenceViewBase = Nothing
        For Each testset As TestSetViewBase In Me.TestSets
            result = FindTestReferenceInTestSetByIdentifier(testset, identifier)
            If result IsNot Nothing Then
                Return result
            End If
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

        Return False
    End Function


    Public Function GetMD5HashOfModel() As Byte()
        Return SerializeHelper.GetMD5Hash(Me.TestPackageModel)
    End Function


End Class