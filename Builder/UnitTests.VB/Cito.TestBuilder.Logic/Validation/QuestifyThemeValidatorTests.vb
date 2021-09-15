
Imports System.IO
Imports Questify.Builder.Logic

<TestClass()>
Public Class QuestifyThemeValidatorTests
    <TestMethod()>
    <DeploymentItem("Cito.TestBuilder.Logic\Validation\basic-qade-theme.zip")>
    Public Sub ZipFileIsQuestifyThemeFile()
        'Arrange
        Dim result As Boolean = False
        Dim errorMessages As New List(Of String)()
        Dim fullname As String = System.IO.Path.Combine(Environment.CurrentDirectory, "basic-qade-theme.zip") : Assert.IsTrue(File.Exists(fullname))

        'Act
        result = QuestifyThemeValidator.TryValidate(fullname, errorMessages)

        'Assert
        Assert.IsTrue(result)
        Assert.IsTrue(errorMessages.Count = 0)
    End Sub
End Class
