Imports Cito.Tester.ContentModel

Public Interface ITestEditorPlugin
    ReadOnly Property Name As String
    ReadOnly Property Description As String
    Function GetTestSectionPropertyEditor() As ITestSectionPartPropertyEditor
    Function GetTestPartPopertyEditor(ByVal assessmentTest As AssessmentTest2) As ITestPartPropertyEditor
    Function GetItemReferencePropertyEditor() As IItemReferencePropertyEditor
    Function GetTestPropertiesEditor() As IAssessmentTestPropertyEditor
    Function IsSupportedView(view As String) As Boolean
    Sub CodeChanged(testRef As TestPackage, newCodeName As String, oldCode As String)
    Sub UpdateName(testPackage As TestPackage, newCodeName As String, oldCode As String)
End Interface
