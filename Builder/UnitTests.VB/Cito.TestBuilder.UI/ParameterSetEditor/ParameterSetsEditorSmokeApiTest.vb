
Imports Questify.Builder.UI

<TestClass()>
Public Class ParameterSetsEditorSmokeApiTest

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub ValidateParameterEditors_SmokeTest()
        Dim pse As New ParameterSetsEditor

        pse.ValidateParameterEditors()

    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub PreItemSave_SmokeTest()
        Dim pse As New ParameterSetsEditor

        pse.PreItemSave()

    End Sub

    <TestMethod(), TestCategory("UILogic"), WorkItem(8900)>
    Public Sub CleanUpEditors_SmokeTest()
        Dim pse As New ParameterSetsEditor

        pse.CleanUpEditors()

    End Sub

End Class
