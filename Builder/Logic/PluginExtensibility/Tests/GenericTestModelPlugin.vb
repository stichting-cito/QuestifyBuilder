
Imports Cito.Tester.ContentModel

Public Class GenericTestModelPlugin
    Implements ITestModelPlugin

    Public Const PLUGIN_NAME As String = "GenericTest"

    Public ReadOnly Property Name As String Implements ITestModelPlugin.Name
        Get
            Return PLUGIN_NAME
        End Get
    End Property
    
    Public Function IsSupportedView(view As String) As Boolean Implements ITestModelPlugin.IsSupportedView
        Return view = Name
    End Function

    Public Function GetItemReference(itemRef As ItemReference2) As TestComponentViewBase Implements ITestModelPlugin.GetItemReference
        Return New GeneralItemReference(itemRef)
    End Function

    Public Function GetTestPart(testPartModel As TestPart2, assesmentTest As AssessmentTest2) As TestPartViewBase Implements ITestModelPlugin.GetTestPart
        Return New GeneralTestPart(testPartModel, assesmentTest)
    End Function

    Public Function GetTestSection(testSectionModel As TestSection2) As TestSectionViewBase Implements ITestModelPlugin.GetTestSection
        Return New GeneralTestSection(testSectionModel)
    End Function

    Public Function IsSupportedTest(viewType As Type) As Boolean Implements ITestModelPlugin.IsSupportedTest
        If viewType Is GetType(GeneralAssessmentTest) OrElse
          viewType Is GetType(GeneralTestPart) OrElse
          viewType Is GetType(GeneralItemReference) OrElse
          viewType Is GetType(GeneralTestSection) Then
            Return True
        End If

        Return False
    End Function

    Public Function ConstructTestSectionView(assessmentTest As TestSection2) As TestSectionViewBase Implements ITestModelPlugin.ConstructTestSectionView
        ' create new test section
        Dim returnValue As New GeneralTestSection(assessmentTest)

        ' iterate through components
        For Each component As TestComponent2 In assessmentTest.Components
            Dim componentViewToAdd As TestComponentViewBase

            If TypeOf component Is TestSection2 Then
                ' another section
                componentViewToAdd = ConstructTestSectionView(DirectCast(component, TestSection2))

            ElseIf TypeOf component Is ItemReference2 Then
                ' item reference
                componentViewToAdd = GetItemReference(DirectCast(component, ItemReference2))
            Else
                Throw New NotSupportedException($"Type '{component.GetType().FullName}' not supported in component collection")
            End If

            returnValue.Components.Add(componentViewToAdd)
        Next

        Return returnValue
    End Function

    Public Function ConstructTestPartView(testPart As TestPart2) As TestPartViewBase Implements ITestModelPlugin.ConstructTestPartView
        ' create new testpart
        Dim returnValue As New GeneralTestPart()
        returnValue.AddDynamicPropertiesFromModel(testPart)
        returnValue.ValidateAllProperties()

        ' iterate test sections
        For Each section As TestSection2 In testPart.Sections
            Dim testSectionView As TestSectionViewBase = ConstructTestSectionView(section)
            returnValue.Sections.Add(testSectionView)
        Next

        Return returnValue
    End Function

    Public Function CreateView(test As AssessmentTest2) As AssessmentTestViewBase Implements ITestModelPlugin.CreateView
        ' create root object and initialize this object with the test model.
        Dim returnValue As New GeneralAssessmentTest(test)

        ' link test parts
        For Each part As TestPart2 In test.TestParts
            Dim testPartView As TestPartViewBase = ConstructTestPartView(part)
            returnValue.TestParts.Add(testPartView)
        Next

        ' add view type to model when it is not already defined.
        If Not test.IncludedViews.Contains(Name) Then
            test.IncludedViews.Add(Name)
        End If

        Return returnValue
    End Function

End Class