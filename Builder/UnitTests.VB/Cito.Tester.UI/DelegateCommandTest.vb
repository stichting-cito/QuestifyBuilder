
Imports Questify.Builder.UI.Commanding

<TestClass()>
Public Class DelegateCommandTest

    <TestMethod()> <TestCategory("Commands")> <WorkItem(8029)>
    Public Sub DelegateCommandCanExecute_WithoutPredicate_Test()
        'Arrange
        Dim executed As Boolean = False
        Dim command As New DelegateCommand(Of Object)("someName", Sub() executed = True)
      
        'Act
        Dim can As Boolean = command.CanExecute("")
      
        'Assert
        Assert.AreEqual(True, can, "Simple delegate without predicate should be able to execute.")
    End Sub

    <TestMethod()> <TestCategory("Commands")> <WorkItem(8029)>
    Public Sub DelegateCommandCanExecute_WithPredicate_Test()
        'Arrange
        Dim executed As Boolean = False
        Dim command As New DelegateCommand(Of Object)("someName", Sub() executed = True, Function() True)
       
        'Act
        Dim can As Boolean = command.CanExecute("")
       
        'Assert
        Assert.AreEqual(True, can)
    End Sub

End Class
