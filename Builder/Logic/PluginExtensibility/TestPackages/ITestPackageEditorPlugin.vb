Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Interface ITestPackageEditorPlugin
    ReadOnly Property Name As String
    ReadOnly Property Description As String
    Function GetTestPackagePropertiesEditor() As ITestPackageEditorPropertyEditor
    Function GetTestReferencePropertiesEditor() As ITestReferenceEditorPropertyEditor
    Function GetTestSetPropertiesEditor() As ITestSetEditorPropertyEditor
    Function IsSupportedView(view As String) As Boolean
    Sub CodeChanged(resourceEntity As TestPackageResourceEntity, newCodeName As String, oldCode As String)
    Sub LockTestOrderInTestSet(testSet As TestSet, propertyEditors As IEnumerable(Of ITestPackagePropertyEditor), doLock As Boolean)
    Sub DeleteTestPackageComponent(testPackage As TestPackage, selectedComponents As List(Of TestPackageNode), resourceEntity As TestPackageResourceEntity)
    Sub PreSave(testPackage As TestPackage)
    Sub AddTests(addToTestSet As TestSet, propertyEditors As IEnumerable(Of ITestPackagePropertyEditor))
End Interface
