Imports Cito.Tester.ContentModel

Public Interface ITestSectionPartPropertyEditor
    Inherits ITestEditorPropertyEditor, ITestResourcesEvents

    Property CurrentDataSource() As TestSection2

    Event ResourceDialogRequest As EventHandler(Of ResourceDialogRequestEventArgs)
    Event SectionItemDatasourceDependentResourceChanged As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs)

End Interface
