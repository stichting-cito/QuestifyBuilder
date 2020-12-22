Imports Cito.Tester.ContentModel

Public Interface ITestPackageModelPlugin
    ReadOnly Property Name As String
    Function GetTestSet(testSetModel As TestSet) As TestSetViewBase
    Function IsSupportedTestPackage(package As Type) As Boolean
    Function IsSupportedView(view As String) As Boolean
    Function ConstructTestReferenceView(model As TestReference) As TestPackageComponentViewBase
    Function ConstructTestSetView(model As TestSet) As TestSetViewBase
    Function CreateView(model As TestPackage) As TestPackageViewBase
End Interface
