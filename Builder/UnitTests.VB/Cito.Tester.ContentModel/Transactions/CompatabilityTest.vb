Imports System.Reflection
Imports Cito.Tester.ContentModel
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class CompatabilityTest

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("Compatibility")> <TestCategory("BugFix")> <Owner("FlBr")>
    <WorkItem(7924)>
    Public Sub AnchorConstructorTest()
        'This is not really a test, but just a way to keep a constructor existing
        'why:
        'The retry plugin, uses this constructor, but since the new model 2.0.0.0, new types exist.
        'We need to add a "Does the constructor still exist test". Since the need for existence is
        'not required/verified by compiling. Since retry is build against model 1.0.0.0 .
        'When testplayer runs, there is a little trick to use the retry plugin with content model 2.0.0.0 
        'and if this constructor is gone, we will get a runtime error.

        'Arrange
        'Act
        'Compile time test.
        Dim tst As New ItemRefTransaction(New TestSection, New ItemReference) 'This should exist

        'Reflection double test.
        Dim t As Type = GetType(ItemRefTransaction)
        Dim ctor As ConstructorInfo = t.GetConstructor(New Type() {GetType(TestSection),
                                                                                   GetType(ItemReference)})

        'Assert
        Assert.IsNotNull(ctor, "The constructor that exists for compatibility issues has been removed")
    End Sub

End Class
