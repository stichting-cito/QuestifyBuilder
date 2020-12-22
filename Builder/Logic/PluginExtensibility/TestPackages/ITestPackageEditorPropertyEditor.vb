Imports Cito.Tester.ContentModel

Public Interface ITestPackageEditorPropertyEditor
    Inherits ITestPackagePropertyEditor, ITestResourcesEvents

    Property CurrentDataSource() As TestPackage

End Interface
