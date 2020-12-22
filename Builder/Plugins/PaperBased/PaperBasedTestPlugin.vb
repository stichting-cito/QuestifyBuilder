Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class PaperBasedTestPlugin
    Implements ITestEditorPlugin, ITestModelPlugin

    Friend Shared PLUGIN_NAME As String = "Word"

    Public ReadOnly Property Name As String Implements ITestModelPlugin.Name, ITestEditorPlugin.Name
        Get
            Return PLUGIN_NAME
        End Get
    End Property

    Public ReadOnly Property Description As String Implements ITestEditorPlugin.Description
        Get
            Return My.Resources.AssessmentTestViewType_Word
        End Get
    End Property

    Public Function IsSupportedView(view As String) As Boolean Implements ITestModelPlugin.IsSupportedView, ITestEditorPlugin.IsSupportedView
        Return view = Name
    End Function

    Public Function GetItemReference(itemRef As ItemReference2) As TestComponentViewBase Implements ITestModelPlugin.GetItemReference
        Return New WordItemReference(itemRef)
    End Function

    Public Function GetTestSectionPropertyEditor() As ITestSectionPartPropertyEditor Implements ITestEditorPlugin.GetTestSectionPropertyEditor
        Return New Word_TestSectionPropertiesEditor()
    End Function

    Public Function GetTestPartPopertyEditor(assessmentTest As AssessmentTest2) As ITestPartPropertyEditor Implements ITestEditorPlugin.GetTestPartPopertyEditor
        Return New Word_TestPartPropertiesEditor(assessmentTest)
    End Function

    Public Function GetItemReferencePropertyEditor() As IItemReferencePropertyEditor Implements ITestEditorPlugin.GetItemReferencePropertyEditor
        Return New Word_ItemReferencePropertiesEditor()
    End Function

    Public Function GetTestPropertiesEditor() As IAssessmentTestPropertyEditor Implements ITestEditorPlugin.GetTestPropertiesEditor
        Return New Word_AssessmentTestPropertiesEditor
    End Function

    Public Function GetTestPart(testPartModel As TestPart2, assesmentTest As AssessmentTest2) As TestPartViewBase Implements ITestModelPlugin.GetTestPart
        Return New WordTestPart(testPartModel, assesmentTest)
    End Function

    Public Function GetTestSection(testSectionModel As TestSection2) As TestSectionViewBase Implements ITestModelPlugin.GetTestSection
        Return New WordTestSection(testSectionModel)
    End Function

    Public Function IsSupportedTest(viewType As Type) As Boolean Implements ITestModelPlugin.IsSupportedTest
        If viewType Is GetType(WordAssessmentTest) OrElse
          viewType Is GetType(WordTestPart) OrElse
          viewType Is GetType(WordItemReference) OrElse
          viewType Is GetType(WordTestSection) Then
            Return True
        End If

        Return False
    End Function

    Public Function ConstructTestSectionView(assessmentTest As TestSection2) As TestSectionViewBase Implements ITestModelPlugin.ConstructTestSectionView
        Dim returnValue As New WordTestSection(assessmentTest)

        For Each component As TestComponent2 In assessmentTest.Components
            Dim componentViewToAdd As TestComponentViewBase

            If TypeOf component Is TestSection2 Then
                componentViewToAdd = ConstructTestSectionView(DirectCast(component, TestSection2))

            ElseIf TypeOf component Is ItemReference2 Then
                componentViewToAdd = GetItemReference(DirectCast(component, ItemReference2))
            Else
                Throw New NotSupportedException($"Type '{component.GetType().FullName}' not supported in component collection")
            End If

            returnValue.Components.Add(componentViewToAdd)
        Next

        Return returnValue
    End Function

    Public Function ConstructTestPartView(testPart As TestPart2) As TestPartViewBase Implements ITestModelPlugin.ConstructTestPartView
        Dim returnValue As New WordTestPart()
        returnValue.AddDynamicPropertiesFromModel(testPart)
        returnValue.ValidateAllProperties()

        For Each section As TestSection2 In testPart.Sections
            Dim testSectionView As TestSectionViewBase = ConstructTestSectionView(section)
            returnValue.Sections.Add(testSectionView)
        Next

        Return returnValue
    End Function

    Public Function CreateView(test As AssessmentTest2) As AssessmentTestViewBase Implements ITestModelPlugin.CreateView
        Dim returnValue As New WordAssessmentTest(test)

        For Each part As TestPart2 In test.TestParts
            Dim testPartView As TestPartViewBase = ConstructTestPartView(part)
            returnValue.TestParts.Add(testPartView)
        Next

        If Not test.IncludedViews.Contains(Name) Then
            test.IncludedViews.Add(Name)
        End If

        Return returnValue
    End Function

    Public Sub CodeChanged(testRef As TestPackage, newCodeName As String, oldCode As String) Implements ITestEditorPlugin.CodeChanged
    End Sub

    Public Sub UpdateName(testPackage As TestPackage, newCodeName As String, oldCode As String) Implements ITestEditorPlugin.UpdateName
    End Sub
End Class
