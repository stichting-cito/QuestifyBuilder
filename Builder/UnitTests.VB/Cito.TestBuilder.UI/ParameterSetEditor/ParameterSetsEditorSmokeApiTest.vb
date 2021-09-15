
Imports Questify.Builder.UI

''' <summary>
''' These are "smoke" tests. No public should fail with just simple initialization.
''' </summary>
<TestClass()>
Public Class ParameterSetsEditorSmokeApiTest

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub ValidateParameterEditors_SmokeTest()
        'Arrange
        Dim pse As New ParameterSetsEditor
       
        'Act
        pse.ValidateParameterEditors() 'This should not fail
        
        'Assert
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub PreItemSave_SmokeTest()
        'Arrange
        Dim pse As New ParameterSetsEditor
        
        'Act
        pse.PreItemSave() 'This should not fail
        
        'Assert
    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub CleanUpEditors_SmokeTest()
        'Arrange
        Dim pse As New ParameterSetsEditor
        
        'Act
        pse.CleanUpEditors() 'This should not fail
        
        'Assert
    End Sub

End Class
