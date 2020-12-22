Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Security

Public NotInheritable Class COMStream
    Implements IStream
    Implements IDisposable


    Private _stream As Stream



    Public Sub New(ByVal sourceStream As Stream)
        _stream = sourceStream
    End Sub

    Private Sub New()
    End Sub



    Public Sub Clone(ByRef ppstm As IStream) Implements System.Runtime.InteropServices.ComTypes.IStream.Clone
        Throw New NotSupportedException()
    End Sub

    Public Sub Commit(ByVal grfCommitFlags As Integer) Implements System.Runtime.InteropServices.ComTypes.IStream.Commit
        Throw New NotSupportedException()
    End Sub

    Public Sub CopyTo(ByVal pstm As IStream, ByVal cb As Long, ByVal pcbRead As IntPtr, ByVal pcbWritten As IntPtr) Implements System.Runtime.InteropServices.ComTypes.IStream.CopyTo
        Throw New NotSupportedException()
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        If Me._stream IsNot Nothing Then
            Me._stream.Close()
            Me._stream.Dispose()
            Me._stream = Nothing
        End If
    End Sub

    Public Sub LockRegion(ByVal libOffset As Long, ByVal cb As Long, ByVal dwLockType As Integer) Implements System.Runtime.InteropServices.ComTypes.IStream.LockRegion
        Throw New NotSupportedException()
    End Sub

    <SecurityCritical()> _
    Public Sub Read(ByVal pv As Byte(), ByVal cb As Integer, ByVal pcbRead As IntPtr) Implements System.Runtime.InteropServices.ComTypes.IStream.Read
        Dim count As Integer = Me._stream.Read(pv, 0, cb)
        If pcbRead <> IntPtr.Zero Then
            Marshal.WriteInt32(pcbRead, count)
        End If
    End Sub

    Public Sub Revert() Implements System.Runtime.InteropServices.ComTypes.IStream.Revert
        Throw New NotSupportedException()
    End Sub

    <SecurityCritical()> _
    Public Sub Seek(ByVal dlibMove As Long, ByVal dwOrigin As Integer, ByVal plibNewPosition As IntPtr) Implements System.Runtime.InteropServices.ComTypes.IStream.Seek
        Dim origin As SeekOrigin = DirectCast(dwOrigin, SeekOrigin)
        Dim pos As Long = Me._stream.Seek(dlibMove, origin)
        If plibNewPosition <> IntPtr.Zero Then
            Marshal.WriteInt64(plibNewPosition, pos)
        End If
    End Sub

    Public Sub SetSize(ByVal libNewSize As Long) Implements System.Runtime.InteropServices.ComTypes.IStream.SetSize
        Me._stream.SetLength(libNewSize)
    End Sub

    Public Sub Stat(ByRef pstatstg As System.Runtime.InteropServices.ComTypes.STATSTG, ByVal grfStatFlag As Integer) Implements System.Runtime.InteropServices.ComTypes.IStream.Stat
        pstatstg = New System.Runtime.InteropServices.ComTypes.STATSTG()
        pstatstg.type = 2
        pstatstg.cbSize = Me._stream.Length
        pstatstg.grfMode = 0
        If Me._stream.CanRead AndAlso Me._stream.CanWrite Then
            pstatstg.grfMode = pstatstg.grfMode Or 2
        ElseIf Me._stream.CanWrite AndAlso Not _stream.CanRead Then
            pstatstg.grfMode = pstatstg.grfMode Or 1
        Else
            Throw New IOException()
        End If
    End Sub

    Public Sub UnlockRegion(ByVal libOffset As Long, ByVal cb As Long, ByVal dwLockType As Integer) Implements System.Runtime.InteropServices.ComTypes.IStream.UnlockRegion
        Throw New NotSupportedException()
    End Sub

    <SecurityCritical()> _
    Public Sub Write(ByVal pv As Byte(), ByVal cb As Integer, ByVal pcbWritten As IntPtr) Implements System.Runtime.InteropServices.ComTypes.IStream.Write
        Me._stream.Write(pv, 0, cb)
        If pcbWritten <> IntPtr.Zero Then
            Marshal.WriteInt32(pcbWritten, cb)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Try
            If _stream IsNot Nothing Then
                _stream.Close()
                _stream.Dispose()
                _stream = Nothing
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub


End Class