Imports Cito.Tester.ContentModel

Public Interface ITestModelPlugin
    ReadOnly Property Name As String
    Function GetItemReference(ByVal itemRef As ItemReference2) As TestComponentViewBase
    Function GetTestPart(testPartModel As TestPart2, assesmentTest As AssessmentTest2) As TestPartViewBase
    Function GetTestSection(testSectionModel As TestSection2) As TestSectionViewBase
    Function IsSupportedTest(viewType As Type) As Boolean
    Function ConstructTestSectionView(assessmentTest As TestSection2) As TestSectionViewBase
    Function ConstructTestPartView(testPart As TestPart2) As TestPartViewBase
    Function CreateView(test As AssessmentTest2) As AssessmentTestViewBase
    Function IsSupportedView(view As String) As Boolean
End Interface
