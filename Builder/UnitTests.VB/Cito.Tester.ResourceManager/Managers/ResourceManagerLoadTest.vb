Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cito.Tester.ResourceManager
Imports System.IO

<TestClass()>
Public Class ResourceManagerLoadTest

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")>
    <DeploymentItem("Cito.Tester.ResourceManager\LeegBank-2012-06-13_TB2.1.export")>
    Public Sub LoadEmptySet_test()
        'Arrange
        'System.IO.Path.Combine(packageUrl, "Manifest.xml"
        Dim toImport As String = Path.Combine(Environment.CurrentDirectory, "LeegBank-2012-06-13_TB2.1.export")
        If File.Exists(toImport) Then
            Dim uri As New Uri(Path.Combine(toImport, "manifest.xml")) 'yeah this is kinda weird,.. but it's called like this,.. plz ask Ruben if possible.

            'Act
            Dim manifest As ResourceManifest
            manifest = ResourceManifest.PreLoad(uri)
            'Verify
            Assert.IsTrue(manifest IsNot Nothing)
        Else
            'This is not part of the test,.. but I'm just verifying the legal import file still exists.
            Assert.Fail("Missing TestFile!!!!") 'obviously this should NOT occur!!!
        End If
    End Sub

    <TestMethod()> <TestCategory("ResourceManager")> <Owner("FlBr")> <TestCategory("BugFix")>
    <DeploymentItem("Cito.Tester.ResourceManager\invalidFile-2012-06-13_TB2.1.wrong")> <WorkItem(7853)>
    Public Sub LoadInvalidFile_ExpectsNoManifest_test()
        'TestBuilder Importeren: TestBuilder Crashed met foutmelding bij het importeren van een niet ondersteund bestands formaat.

        'Arrange
        Dim toImport As String = Path.Combine(Environment.CurrentDirectory, "invalidFile-2012-06-13_TB2.1.wrong")
        If File.Exists(toImport) Then
            Dim uri As New Uri(Path.Combine(toImport, "manifest.xml")) 'yeah this is kinda weird,.. but it's called like this,.. plz ask Ruben if possible.

            'Act
            Dim manifest As ResourceManifest
            manifest = ResourceManifest.PreLoad(uri)
            'Verify
            Assert.IsTrue(manifest Is Nothing) '<--We expect NO manifest
        Else
            'This is not part of the test,.. but I'm just verifying the legal import file still exists.
            Assert.Fail("Missing TestFile!!!!") 'obviously this should NOT occur!!!
        End If
    End Sub


End Class
