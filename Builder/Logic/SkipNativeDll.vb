Imports System.IO

Public Class SkipNativeDll
    Implements IDisposable
    Private ReadOnly _nativeDlls As New HashSet(Of String)() From {
        "MEDIAINFO.DLL",
        "LIBGLESV2.DLL",
        "LIBEGL.DLL",
        "LIBCEF.DLL"
    }

    Private _disposed As Boolean

    Public Sub New()
        For Each nativeDll As String In _nativeDlls
            Dim fileToRename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nativeDll)
            If Not File.Exists(fileToRename) Then
                Continue For
            End If

            Try
                Dim newName = fileToRename & ".native"
                If File.Exists(newName) Then
                    File.Delete(fileToRename)
                Else
                    File.Move(fileToRename, newName)
                End If
            Catch
            End Try

        Next
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If _disposed Then
            Return
        End If

        If disposing Then
            RenameFilesToOriginalName()
        End If

        _disposed = True
    End Sub

    Private Sub RenameFilesToOriginalName()
        For Each nativeDll As String In _nativeDlls
            Dim fileToRename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nativeDll & ".native")
            If Not File.Exists(fileToRename) Then
                Continue For
            End If

            Try
                Dim oldName = fileToRename.TrimEnd(".native".ToCharArray())
                If Not File.Exists(oldName) Then
                    File.Move(fileToRename, oldName)
                End If
            Catch generatedExceptionName As Exception
            End Try
        Next
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub
End Class
