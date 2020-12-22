﻿'The following code was generated by Microsoft Visual Studio 2005.
'The test owner should check each test for validity.
Imports System.IO
Imports System.Diagnostics
Imports Cito.Tester.Common
Imports Cito.Tester.Server.API
Imports Cito.Tester.Server.API.ContentModel
Imports System.Threading
Imports Questify.Builder.Packaging
Imports Questify.Builder.UnitTests.Framework

'''<summary>
'''This is a test class for Cito.Tester.ResourceManager.CitoTesterServerPackageManager and is intended
'''to contain all Cito.Tester.ResourceManager.CitoTesterServerPackageManager Unit Tests
'''</summary>
<TestClass()> _
Public Class CitoTesterServerPackageManagerTest

    '''<summary>
    '''A test for DownloadPackage(ByVal System.Uri)
    '''</summary>
    <TestMethod()> <Ignore()> <Description("Uitgeschakeld ivm verhuizen unit tests,... todo: Repareren.")>
    Public Sub CitoTesterServerPackageManager_DownloadPackageTest()
        Dim session As SessionInfo = CTContentSampleDataHelper.GenerateNewSessionInfoObject()

        ' create secure session on server
        Dim authService As New AdministrationServices("LOCALTESTRUN\unittest", "=7ecHaw8-aq_pe!32geWR3s?")
        Dim secureSession As SecureSessionValue = authService.CreateSecureSession(session.ApplicationId, session.PackageLocation.Uri.ToString(), session.TestTaker.Name, session.SessionId, session.TestTaker.Id, TestPlayerMode.Normal, Nothing, Nothing)
        TestPlayerServices.Instance.InitServiceWithSecureToken(secureSession.Token, secureSession.Checksum)

        Dim target As CitoTesterServerPackageManager = New CitoTesterServerPackageManager()

        Dim downloadResult As Boolean = target.DownloadPackage("")
        Assert.IsTrue(downloadResult, "Download result should result 'True'")

        Dim packageFileInfo As New FileInfo(target.PackageLocation.LocalPath.Substring(0, target.PackageLocation.LocalPath.Length - 1))
        Assert.IsTrue(packageFileInfo.Exists, "Package is not downloaded")

    End Sub

    <TestMethod(), WorkItem(20568)>
    <ExpectedException(GetType(Exception))> <Ignore()>
    Public Sub CitoTesterServerPackageManager_RemoveLockedLocalPackage_ShouldThrowExceptionTest()

        Dim fileName As String = Path.GetTempFileName()
        Using t As FileStream = File.Create(fileName) ' lock file
            CitoTesterServerPackageManager.RemovePackage(fileName) ' try to remove
        End Using

        Assert.IsTrue(File.Exists(fileName), "The file should NOT be deleted.")

        File.Delete(fileName) ' clean up
    End Sub

    <TestMethod(), WorkItem(20568)> <Ignore()>
    Public Sub CitoTesterServerPackageManager_RemoveLocalPackageTest()

        Dim fileName As String = Path.GetTempFileName()
        Using t As FileStream = File.Create(fileName)
        End Using

        CitoTesterServerPackageManager.RemovePackage(fileName)

        Assert.IsFalse(File.Exists(fileName), "The file should be deleted.")
    End Sub

    <TestMethod(), WorkItem(20568)> <Ignore()>
    Public Sub CitoTesterServerPackageManager_RemoveLockedLocalPackage_UnlockTest()

        Dim fileName As String = Path.GetTempFileName()

        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf LockFile), New Tuple(Of String, Int32)(fileName, 1000))
        Thread.Sleep(500)
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Remove), fileName)

        Thread.Sleep(3000)

        Debug.Print(String.Format("File exists: {0}", File.Exists(fileName)))

        Assert.IsFalse(File.Exists(fileName), "The file should be deleted.")

        If (File.Exists(fileName)) Then
            File.Delete(fileName) ' clean up
        End If

    End Sub

    <TestMethod(), WorkItem(20568)> <Ignore()>
    Public Sub CitoTesterServerPackageManager_RemoveLockedLocalPackage_RemoveFail()

        Dim fileName As String = Path.GetTempFileName()

        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf LockFile), New Tuple(Of String, Int32)(fileName, 3000))
        Thread.Sleep(500)
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Remove), fileName)

        Thread.Sleep(3000)

        Debug.Print(String.Format("File exists: {0}", File.Exists(fileName)))

        Assert.IsTrue(File.Exists(fileName), "The file should not be removed")

        If (File.Exists(fileName)) Then
            File.Delete(fileName) ' clean up
        End If

    End Sub

    Shared Sub LockFile(data As Object)

        Dim dataObject As Tuple(Of String, Int32) = DirectCast(data, Tuple(Of String, Int32))

        Dim fileName As String = dataObject.Item1
        Using t As FileStream = File.Create(fileName) ' lock file

            Debug.Print("Locked file.")
            Thread.Sleep(dataObject.Item2) ' keep locked for n milliseconds
            Debug.Print("Unlock file.")

        End Using
    End Sub

    Private Shared Sub Remove(data As Object)

        Debug.Print("Remove thread triggered.")

        Dim fileName As String = data.ToString()
        CitoTesterServerPackageManager.RemovePackage(fileName) ' try to remove

    End Sub
End Class